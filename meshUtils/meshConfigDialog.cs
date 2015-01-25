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
using System.Diagnostics;

namespace meshStatus
{
    public partial class meshConfigDialog : Form
    {
        meshConfigUtils config;

         // Content item for the combo box
        private class cbItem
        {
            public EventLogEntryType Value;
            public cbItem(EventLogEntryType value)
            {
                Value = value;
            }
            public override string ToString()
            {
                return Value.ToString();
            }
        }
        public meshConfigDialog(meshConfigUtils cfg)
        {
            InitializeComponent();
            config = cfg;
            userName.Text = config.Decrypt(config.config.userName);
            userPassword.Text = config.Decrypt(config.config.userPassword);
            serverAddress.Text = config.config.serverAddress;

            if (config.config.serverTimeout < config.minServerTimeout)
            {
                config.config.serverTimeout = config.minServerTimeout;
            }

            if (config.config.serviceTimeout < config.minServiceTimeout)
            {
                config.config.serviceTimeout = config.minServiceTimeout;
            }

            serverInterval.Text = config.config.serverTimeout.ToString();
            serviceInterval.Text = config.config.serviceTimeout.ToString();

            filePath.Text = config.config.filePath;


                        if (config.config.debugLevel < EventLogEntryType.Error)
            {
                config.config.debugLevel = EventLogEntryType.Error;
            }

            if (config.config.debugLevel > EventLogEntryType.FailureAudit)
            {
                config.config.debugLevel = EventLogEntryType.FailureAudit;
            }

            debugCombo.Items.Add(new cbItem(EventLogEntryType.Error));
            debugCombo.Items.Add(new cbItem(EventLogEntryType.Warning));
            debugCombo.Items.Add(new cbItem(EventLogEntryType.Information));
            debugCombo.Items.Add(new cbItem(EventLogEntryType.SuccessAudit));
            debugCombo.Items.Add(new cbItem(EventLogEntryType.FailureAudit));
            debugCombo.SelectedIndex = debugCombo.FindStringExact(config.config.debugLevel.ToString());

            dateTimeFileName.Checked = config.config.dateTimeFileName;
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
                    config.config.filePath = filePath.Text;
                    cbItem tmp = (cbItem)debugCombo.SelectedItem;
                    config.config.dateTimeFileName = dateTimeFileName.Checked;
                    config.config.debugLevel = tmp.Value;
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

        private void serverInterval_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(serverInterval.Text) < config.minServerTimeout)
            {
                serverInterval.Text = config.minServerTimeout.ToString();
            }
        }

        private void serviceInterval_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(serviceInterval.Text) < config.minServiceTimeout)
            {
                serviceInterval.Text = config.minServiceTimeout.ToString();
            }
        }

        private void debugCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbItem tmp = (cbItem)debugCombo.SelectedItem;
        }
    }
}
