using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ex_1
{
    public partial class Form1 : Form
    {
        prdComdbEntities ctx = new prdComdbEntities();
        short ce;
        string a;
        int q;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = ctx.commande.ToList();

            //var q = from ca in ctx.client select new { name = ca.nom + " " + ca.prenom, number = ca.numC };

            var q = ctx.client.Select(s => new { name = s.nom + " " + s.prenom, number = s.numC });
            comboBox1.DataSource = q.ToList();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "number";

            //q = from ca in ctx.produit select new { name = ca.libelle, number = ca.numP };

            q = ctx.produit.Select(s => new { name = s.libelle, number = s.numP });

            comboBox2.DataSource = q.ToList();
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "number";
            this.CONNECT();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        public void CONNECT()
        {

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ctx.commande.ToList();
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ce = short.Parse(comboBox1.SelectedValue.ToString());
            //MessageBox.Show("1");
            if (dateTimePicker1.Value < dateTimePicker2.Value)
            {
                //MessageBox.Show(comboBox1.SelectedValue.ToString());
                dataGridView1.DataSource = null;
                var q = from c in ctx.commande where c.client.numC == ce && c.dateCom < dateTimePicker2.Value && c.dateCom > dateTimePicker1.Value select c;

                var v = ctx.commande.Where(c => c.client.numC == ce && c.dateCom < dateTimePicker2.Value && c.dateCom > dateTimePicker1.Value).ToList();

                dataGridView1.DataSource = q.ToList();
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            else
                dataGridView1.DataSource = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ADD add = new ADD();
            add.ShowDialog();
            this.CONNECT();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int num = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            string nom = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
           
                
            alter alter = new alter(num, nom ,DateTime.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()));

            alter.ShowDialog();

            this.CONNECT();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {

                commande c = ctx.commande.Find(dataGridView1.SelectedRows[0].Cells[0].Value);

                ctx.commande.Remove(c); ctx.SaveChanges();
                this.CONNECT();
            }

        }
    }
}
 