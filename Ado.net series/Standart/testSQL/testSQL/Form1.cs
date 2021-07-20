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

namespace testSQL
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=serie_4;Integrated Security=true");
        SqlDataAdapter Da;
        DataTable DT = new DataTable();
        CurrencyManager CM;
        SqlCommandBuilder CMB;
        public Form1()
        {
            InitializeComponent();

            Da = new SqlDataAdapter("select * from ETUDIANT", cn);
            Da.Fill(DT);

            textNUM.DataBindings.Add("text", DT, "NumEtudiant");
            textNOM.DataBindings.Add("text", DT, "Nom");
            textPREN.DataBindings.Add("text", DT, "Prénom");
            textMOY.DataBindings.Add("text", DT, "moyenne");

            CMB = new SqlCommandBuilder(Da);
            CM = (CurrencyManager) this.BindingContext[DT];

            labelPosition.Text = (CM.Position + 1) + "/" + (DT.Rows.Count);

            comboBox1.DataSource = DT;
            comboBox1.DisplayMember = "Nom";
            comboBox1.ValueMember = "NumEtudiant";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            CM.Position --;
            labelPosition.Text = (CM.Position + 1) + "/" + (DT.Rows.Count);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CM.Position = 0;
            labelPosition.Text = (CM.Position + 1) + "/" + (DT.Rows.Count);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CM.Position ++;
            labelPosition.Text = (CM.Position + 1) + "/" + (DT.Rows.Count);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CM.Position = DT.Rows.Count-1;
            labelPosition.Text = (CM.Position + 1) + "/" + (DT.Rows.Count);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CM.AddNew();
            textNUM.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CM.EndCurrentEdit();
            Da.Update(DT);

            //----------------------------------------
            MessageBox.Show("Added Saccessfuly", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
            labelPosition.Text = (CM.Position + 1) + "/" + (DT.Rows.Count);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CM.RemoveAt(CM.Position);
            CM.EndCurrentEdit();
            Da.Update(DT);

            //----------------------------------------
            MessageBox.Show("Deleted Saccessfuly", "delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            labelPosition.Text = (CM.Position + 1) + "/" + (DT.Rows.Count);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CM.EndCurrentEdit();
            Da.Update(DT);
            CM.Refresh();

            //----------------------------------------
            MessageBox.Show("Edited Saccessfuly", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            labelPosition.Text = (CM.Position + 1) + "/" + (DT.Rows.Count);
        }
    }
}
