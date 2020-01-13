using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoServiceLibrary
{
    public static class EventsInForm
    {
        public static List<char> strTextBox = new List<char>();
        public static void KeyPressOnlyNumb(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        /*public static void KeyPressFloat(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.KeyChar == ',')
                e.KeyChar = '.';
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '.' || e.KeyChar == ',')
                && (textBox.Text.IndexOf(".") == -1) && (textBox.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            if (textBox.Text.Split('.').ToArray().Length > 1
                && textBox.Text.Split('.').ToArray().Last().Length > 1 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }*/
        public static void KeyPressFloat(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.KeyChar == '.')
                e.KeyChar = ',';
            if (!(Char.IsDigit(e.KeyChar)) && !((e.KeyChar == '.' || e.KeyChar == ',')
                && (textBox.Text.IndexOf(",") == -1) && (textBox.Text.Length != 0)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            if (textBox.Text.Split(',').ToArray().Length > 1
                && textBox.Text.Split(',').ToArray().Last().Length > 1 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        public static void KeyPressUpper(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsUpper(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
