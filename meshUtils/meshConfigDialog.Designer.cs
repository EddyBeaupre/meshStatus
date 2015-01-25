namespace meshStatus
{
    partial class meshConfigDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.userPassword = new System.Windows.Forms.TextBox();
            this.serverInterval = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.filePath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serverAddress = new TCPUtils.IPAddressControlBox.IPAddressControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.serviceInterval = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.debugCombo = new System.Windows.Forms.ComboBox();
            this.dateTimeFileName = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Utilisateur :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mot de passe :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Adresse du contrôleur :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Intervalle :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Répertoire d’exportation :";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(128, 19);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(131, 20);
            this.userName.TabIndex = 5;
            // 
            // userPassword
            // 
            this.userPassword.Location = new System.Drawing.Point(348, 19);
            this.userPassword.Name = "userPassword";
            this.userPassword.PasswordChar = '*';
            this.userPassword.Size = new System.Drawing.Size(115, 20);
            this.userPassword.TabIndex = 6;
            // 
            // serverInterval
            // 
            this.serverInterval.Location = new System.Drawing.Point(348, 48);
            this.serverInterval.Name = "serverInterval";
            this.serverInterval.Size = new System.Drawing.Size(56, 20);
            this.serverInterval.TabIndex = 8;
            this.serverInterval.Validating += new System.ComponentModel.CancelEventHandler(this.serverInterval_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "secondes";
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(138, 19);
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Size = new System.Drawing.Size(326, 20);
            this.filePath.TabIndex = 10;
            this.filePath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.filePath_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.serverAddress);
            this.groupBox1.Controls.Add(this.userName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.serverInterval);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.userPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 83);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Générale";
            // 
            // serverAddress
            // 
            this.serverAddress.AllowInternalTab = false;
            this.serverAddress.AutoHeight = true;
            this.serverAddress.BackColor = System.Drawing.SystemColors.Window;
            this.serverAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.serverAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.serverAddress.Location = new System.Drawing.Point(128, 51);
            this.serverAddress.MinimumSize = new System.Drawing.Size(87, 20);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.ReadOnly = false;
            this.serverAddress.Size = new System.Drawing.Size(87, 20);
            this.serverAddress.TabIndex = 10;
            this.serverAddress.Text = "...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateTimeFileName);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.serviceInterval);
            this.groupBox2.Controls.Add(this.filePath);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 78);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(200, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "secondes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(76, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Intervalle :";
            // 
            // serviceInterval
            // 
            this.serviceInterval.Location = new System.Drawing.Point(138, 46);
            this.serviceInterval.Name = "serviceInterval";
            this.serviceInterval.Size = new System.Drawing.Size(56, 20);
            this.serviceInterval.TabIndex = 11;
            this.serviceInterval.Validating += new System.ComponentModel.CancelEventHandler(this.serviceInterval_Validating);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.debugCombo);
            this.groupBox3.Location = new System.Drawing.Point(12, 188);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 47);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Journalisation";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Niveau minimum :";
            // 
            // debugCombo
            // 
            this.debugCombo.FormattingEnabled = true;
            this.debugCombo.Location = new System.Drawing.Point(106, 19);
            this.debugCombo.Name = "debugCombo";
            this.debugCombo.Size = new System.Drawing.Size(121, 21);
            this.debugCombo.TabIndex = 14;
            this.debugCombo.SelectedIndexChanged += new System.EventHandler(this.debugCombo_SelectedIndexChanged);
            // 
            // dateTimeFileName
            // 
            this.dateTimeFileName.AutoSize = true;
            this.dateTimeFileName.Location = new System.Drawing.Point(260, 49);
            this.dateTimeFileName.Name = "dateTimeFileName";
            this.dateTimeFileName.Size = new System.Drawing.Size(187, 17);
            this.dateTimeFileName.TabIndex = 14;
            this.dateTimeFileName.Text = "Nom de fichier avec date et heure";
            this.dateTimeFileName.UseVisualStyleBackColor = true;
            // 
            // meshConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 253);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "meshConfigDialog";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.meshConfigDialog_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox userPassword;
        private System.Windows.Forms.TextBox serverInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox serviceInterval;
        private TCPUtils.IPAddressControlBox.IPAddressControl serverAddress;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox debugCombo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox dateTimeFileName;
    }
}