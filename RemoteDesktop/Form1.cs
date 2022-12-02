using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using AxMSTSCLib;
using MSTSCLib;
namespace RemoteDesktop
{
    public partial class Form1 : Form
    {
        private AxMSTSCLib.AxMsRdpClient9NotSafeForScripting rdpc = null;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Dialog_Login dialog_Login = new Dialog_Login();
            dialog_Login.ShowDialog();
            OnCreateControl();
   
            rdpc.Server = dialog_Login.Server;
            rdpc.AdvancedSettings2.RDPPort = dialog_Login.Port;
            rdpc.UserName = dialog_Login.User;
            rdpc.AdvancedSettings2.ClearTextPassword = dialog_Login.Password;
            //var settings = (rdpc.GetOcx() as IMsRdpExtendedSettings);
            //object otrue = true;
            //settings.set_Property("ConnectToChildSession", ref otrue);
            rdpc.AdvancedSettings9.EnableCredSspSupport = true;
            rdpc.AdvancedSettings9.SmartSizing = false;
            rdpc.AdvancedSettings9.DisplayConnectionBar = false;
            rdpc.AdvancedSettings9.RedirectSmartCards = true;
            rdpc.ConnectingText = "......";
            rdpc.Connect();
            //Machine machine = new Machine();
            //machine.MachineName = "192.168.0.16";
            //machine.Password = "";
            //machine.UserName = "HS";
            //machine.Port = 3389;
            //Connect(machine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }


        protected void OnCreateControl()
        {
            rdpc = new AxMsRdpClient9NotSafeForScripting();
            ((System.ComponentModel.ISupportInitialize)(this.rdpc)).BeginInit();
            this.SuspendLayout();
            rdpc.OnDisconnected += new AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEventHandler(rdpc_OnDisconnected);
            rdpc.Width = 1920;
            rdpc.Height = 1080;
            rdpc.Dock = DockStyle.Fill;
            this.Controls.Add(rdpc);
            this.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdpc)).EndInit();
        }

        void rdpc_OnDisconnected(object sender, AxMSTSCLib.IMsTscAxEvents_OnDisconnectedEvent e)
        {
            //處理斷開連接
        }


        public void Disconnect()
        {
            try
            {
                if (rdpc.Connected == 1)
                {
                    rdpc.Disconnect();
                }
            }
            catch (Exception)
            {

            }

        }
        private void SetRdpClientProperties(Machine parMachine)
        {
            rdpc.Server = parMachine.MachineName;
            rdpc.AdvancedSettings2.RDPPort = parMachine.Port;
            rdpc.UserName = parMachine.UserName;
            rdpc.Domain = parMachine.DomainName;
            if (parMachine.Password != "")
            {
                rdpc.AdvancedSettings5.ClearTextPassword = parMachine.Password;
            }
            rdpc.AdvancedSettings5.RedirectDrives = parMachine.ShareDiskDrives;
            rdpc.AdvancedSettings5.RedirectPrinters = parMachine.SharePrinters;
            rdpc.ColorDepth = (int)parMachine.ColorDepth;
        }

        public void Connect(Machine parMachine)
        {
            SetRdpClientProperties(parMachine);
            rdpc.Connect();
        }

        //遠程主機配置
        [Serializable()]
        public class Machine
        {
            private string _RemoteDesktopConnectionName;
            public string RemoteDesktopConnectionName
            {
                get { return _RemoteDesktopConnectionName; }
                set { _RemoteDesktopConnectionName = value; }
            }

            private string _MachineName;
            public string MachineName
            {
                get { return _MachineName; }
                set { _MachineName = value; }
            }
            private string _DomainName;
            public string DomainName
            {
                get { return _DomainName; }
                set { _DomainName = value; }
            }

            private string _UserName;
            public string UserName
            {
                get { return _UserName; }
                set { _UserName = value; }
            }

            private string _Password;
            public string Password
            {
                get { return _Password; }
                set { _Password = value; }
            }

            private bool _AutoConnect;
            public bool AutoConnect
            {
                get { return _AutoConnect; }
                set { _AutoConnect = value; }
            }

            private bool _ShareDiskDrives;
            public bool ShareDiskDrives
            {
                get { return _ShareDiskDrives; }
                set { _ShareDiskDrives = value; }
            }

            private bool _SharePrinters;
            public bool SharePrinters
            {
                get { return _SharePrinters; }
                set { _SharePrinters = value; }
            }

            private bool _SavePassword;
            public bool SavePassword
            {
                get { return _SavePassword; }
                set { _SavePassword = value; }
            }

            private Colors _ColorDepth;
            public Colors ColorDepth
            {
                get { return _ColorDepth; }
                set { _ColorDepth = value; }
            }

            public int Port
            {
                get
                {
                    return _Port;
                }

                set
                {
                    _Port = value;
                }
            }

            private int _Port;


            public enum Colors
            {
                HighColor15 = 15,
                HighColor16 = 16,
                Color256 = 8,
                TrueColor = 24
            }
        }
    }
}
