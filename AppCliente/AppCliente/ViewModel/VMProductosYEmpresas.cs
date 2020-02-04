using AppCliente.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using VistaDelModelo;

namespace AppCliente.ViewModel
{
    public class VMProductosYEmpresas
    {


        #region Metodos
        public async void BuscarProductos(string StrParametroBusqueda, string StrDia, Guid UidEstado, Guid UidColonia, Guid UidBusquedaCategorias, string StrNombreEmpresa)
        {
            using (HttpClient _WebApi = new HttpClient())
            {
                string _URL = "https://www.godeliverix.net/api/Producto/GetBuscarProductosCliente?StrParametroBusqueda=" + StrParametroBusqueda + "&StrDia=" + StrDia + "&UidEstado=" + UidEstado + "&UidColonia=" + UidColonia + "&UidBusquedaCategorias=" + UidBusquedaCategorias + "&StrNombreEmpresa=" + StrNombreEmpresa + "";
                var content = await _WebApi.GetStringAsync(_URL);
                var obj = JsonConvert.DeserializeObject<ResponseHelper>(content).Data.ToString();
                App.MVProducto = JsonConvert.DeserializeObject<VMProducto>(obj);
            }
        }
        #endregion
    }
}
