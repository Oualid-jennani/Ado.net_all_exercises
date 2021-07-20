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
    public partial class LOG : Form
    {
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlCommand cmd;
        SqlDataAdapter Da;
        DataTable DT = new DataTable();
        int PZ, posX, posY;
        Main MN = new Main();
        
        string name, passW;
        public LOG()
        {
            InitializeComponent();
            User();
        }

        void User()
        {
            DT.Clear();
            cmd = new SqlCommand("Select * From ADMIN", cn);
            Da = new SqlDataAdapter(cmd);
            Da.Fill(DT);
            passW = DT.Rows[0][0].ToString();
            name = DT.Rows[0][1].ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PSW PS = new PSW();
            PS.Show();
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            bunifuTextbox1._TextBox.Multiline = false;
            bunifuTextbox1._TextBox.ForeColor = Color.Yellow;
            bunifuTextbox2._TextBox.ForeColor = Color.Yellow;

            label5.Visible = false;
            label6.Visible = false;
        }

        private void bunifuTextbox2_OnTextChange(object sender, EventArgs e)
        {
            bunifuTextbox2._TextBox.Multiline = false;
            bunifuTextbox2._TextBox.ForeColor = Color.Yellow;
            if (bunifuSwitch1.Value == true) bunifuTextbox2._TextBox.PasswordChar = '*';
            else bunifuTextbox2._TextBox.PasswordChar = '\0';
            label6.Visible = false;
            AcceptButton = button1;
        }

        private void bunifuSwitch1_Click(object sender, EventArgs e)
        {
            if (bunifuSwitch1.Value == true) bunifuTextbox2._TextBox.PasswordChar = '*';
            else bunifuTextbox2._TextBox.PasswordChar = '\0';
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            PZ = 1;
            posX = e.X;
            posY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (PZ == 1)
            {
                this.SetDesktopLocation(MousePosition.X - posX, MousePosition.Y - posY);
            }
           
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            PZ = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string nameTest = Util.MD5Hash(bunifuTextbox1.text);
            string passTest = Util.MD5Hash(bunifuTextbox2.text);


            if (nameTest == name && passTest == passW)
            {
                Main mn = new Main();
                MN.Show();
                this.Hide();
            }
            else
            {
                if (nameTest != name && nameTest != "")
                {
                    bunifuTextbox1._TextBox.ForeColor = Color.Red;
                    label5.Visible = true;
                    bunifuTextbox2.text = "";
                }
                if (passTest != passW && nameTest == name)
                {
                    bunifuTextbox2._TextBox.ForeColor = Color.Red;
                    label6.Visible = true;
                }
            }
        }
    }
}
