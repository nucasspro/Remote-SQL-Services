using System.Collections.Generic;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Forms;

namespace Remote_SQL_Services
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object ServicesController { get; private set; }
        public List<string> list = new List<string>();
        public NotifyIcon notifi = new NotifyIcon();

        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        #region Event

        private void btnTurnOn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in list)
            {
                TurnOn(item);
            }
            Notification("All SQL Services has been started!");
        }

        private void btn_TurnOff_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in list)
            {
                TurnOff(item);
            }
            Notification("All SQL Services has been stopped!");
        }

        #endregion Event

        #region Method

        private void Load()
        {
            list.Add("MSSQLFDLauncher");
            list.Add("MSSQLSERVER");
            list.Add("SQLSERVERAGENT");
            list.Add("MSSQLServerOLAPService");
            list.Add("SSASTELEMETRY");
            list.Add("SQLTELEMETRY");
            list.Add("SQL Server Distributed Replay Client");
            list.Add("SQL Server Distributed Replay Controller");
            list.Add("MsDtsServer130");
            list.Add("SSISTELEMETRY130");
            list.Add("MSSQLLaunchpad");
            list.Add("ReportServer");
            list.Add("SQLWriter");
            notifi.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
            notifi.Visible = true;
        }

        private void TurnOff(string service_name)
        {
            ServiceController services = new ServiceController(service_name);
            if (services.Status == ServiceControllerStatus.Stopped)
            {
                return;
            }
            services.Stop();
            services.WaitForStatus(ServiceControllerStatus.Stopped);
        }

        private void TurnOn(string service_name)
        {
            ServiceController services = new ServiceController(service_name);
            if (services.Status == ServiceControllerStatus.Running)
            {
                return;
            }
            services.Start();
            services.WaitForStatus(ServiceControllerStatus.Running);
        }

        private void Notification(string message)
        {
            notifi.BalloonTipText = message;
            notifi.ShowBalloonTip(1000);
        }

        #endregion Method
    }
}