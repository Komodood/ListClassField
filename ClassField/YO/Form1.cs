using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ClassField;

namespace YO
{
    public partial class Form1 : Form
    {

        public static DateTime Date { get; set; }
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
            Competitor.NameJury = textBox4.Text;
            Competitor.AllScore = double.Parse(textBox2.Text) + double.Parse(textBox3.Text);
            var stream = new MemoryStream();
            pictureBox1.Image.Save(stream, ImageFormat.Jpeg);
            Competitor.Photo = stream.ToArray();
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

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var item = (Competitor)listBox1.Items[index];
                var ff = new ReadOnlyForm() { Competitor = item };
                if (ff.ShowDialog(this) == DialogResult.OK)
                {
                    listBox1.Items.Remove(item);
                    listBox1.Items.Insert(index, item);
                }
            }
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





        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Competitor)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem is Blockjury)
            {
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.DisplayMember = "NameJury";
            var Jury = new Form3();
            Jury.ShowDialog();
            if (Jury.DialogResult == DialogResult.OK)
            {
                listBox2.Items.Add(Form3.Data4);
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox2.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var item = (Blockjury)listBox2.Items[index];
                var ff = new ReadOnlyFORJURY() { Blockjury = item };
                if (ff.ShowDialog(this) == DialogResult.OK)
                {
                    listBox2.Items.Remove(item);
                    listBox2.Items.Insert(index, item);
                }
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Таблица|*.ResultTable" };
            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;
            var ReTab = new TableOfResult()
            {
                Competitions = label2.Text,
                CategoryCompetition = label3.Text,
                AgeСategory = label4.Text,
                CityofComp = label8.Text,
                DateCompetition = Date,
                NameJury = listBox2.Items.OfType<Blockjury>().ToList(),
                ListCompetitors = listBox1.Items.OfType<Competitor>().ToList(),
            };
            var xs = new XmlSerializer(typeof(TableOfResult));
            var file = File.Create(sfd.FileName);
            xs.Serialize(file, ReTab);
            file.Close();
        }

        private void загрузитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Таблица|*.ResultTable" };
            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;
            var xs = new XmlSerializer(typeof(TableOfResult));
            var file = File.OpenRead(ofd.FileName);
            var ReTab = (TableOfResult)xs.Deserialize(file);
            file.Close();
            label2.Text = ReTab.Competitions;
            label3.Text = ReTab.CategoryCompetition;
            label4.Text = ReTab.AgeСategory;
            label8.Text = ReTab.CityofComp;
            label9.Text = ReTab.DateCompetition.ToLongDateString();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            foreach (var Competitor in ReTab.ListCompetitors)
            {
                listBox1.Items.Add(Competitor);
            }
            foreach (var Jury in ReTab.NameJury)
            {
                listBox2.Items.Add(Jury);
            }
        }
    }
}




