using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ex_1
{
    public partial class alter : Form
    {
        prdComdbEntities ctx = new prdComdbEntities();

        int Num;
        public alter(int num , string nom, DateTime date)
        {
            InitializeComponent();

            dateTimePicker1.Value = date;
            label4.Text = num.ToString();

     
           
            var q = from ca in ctx.client select new { name = ca.nom + " " + ca.prenom, number = ca.numC };

            var v = ctx.client.Select(s => new { name = s.nom + " " + s.prenom, num = s.numC });


            comboBox1.DataSource = q.ToList();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "number";

            comboBox1.SelectedIndex = comboBox1.FindString(nom);

            this.Num = num;

        }

        private void Ok_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {

                commande c = ctx.commande.Find(Num);
                c.dateCom = dateTimePicker1.Value;
                c.client = ctx.client.Find(comboBox1.SelectedValue);
                ctx.SaveChanges();

                this.Close();
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
         
        }
    }
}
