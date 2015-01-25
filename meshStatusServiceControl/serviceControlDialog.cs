using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using meshUtils;
using meshStatus;

namespace meshStatusServiceControl
{
    public partial class serviceControlDialog : Form
    {
        public serviceControlDialog()
        {
            InitializeComponent();

            try
            {
                switch (serviceStatus("meshStatusService"))
                {
                    case "Running":
                        startButton.Text = "Arrêt";
                        statusLabel.Text = "Service démarrer";
                        break;
                    case "Stopped":
                        startButton.Text = "Démarrage";
                        statusLabel.Text = "Service arreter";
                        break;
                    default:
                        startButton.Text = "Désactivé";
                        startButton.Enabled = false;
                        statusLabel.Text = "L'état du service est inconnue";
                        break;
                }
            }
            catch (Exception e)
            {
                startButton.Text = "!!! Erreur !!!";
                startButton.Enabled = false;
                statusLabel.Text = e.ToString();
                throw;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (serviceStatus("meshStatusService"))
                {
                    case "Running":
                        startButton.Text = "Démarrage";
                        statusLabel.Text = "Service en arret";
                        StopService("meshStatusService", 30000);
                        statusLabel.Text = "Service arreter";
                        break;
                    case "Stopped":
                        startButton.Text = "Arrêt";
                        statusLabel.Text = "Service en démarrage";
                        StartService("meshStatusService", 30000);
                        statusLabel.Text = "Service démarrer";
                        break;
                    default:
                        startButton.Text = "Désactivé";
                        startButton.Enabled = false;
                        statusLabel.Text = "L'état du service est inconnue";
                        break;
                }
            }
            catch (Exception ex)
            {
                startButton.Text = "!!! Erreur !!!";
                startButton.Enabled = false;
                statusLabel.Text = ex.ToString();
            }
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            meshConfigUtils config = new meshConfigUtils();
            config.ReadXML();
            meshConfigDialog md = new meshConfigDialog(config);
            md.ShowDialog();

            switch (serviceStatus("meshStatusService"))
            {
                case "Running":
                    switch (MessageBox.Show("Redémarrer le service?", "Service en marche", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            statusLabel.Text = "Redémarrage";
                            RestartService("meshStatusService", 30000);
                            statusLabel.Text = "Service démarrer";
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        public void StartService(string serviceName, int timeoutMilliseconds)
        {
            try
            {

                ServiceController service = new ServiceController(serviceName);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(timeoutMilliseconds));
            }
            catch
            {
                statusLabel.Text = "Erreur lors du démarrage";
            }
        }

        public void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch
            {
                // ...
            }
        }

        public void RestartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                // ...
            }
        }

        public String serviceStatus(string serviceName)
        {
            ServiceController service = new ServiceController("meshStatusService");
            return service.Status.ToString();
        }
    }
}


