namespace BingMap
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
            this.BtnLogin = new System.Windows.Forms.Button();
            this.TbName = new System.Windows.Forms.TextBox();
            this.LlbNewAcount = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TbPassword = new System.Windows.Forms.TextBox();
            this.LlbGuestLogin = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnLogin
            // 
            this.BtnLogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnLogin.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnLogin.Location = new System.Drawing.Point(220, 277);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(110, 32);
            this.BtnLogin.TabIndex = 2;
            this.BtnLogin.Text = "Login";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // TbName
            // 
            this.TbName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbName.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbName.Location = new System.Drawing.Point(175, 189);
            this.TbName.MaximumSize = new System.Drawing.Size(200, 25);
            this.TbName.Name = "TbName";
            this.TbName.Size = new System.Drawing.Size(200, 25);
            this.TbName.TabIndex = 0;
            // 
            // LlbNewAcount
            // 
            this.LlbNewAcount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LlbNewAcount.AutoSize = true;
            this.LlbNewAcount.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LlbNewAcount.Location = new System.Drawing.Point(173, 333);
            this.LlbNewAcount.Name = "LlbNewAcount";
            this.LlbNewAcount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LlbNewAcount.Size = new System.Drawing.Size(60, 17);
            this.LlbNewAcount.TabIndex = 3;
            this.LlbNewAcount.TabStop = true;
            this.LlbNewAcount.Text = "新建帳號";
            this.LlbNewAcount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbNewAcount_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnLogin);
            this.panel1.Controls.Add(this.TbPassword);
            this.panel1.Controls.Add(this.TbName);
            this.panel1.Controls.Add(this.LlbGuestLogin);
            this.panel1.Controls.Add(this.LlbNewAcount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 500);
            this.panel1.TabIndex = 3;
            // 
            // TbPassword
            // 
            this.TbPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TbPassword.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TbPassword.Location = new System.Drawing.Point(175, 233);
            this.TbPassword.MaximumSize = new System.Drawing.Size(200, 25);
            this.TbPassword.Name = "TbPassword";
            this.TbPassword.Size = new System.Drawing.Size(200, 25);
            this.TbPassword.TabIndex = 1;
            // 
            // LlbGuestLogin
            // 
            this.LlbGuestLogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LlbGuestLogin.AutoSize = true;
            this.LlbGuestLogin.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LlbGuestLogin.Location = new System.Drawing.Point(323, 333);
            this.LlbGuestLogin.Name = "LlbGuestLogin";
            this.LlbGuestLogin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LlbGuestLogin.Size = new System.Drawing.Size(60, 17);
            this.LlbGuestLogin.TabIndex = 4;
            this.LlbGuestLogin.TabStop = true;
            this.LlbGuestLogin.Text = "訪客登入";
            this.LlbGuestLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlbGuestLogin_LinkClicked);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 500);
            this.Controls.Add(this.panel1);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.TextBox TbName;
        private System.Windows.Forms.LinkLabel LlbNewAcount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TbPassword;
        private System.Windows.Forms.LinkLabel LlbGuestLogin;
    }
}