﻿using System;
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
    public partial class ReadOnlyFORJURY : Form
    {
        public Blockjury Blockjury { get; set; }
        public ReadOnlyFORJURY()
        {
            InitializeComponent();
        }

        private void ReadOnlyFORJURY_Load(object sender, EventArgs e)
        {
            textBox1.Text = Blockjury.NameJury;
            textBox2.Text = Blockjury.ListJury.ToString();
            var ms = new MemoryStream(Blockjury.Photo);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}