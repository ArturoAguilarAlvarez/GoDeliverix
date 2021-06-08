using Dapper;
using DataAccess.Common;
using DataAccess.Models;
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

        public IEnumerable<ListboxView> ReadAllCountries()
        {
            string query = "select UidPais as Uid, Nombre as Name from Paises order by Nombre";

            return this.Query<ListboxView>(query, null);
        }
        public IEnumerable<ListboxView> ReadAllStates(Guid countryUid)
        {
            string query = "select UidEstado as Uid, Nombre as Name from estados where UidPais = @Uid order by Nombre";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", countryUid);
            return this.Query<ListboxView>(query, parameters);
        }
        public IEnumerable<ListboxView> ReadAllMunicipalities(Guid stateUid)
        {
            string query = "select UidMunicipio as Uid, Nombre as Name from Municipios where UidEstado = @Uid order by nombre";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", stateUid);
            return this.Query<ListboxView>(query, parameters);
        }
        public IEnumerable<ListboxView> ReadAllCities(Guid municipalityUid)
        {
            string query = "select UidCiudad as Uid, Nombre as Name from Ciudades where UidMunicipio = @Uid order by Nombre";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", municipalityUid);
            return this.Query<ListboxView>(query, parameters);
        }
        public IEnumerable<ListboxView> ReadAllNeighborhoods(Guid cityUid)
        {
            string query = "select UidColonia as Uid, Nombre as Name from Colonia where UidCiudad = @Uid order by Nombre";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Uid", cityUid);
            return this.Query<ListboxView>(query, parameters);
        }
    }
}
