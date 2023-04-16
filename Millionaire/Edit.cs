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
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
            using (var reader = new StreamReader("Questions.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    domainUpDown1.Items.Add(values[0]);
                }
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            string selectedItem = domainUpDown1.SelectedItem.ToString();
            using (var reader = new StreamReader("Questions.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    if (values[0] == selectedItem)
                    {
                        string question = values[2].Replace("\"", "").Replace("^", "");
                        string[] answers = new string[] { values[3].Replace("\"", "").Replace("^", ""), values[4].Replace("\"", "").Replace("^", ""), values[5].Replace("\"", "").Replace("^", ""), values[6].Replace("\"", "").Replace("^", "") };

                        textBox1.Text = question;
                        if (answers.Length > 0) textBox2.Text = answers[0];
                        if (answers.Length > 1) textBox3.Text = answers[1];
                        if (answers.Length > 2) textBox4.Text = answers[2];
                        if (answers.Length > 3) textBox5.Text = answers[3];
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedItem = domainUpDown1.SelectedItem.ToString();
            string[] lines = File.ReadAllLines("Questions.csv");
            using (var reader = new StreamReader("Questions.csv"))
            {
                int rowToUpdate = lines.Length - int.Parse(selectedItem); 

                string[] fields = lines[rowToUpdate].Split(',');

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
                lines[rowToUpdate] = string.Join(",", fields);

            }
            using (var writer = new StreamWriter("Questions.csv"))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }

            MessageBox.Show("Вопрос успешно изменен");
        }
    }
}
