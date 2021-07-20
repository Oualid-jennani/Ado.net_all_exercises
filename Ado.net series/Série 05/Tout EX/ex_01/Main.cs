using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ex_01
{
    public partial class Main : Form
    {
        EX1 ex1 = new EX1();
        EX2 ex2 = new EX2();
        EX4 ex4 = new EX4();

        int PZ, posX, posY;

        public Main()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ex1.TopLevel = false;
            panel1.Controls.Add(ex1);
            ex2.Hide();
            ex4.Hide();

            ex1.Show();
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ex2.TopLevel = false;
            panel1.Controls.Add(ex2);

            ex1.Hide();
            ex4.Hide();

            ex2.Show();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            ex4.TopLevel = false;

            panel1.Controls.Add(ex4);

            ex1.Hide();
            ex2.Hide();

            ex4.Show();
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            PZ = 1;
            posX = e.X;
            posY = e.Y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (PZ == 1)
            {
                this.SetDesktopLocation(MousePosition.X - posX, MousePosition.Y - posY);
            }
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            PZ = 0;
        }
    }
}
