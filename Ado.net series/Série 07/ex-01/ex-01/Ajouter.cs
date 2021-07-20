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
    public partial class Ajouter : Form
    {
        Form1 F = new Form1();
        int PZ, posX, posY;
        public Ajouter()
        {
            InitializeComponent();
        }

        private void Ajouter_MouseDown(object sender, MouseEventArgs e)
        {
            PZ = 1;
            posX = e.X;
            posY = e.Y;
        }

        private void Ajouter_MouseMove(object sender, MouseEventArgs e)
        {
            if (PZ == 1)
            {
                this.SetDesktopLocation(MousePosition.X - posX, MousePosition.Y - posY);
            }
        }

        private void Ajouter_MouseUp(object sender, MouseEventArgs e)
        {
            PZ = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    F.cn.Open();
                    F.cmd = new SqlCommand("insert into etudiant (nom,prenom) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "')", F.cn);
                    F.cmd.ExecuteNonQuery();
                    F.cn.Close();
                    this.Close();
                    MessageBox.Show("Bien ajouter", "Ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("pas information !!!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            F.Datasource();
        }
    }
}
