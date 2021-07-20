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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        SqlCommandBuilder cb;
        DataRelation dr;

        BindingSource Bs = new BindingSource();
        BindingSource Bs2 = new BindingSource();

        public Form2()
        {

            InitializeComponent();
            refresh();
        }
        public void refresh()
        {
            ds.Reset();
            Bs.DataSource = null;

            con.Open();

            da = new SqlDataAdapter(" select * from EVALUER", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(ds, "EVALUER");

            da = new SqlDataAdapter(" select * from ETUDIANT", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(ds, "ETUDIANT");

            dr = new DataRelation("ET_EV", ds.Tables["ETUDIANT"].Columns["NumEtudiant"], ds.Tables["EVALUER"].Columns["NumEtudiant"]);

            ds.Relations.Add(dr);

            cb = new SqlCommandBuilder(da);

            Bs.DataSource = ds;
            Bs.DataMember = "ETUDIANT";
            dg_etudiant.DataSource = Bs;


            Bs2.DataSource = Bs;
            Bs2.DataMember = "ET_EV";
            dataGridView1.DataSource = Bs2;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            ds.Tables["EVALUER"].Columns.Add("Nom ", typeof(string), "Parent(ET_EV).nom");
            dataGridView1.Columns[4].DisplayIndex = 0;

            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
          


            double moy = double.TryParse(bunifuMetroTextbox4.Text.ToString(), out moy) ? moy : 0;
            DataRow Ligne = ds.Tables["Etudiant"].NewRow();
            Ligne[1] = bunifuMetroTextbox6.Text;
            Ligne[2] = bunifuMetroTextbox5.Text;
            Ligne[3] = moy;
            ds.Tables["Etudiant"].Rows.Add(Ligne);
            da.Update(ds.Tables["Etudiant"]);

            refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double moy = double.TryParse(bunifuMetroTextbox4.Text.ToString(), out moy) ? moy : 0;
            DataRow Ligne = ds.Tables["Etudiant"].Rows.Find(Convert.ToInt32(dg_etudiant.SelectedRows[0].Cells[0].Value.ToString()));
            Ligne[1] = bunifuMetroTextbox6.Text;
            Ligne[2] = bunifuMetroTextbox5.Text;
            Ligne[3] = moy;
            da.Update(ds.Tables["Etudiant"]);

            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataRow Ligne = ds.Tables["Etudiant"].Rows.Find(Convert.ToInt32(dg_etudiant.SelectedRows[0].Cells[0].Value.ToString()));
                Ligne.Delete();
                da.Update(ds.Tables["Etudiant"]);
            }

            refresh();
        }


        private void dg_etudiant_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                bunifuMetroTextbox7.Text = dg_etudiant.SelectedRows[0].Cells[0].Value.ToString();
                bunifuMetroTextbox6.Text = dg_etudiant.SelectedRows[0].Cells[1].Value.ToString();
                bunifuMetroTextbox5.Text = dg_etudiant.SelectedRows[0].Cells[2].Value.ToString();
                bunifuMetroTextbox4.Text = dg_etudiant.SelectedRows[0].Cells[3].Value.ToString();
            }catch(Exception ex) { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control cr in bunifuGradientPanel2.Controls)
            {
                cr.ResetText();
            }
        }
    }
}
