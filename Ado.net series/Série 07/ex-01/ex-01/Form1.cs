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

using CrystalDecisions;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Mail;

namespace ex_01
{
    public partial class Form1 : Form
    {
        public  SqlConnection cn = new SqlConnection(@"Server=DESKTOP-4TIUIM2\SQLEXPRESS;DataBase=GestionNotes;Integrated Security=true");
        public SqlCommand cmd;
        public SqlDataAdapter Da,Da2;
        public DataTable DT = new DataTable();
        public DataTable DT2 = new DataTable();
        public DataSet Ds = new DataSet();
        public DataView dv1, dv2;
        public Form1()
        {
            InitializeComponent();

            Datasource(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrystalReport1 cr1 = new CrystalReport1();
            crystalReportViewer1.ReportSource = cr1;
            crystalReportViewer1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CrystalReport2 cr2 = new CrystalReport2();
            crystalReportViewer1.ReportSource = cr2;
            crystalReportViewer1.Refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                int id = int.TryParse(comboBox1.SelectedValue.ToString(), out id) ? id : -1;
                CrystalReport3 cr3 = new CrystalReport3();
                cr3.SetParameterValue("Num", id);
                crystalReportViewer1.ReportSource = cr3;
                crystalReportViewer1.Refresh();
            }
        }
        //----------------------------------------------------------------------------
        private void button7_Click(object sender, EventArgs e)
        {
            Ajouter Aj = new Ajouter();
            Aj.ShowDialog();
            this.Datasource();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dv1.RowFilter = "nom like '%" + textBox1.Text + "%'";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder CMB = new SqlCommandBuilder(Da);
            Da.Update(Ds.Tables["etudiant"]);
            Datasource();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument RD = new ReportDocument();
                RD.Load(Environment.CurrentDirectory + @"\CrystalReport1.rpt");
                RD.Database.Tables["etudiant"].SetDataSource(Ds.Tables["etudiant"]);
                ExportOptions EXPO = new ExportOptions();
                DiskFileDestinationOptions DISCK = new DiskFileDestinationOptions();

                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Pdf Files|*.pdf";//Pdf

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DISCK.DiskFileName = sfd.FileName;
                }

                EXPO = RD.ExportOptions;
                {
                    EXPO.ExportDestinationType = ExportDestinationType.DiskFile;
                    EXPO.ExportFormatType = ExportFormatType.PortableDocFormat;//Pdf
                    EXPO.ExportDestinationOptions = DISCK;
                    EXPO.ExportFormatOptions = new PdfRtfWordFormatOptions();//Pdf
                }
                RD.Export();
            }
            catch (Exception ex)
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument RD = new ReportDocument();
                RD.Load(Environment.CurrentDirectory + @"\CrystalReport1.rpt");
                RD.Database.Tables["etudiant"].SetDataSource(Ds.Tables["etudiant"]);
                ExportOptions EXPO = new ExportOptions();
                DiskFileDestinationOptions DISCK = new DiskFileDestinationOptions();

                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Excel|*.xls";//excel

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DISCK.DiskFileName = sfd.FileName;
                }

                EXPO = RD.ExportOptions;
                {
                    EXPO.ExportDestinationType = ExportDestinationType.DiskFile;
                    EXPO.ExportFormatType = ExportFormatType.Excel;//excel
                    EXPO.ExportDestinationOptions = DISCK;
                    EXPO.ExportFormatOptions = new ExcelFormatOptions();//excel
                }
                RD.Export();
            }
            catch (Exception ex)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //SmtpMail.SmtpServer.Insert(0, "LE NOM DU SERVEUR SMTP");
            //    //System.Net.Mail.MailMessage Msg = new System.Net.Mail.MailMessage();
            //    //Msg.To = "ADRESSE EMAIL DESTINATAIRE";
            //    //Msg.From = "ADRESSE EMAIL D’ENVOI";
            //    //Msg.Subject = "Crystal Report Attachment ";
            //    //Msg.Body = "Crystal Report Attachment ";
            //    //Msg.Attachments.Add(new MailAttachment("CHEMIN DU FICHIER A EXPORTER"));
            //    //System.Web.Mail.SmtpMail.Send(Msg);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //try
            //{
            //    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage("w1234monky@gmail.com", "w1234monky@gmail.com", "w1234monky@gmail.com", "w1234monky@gmail.com");
            //    msg.IsBodyHtml = true;
            //    SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
            //    //sc.UseDefaultCredentials = false;
            //    NetworkCredential cre = new NetworkCredential("w1234monky@gmail.com", "28@jennani");//your mail password
            //    sc.Credentials = cre;
            //    sc.EnableSsl = true;
            //    sc.Send(msg);
            //    MessageBox.Show("Mail Send");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
                int id = int.TryParse(comboBox2.SelectedValue.ToString(), out id) ? id : -1;
                CrystalReport4 cr4 = new CrystalReport4();
                cr4.SetParameterValue("Num", id);
                crystalReportViewer1.ReportSource = cr4;
                crystalReportViewer1.Refresh();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CrystalReport5 cr5 = new CrystalReport5();
            crystalReportViewer2.ReportSource = cr5;
            crystalReportViewer2.Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CrystalReport6 cr6 = new CrystalReport6();
            crystalReportViewer2.ReportSource = cr6;
            crystalReportViewer2.Refresh();
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            DialogResult Res = MessageBox.Show("voulez-vous vraiment supprimer ?", "Confermation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Res == DialogResult.Yes)
            {
                SqlCommandBuilder CMB = new SqlCommandBuilder(Da);
                Da.Update(Ds.Tables["etudiant"]);
                Datasource();
            }
        }

        public void Datasource()
        {
            cn.Open();
            DT.Clear();
            Da = new SqlDataAdapter("select * from filiere", cn);
            Da.Fill(DT);
            comboBox1.DataSource = DT;
            comboBox1.DisplayMember = "code";
            comboBox1.ValueMember = "numF";
            //--------------------------------------------------
            DT2.Clear();
            Da2 = new SqlDataAdapter("select numE,nom+' '+prenom as NP from etudiant", cn);
            Da2.Fill(DT2);
            comboBox2.DataSource = DT2;
            comboBox2.DisplayMember = "NP";
            comboBox2.ValueMember = "numE";
            //--------------------------------------------------
            Ds.Clear();
            Da = new SqlDataAdapter("select * from etudiant", cn);
            Da.Fill(Ds, "etudiant");

            dv1 = new DataView(Ds.Tables["ETUDIANT"], "", "nom", DataViewRowState.CurrentRows);
            dv1.Sort = "nom ASC";
            dv1.AllowEdit = true;
            dv1.AllowNew = true;
            dv1.AllowDelete = true;

            dataGridView1.DataSource = dv1;
            cn.Close();
            this.comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged += new EventHandler(comboBox2_SelectedIndexChanged);
        }
    }
}
