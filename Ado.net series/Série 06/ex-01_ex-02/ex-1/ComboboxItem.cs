using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_1
{
    public class ComboboxItem
    {
        private string Text;
        private string Value;

        public string C_Text
        {
            get { return Text; }
            set { Text = value; }
        }

        public string C_Value
        {
            get { return Value; }
            set { Value = value; }
        }
        public ComboboxItem()
        {
        }
        public ComboboxItem(string text,string value)
        {
            this.C_Text = text;
            this.C_Value = value;
        }
        public override string ToString() { return "Ville : " + C_Text + "  Value : " + C_Value; }
    }
}
