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

namespace ex_02
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlDataAdapter Da;
        DataSet ds = new DataSet();
        DataTable DT = new DataTable();


        /*--------------------------------------------------*/

        OdbcConnection cn2 = new OdbcConnection(@"Driver={SQL SERVER};Server=DESKTOP-4TIUIM2\SQLEXPRESS;Database=serie_4");
        OdbcCommand cmd;
        OdbcDataReader dr;

        //--------1
        int position;
        //--------2
        int position_2 = 1, count;
        //--------3
        int position_3 = 0, max, min;

        public Form1()
        {
            InitializeComponent();
            //--------1
            Da = new SqlDataAdapter("select E.NumEtudiant,E.Nom+' '+E.Prénom as'nomSTG' ,G.moyenne from generalparetudiant G inner join ETUDIANT E on  E.NumEtudiant=G.NumEtudiant", cn);
            Da.Fill(ds);
            //--------2
            cn2.Open();
            cmd = new OdbcCommand("select  count(*) from generalparetudiant G inner join ETUDIANT E on  E.NumEtudiant=G.NumEtudiant ", cn2);
            dr = cmd.ExecuteReader();
            dr.Read();
            count = int.Parse(dr[0].ToString());
            dr.Close();cn2.Close();
            //--------3
            cn2.Open();
            cmd = new OdbcCommand("select  max(E.NumEtudiant),min(E.NumEtudiant) from generalparetudiant G inner join ETUDIANT E on  E.NumEtudiant=G.NumEtudiant ", cn2);
            dr = cmd.ExecuteReader();
            dr.Read();
            max = int.Parse(dr[0].ToString());
            min = int.Parse(dr[1].ToString());
            dr.Close(); cn2.Close();
            
        }

        //-------1
        /*********************************************************************************************************************************************/

        private void button1_Click(object sender, EventArgs e)
        {
            position = 0;
            PreSuiV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (position == 0) position = ds.Tables[0].Rows.Count - 1;
            else position--;
            PreSuiV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (position == ds.Tables[0].Rows.Count - 1) position = 0;
            else position++;
            PreSuiV();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            position = ds.Tables[0].Rows.Count - 1;
            PreSuiV();
        }
        public void PreSuiV()
        {
            textBox1.Text = ds.Tables[0].Rows[position][0].ToString();
            textBox2.Text = ds.Tables[0].Rows[position][1].ToString();
            textBox3.Text = ds.Tables[0].Rows[position][2].ToString();
        }

        //-------2
        /*********************************************************************************************************************************************/

        private void button7_Click(object sender, EventArgs e)
        {
            if (position_2 == 1) position_2 = count;
            else position_2--;
            PreSuiV_2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (position_2 == count) position_2 = 1;
            else position_2++;
            PreSuiV_2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            position_2 = count;
            PreSuiV_2();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            position_2 = 1;
            PreSuiV_2();
        }

        public void PreSuiV_2()
        {
            try
            {
                cn2.Open();
                cmd = new OdbcCommand("select * from (select ROW_NUMBER() OVER (ORDER BY E.NumEtudiant) AS RowNum , E.NumEtudiant,E.Nom+' '+E.Prénom as'nomSTG' ,G.moyenne from generalparetudiant G inner join ETUDIANT E on  E.NumEtudiant=G.NumEtudiant )AS [MyDerivedTable] WHERE  RowNum =" + position_2 + "", cn2);
                dr = cmd.ExecuteReader();

                dr.Read();
                textBox6.Text = dr[1].ToString();
                textBox5.Text = dr[2].ToString();
                textBox4.Text = dr[3].ToString();

                dr.Close();
                cn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dr.Close();
                cn2.Close();
            }
        }

        //-------3
        /*********************************************************************************************************************************************/

        private void button12_Click(object sender, EventArgs e)
        {
            premiere();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (position_3 == max) premiere();
            else
            suiv();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            if (position_3 == min) derniere();
            else
            pres();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            derniere();
        }

        public void pres()
        {
            try
            {
                cn2.Open();
                cmd = new OdbcCommand("select top(1) E.NumEtudiant,E.Nom + ' ' + E.Prénom as'nomSTG' ,G.moyenne from generalparetudiant G inner join ETUDIANT E on E.NumEtudiant = G.NumEtudiant where E.NumEtudiant<" + position_3 + "order by E.NumEtudiant desc", cn2);
                dr = cmd.ExecuteReader();

                dr.Read();
                position_3 = int.Parse(dr[0].ToString());
                textBox9.Text = dr[0].ToString();
                textBox8.Text = dr[1].ToString();
                textBox7.Text = dr[2].ToString();

                dr.Close();
                cn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dr.Close();
                cn2.Close();
            }
        }
        public void suiv()
        {
            try
            {
                cn2.Open();
                cmd = new OdbcCommand("select top(1) E.NumEtudiant,E.Nom + ' ' + E.Prénom as'nomSTG' ,G.moyenne from generalparetudiant G inner join ETUDIANT E on E.NumEtudiant = G.NumEtudiant where E.NumEtudiant>" + position_3 + "order by E.NumEtudiant", cn2);
                dr = cmd.ExecuteReader();

                dr.Read();
                position_3 = int.Parse(dr[0].ToString());
                textBox9.Text = dr[0].ToString();
                textBox8.Text = dr[1].ToString();
                textBox7.Text = dr[2].ToString();

                dr.Close();
                cn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dr.Close();
                cn2.Close();
            }

        }
        public void premiere()
        {
            try
            {
                cn2.Open();
                cmd = new OdbcCommand("select top(1) E.NumEtudiant,E.Nom + ' ' + E.Prénom as'nomSTG' ,G.moyenne from generalparetudiant G inner join ETUDIANT E on E.NumEtudiant = G.NumEtudiant ", cn2);
                dr = cmd.ExecuteReader();

                dr.Read();
                position_3 = int.Parse(dr[0].ToString());
                textBox9.Text = dr[0].ToString();
                textBox8.Text = dr[1].ToString();
                textBox7.Text = dr[2].ToString();

                dr.Close();
                cn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dr.Close();
                cn2.Close();
            }
        }
        public void derniere()
        {
            try
            {
                cn2.Open();
                cmd = new OdbcCommand("select top(1) E.NumEtudiant,E.Nom + ' ' + E.Prénom as'nomSTG' ,G.moyenne from generalparetudiant G inner join ETUDIANT E on E.NumEtudiant = G.NumEtudiant order by E.NumEtudiant desc", cn2);
                dr = cmd.ExecuteReader();

                dr.Read();
                position_3 = int.Parse(dr[0].ToString());
                textBox9.Text = dr[0].ToString();
                textBox8.Text = dr[1].ToString();
                textBox7.Text = dr[2].ToString();

                dr.Close();
                cn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                dr.Close();
                cn2.Close();
            }

        }


    }
}
