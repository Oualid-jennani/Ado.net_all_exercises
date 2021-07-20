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
using System.Data.Odbc;

namespace ex_01
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlDataAdapter Da;
        DataTable DT = new DataTable();
        DataTable DT2 = new DataTable();

        /*--------------------------------------------------*/

        OdbcConnection cn2 = new OdbcConnection(@"Driver={SQL SERVER};Server=DESKTOP-4TIUIM2\SQLEXPRESS;Database=serie_4");
        OdbcCommand cmd;

        public Form1()
        {
            InitializeComponent();
            Da = new SqlDataAdapter("select NumEtudiant, nom+' '+prénom as'nom' from ETUDIANT", cn);
            Da.Fill(DT);

            comboBox1.DataSource = DT;
            comboBox1.DisplayMember = "nom";
            comboBox1.ValueMember = "NumEtudiant";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.TryParse(comboBox1.SelectedValue.ToString(), out id) ? id : 0;


            string req = "select Et.NumEtudiant, ET.Nom,Et.Prénom,m.LibelléMat,m.CoeffMat, AVG( Note )as'note' from  ETUDIANT Et inner join EVALUER e  on Et.NumEtudiant = e.NumEtudiant  inner join MATIERE m on m.NumMat=e.NumMat" +
                 " where Et.NumEtudiant=" + id + " group by Et.NumEtudiant,ET.Nom,Et.Prénom,m.LibelléMat,m.CoeffMat";

            try
            {
                Da = new SqlDataAdapter(req, cn);
                DT2.Clear();
                Da.Fill(DT2);
                dataGridView1.DataSource = DT2;
                cn.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            /*--------------------------------------------------*/
            dataGridView2.Rows.Clear();
            try
            {
                cn2.Open();
                cmd = new OdbcCommand(req, cn2);
                OdbcDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView2.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4],dr[5]);
                }
                dr.Close();
                cn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView2.Columns.Add("ID", "ID");
            dataGridView2.Columns.Add("Nom", "Nom");
            dataGridView2.Columns.Add("Prenom", "Prenom");
            dataGridView2.Columns.Add("matier", "matier");
            dataGridView2.Columns.Add("Coefficient", "Coefficient");
            dataGridView2.Columns.Add("Note", "Note");
        }
    }
}
