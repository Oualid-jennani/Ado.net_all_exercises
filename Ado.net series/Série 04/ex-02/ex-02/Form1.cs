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

namespace ex_02
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Server=.\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlCommand cmd;
        SqlDataAdapter Da;
        DataSet Ds = new DataSet();
        DataRelation dr,dr1;

        BindingSource Bs = new BindingSource();
        public Form1()
        {
            InitializeComponent();

            Da = new SqlDataAdapter("select * from ETUDIANT", cn);
            Da.Fill(Ds, "ETUDIANT");
            Da = new SqlDataAdapter("select * from EVALUER", cn);
            Da.Fill(Ds, "EVALUER");
            dr = new DataRelation("EtudiantEvalue", Ds.Tables["ETUDIANT"].Columns["NumEtudiant"], Ds.Tables["EVALUER"].Columns["NumEtudiant"]);
            Ds.Relations.Add(dr);
            //---------------------------------------------------------
            Da = new SqlDataAdapter("select * from MATIERE", cn);
            Da.Fill(Ds, "MATIERE");
            dr1 = new DataRelation("ValueMatiere", Ds.Tables["MATIERE"].Columns["NumMat"], Ds.Tables["EVALUER"].Columns["NumMat"]);
            Ds.Relations.Add(dr1);

            Da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            // <==1
            //dataGridView1.DataSource = Ds.Tables["ETUDIANT"];
            //Ds.Tables["EVALUER"].Columns.Add("Nom ", typeof(string), "Parent(EtudiantEvalue).nom");


            Bs.DataSource = Ds;
            Bs.DataMember = "ETUDIANT";
            dataGridView1.DataSource = Bs;

            BindingSource Bs2 = new BindingSource();
            Bs2.DataSource = Bs;
            Bs2.DataMember = "EtudiantEvalue";
            dataGridView2.DataSource = Bs2;

            //dataGridView2.Columns[0].Visible = false;
            //dataGridView2.Columns[1].Visible = false;
            Ds.Tables["EVALUER"].Columns.Add("Nom ", typeof(string), "Parent(EtudiantEvalue).nom");
            dataGridView2.Columns[4].DisplayIndex = 0;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // <==1
            //DataView dv = new DataView(Ds.Tables["ETUDIANT"]);
            //DataRowView ligne = dv[dataGridView1.CurrentRow.Index];
            //dataGridView2.DataSource = ligne.CreateChildView(dr);

            //dataGridView2.Columns[0].Visible = false;
            //dataGridView2.Columns[1].Visible = false;
            //dataGridView2.Columns[4].DisplayIndex = 0;
        }
    }
}
