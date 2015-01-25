using System;
using System.Diagnostics;
using meshUtils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace meshStatus
{
    public partial class meshStatusGUI : Form
    {

        private delegate void mainCallback_delegate(int status, Object data);
        private wlcPooler pooler = null;
        private Boolean scanStatus = false;
        private Boolean exitApp = false;
        private int splitHeight;
        private int splitWidth;
        private int buttonHeight;
        private int buttonWidth;
        private List<Button> buttonList = new List<Button>();
        private meshConfigUtils config = new meshConfigUtils();

        private EventLogger eventLogger = null;

        public meshStatusGUI()
        {
            InitializeComponent();
            config.ReadXML();
            statusLabel.Text = "En attente de démarrage";

            for (int i = 0; i < 100; i++)
            {
                Button bt = new Button();
                bt.Location = new Point(0, 0);
                bt.Height = 0;
                bt.Width = 0;
                bt.BackColor = SystemColors.ButtonFace;
                bt.ForeColor = SystemColors.ControlText;
                this.Controls.Add(bt);
                buttonList.Add(bt);

                eventLogger = new EventLogger(eventLog, "meshStatusGUI", "Application", config.config.debugLevel);
                
            }
        }

        public void mainCallback(int status, Object data)
        {
            switch (status)
            {
                case 2:
                    if(statusStrip.InvokeRequired)
                    {
                        mainCallback_delegate d = new mainCallback_delegate(mainCallback);
                        this.Invoke(d, new object[] { status, data });
                    }
                    else
                    {
                        statusLabel.Text = data.ToString();
                    }
                    break;
                case 1:
                    // monitor is started and running for device.
                    if (this.InvokeRequired)
                    {
                        mainCallback_delegate d = new mainCallback_delegate(mainCallback);
                        this.Invoke(d, new object[] { status, data });
                    }
                    else
                    {
                        DateTime timeStamp = DateTime.Now;
                        int i = 0;
                        if (data != null) { 
                        foreach (meshInfo s in (List<meshInfo>)data)
                        {
                            buttonList[i].Text = s.getName + Environment.NewLine + s.getParent + " (" + s.getSNR + ")" + Environment.NewLine;
                            if (s.getSNR >= 20)
                            {
                                buttonList[i].BackColor = Color.Green;
                                buttonList[i].ForeColor = Color.White;
                            }
                            else if ( (s.getSNR < 20) & (s.getSNR >= 16) )
                            {
                                buttonList[i].BackColor = Color.Yellow;
                                buttonList[i].ForeColor = Color.Black;
                            }
                            else if ((s.getSNR <16) & (s.getSNR >= 1)) 
                            {
                                buttonList[i].BackColor = Color.Red;
                                buttonList[i].ForeColor = Color.Black;
                            }
                            else {
                                buttonList[i].BackColor = Color.LightGreen;
                                buttonList[i].ForeColor = Color.Black;
                            }
                            i++;
                        }

                        for (; i < 100; i++)
                        {
                            buttonList[i].Text = "";
                            buttonList[i].BackColor = SystemColors.ButtonFace;
                            buttonList[i].ForeColor = SystemColors.ControlText;
                        }
                        }
                    }
                    break;
                case 0:
                    if (this.InvokeRequired)
                    {
                        mainCallback_delegate d = new mainCallback_delegate(mainCallback);
                        this.Invoke(d, new object[] { status, data });
                    }
                    else
                    {
                        scanStatus = false;
                        marcheArretToolStripMenuItem.Text = "Démarrage";
                        marcheArretToolStripMenuItem.Enabled = true;
                        configurationToolStripMenuItem.Enabled = true;
                        statusLabel.Text = "En attente de démarrage";
                        if (exitApp)
                        {
                            Application.Exit();
                        }
                    }

                    break;
                default:
                    break;
            }
        }


        private void meshStatusGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (scanStatus)
            {
                e.Cancel = true;
                exitApp = true; 
                pooler.threadRun = false;
                configurationToolStripMenuItem.Enabled = false;
                marcheArretToolStripMenuItem.Enabled = false;
            }
            
        }

        private void meshStatusGUI_Resize(object sender, EventArgs e)
        {
            int i = 0;

            splitHeight = statusStrip.Top - menuStrip1.Bottom;
            buttonHeight = splitHeight / 10;
            splitWidth = menuStrip1.Width ;
            buttonWidth = splitWidth / 10;

            for (int y = menuStrip1.Bottom; y < splitHeight ; y += buttonHeight)
            {
                for (int x = menuStrip1.Left; x < splitWidth ; x += buttonWidth)
                {
                    buttonList[i].Location = new Point(x, y);
                    buttonList[i].Height = buttonHeight;
                    buttonList[i].Width = buttonWidth;
                    i++;
                }
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meshConfigDialog md = new meshConfigDialog(config);

            md.ShowDialog();

        }

        private void sortieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void marcheArretToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (!scanStatus)
                {
                        scanStatus = true;
                        marcheArretToolStripMenuItem.Text = "Arrêt";
                        configurationToolStripMenuItem.Enabled = false;
                        pooler = new wlcPooler(Convert.ToInt32(config.config.serverTimeout), config.config.serverAddress, config.Decrypt(config.config.userName), config.Decrypt(config.config.userPassword), mainCallback, eventLogger);

                }
                else
                {
                    pooler.threadRun = false;
                    marcheArretToolStripMenuItem.Enabled = false;
                }
            }
        
    }
}
