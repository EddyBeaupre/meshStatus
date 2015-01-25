using meshUtils;
using System;
using System.Windows.Forms;


namespace meshConfigEditor
{
    public partial class meshConfigDialog : Form
    {
        
        meshConfigUtils config = new meshConfigUtils();

        public meshConfigDialog()
        {
            InitializeComponent();

            config.ReadXML();
            userName.Text = config.Decrypt(config.config.userName);
            userPassword.Text =  config.Decrypt(config.config.userPassword);
            serverAddress.Text = config.config.serverAddress;
                        if(config.config.serverTimeout < 20 ) {
                config.config.serverTimeout = 20;
            }
                        serverDelay.Text = config.config.serverTimeout.ToString();
            filePath.Text = config.config.filePath;
        }

        private void filePath_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            config.config.filePath = fbd.SelectedPath;
            filePath.Text = config.config.filePath;
        }

        private void saveConfig_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void doSaveConfig()
        {
            config.config.userName = config.Encrypt(userName.Text);
            config.config.userPassword = config.Encrypt(userPassword.Text);
            config.config.serverAddress = serverAddress.Text;
            config.config.serverTimeout = Convert.ToInt32(serverDelay.Text);
            if (config.config.serverTimeout < 20)
            {
                config.config.serverTimeout = 20;
            }
            config.config.filePath = filePath.Text;
            config.WriteXML();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch(MessageBox.Show("Sauvegarder la configuration? ?", "Sortie", MessageBoxButtons.YesNoCancel)) {
                case DialogResult.Yes:
                    doSaveConfig();
                break;
                case DialogResult.No:
                break;
                default:
                e.Cancel = true;
                break;
            }
                
            }
    }
}
