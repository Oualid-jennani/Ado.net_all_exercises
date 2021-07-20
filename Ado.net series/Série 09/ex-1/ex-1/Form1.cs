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
    public partial class Form1 : Form
    {
        public DataClasses1DataContext db = new DataClasses1DataContext();
        public int indexR;
        public Form1()
        {
            InitializeComponent();
            refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.5;

            Ajout aj = new Ajout();
            aj.ShowDialog();
            refresh();

            this.Opacity = 1;
        }
        public void refresh()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = db.commande;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.5;

            Modf Md = new Modf();
            Md.pos = int.Parse(dataGridView1.CurrentRow.Index.ToString());
            Md.com = int.Parse(dataGridView1.SelectedRows[0].Cells["numCom"].Value.ToString());
            Md.Text = "Commande N° " + Md.com.ToString();
            Md.ShowDialog();
            refresh();

            this.Opacity = 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            commande C = db.commande.FirstOrDefault(CM => CM.numCom.Equals(dataGridView1.SelectedRows[0].Cells["numCom"].Value.ToString()));
            db.commande.DeleteOnSubmit(C);
            db.SubmitChanges();
           
            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //------1
            //var com = from c in db.commande join lc in db.ligneCommande on c equals (lc.commande) join p in db.produit on lc.produit equals (p) into gj
            //           select new
            //           {
            //               cmd1 = c.numC,
            //               cmd2 = c.dateCom,
            //               cmd3 = c.client.numC,
            //               cmd4 = c.client.ToString(),
            //               total = gj.Sum(x => x.pu)
            //           };
            //var comT = from T in com where T.total >= 1000 select T;
            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = comT;

            //------2
            var q = from c in db.commande where c.ligneCommande.Sum(lc => lc.produit.pu * lc.qt) >= 1000 select c;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = q;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var com = from c in db.commande  join lc in db.ligneCommande on c equals(lc.commande) join p in  db.produit on lc.produit equals(p) where p.numP ==2 select c;
            //var com = from c in db.commande where c.ligneCommande.Any(x => x.produit.numP == 2)select c ;
            //var com = db.commande.Where(x => x.ligneCommande.Any(p => p.produit.numP == 2));

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = com;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var com = from c in db.commande orderby c.dateCom select c;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = com ;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //------1
            //var com = from c in db.commande
            //          join lc in db.ligneCommande on c equals (lc.commande)
            //          join p in db.produit on lc.produit equals (p) into gj
            //          select new
            //          {
            //              cmd1 = c.numC,
            //              cmd2 = c.dateCom,
            //              cmd3 = c.client.numC,
            //              cmd4 = c.client.ToString(),
            //              cmd5 = c.dateCom,
            //              total = gj.Sum(x => x.pu)
            //          };
            //var comT = from T in com where T.total >= 500 && T.cmd5 > new  DateTime(2017,11,1) && T.cmd5 < new DateTime(2017, 11, 15) select T;

            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = comT;

            //------2
            var q = from c in db.commande where c.ligneCommande.Sum(lc => lc.produit.pu * lc.qt) >= 500 && c.dateCom >= new DateTime(2017, 11, 1) && c.dateCom <= new DateTime(2017, 11,  15)
                    && c.client.nom == "walid" select c;

            var v = db.commande.Where(c => c.ligneCommande.Sum(lc => lc.produit.pu * lc.qt) >= 500 && c.dateCom >= new DateTime(2017, 11, 1) && c.dateCom <= new DateTime(2017, 11, 15) && c.client.nom == "walid").ToList();

            //------3
            //var q = from c in db.commande where c.client.nom == "walid" && c.dateCom > new DateTime(2017, 11, 1) &&
            //        c.dateCom < new DateTime(2017, 11, 15) && c.ligneCommande.Sum(lc => lc.produit.pu * lc.qt) >= 500 select c;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = q;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var q1 = from c in db.commande where c.ligneCommande.Any(lc => lc.numP == 2) && c.ligneCommande.Count(lc => lc.numP == 2) == 1 select c;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = q1;
        }
    }
}
