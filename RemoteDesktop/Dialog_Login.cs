using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDesktop
{
    public partial class Dialog_Login : Form
    {
        public string Server
        {
            get
            {
                return this.textBox_Server.Text;
            }
        }
        public string User
        {
            get
            {
                return this.textBox_User.Text;
            }
        }
        public string Password
        {
            get
            {
                return this.textBox_Password.Text;
            }
        }
        public int Port
        {
            get
            {
                int temp = 0;
                int.TryParse(this.textBox_Port.Text, out temp);
                return temp;
            }
        }

        public Dialog_Login()
        {
            InitializeComponent();
        }

        private void Dialog_Login_Load(object sender, EventArgs e)
        {
            this.button_確認.Click += Button_確認_Click;
        }

        private void Button_確認_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
