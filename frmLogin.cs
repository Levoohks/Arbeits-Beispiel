using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Firebase

using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Crypto_Alt_Gen
{
    public partial class frmLogin : Form
    {

        int mov;
        int movX;
        int movY;

        //Defines Interal Version for Updates
        private string curVer = "1";

        private string _errorOut;

        public frmLogin()
        {
            InitializeComponent();
        }

        IFirebaseClient client;

        IFirebaseConfig ifc = new FirebaseConfig
        {
            AuthSecret = "XU4hU2oQsdQeOlOwnNimqYWyOyAIHLSGTV5hmWzE",
            BasePath = "https://altgen-4c3f8.firebaseio.com/"
        };

        private void btnClick_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void checkForUpdate()
        {

            var result = client.Get("Server/" + frmLogin._txtUsername);
            UpdateClass std = result.ResultAs<UpdateClass>();
            string ver = std.Version;
            if (curVer != ver)
            {
                informationBox("The Application needs a Update!" + Environment.NewLine + "Download the latest version from our Website");
                Process.Start("https://www.cryptogen.de/download/");
                Environment.Exit(0);
            }

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FirebaseClient(ifc);
            }
            catch (Exception e1)
            {
                _errorOut = e1.ToString();
                errorBox("Failed connection to Server!");
                Environment.Exit(0);
            }

            checkForUpdate();

        }

        public static void informationBox(string Message)
        {
            MessageBox.Show(Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void errorBox(string Message)
        {
            MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void pnlTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void pnlTop_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        public static string _txtUsername;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FirebaseResponse res = client.Get(@"Users/" + txtUsername.Text);

            UserClass ResUser = res.ResultAs<UserClass>();

            UserClass CurUser = new UserClass()
            {
                UserName = txtUsername.Text,
                Password = txtPassword.Text
            };

            if (UserClass.IsEqual(ResUser, CurUser))
            {
                informationBox("Welcome, " + txtUsername.Text);
                _txtUsername = txtUsername.Text;
                Form frmMain = new frmMain();
                frmMain.Show();
                this.Hide();
            }
            else
            {
                errorBox("Wrong Username / Password");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.cryptogen.de/help/");
        }
    }
}