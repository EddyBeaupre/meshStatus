using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using meshUtils;

namespace meshStatus
{
    public partial class meshConfigDialog : Form
    {
        meshConfigUtils config;
        public meshConfigDialog(meshConfigUtils cfg)
        {
            InitializeComponent();
            config = cfg;
            userName.Text = config.Decrypt(config.config.userName);
            userPassword.Text = config.Decrypt(config.config.userPassword);
            serverAddress.Text = config.config.serverAddress;
            if (config.config.serverTimeout < 20)
            {
                config.config.serverTimeout = 20;
            }

            if (config.config.serviceTimeout < 60)
            {
                config.config.serviceTimeout = 60;
            }
            serverInterval.Text = config.config.serverTimeout.ToString();
            serviceInterval.Text = config.config.serviceTimeout.ToString();

            filePath.Text = config.config.filePath;
        }

        private void meshConfigDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (MessageBox.Show("Sauvegarder la configuration? ?", "Sortie", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Yes:
                    config.config.userName = config.Encrypt(userName.Text);
                    config.config.userPassword = config.Encrypt(userPassword.Text);
                    config.config.serverAddress = serverAddress.Text;
                    config.config.serverTimeout = Convert.ToInt32(serverInterval.Text);
                    config.config.serviceTimeout = Convert.ToInt32(serviceInterval.Text);
                    if (config.config.serverTimeout < 20)
                    {
                        config.config.serverTimeout = 20;
                    }

                    if (config.config.serviceTimeout < 60)
                    {
                        config.config.serviceTimeout = 60;
                    }
                    config.config.filePath = filePath.Text;
                    config.WriteXML();
                    break;
                case DialogResult.No:
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }

        private void filePath_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            config.config.filePath = fbd.SelectedPath;
            filePath.Text = config.config.filePath;
        }
    }
}
