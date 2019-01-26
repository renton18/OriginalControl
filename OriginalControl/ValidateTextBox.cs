using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OriginalControl
{
    public partial class ValidateTextBox: UserControl
    {
        public StringType stringType { get; set; } = StringType.HALF_NUMBER;

        ErrorProvider errorProvider = new ErrorProvider();
        public enum StringType
        {
            HALF_NUMBER,
            HALF_NUMBER_DOT,
            HALF_ALPHA_NUMBER,
            HALF_ALPHA_NUMBER_SYMBOL
        }
        public ValidateTextBox()
        {
            InitializeComponent();
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            string regexString = "";
            string errorString = "";

            switch (stringType)
            {
                case StringType.HALF_NUMBER:
                    regexString = @"^[0-9]*$";
                    errorString = "半角数字";
                    break;
                case StringType.HALF_NUMBER_DOT:
                    regexString = @"^\d*\.*\d*$";
                    errorString = "半角数字(小数点以下含む)";
                    break;
                case StringType.HALF_ALPHA_NUMBER:
                    regexString = @"^[0-9a-zA-Z]*$";
                    errorString = "半角英数字";
                    break;
                case StringType.HALF_ALPHA_NUMBER_SYMBOL:
                    regexString = @"^[!-~]*$";
                    errorString = "半角英数字記号";
                    break;
            }

            Regex regex = new Regex(regexString);
            if (!regex.IsMatch(((Control)sender).Text))
            {
                errorProvider.SetError((Control)sender, errorString);
                e.Cancel = true;
            }
        }

        private void textBox_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((TextBox)sender, null);
        }
    }
}
