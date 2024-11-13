using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CritoZLP
{
    public partial class Form1 : Form
    {
        public static string inputFile = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt) |*.txt|All files (*.*) | *.*";
            openFileDialog1.RestoreDirectory = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            inputFile = openFileDialog1.FileName;
            Encryption encryption = new Encryption();

            if (comboBox1.Text == "Расшифровать файл")
            {
                encryption.Decrypt(inputFile, inputFile);
            }
            else if (comboBox1.Text == "Зашифровать файл")
            {
                encryption.Encrypt(inputFile, inputFile);
            }
            else
            {
                MessageBox.Show("idiota");
            }
            MessageBox.Show("Complite");
        }
    }
}
