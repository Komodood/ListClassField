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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = listBox1.SelectedItem is Competitor;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (e.KeyChar <= 47 || e.KeyChar >= 58 || e.KeyChar == 44)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.DisplayMember = "Name";
            var Competitor = new Competitor();
            Competitor.Score1 = double.Parse(textBox2.Text);
            Competitor.Name = textBox1.Text;
            Competitor.Score2 = double.Parse(textBox3.Text);
            Competitor.Name = textBox1.Text;
            Competitor.AllScore = double.Parse(textBox2.Text) + double.Parse(textBox3.Text);
            listBox1.Items.Add(Competitor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var FormWithData = new Form2();
            FormWithData.ShowDialog();
            if (FormWithData.DialogResult == DialogResult.OK)
            {
                label2.Text = FormWithData.Data1;
                label3.Text = FormWithData.Data2;
                label4.Text = FormWithData.Data3;
                label8.Text = FormWithData.Data4;
                label9.Text = FormWithData.Data5;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Фотография|*.jpg" };
            var dr = ofd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            label1.Text = listBox1.DisplayMember.;


        }
    }
}
