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
    public partial class EX2 : Form
    {
        DataSet Ds = new DataSet();
        DataTable DT = new DataTable();
        DataRow DR;
        public EX2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //foreach (DataGridViewTextBoxColumn DC in dataGridView1.Columns)
                //    DT.Columns.Add(DC.HeaderText.ToString());

                foreach (DataGridViewRow DG in dataGridView1.Rows)
                {
                    DR = DT.NewRow();

                    for (int i = 0; i < DG.Cells.Count; i++)
                    {
                        DR[i] = DG.Cells[i].Value;
                    }
                    DT.Rows.Add(DG);
                }



                Ds.Tables.Add(DT);
                Ds.WriteXml("" + textBox1.Text + ".xml");
                Ds.Clear();
                dataGridView1.Columns.Clear();

                DT.Clear();
                MessageBox.Show("Exported successful");
            }
            catch (Exception ex)
            {
                Ds.Clear();
                DT.Clear();
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ds.Clear();
            Ds.Reset();
            dataGridView1.Columns.Clear();
            Ds.ReadXml("" + textBox1.Text + ".xml");
            dataGridView1.DataSource = Ds.Tables[0];
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DT.Clear();
            Ds.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            DT.Clear();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (textBox2.Text != "")
            {
                try
                {
                    DT.Columns.Add(textBox2.Text);
                    dataGridView1.Columns.Add(textBox2.Text, textBox2.Text);
                }
                catch (Exception ex)
                {
                    Ds.Clear();
                    DT.Clear();
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
