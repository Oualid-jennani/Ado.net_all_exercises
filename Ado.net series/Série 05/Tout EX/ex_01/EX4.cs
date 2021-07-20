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
   
    public partial class EX4 : Form
    {
        EX1 E1 = new EX1();
        SqlConnection cn1 = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlConnection cn2 = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");

        DataTable DT = new DataTable();

        SqlDataReader dr, dr1;
        SqlCommand cmd;

        public EX4()
        {
            InitializeComponent();

            dataGridView1.Columns.Add("nombre1", "nombre matiere");
            dataGridView1.Columns.Add("nombre2", "Module");
            dataGridView1.Columns.Add("nombre2", "nombre disponible");

            /******************************/

            E1.cn.Open();
            E1.Da = new SqlDataAdapter( "select distinct NumMat from EVALUER", E1.cn);
            E1.Da.Fill(DT);
            comboBox1.DataSource = DT;
            comboBox1.DisplayMember = "NumMat";
            comboBox1.ValueMember = "NumMat";

            /******************************/

            E1.cmd = new SqlCommand("select distinct NumMat,LibelléMat  from MATIERE", E1.cn);
            dr = E1.cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1],count(int.Parse(dr[0].ToString())).ToString());
            }

            E1.cn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            E1.cn.Open();
            dataGridView1.Rows.Clear();

            E1.cmd = new SqlCommand("select distinct NumMat,LibelléMat  from MATIERE", E1.cn);
            dr = E1.cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], count(int.Parse(dr[0].ToString())).ToString());
            }

            E1.cn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = int.TryParse(comboBox1.SelectedValue.ToString(), out id) ? id : 0;

                cn2.Open();
                dataGridView1.Rows.Clear();

                E1.cmd = new SqlCommand("select NumMat,LibelléMat  from MATIERE where NumMat=" + id + "", cn2);
                dr = E1.cmd.ExecuteReader();
                dr.Read();

                dataGridView1.Rows.Add(dr[0], dr[1], count(int.Parse(dr[0].ToString())).ToString());

                cn2.Close();
            }
            catch (Exception ex)
            {
                cn2.Close();
            }
        }

        public int count(int cont)
        {

            int id = cont;
           
            cn1.Open();
            cmd = new SqlCommand("countSTG", cn1);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
            param.Value = id;
            cmd.Parameters.Add(param);
            dr1 = cmd.ExecuteReader();
            dr1.Read();
            int res = int.Parse(dr1[0].ToString());
            cn1.Close();

            return res;
        }
    }
}
