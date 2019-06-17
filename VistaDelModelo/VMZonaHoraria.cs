using DBControl;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VistaDelModelo
{
    public class VMZonaHoraria
    {
        Conexion oConexion;
        ZonaHoraria oZonaHoraria;
        DbZonaHoraria oDbZonaHoraria;
        public string Id { get; set; }
        public Guid UidRegistroZonaPais { get; set; }
        public string NombreEstandar { get; set; }
        public string NombreCompleto { get; set; }
        public bool IsSelected { get; set; }

        public List<VMZonaHoraria> ListaDeZonas { get; set; }
        public List<VMZonaHoraria> ListaSeleccionadas { get; set; }

        public void RecuperarZonasHorarias()
        {
            ListaDeZonas = new List<VMZonaHoraria>();
            oDbZonaHoraria = new DbZonaHoraria();
            if (ListaSeleccionadas == null)
            {
                ListaSeleccionadas = new List<VMZonaHoraria>();
            }

            foreach (DataRow item in oDbZonaHoraria.ObtenerTodasLasZonasHorarias().Rows)
            {
                bool seleccion = false;
                if (ListaSeleccionadas.Exists(z => z.Id == item["IdZonaHoraria"].ToString()))
                {
                    seleccion = true;
                }
                ListaDeZonas.Add(new VMZonaHoraria()
                {

                    Id = item["IdZonaHoraria"].ToString(),
                    NombreEstandar = item["NombreEstandar"].ToString(),
                    NombreCompleto = item["NombreCompleto"].ToString(),
                    IsSelected = seleccion
                });
            }
        }

        public void SeleccionarZonaHoraria(string idZona)
        {
            if (!ListaSeleccionadas.Exists(z => z.Id == idZona))
            {
                var objeto = ListaDeZonas.Find(z => z.Id == idZona);
                ListaSeleccionadas.Add(objeto);
            }

        }

        public void desSeleccionarZonaHoraria(string idZona)
        {
            var objeto = ListaSeleccionadas.Find(z => z.Id == idZona);

            ListaSeleccionadas.Remove(objeto);
        }

        public void GuardarZonasPais(string UidPais)
        {
            try
            {
                oDbZonaHoraria = new DbZonaHoraria();
                oDbZonaHoraria.EliminarZonasHorariasPorPais(UidPais);
                foreach (var item in ListaSeleccionadas)
                {
                    oZonaHoraria = new ZonaHoraria();
                    oZonaHoraria.Id = item.Id;
                    oZonaHoraria.GuardarZonaHorariaConPais(UidPais);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void BuscarZonaHorariaDePais(string UidPais = "", string UidZonaHoraria = "")
        {
            SqlCommand Comando = new SqlCommand();
            ListaSeleccionadas = new List<VMZonaHoraria>();
            oConexion = new Conexion();
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "asp_BuscarZonasHorarias";

                if (!string.IsNullOrEmpty(UidPais))
                {
                    Comando.Parameters.Add("@UidPais", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidPais"].Value = new Guid(UidPais);
                }

                if (!string.IsNullOrEmpty(UidZonaHoraria))
                {
                    Comando.Parameters.Add("@UidRelacionZonaHorariaPais", SqlDbType.UniqueIdentifier);
                    Comando.Parameters["@UidRelacionZonaHorariaPais"].Value = new Guid(UidZonaHoraria);
                }
                if (!string.IsNullOrEmpty(UidPais))
                {
                    foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                    {
                        if (!ListaSeleccionadas.Exists(z => z.Id == item["IdZonaHoraria"].ToString()))
                        {
                            ListaSeleccionadas.Add(new VMZonaHoraria()
                            {
                                Id = item["IdZonaHoraria"].ToString(),
                                NombreEstandar = item["NombreEstandar"].ToString(),
                                NombreCompleto = item["NombreCompleto"].ToString(),
                            });
                        }
                    }
                }
                if (!string.IsNullOrEmpty(UidZonaHoraria))
                {
                    foreach (DataRow item in oConexion.Busquedas(Comando).Rows)
                    {
                        if (!ListaSeleccionadas.Exists(z => z.Id == item["IdZonaHoraria"].ToString()))
                        {
                            ListaSeleccionadas.Add(new VMZonaHoraria()
                            {
                                UidRegistroZonaPais = new Guid(item["UidZonaHorariaPais"].ToString()),
                                Id = item["IdZonaHoraria"].ToString(),
                                NombreEstandar = item["NombreEstandar"].ToString(),
                                NombreCompleto = item["NombreCompleto"].ToString(),
                            });
                        }
                    }
                }



            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GuardarZonasEstados(string UidZonaHoraria, List<VMDireccion> listaDeEstados)
        {
            oZonaHoraria = new ZonaHoraria();
            foreach (var item in listaDeEstados)
            {
                oZonaHoraria.Uid = UidZonaHoraria;
                oZonaHoraria.GuardarZonaHorariaConEstados(item.UidEstado);
            }
        }

        public DataTable ObtenerZonasDelPais(string uidPais)
        {
            oDbZonaHoraria = new DbZonaHoraria();
            return oDbZonaHoraria.ObtenerZonasPorPais(uidPais);
        }
    }
}
