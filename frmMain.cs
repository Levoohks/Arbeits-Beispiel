using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class frmMain : Form
    {

        int mov;
        int movX;
        int movY;

        public frmMain()
        {
            InitializeComponent();
        }

        IFirebaseClient client;

        IFirebaseConfig ifc = new FirebaseConfig
        {
            AuthSecret = "XU4hU2oQsdQeOlOwnNimqYWyOyAIHLSGTV5hmWzE",
            BasePath = "https://altgen-4c3f8.firebaseio.com/"
        };

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
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

        private void label3_Click(object sender, EventArgs e)
        {
            pnlUserSettings.Visible = false;
        }

        private string _errorOut;

        public static void errorBox(string Message)
        {
            MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void informationBox(string Message)
        {
            MessageBox.Show(Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmMain_Load(object sender, EventArgs e)
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

            pnlUserSettings.Visible = false;
            pnlAddCoins.Visible = false;

            var result = client.Get("Users/" + frmLogin._txtUsername);
            CoinsClass std = result.ResultAs<CoinsClass>();
            int _Coins = std.Coins;
            lblCoins.Text = _Coins.ToString();
            Properties.Settings.Default.coins = _Coins;
            Properties.Settings.Default.Save();

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            pnlUserSettings.Visible = true;

            var result = client.Get("Users/" + frmLogin._txtUsername);
            CoinsClass std = result.ResultAs<CoinsClass>();
            int _Coins = std.Coins;
            lblCoins.Text = _Coins.ToString();
            Properties.Settings.Default.coins = _Coins;
            Properties.Settings.Default.Save();

        }

        public static int usrCoins;

        public void removeCoins(int _coins)
        {
            CoinsClass std = new CoinsClass()
            {
                Coins = Properties.Settings.Default.coins - _coins
            };
            Properties.Settings.Default.Save();
            var clUpdater = client.Update("Users/" + frmLogin._txtUsername, std);
            Properties.Settings.Default.coins = std.Coins;
        }

        public void addCoins(int _coins)
        {
            CoinsClass std = new CoinsClass()
            {
                Coins = Properties.Settings.Default.coins + _coins
            };
            Properties.Settings.Default.Save();
            var clUpdater = client.Update("Users/" + frmLogin._txtUsername, std);
            Properties.Settings.Default.coins = std.Coins;
        }

        public void setKeyUsed(string _Bool)
        {
            KeyClass std = new KeyClass()
            {
                used = "True" 
            };
            var clUpdater = client.Update("Keys/" + txtKey.Text, std);
        }

        private void btnGenMinecraft_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.coins > 99)
            {
                informationBox("Bought!");
                removeCoins(100);
            }
            else
            {
                errorBox("Not enough Coins!");
            }
        }

        private void btnGenFortnite_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.coins > 299)
            {
                informationBox("Bought!");
                removeCoins(300);
            }
            else
            {
                errorBox("Not enough Coins!");
            }
        }

        private void btnGenSpotify_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.coins > 149)
            {
                informationBox("Bought!");
                removeCoins(150);
            }
            else
            {
                errorBox("Not enough Coins!");
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            FirebaseResponse res = client.Get(@"Keys/" + txtKey.Text);

            KeyClass ResUser = res.ResultAs<KeyClass>();

            KeyClass CurKey = new KeyClass()
            {
                Key = txtKey.Text,
            };

            if (KeyClass.IsEqual(ResUser, CurKey))
            {
               CurKey.used = ResUser.used;
                CurKey.amount = ResUser.amount;

               if (CurKey.used == "False")
                {
                    informationBox("Code '" + CurKey.Key + "' Successfully redeemed!" + Environment.NewLine + "+ Added " + CurKey.amount + " coins");
                    setKeyUsed("True");
                    addCoins(CurKey.amount);
                }
                else
                {
                    errorBox("Wrong Code or already used!");
                }
            }
            else
            {
                errorBox("Wrong Code or already used!");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            pnlAddCoins.Visible = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            pnlAddCoins.Visible = true;
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {

        }
    }
}