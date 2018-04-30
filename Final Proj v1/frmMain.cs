using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Proj_v1
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            txtFirstName.Focus();
        }

        private void peopleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.peopleBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.phone2DataSet);
            this.peopleTableAdapter.Fill(this.phone2DataSet.People);

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'phone2DataSet.People' table. You can move, or remove it, as needed.
            this.peopleTableAdapter.Fill(this.phone2DataSet.People);

        }


        private bool IsValidData()
        {
            return Validator.IsPresent(txtFirstName) &&
                   Validator.IsPresent(txtLastName) &&
                   //Validator.IsPresent(txtEmail) &&
                   Validator.IsPresent(txtMobile);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            
            string pth = Application.StartupPath;
            string connectionString = String.Format("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename={0}\\Phone2.mdf;Integrated Security = True; Connect Timeout = 30", pth);
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                if (IsValidData())
                {


                    try
                    {
                        connection.Open();
                        
                        SqlCommand cmd = new SqlCommand("INSERT People(First, Last, Mobile, Email, Grp)  VALUES ('" + txtFirstName.Text + "', '" + txtLastName.Text + "' ,'" + txtMobile.Text + "' , '" + txtEmail.Text + "', '" + comboBoxCategory.Text + "')", connection);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Success!");
                        
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();

                        // Display();
                        this.peopleTableAdapter.Fill(this.phone2DataSet.People);
                    }
                }
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this contact?", "Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string pth = Application.StartupPath;
                string connectionString = String.Format("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename={0}\\Phone2.mdf;Integrated Security = True; Connect Timeout = 30", pth);
                SqlConnection connection = new SqlConnection(connectionString);
                //SqlConnection connection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Phone2.mdf;Integrated Security = True; Connect Timeout = 30");
                using (connection)

                {


                    connection.Open();
                    SqlCommand cmd_2 = new SqlCommand("Delete From People WHERE (Mobile='" + txtMobile.Text + "') ", connection);

                    cmd_2.ExecuteNonQuery();

                    MessageBox.Show("Contact Deleted Successfully!!!");


                    connection.Close();


                }
                this.peopleTableAdapter.Fill(this.phone2DataSet.People);
            }
            else return;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtFirstName.Focus();
            comboBoxCategory.SelectedIndex = -1;
        }

        private void peopleDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void peopleDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            txtFirstName.Text = peopleDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            txtLastName.Text = peopleDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtMobile.Text = peopleDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtEmail.Text = peopleDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            comboBoxCategory.Text = peopleDataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            frmSignIn form = new frmSignIn();
            this.Close();
            form.Show();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string pth = Application.StartupPath;
            string connectionString = String.Format("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename={0}\\Phone2.mdf;Integrated Security = True; Connect Timeout = 30", pth);
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                if (IsValidData())
                {


                    try
                    {
                        connection.Open();

                        SqlCommand cmd_2 = new SqlCommand("UPDATE People SET First='"+txtFirstName.Text+ "',Last='" + txtLastName.Text + "', Mobile='" + txtNewPhoneNumber.Text + "',Email='" + txtEmail.Text + "' WHERE (Mobile='"+txtMobile.Text+"') ", connection);
                        cmd_2.ExecuteNonQuery();

                        MessageBox.Show("Success!");
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();

                        // Display();
                        this.peopleTableAdapter.Fill(this.phone2DataSet.People);
                    }
                }
            }
            this.peopleTableAdapter.Fill(this.phone2DataSet.People);
        }

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.peopleTableAdapter.Search(this.phone2DataSet.People, firstToolStripTextBox.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void searchToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmAbout Form = new frmAbout();
            Form.ShowDialog();

        }
    }
    }

