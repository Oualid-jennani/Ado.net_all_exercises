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

namespace ex_01
{
    public partial class EX1 : Form
    {
        public SqlConnection cn = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        public SqlCommand cmd;
        public SqlDataAdapter Da;
        public DataSet Ds = new DataSet();
        DataView dv1, dv2;

        public EX1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Datasource();
        }

        public void Datasource()
        {
            Ds.Clear();
            cn.Open();
            Da = new SqlDataAdapter("select * from MATIERE", cn);
            Da.Fill(Ds, "EVALUER");
            Da = new SqlDataAdapter("select * from ETUDIANT", cn);
            Da.Fill(Ds, "ETUDIANT");

            dv1 = new DataView(Ds.Tables["ETUDIANT"], "", "nom", DataViewRowState.CurrentRows);
            //dv1.Sort = "nom ASC";
            //dv1.AllowEdit = true;
            //dv1.AllowNew = true;
            //dv1.AllowDelete = true;

            dv2 = new DataView(Ds.Tables["EVALUER"], "", "LibelléMat", DataViewRowState.CurrentRows);
            dv2.AllowEdit = true;
            dv2.AllowNew = true;
            dv2.AllowDelete = true;

            dataGridView1.DataSource = dv1;
            dataGridView2.DataSource = dv2;
            cn.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dv2.RowFilter = "LibelléMat like '%" + textBox2.Text + "%'";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder CMB = new SqlCommandBuilder(Da);
            Da.Update(Ds.Tables[1]);
            Datasource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ajouter Aj = new Ajouter();
            Aj.ShowDialog();
            this.Datasource();
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            DialogResult Res= MessageBox.Show("voulez-vous vraiment supprimer ?", "Confermation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Res == DialogResult.Yes)
            {
                SqlCommandBuilder CMB = new SqlCommandBuilder(Da);
                Da.Update(Ds.Tables[1]);
                Datasource();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dv1.RowFilter = "nom like '%"+textBox1.Text+"%'";
        }
    }
}
