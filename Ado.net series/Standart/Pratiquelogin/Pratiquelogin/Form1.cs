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

namespace Pratiquelogin
{
    
    public partial class Login : Form
    {
        SqlConnection con;
        public Login()
        {
            string cons = @"Server = DESKTOP-4TIUIM2\SQLEXPRESS; DataBase = serie_4; Integrated Security = true";
            con = new SqlConnection(cons);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ajouter ajouter = new ajouter();
            ajouter.ShowDialog();
        }

        private void btn_conex_Click(object sender, EventArgs e)
        {
           string pass = Util.MD5Hash(tb_pass.Text);
            try
            {
                con.Open();
                string rqt = "select COUNT(*) from ADMIN where [Name] = @login  and [PassW] = @pass  ";
            
                SqlParameter p1 = new SqlParameter("@login", tb_nom.Text);
                SqlParameter p2 = new SqlParameter("@pass", pass);             
                SqlCommand com = new SqlCommand(rqt, con);
                com.Parameters.Add(p1);
                com.Parameters.Add(p2);
                SqlDataReader dr = com.ExecuteReader();

                dr.Read();
                Console.WriteLine(dr[0].ToString());
                Console.WriteLine(pass);
                    if (dr[0].ToString()=="1")
                    {
                   
                            MessageBox.Show("Binevenu Admine '" + tb_nom.Text + "'","Binevenu",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            Login login = new Login();
                            login.Hide();
                            Form2 form2 = new Form2();
                            form2.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("le nom de utilisateur ou le mote de passe incorecte !!!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    dr.Close();
            }
            catch (Exception ve)
            {
                MessageBox.Show(ve.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_annuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_changepass_Click(object sender, EventArgs e)
        {

        }
    }
}
