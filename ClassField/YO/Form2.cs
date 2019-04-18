using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassField;

namespace YO
{
    public partial class Form2 : Form
    {
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }
        public string Data5 { get; set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var NameSpace = new TableOfResult();
            NameSpace.Competitions = Data1 = textBox1.Text;
            NameSpace.CategoryCompetition = Data2 = textBox3.Text;
            NameSpace.AgeСategory = Data3 = textBox2.Text;
            Data4 = textBox4.Text;
            Data5 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            DialogResult = DialogResult.OK;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
