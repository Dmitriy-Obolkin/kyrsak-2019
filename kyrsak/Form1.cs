using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;//криптографические службы, включающие безопасное кодирование и декодирование данных

namespace kyrsak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //"Сгенерировать ключ" - вызов функции генерации ключа
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Encoding.UTF8.GetString(cryptinion.AesManaged.GenerateKey("aes.config", "AES"));
            string lines = textBox1.Text;
            System.IO.File.WriteAllText(@"Key.txt", lines);
        }

        //"Зашифровать" - вызов функции шифрования текста
        private void button2_Click(object sender, EventArgs e)
        {
            byte[] f = cryptinion.AesManaged.EncryptStringUsingFileKey2("aes.config", "AES", richTextBox1.Text);
            richTextBox2.Text = Convert.ToBase64String(f);
            string lines = textBox1.Text;
            System.IO.File.WriteAllText(@"Key.txt", lines);
        }

        //Загрузка ключа из файла
        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text = System.IO.File.ReadAllText(@"Key.txt");
        }

        //Запись шифротекста в файл Output.txt
        private void button3_Click(object sender, EventArgs e)
        {
            string lines = richTextBox2.Text;
            System.IO.File.WriteAllText(@"Output.txt", lines);
        }

        //Открытие файла исходного текста
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
                richTextBox1.Text = System.IO.File.ReadAllText(openFile.FileName);
        }
    }
}
