using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassField;

namespace YO
{
    public partial class ReadOnlyForm : Form
    {
        public ReadOnlyForm()
        {
            InitializeComponent();
        }

        public Competitor Competitor { get; set; }
        

        private void ReadOnlyForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Competitor.Name;
            textBox2.Text = Competitor.Score1.ToString();
            textBox3.Text = Competitor.Score2.ToString();
            textBox4.Text = Competitor.NameJury;
            textBox5.Text = Competitor.AllScore.ToString();
            var ms = new MemoryStream(Competitor.Photo);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult =  DialogResult.OK;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
