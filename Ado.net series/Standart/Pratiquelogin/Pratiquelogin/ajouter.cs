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
    public partial class ajouter : Form
    {
        SqlConnection con;
        public ajouter()
        {
            string cons = @"Server=DESKTOP-4TIUIM2\SQLEXPRESS; Database=Exercise02; Integrated Security=true";
            con = new SqlConnection(cons);
            InitializeComponent();
        }

        private void btn_annuler_Click(object sender, EventArgs e)
        {
            tb_nom.Text = "";
            tb_pass.Text = "";
        }

        private void btn_ajouter_Click(object sender, EventArgs e)
        {
           string pass =   Util.MD5Hash(tb_pass.Text);
            try
            {
                con.Open();
                string reqt = "insert into [dbo].[logine] (nom_utilisateur,mot_de_passe,question) values('" + tb_nom.Text + "', '" +pass+ "', '"+tb_alterpass+"')";
                SqlCommand cmd = new SqlCommand(reqt, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception d)
            {
                MessageBox.Show(d.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
