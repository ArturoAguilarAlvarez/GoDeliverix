using System;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Windows;

namespace Deliverix.Wpf.Distribuidores
{
    /// <summary>
    /// Lógica de interacción para DataBase.xaml
    /// </summary>
    public partial class DataBase : Window
    {
        public DataBase()
        {
            InitializeComponent();
            try
            {
                CargarElementos();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        private void CargarElementos()
        {
            try
            {
                if (!ValidarEstatusServicio("SQLBROWSER").Equals("Running"))
                {
                    ServiceController _serviceSQLBROWSER = new ServiceController("SQLBROWSER");
                    ServiceHelper.ChangeStartMode(_serviceSQLBROWSER, System.ServiceProcess.ServiceStartMode.Automatic);
                    Start(_serviceSQLBROWSER);
                }

                if (ValidarEstatusServicio("SQLBROWSER").Equals("Running"))
                {
                    string[] instancias = GetSQLServerList();

                    for (int i = 0; i < instancias.Count(); i++)
                    {
                        cbInstancia.Items.Add(instancias[i].ToString());
                    }
                }

                if (cbInstancia.Items.Count > 0) { cbInstancia.SelectedIndex = 0; }
            }
            catch (Exception a) { MessageBox.Show(a.Message); }
        }

        private static class ServiceHelper
        {
            [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern Boolean ChangeServiceConfig(
                IntPtr hService,
                UInt32 nServiceType,
                UInt32 nStartType,
                UInt32 nErrorControl,
                String lpBinaryPathName,
                String lpLoadOrderGroup,
                IntPtr lpdwTagId,
                [In] char[] lpDependencies,
                String lpServiceStartName,
                String lpPassword,
                String lpDisplayName);

            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            static extern IntPtr OpenService(
                IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

            [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr OpenSCManager(
                string machineName, string databaseName, uint dwAccess);

            [DllImport("advapi32.dll", EntryPoint = "CloseServiceHandle")]
            public static extern int CloseServiceHandle(IntPtr hSCObject);

            private const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;
            private const uint SERVICE_QUERY_CONFIG = 0x00000001;
            private const uint SERVICE_CHANGE_CONFIG = 0x00000002;
            private const uint SC_MANAGER_ALL_ACCESS = 0x000F003F;

            public static void ChangeStartMode(ServiceController svc, System.ServiceProcess.ServiceStartMode mode)
            {
                var scManagerHandle = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);
                if (scManagerHandle == IntPtr.Zero)
                {
                    throw new ExternalException("Open Service Manager Error");
                }

                var serviceHandle = OpenService(
                    scManagerHandle,
                    svc.ServiceName,
                    SERVICE_QUERY_CONFIG | SERVICE_CHANGE_CONFIG);

                if (serviceHandle == IntPtr.Zero)
                {
                    throw new ExternalException("Open Service Error");
                }

                var result = ChangeServiceConfig(
                    serviceHandle,
                    SERVICE_NO_CHANGE,
                    (uint)mode,
                    SERVICE_NO_CHANGE,
                    null,
                    null,
                    IntPtr.Zero,
                    null,
                    null,
                    null,
                    null);

                if (result == false)
                {
                    int nError = Marshal.GetLastWin32Error();
                    var win32Exception = new Win32Exception(nError);
                    throw new ExternalException("Could not change service start type: "
                        + win32Exception.Message);
                }

                CloseServiceHandle(serviceHandle);
                CloseServiceHandle(scManagerHandle);
            }
        }

        private static string[] GetSQLServerList()
        {
            SqlDataSourceEnumerator dse = SqlDataSourceEnumerator.Instance;
            DataTable dt = dse.GetDataSources();
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            string[] SQLServers = new string[dt.Rows.Count];
            int f = -1;
            foreach (DataRow r in dt.Rows)
            {
                string SQLServer = r["ServerName"].ToString();
                string Instance = r["InstanceName"].ToString();
                if (Instance != null && !string.IsNullOrEmpty(Instance))
                {
                    SQLServer += @"\" + Instance;
                }
                SQLServers[System.Math.Max(System.Threading.Interlocked.Increment(ref f), f - 1)] = SQLServer;
            }
            Array.Sort(SQLServers);
            return SQLServers;
        }

        private string ValidarEstatusServicio(string NombreServicio)
        {
            ServiceController sc = new ServiceController(NombreServicio);

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    return "Running";
                case ServiceControllerStatus.Stopped:
                    return "Stopped";
                case ServiceControllerStatus.Paused:
                    return "Paused";
                case ServiceControllerStatus.StopPending:
                    return "Stopping";
                case ServiceControllerStatus.StartPending:
                    return "Starting";
                default:
                    return "Status Changing";
            }
        }

        static void Start(ServiceController _service)
        {
            if (!(_service.Status == ServiceControllerStatus.Running || _service.Status == ServiceControllerStatus.StartPending))
                _service.Start();

            _service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 1, 0));
        }

        public static void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec1));
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    millisec1 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void RestartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    millisec1 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void MontarBD_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string _SQLConnectionString = @"Data Source = " + cbInstancia.Text + "; Initial Catalog = master; Integrated Security = True";
                string path = @"BaseDeDatos\script.sql";
                string script = string.Empty;

                for (int i = 0; i < System.IO.Path.GetFullPath(path).Split(System.IO.Path.DirectorySeparatorChar).Length; i++)
                {
                    try { script = File.ReadAllText(path); break; }
                    catch (Exception) { path = @"..\" + path; }
                }

                SqlConnection conn = new SqlConnection(_SQLConnectionString);
                ExecuteBatchNonQuery(script, conn);


                MessageBox.Show("La base de datos fue creada y se ha configurado el equipo correctamente", "Configuración exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                Properties.Settings.Default["Source"] = cbInstancia.Text;
                Properties.Settings.Default.Save();

                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"GoDeliverixDistribuidores");
                key.SetValue("Source", cbInstancia.Text);
                key.Close();

                StopService("SQLBROWSER", 1000);

                this.Close();
            }
            catch (Exception a)
            {
                string cadena = "Error: " + a.Message;
                MessageBox.Show(cadena, "La base de datos no fue montada", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExecuteBatchNonQuery(string sql, SqlConnection conn)
        {
            string sqlBatch = string.Empty;
            SqlCommand cmd = new SqlCommand(string.Empty, conn);
            conn.Open();
            sql += "\nGO";  
            try
            {
                foreach (string line in sql.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line.ToUpperInvariant().Trim() == "GO")
                    {
                        cmd.CommandText = sqlBatch;
                        cmd.ExecuteNonQuery();
                        sqlBatch = string.Empty;
                    }
                    else
                    {
                        sqlBatch += line + "\n";
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                conn.Close();
            }
        }

        private void btnPruebaConexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string source = string.Empty, a = string.Empty, servidor = string.Empty, instancia = string.Empty;
                int index;
                try
                {
                    a = cbInstancia.Text;
                    index = a.LastIndexOf(@"\");
                    servidor = a.Substring(0, index);
                    instancia = a.Substring(index + 1);
                }
                catch (Exception)
                {
                    source = servidor;
                }

                source = servidor + @"\" + instancia;

                SqlConnection _cadena = new SqlConnection(@"Data Source=" + source + ";Initial Catalog=master;Integrated Security=True");
                _cadena.Open();
                MessageBox.Show("Se estableció una conexión exitosa", "Conexión exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception r)
            {
                MessageBox.Show("No se pudo establecer conexión" + r.Message, "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
