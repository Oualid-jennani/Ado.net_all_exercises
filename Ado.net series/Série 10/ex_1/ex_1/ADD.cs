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
    public partial class ADD : Form
    {
        prdComdbEntities ctx = new prdComdbEntities();
        public ADD()
        {
            InitializeComponent();

            var q = from c in ctx.client select new { name = c.nom + " " + c.prenom, number = c.numC };
            //var qa = from c in db.clients select c.numC;
            comboBox1.DataSource = q.ToList();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "number";
        }
        private void Ok_Click(object sender, EventArgs e)
        {
            commande c = new commande();
            c.dateCom = dateTimePicker1.Value;
            c.client = ctx.client.Find(comboBox1.SelectedValue);

            ctx.commande.Add(c); ctx.SaveChanges();
            this.Close();
        }
    }
}
