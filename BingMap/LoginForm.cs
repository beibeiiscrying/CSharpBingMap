using BingMap.DB;
using BingMap.Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BingMap
{
    public partial class LoginForm : Form
    {
        private List<TextBox> m_TbList = new List<TextBox>(); 
            //{ TbName, TbPassword };
        public LoginForm()
        {
            InitializeComponent();

            string[] defaultTipString = { "Name", "Password" };
            m_TbList.Add(TbName);
            m_TbList.Add(TbPassword);
            foreach(var tb in m_TbList) {
                tb.ForeColor = SystemColors.ScrollBar;
                tb.Tag = m_TbList.IndexOf(tb);
                tb.Text = defaultTipString[(int)tb.Tag];
                tb.Enter += Tb_Enter;
                tb.Leave += Tb_Leave;
            }
        }

        private void Tb_Enter(object sender, EventArgs e)
        {

            TextBox tb = sender as TextBox;
            if (tb.ForeColor == SystemColors.ScrollBar) { // the TextBox is Empty.
                tb.Text = "";
                tb.ForeColor = Color.Black;
                if((int)tb.Tag == 1) {  tb.PasswordChar = '*'; } // if tb is TbPassWord
            }
        }

        private void Tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null || tb.Text.Length != 0) { return; }
            SetTbInit(tb);
        }

        private void SetTbInit(TextBox tb)
        {
            string[] defaultTipString = { "Name", "Password" };
            tb.PasswordChar = (char)0;
            tb.ForeColor = SystemColors.ScrollBar;
            tb.Text = defaultTipString[(int)tb.Tag];
        }

        public void OpenMap()
        {
            new MainForm(this).Show();
            Hide();
        }
        
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string name = this.TbName.Text;
            if (!DbHelper.CheckUserLogin(name, this.TbPassword.Text)) {
                MessageBox.Show("Login Fail");
                return;
            } 

            foreach(var tb in m_TbList) {
                tb.Text = String.Empty;
                SetTbInit(tb);
            }
            UserManager.Instance.UserLogin(name);
            OpenMap();
        }

        private void LlbGuestLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserManager.Instance.GuestLogin();
            OpenMap();
        }

        private void LlbNewAcount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string name = this.TbName.Text;
            string msg = string.Concat("Insert User ", name, " Success");
            if (DbHelper.AddUser(name, this.TbPassword.Text) <= 0) {
                msg = "Insert User Fail";
            } 
            MessageBox.Show(msg);
        }
    }
}
