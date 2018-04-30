using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;


namespace Final_Proj_v1
{
    public partial class frmSignIn : Form
    {
        public frmSignIn()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(3000);
            InitializeComponent();
            t.Abort();
            this.Show();
            this.Focus();
            this.TopMost = true;
            

            
        }

        public void StartForm()
        {
            Application.Run(new frmSplashScreen());
        }

        private void frmSignIn_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'userAuthenticationDataSet.UserData' table. You can move, or remove it, as needed.
            this.userDataTableAdapter.Fill(this.userAuthenticationDataSet.UserData);

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\UserAuthentication.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Select Users From UserData where Users='" + txtUserName.Text + "' and Password='" + txtPassword.Text + "'", con);

           DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0 && (txtUserName.Text!="admin"||txtUserName.Text!="Admin"))
            {
                this.Hide();
                frmMain ss = new frmMain();

                ss.Show();

            }
            else
            {
                MessageBox.Show("Please check your Username and Password","Wrong Credentials",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();
            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            
        }
    }
}
