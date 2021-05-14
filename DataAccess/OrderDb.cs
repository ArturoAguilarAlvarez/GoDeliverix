using Dapper;
using DataAccess.Common;
using DataAccess.Models;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDb : BaseDapper
    {
        public OrderDb() { }

        public CommonListViewSource<PurchaseHistory> ReadAllUserPurchases(Guid uidUser, int pageSize = 25, int pageNumber = 0)
        {
            string query = @"
SELECT O.UidOrden                          AS PurchaseUid,
       FDC.UidFormaDeCobro                 AS PaymentMethodUid,
       EC.UidEstatusCobro                  AS PaymentStatusUid,
       O.DtmFechaDeCreacion                AS Date,
       O.IntFolio                          AS Folio,
       FDC.VchNombre                       AS PaymentMethod,
       EC.VchNombre                        AS PaymentStatus,
       O.MTotal                            AS Total,
       O.AddressIdentifier,
       O.Count,
       OS.UidRelacionOrdenSucursal         AS OrderUid,
       S.UidSucursal                       AS BrancheUid,
       EDO.UidEstatus                      AS StatusUid,
       S.UidEmpresa                        AS CompanyUid,
       E.NombreComercial                   AS Company,
       I.NVchRuta                          AS CompanyImg,
       S.Identificador                     AS Branch,
       OS.IntFolio                         AS BranchFolio,
       OS.BIntCodigoEntrega                AS DeliveryCode,
       EDO.VchNombre                       AS Status,
       OS.MTotalSucursal                   AS Total,
       CAST(OT.MPropina AS DECIMAL(10, 2)) AS Tips,
       CAST(T.MCosto AS DECIMAL(10, 2))    AS Delivery,
       OS.DescuentoMonedero                AS WalletDiscount,
       OS.ComisionPagoTarjeta              AS CardPaymentComission,
       OS.ComisionPagoTarjetaRepartidor    AS DeliveryCardPaymentComission,
       OS.IncludeCPTS,
       Os.IncludeCPTD,
       P.UidProducto                       AS ProductUid,
       OP.UidOrden                         AS OrderUid,
       P.VchNombre                         AS Name,
       OP.intCantidad                      AS Quantity
FROM (
         SELECT payload.*,
                [Count] = COUNT(*) OVER ()
         FROM (SELECT T.UidOrden,
                      T.DtmFechaDeCreacion,
                      T.MTotal,
                      OU.IntFolio,
                      TD.Identificador AS AddressIdentifier
               FROM Ordenes T
                        INNER JOIN OrdenUsuario OU ON T.UidOrden = OU.UidOrden
                        INNER JOIN Direccion TD ON TD.UidDireccion = T.UidDireccion
               WHERE OU.UidUsuario = @uidUsuario
              ) payload
         ORDER BY payload.IntFolio DESC
         OFFSET @PageSize * @PageNumber ROWS FETCH NEXT @PageSize ROWS ONLY) AS O
         INNER JOIN OrdenFormaDeCobro OFC ON O.UidOrden = OFC.UidOrden
         INNER JOIN FormaDeCobro FDC ON OFC.UidFormaDeCobro = FDC.UidFormaDeCobro
         INNER JOIN EstatusCobro EC ON EC.UidEstatusCobro = (SELECT TOP 1 tbco.UidEstatusCobro
                                                             FROM BitacoraCobroOrden tbco
                                                             WHERE tbco.UidOrdenFormaDeCobro = OFC.UidOrdenFormaDeCobro
                                                             ORDER BY tbco.DtmFechaDeRegistro DESC)
         INNER JOIN OrdenSucursal OS ON OS.UidOrden = O.UidOrden
         INNER JOIN Sucursales S ON S.UidSucursal = OS.UidSucursal
         INNER JOIN Empresa E ON S.UidEmpresa = E.UidEmpresa
         INNER JOIN (SELECT TIE.UidEmpresa,
                            TI.UIdImagen,
                            TI.NVchRuta
                     FROM ImagenEmpresa TIE
                              INNER JOIN Imagenes TI ON TI.UIdImagen = TIE.UidImagen
                     WHERE TI.NVchRuta LIKE '%FotoPerfil%') AS I ON I.UidEmpresa = E.UidEmpresa
    --INNER JOIN ImagenEmpresa IE on E.UidEmpresa = IE.UidEmpresa

         INNER JOIN EstatusDeOrden EDO ON EDO.UidEstatus = (SELECT TOP 1 T.UidEstatusDeOrden
                                                            FROM BitacoraOrdenEstatus T
                                                            WHERE T.UidOrden = OS.UidRelacionOrdenSucursal
                                                            ORDER BY DtmFecha DESC)
         INNER JOIN OrdenTarifario OT ON OT.UidOrden = OS.UidRelacionOrdenSucursal
         INNER JOIN Tarifario T ON OT.UidTarifario = T.UidRegistroTarifario
         INNER JOIN OrdenProducto OP ON OP.UidOrden = OS.UidRelacionOrdenSucursal
         INNER JOIN SeccionProducto SP ON SP.UidSeccionProducto = OP.UidSeccionProducto
         INNER JOIN Productos P ON SP.UidProducto = P.UidProducto
WHERE 1 = 1
ORDER BY Folio DESC";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@uidUsuario", uidUser);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageNumber", pageNumber);

            Dictionary<Guid, PurchaseHistory> lookup = new Dictionary<Guid, PurchaseHistory>();
            IEnumerable<PurchaseHistory> result;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                result = conn.Query<PurchaseHistory, OrderHistory, ProductOrderHistory, PurchaseHistory>(query,
                    (tPurchase, tOrder, tProduct) =>
                    {
                        if (lookup.TryGetValue(tPurchase.PurchaseUid, out PurchaseHistory tEntry) == false)
                        {
                            tEntry = tPurchase;
                            tEntry.Orders = new List<OrderHistory>();
                            lookup.Add(tEntry.PurchaseUid, tEntry);
                        }

                        if (tOrder.OrderUid != Guid.Empty && !tEntry.Orders.Any(i => i.OrderUid == tOrder.OrderUid))
                        {
                            tEntry.Orders.Add(tOrder);
                        }

                        if (tProduct != null)
                        {
                            if (tProduct.ProductUid != Guid.Empty)
                            {
                                int index = tEntry.Orders.FindIndex(o => o.OrderUid == tProduct.OrderUid);
                                if (index >= 0)
                                {
                                    tEntry.Orders[index].Products.Add(tProduct);
                                }
                            }
                        }

                        return tEntry;
                    },
                    parameters,
                    commandType: System.Data.CommandType.Text,
                    splitOn: "PurchaseUid,OrderUid,ProductUid").Distinct().ToList();
            }

            return new CommonListViewSource<PurchaseHistory>()
            {
                Payload = result,
                Count = result.Count() > 0 ? result.First().Count : 0
            };
        }
    }
}
