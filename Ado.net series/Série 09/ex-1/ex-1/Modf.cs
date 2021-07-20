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
    public partial class Modf : Form
    {
        Form1 fr = new Form1();
        public int pos, com;
        public Modf()
        {
            InitializeComponent();
            var comBX = from bx in fr.db.client select new { name = bx.nom + " " + bx.prenom, num = bx.numC };
            comboBox1.DataSource = comBX;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "num";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cl = fr.db.client.FirstOrDefault(c => c.numC.Equals(comboBox1.SelectedValue.ToString()));

            commande C = fr.db.commande.FirstOrDefault(CM => CM.numCom.Equals(fr.dataGridView1.Rows[pos].Cells["numCom"].Value.ToString()));
            //commande C = fr.db.commande.FirstOrDefault(CM => CM.numCom.Equals(this.com));

            C.dateCom = dateTimePicker1.Value;
            C.client = cl;
            fr.db.SubmitChanges();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
