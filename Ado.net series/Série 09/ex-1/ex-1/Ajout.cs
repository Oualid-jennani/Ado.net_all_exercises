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
    public partial class Ajout : Form
    {
        Form1 fr = new Form1();
        public Ajout()
        {
            InitializeComponent();
            var comBX = from bx in fr.db.client select new { name = bx.nom+" "+bx.prenom,num= bx.numC };
            comboBox1.DataSource = comBX;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "num";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cl = fr.db.client.FirstOrDefault(c => c.numC.Equals(comboBox1.SelectedValue.ToString()));

            commande C = new commande();
            C.dateCom = dateTimePicker1.Value;
            C.client = cl;
            fr.db.commande.InsertOnSubmit(C);
            fr.db.SubmitChanges();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
