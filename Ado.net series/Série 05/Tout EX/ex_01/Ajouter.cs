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
        EX1 E1 = new EX1();
        int PZ, posX, posY;
        public Ajouter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    E1.cn.Open();
                    E1.cmd = new SqlCommand("insert into ETUDIANT (nom,prénom) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "')", E1.cn);
                    E1.cmd.ExecuteNonQuery();
                    E1.cn.Close();
                    this.Close();
                    MessageBox.Show("Bien ajouter", "Ajout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("pas information !!!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            E1.Datasource();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ajouter_MouseUp(object sender, MouseEventArgs e)
        {
            PZ = 0;
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
    }
}
