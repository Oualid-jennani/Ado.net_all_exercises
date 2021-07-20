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
    public partial class EX1_EX2 : Form
    {

        public EX1_EX2()
        {
            InitializeComponent();
            ComboboxItem item1 = new ComboboxItem();
            item1.C_Text = "Oujda";
            item1.C_Value = "123";
            ComboboxItem item2 = new ComboboxItem();
            item2.C_Text = "Casablanca";
            item2.C_Value = "126";
            ComboboxItem item3 = new ComboboxItem();
            item3.C_Text = "Rabat";
            item3.C_Value = "185";
            ComboboxItem item4 = new ComboboxItem();
            item4.C_Text = "Tanger";
            item4.C_Value = "147";
            ComboboxItem item5 = new ComboboxItem();
            item5.C_Text = "Fes";
            item5.C_Value = "196";
            ComboboxItem item6 = new ComboboxItem();
            item6.C_Text = "Marrakech";
            item6.C_Value = "145";
            ComboboxItem item7 = new ComboboxItem();
            item7.C_Text = "Agadir";
            item7.C_Value = "111";
            ComboboxItem item8 = new ComboboxItem();
            item8.C_Text = "Dakhla";
            item8.C_Value = "138";

            List<ComboboxItem> items = new List<ComboboxItem> { item1, item2, item3, item4, item5, item6, item7, item8, new ComboboxItem("istamboul","###") };

            this.comboBox1.DataSource = items;
            this.comboBox1.DisplayMember = "C_Text";
            this.comboBox1.ValueMember = "C_Value";

            this.comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
        }
        //EX1
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Indx = comboBox1.SelectedIndex;
            ComboboxItem selectedCMB = (ComboboxItem)comboBox1.SelectedItem;
            MessageBox.Show(String.Format("Index: [{0}] {1}", Indx, selectedCMB.ToString()));
        }

        private void clientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.clientBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.aDO_serie6DataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'aDO_serie6DataSet.Client'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.clientTableAdapter.Fill(this.aDO_serie6DataSet.Client);

        }

    }
}
