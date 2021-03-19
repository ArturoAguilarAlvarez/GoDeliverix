using Dapper;
using DataAccess.Common;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AddressDb : BaseDapper
    {
        public IEnumerable<AddressCustomer> ReadAllUserAddress(Guid uid)
        {
            string query = $@"
SELECT 
    D.[UidDireccion] AS [Uid],
    D.[UidPais],
    D.[UidEstado],
    D.[UidMunicipio],
    D.[UidCiudad],
    D.[UidColonia],
    D.[Calle0],
    D.[Calle1],
    D.[Calle2],
    D.[Manzana],
    D.[Lote],
    D.[CodigoPostal],
    D.[Referencia],
    D.[Identificador],
    D.[BPredeterminada],
    U.VchLatitud AS Latitude,
    U.VchLongitud AS Longitude
FROM [Direccion] AS D
    INNER JOIN [DireccionUsuario] AS DU ON DU.[UidDireccion] = D.UidDireccion
    INNER JOIN [DireccionUbicacion] AS DM ON DM.UidDireccion = D.[UidDireccion]
    INNER JOIN [Ubicacion] AS U ON U.[UidUbicacion] = DM.[UidUbicacion]
WHERE DU.UidUsuario = @UidUsuario
ORDER BY D.Identificador DESC";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UidUsuario", uid);

            return this.Query<AddressCustomer>(query, parameters);
        }

        public IEnumerable<NeighborhoodSearch> SearchNeighborhood(string name)
        {
            string query = @"sp_BusquedaColoniasTienda";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@filter", name);

            return this.Query<NeighborhoodSearch>(query, parameters, System.Data.CommandType.StoredProcedure);
        }
    }
}
