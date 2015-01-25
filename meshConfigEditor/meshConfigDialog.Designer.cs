namespace meshConfigEditor
{
    partial class meshConfigDialog
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.userName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.userPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.serverAddress = new IPAddressControlLib.IPAddressControl();
            this.label3 = new System.Windows.Forms.Label();
            this.serverDelay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.filePath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.saveConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(100, 13);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(217, 20);
            this.userName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Utilisateur :";
            // 
            // userPassword
            // 
            this.userPassword.Location = new System.Drawing.Point(100, 40);
            this.userPassword.Name = "userPassword";
            this.userPassword.PasswordChar = '*';
            this.userPassword.Size = new System.Drawing.Size(217, 20);
            this.userPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mot de passe :";
            // 
            // serverAddress
            // 
            this.serverAddress.AllowInternalTab = false;
            this.serverAddress.AutoHeight = true;
            this.serverAddress.BackColor = System.Drawing.SystemColors.Window;
            this.serverAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.serverAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.serverAddress.Location = new System.Drawing.Point(100, 67);
            this.serverAddress.MinimumSize = new System.Drawing.Size(87, 20);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.ReadOnly = false;
            this.serverAddress.Size = new System.Drawing.Size(87, 20);
            this.serverAddress.TabIndex = 4;
            this.serverAddress.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Serveur :";
            // 
            // serverDelay
            // 
            this.serverDelay.Location = new System.Drawing.Point(257, 67);
            this.serverDelay.Name = "serverDelay";
            this.serverDelay.Size = new System.Drawing.Size(60, 20);
            this.serverDelay.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(209, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Delais :";
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(100, 94);
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Size = new System.Drawing.Size(217, 20);
            this.filePath.TabIndex = 8;
            this.filePath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.filePath_MouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Répertoire :";
            // 
            // saveConfig
            // 
            this.saveConfig.Location = new System.Drawing.Point(241, 121);
            this.saveConfig.Name = "saveConfig";
            this.saveConfig.Size = new System.Drawing.Size(75, 23);
            this.saveConfig.TabIndex = 10;
            this.saveConfig.Text = "Sortie";
            this.saveConfig.UseVisualStyleBackColor = true;
            this.saveConfig.Click += new System.EventHandler(this.saveConfig_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 157);
            this.Controls.Add(this.saveConfig);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serverDelay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.serverAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userName);
            this.Name = "Form1";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userPassword;
        private System.Windows.Forms.Label label2;
        private IPAddressControlLib.IPAddressControl serverAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serverDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button saveConfig;
    }
}

