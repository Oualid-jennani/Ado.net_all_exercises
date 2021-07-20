using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserControls
{
    public partial class Form1 : Form
    {
        UserControl1 U1 = new UserControl1();
        UserControl2 U2 = new UserControl2();
        int PZ, posX, posY;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel1.Width = 200;
            pictureBox1.Visible = true;
            pictureBox3.Visible = false;
            bunifuFlatButton1.Visible = true;
            bunifuFlatButton2.Visible = true;
            bunifuFlatButton3.Visible = true;
            label1.Visible = true;
            pictureBox4.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Width = 40;
            pictureBox1.Visible = false;
            pictureBox3.Visible = true;
            bunifuFlatButton1.Visible = false;
            bunifuFlatButton2.Visible = false;
            bunifuFlatButton3.Visible = false;
            label1.Visible = false;
            pictureBox4.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            U2.TabStop = false;
            panel3.Controls.Add(U1);
            U2.Hide();

            U1.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            U1.TabStop = false;
            panel3.Controls.Add(U2);
            U1.Hide();

            U2.Show();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            PZ = 1;
            posX = e.X;
            posY = e.Y;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            PZ = 0;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (PZ == 1)
            {
                this.SetDesktopLocation(MousePosition.X - posX, MousePosition.Y - posY);
            }
        }
    }
}
