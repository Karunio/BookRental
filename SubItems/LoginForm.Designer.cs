namespace BookRental.SubItems
{
    partial class LoginForm
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.TxtUserId = new MetroFramework.Controls.MetroTextBox();
            this.TxtUserPwd = new MetroFramework.Controls.MetroTextBox();
            this.BtnOk = new MetroFramework.Controls.MetroButton();
            this.BtnCancel = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.Location = new System.Drawing.Point(129, 93);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(63, 23);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "ID";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Location = new System.Drawing.Point(129, 122);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(63, 23);
            this.metroLabel2.TabIndex = 0;
            this.metroLabel2.Text = "Password";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtUserId
            // 
            // 
            // 
            // 
            this.TxtUserId.CustomButton.Image = null;
            this.TxtUserId.CustomButton.Location = new System.Drawing.Point(159, 1);
            this.TxtUserId.CustomButton.Name = "";
            this.TxtUserId.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.TxtUserId.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TxtUserId.CustomButton.TabIndex = 1;
            this.TxtUserId.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TxtUserId.CustomButton.UseSelectable = true;
            this.TxtUserId.CustomButton.Visible = false;
            this.TxtUserId.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TxtUserId.Lines = new string[0];
            this.TxtUserId.Location = new System.Drawing.Point(202, 93);
            this.TxtUserId.MaxLength = 12;
            this.TxtUserId.Name = "TxtUserId";
            this.TxtUserId.PasswordChar = '\0';
            this.TxtUserId.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtUserId.SelectedText = "";
            this.TxtUserId.SelectionLength = 0;
            this.TxtUserId.SelectionStart = 0;
            this.TxtUserId.ShortcutsEnabled = true;
            this.TxtUserId.Size = new System.Drawing.Size(181, 23);
            this.TxtUserId.TabIndex = 0;
            this.TxtUserId.UseSelectable = true;
            this.TxtUserId.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TxtUserId.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.TxtUserId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUserId_KeyPress);
            // 
            // TxtUserPwd
            // 
            // 
            // 
            // 
            this.TxtUserPwd.CustomButton.Image = null;
            this.TxtUserPwd.CustomButton.Location = new System.Drawing.Point(159, 1);
            this.TxtUserPwd.CustomButton.Name = "";
            this.TxtUserPwd.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.TxtUserPwd.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TxtUserPwd.CustomButton.TabIndex = 1;
            this.TxtUserPwd.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TxtUserPwd.CustomButton.UseSelectable = true;
            this.TxtUserPwd.CustomButton.Visible = false;
            this.TxtUserPwd.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TxtUserPwd.Lines = new string[0];
            this.TxtUserPwd.Location = new System.Drawing.Point(202, 122);
            this.TxtUserPwd.MaxLength = 32767;
            this.TxtUserPwd.Name = "TxtUserPwd";
            this.TxtUserPwd.PasswordChar = '●';
            this.TxtUserPwd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtUserPwd.SelectedText = "";
            this.TxtUserPwd.SelectionLength = 0;
            this.TxtUserPwd.SelectionStart = 0;
            this.TxtUserPwd.ShortcutsEnabled = true;
            this.TxtUserPwd.Size = new System.Drawing.Size(181, 23);
            this.TxtUserPwd.TabIndex = 1;
            this.TxtUserPwd.UseSelectable = true;
            this.TxtUserPwd.UseSystemPasswordChar = true;
            this.TxtUserPwd.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TxtUserPwd.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.TxtUserPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtUserPwd_KeyPress);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(168, 175);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(81, 41);
            this.BtnOk.TabIndex = 2;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseSelectable = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(263, 175);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(81, 41);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseSelectable = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 239);
            this.ControlBox = false;
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.TxtUserPwd);
            this.Controls.Add(this.TxtUserId);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "LoginForm";
            this.Resizable = false;
            this.Text = "Login";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox TxtUserId;
        private MetroFramework.Controls.MetroTextBox TxtUserPwd;
        private MetroFramework.Controls.MetroButton BtnOk;
        private MetroFramework.Controls.MetroButton BtnCancel;
    }
}