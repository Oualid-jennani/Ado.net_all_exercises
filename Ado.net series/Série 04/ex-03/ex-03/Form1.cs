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
namespace ex_03
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Server=.\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlCommand cmd;
        DataSet Ds = new DataSet();
        SqlDataAdapter parentAdapter;
        SqlDataAdapter childAdapter;

        public Form1()
        {
            InitializeComponent();

            parentAdapter = new SqlDataAdapter("SELECT * FROM ETUDIANT", cn);
            parentAdapter.Fill(Ds, "ETUDIANT");

            childAdapter = new SqlDataAdapter("SELECT * FROM EVALUER", cn);
            childAdapter.Fill(Ds, "EVALUER");

            SqlCommandBuilder parentCmdBuilder = new SqlCommandBuilder(parentAdapter);
            SqlCommandBuilder childCmdBuilder = new SqlCommandBuilder(childAdapter);

            Ds.Relations.Add(new DataRelation("ParentChildRelation", Ds.Tables["ETUDIANT"].Columns["NumEtudiant"], Ds.Tables["EVALUER"].Columns["NumEtudiant"]));


            BindingSource bs = new BindingSource();
            bs.DataSource = Ds.Tables["ETUDIANT"];
            bindingNavigator1.BindingSource = bs;
            bindingNavigator2.BindingSource = bs;
            dataGridView1.DataSource = bs;
        }


        public void Chanched()
        {
            //SqlTransaction sqlTrans = cn.BeginTransaction();

            try
            {
                //childAdapter.SelectCommand.Transaction = sqlTrans;
                //childCmdBuilder.GetInsertCommand().Transaction = sqlTrans;
                childAdapter.Update(Ds.Tables["EVALUER"]);

                //parentAdapter.SelectCommand.Transaction = sqlTrans;
                //parentCmdBuilder.GetInsertCommand().Transaction = sqlTrans;
                parentAdapter.Update(Ds.Tables["ETUDIANT"]);

                //sqlTrans.Commit();
            }
            catch (Exception ex)
            {
                //sqlTrans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            Chanched();
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            Chanched();
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Chanched();
        }
    }
}
