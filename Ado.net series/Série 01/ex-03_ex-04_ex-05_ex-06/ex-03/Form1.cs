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
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlDataAdapter Da;
        DataSet ds = new DataSet();
        DataTable DT = new DataTable();

        /************************************/

        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        public void combX()
        {

            comboBox1.DataSource = null;
            ds.Tables.Clear();

            Da = new SqlDataAdapter("select NumEtudiant from ETUDIANT", cn);
            Da.Fill(ds);

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "NumEtudiant";
            comboBox1.ValueMember = "NumEtudiant";
            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds.Tables.Clear();
            Da = new SqlDataAdapter("select * From ETUDIANT Where nom like '%" + textBox1.Text + "%'", cn);
            Da.Fill(ds,"saersh");
            dataGridView1.DataSource = ds.Tables["saersh"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            combX();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.TryParse(comboBox1.SelectedValue.ToString(), out id) ? id : -1;
            if (id != -1)
            {
                try
                {
                    cn.Open();
                    cmd = new SqlCommand("select * from ETUDIANT where NumEtudiant = " + id + "", cn);
                    dr = cmd.ExecuteReader();

                    dr.Read();
                    textBox2.Text = dr[0].ToString();
                    textBox3.Text = dr[1].ToString();
                    textBox4.Text = dr[2].ToString();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    cn.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cmd = new SqlCommand("delete from EVALUER where NumEtudiant = " + textBox2.Text.ToString() + "" +
                    " \n delete from ETUDIANT where NumEtudiant = " + textBox2.Text.ToString() + "", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
            combX();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cmd = new SqlCommand("update ETUDIANT set nom ='" + textBox3.Text.ToString() + "',prénom = '" + textBox4.Text.ToString() + "' where NumEtudiant = " + textBox2.Text.ToString() + "", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cmd = new SqlCommand("insert into ETUDIANT(nom,prénom) values('" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() +"')", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
            combX();
        }
    }
}
