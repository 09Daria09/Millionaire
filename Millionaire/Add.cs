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

namespace Millionaire
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] lastQuestion = null;
            string[] lines1 = File.ReadAllLines("Questions.csv");
            using (var reader = new StreamReader("Questions.csv"))
            {
                lastQuestion = lines1[0].Split(',');
            }


            string data = null;
            string[] lines = File.ReadAllLines("Questions.csv");
            using (var reader = new StreamReader("Questions.csv"))
            {

                string[] fields = new string[7];
                int num = int.Parse(lastQuestion[0]);
                int price = int.Parse(lastQuestion[1]) * 2;
                fields[0] = $"{++num}";
                fields[1] = $"{price}";
                fields[2] = textBox1.Text;
                fields[3] = $"\"{textBox2.Text}\"";
                fields[4] = $"\"{textBox3.Text}\"";
                fields[5] = $"\"{textBox4.Text}\"";
                fields[6] = $"\"{textBox5.Text}\"";
                if (radioButton1.Checked)
                {
                    fields[3] = $"^\"{textBox2.Text}\"";
                }
                if (radioButton2.Checked)
                {
                    fields[4] = $"^\"{textBox3.Text}\"";
                }
                if (radioButton3.Checked)
                {
                    fields[5] = $"^\"{textBox4.Text}\"";
                }
                if (radioButton4.Checked)
                {
                    fields[6] = $"^\"{textBox5.Text}\"";
                }
                data = $"{fields[0]},{fields[1]},{fields[2]},{fields[3]},{fields[4]},{fields[5]},{fields[6]}";
            }
            string oldData = null;
            using (StreamReader reader = new StreamReader("Questions.csv"))
            {
                oldData = reader.ReadToEnd();
            }
            using (StreamWriter writer = new StreamWriter("Questions.csv", false))
            {
                writer.WriteLine(data);
                writer.Write(oldData);
            }
            MessageBox.Show("Вопрос успешно добавлен");
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";
            textBox5.Text = " ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
