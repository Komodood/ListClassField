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
    public partial class Form3 : Form
    {
        public static Blockjury Data4 { get; set; }
        public List<string> Data5 { get; set; }

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] Names;
            char[] charSeparators = new char[] { ',' };
            var Jury = new Blockjury();
            Jury.ListJury = new List<string>();
            Jury.NameJury = textBox6.Text;
            Names = textBox5.Text.Split(charSeparators);
            for (int i = 0; i < Names.Length; i++)
            {
                Jury.ListJury.Add(Names[i]); /*Data5.Add(Names[i])*/;
            }
            DialogResult = DialogResult.OK;
            Data4 = Jury;
        }

        

        private void pictureBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Фотография|*.jpg" };
            var dr = ofd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                pictureBox3.Image = Image.FromFile(ofd.FileName);
            }
        }

        
    }
}
