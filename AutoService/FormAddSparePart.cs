using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoService
{
    public partial class FormAddSparePart : Form
    {
        Form1 mainForm;
        public FormAddSparePart()
        {
            InitializeComponent();
        }
        public FormAddSparePart(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }
    }
}
