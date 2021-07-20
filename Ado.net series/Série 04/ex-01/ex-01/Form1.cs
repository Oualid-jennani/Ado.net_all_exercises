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
        SqlConnection cn = new SqlConnection(@"Server=.\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlCommand cmd;
        SqlDataAdapter Da;
        DataSet Ds = new DataSet();
        DataRelation dr;
        public Form1()
        {
            InitializeComponent();

            Da = new SqlDataAdapter("select * from ETUDIANT", cn);
            Da.Fill(Ds, "ETUDIANT");

            Da = new SqlDataAdapter("select * from EVALUER", cn);
            Da.Fill(Ds, "EVALUER");

            //Da = new SqlDataAdapter("select * from MATIERE", cn);
            //Da.Fill(Ds, "MATIERE");


            DataColumn Parent = Ds.Tables["ETUDIANT"].Columns["NumEtudiant"];
            DataColumn Child = Ds.Tables["EVALUER"].Columns["NumEtudiant"];

            //DataColumn Parent2 = Ds.Tables["MATIERE"].Columns["NumMat"];
            //DataColumn Child2 = Ds.Tables["EVALUER"].Columns["NumMat"];



            dr = new DataRelation("EtudiantEvalue", Parent, Child);
            Ds.Relations.Add(dr);

            //dr = new DataRelation("ValueMatiere", Parent2, Child2);
            //Ds.Relations.Add(dr);


            dataGrid1.SetDataBinding(Ds, "ETUDIANT");
            //dataGrid1.DataSource = Ds.Tables[0];
        }
      
    }
}
