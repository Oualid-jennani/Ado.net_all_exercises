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
using System.Security.Cryptography;

namespace ex_01
{
    public partial class PSW : Form
    {

        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlCommand cmd;
        SqlDataAdapter Da;
        DataTable DT = new DataTable();
        string passW;
        int PZ, posX, posY;
        public PSW()
        {
            InitializeComponent();
            textBox1.PasswordChar = '*';
            User();
        }
        void User()
        {
            DT.Clear();
            cmd = new SqlCommand("Select * From ADMIN", cn);
            Da = new SqlDataAdapter(cmd);
            Da.Fill(DT);
            passW = DT.Rows[0][0].ToString();
        }

        private void PassW_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Left--;
            if (label4.Left == -200) label4.Left = 300;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text == " ") textBox1.Text = "";
            if (textBox1.Text != "") label1.Visible = false;
            else
            {
                label1.Visible = true;
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            if (textBox3.Text == " ") textBox3.Text = "";
            if (textBox3.Text != "") button1.Visible = true;
            else button1.Visible = false;
            if (textBox2.Text == "") textBox3.Text = "";
            if (textBox3.Text != "") label3.Visible = false;
            else label3.Visible = true;
            label10.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool changePS = false;
            bool changeNeme = false;
            try
            {
                if (Util.MD5Hash(textBox1.Text) == passW)
                {
                    label9.Visible = false;
                    if (textBox2.Text != "")
                    {
                        if (textBox2.Text == textBox3.Text)
                        {
                            label10.Visible = false;
                            changePS = true;
                        }
                        else
                        {
                            label10.Visible = true;
                        }
                    }
                    else
                    {
                        label10.Visible = false;
                    }
                    if (bunifuCheckbox1.Visible == true && label10.Visible == false)
                    {
                        if (textBox4.Text != "")
                        {
                            changeNeme = true;
                        }
                    }
                }
                else
                {
                    label9.Visible = true;
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }


                if (changePS == true && changeNeme == false)
                {
                    DialogResult REZ = MessageBox.Show("Do you really want to change your password", "Change password", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (REZ == DialogResult.OK)
                    {
                        ChangeP();
                        MessageBox.Show("Change Susseccfully");
                    }
                    cleare();

                }
                else if (changePS == false && changeNeme == true)
                {
                    DialogResult REZ = MessageBox.Show("You really want to change your username", "Change User Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (REZ == DialogResult.OK)
                    {
                        ChangeN();
                        MessageBox.Show("Change Susseccfully");
                    }
                    cleare();
                }
                else if (changePS == true && changeNeme == true)
                {
                    DialogResult REZ = MessageBox.Show("You really want to change your username and password", "Change password and User Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (REZ == DialogResult.OK)
                    {
                        ChangeP();
                        ChangeN();
                        MessageBox.Show("Change Susseccfully");
                    }
                    cleare();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                cn.Close();
            }
            User();
        }

        private void PassW_MouseDown(object sender, MouseEventArgs e)
        {
            PZ = 1;
            posX = e.X;
            posY = e.Y;
        }

        private void PassW_MouseUp(object sender, MouseEventArgs e)
        {
            PZ = 0;
        }

        private void PassW_MouseMove(object sender, MouseEventArgs e)
        {
            if (PZ == 1)
            {
                this.SetDesktopLocation(MousePosition.X - posX, MousePosition.Y - posY);
            }
        }

        private void bunifuSwitch1_Click(object sender, EventArgs e)
        {
            if (bunifuSwitch1.Value == false)
            {
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
            }
            else
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked == true)
            {
                label7.Visible = true;
                textBox4.Visible = true;
                textBox4.Text = "";
            }
            else
            {
                label7.Visible = false;
                textBox4.Visible = false;
            }
            if (bunifuCheckbox1.Checked == false && textBox3.Text == "")
            {
                button1.Visible = false;
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox2.Text == " ") textBox2.Text = "";
            if (textBox1.Text == "") textBox2.Text = "";
            if (textBox2.Text != "") label2.Visible = false;
            else
            {
                label2.Visible = true;
                textBox3.Text = "";
                label10.Visible = false;
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "") textBox4.Text = "";
            if (textBox4.Text != "")
            {
                label7.Visible = false;
            }
            else label7.Visible = true;
            if (textBox4.Text == " ") textBox4.Text = "";
            if (textBox4.Text != "" || textBox4.Text == "" && textBox3.Text != "")
            {
                button1.Visible = true;
            }
            else button1.Visible = false;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            textBox3.Focus();
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            textBox4.Focus();
        }

        private void PSW_MouseDown(object sender, MouseEventArgs e)
        {
            PZ = 1;
            posX = e.X;
            posY = e.Y;
        }

        private void PSW_MouseMove(object sender, MouseEventArgs e)
        {
            if (PZ == 1)
            {
                this.SetDesktopLocation(MousePosition.X - posX, MousePosition.Y - posY);
            }
            
        }

        private void PSW_MouseUp(object sender, MouseEventArgs e)
        {
            PZ = 0;
        }

        void ChangeN()
        {
            SqlParameter p = new SqlParameter("@name", Util.MD5Hash(textBox4.Text));
            cmd = new SqlCommand("Update ADMIN Set Name = @name", cn);
            cmd.Parameters.Add(p);
                  
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        void ChangeP()
        {
            SqlParameter p = new SqlParameter("@passw", Util.MD5Hash(textBox3.Text));
            cmd = new SqlCommand("Update ADMIN Set PassW = @passw", cn);
            cmd.Parameters.Add(p);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        public void cleare()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
    }
}
