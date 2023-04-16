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
    public partial class Remove : Form
    {
        public Remove()
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
                        
                        textBox5.Text = question;
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
            // Получаем выбранный элемент из DomainUpDown
            string selectedItem = domainUpDown1.SelectedItem.ToString();

            // Читаем все строки из файла CSV в массив строк
            string[] lines = File.ReadAllLines("Questions.csv");

            // Получаем номер строки, которую нужно удалить
            int rowToRemove = lines.Length - int.Parse(selectedItem);

            // Удаляем строку из массива
            List<string> linesList = lines.ToList();
            linesList.RemoveAt(rowToRemove);
            lines = linesList.ToArray();

            // Перенумеруем оставшиеся строки в файле CSV
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');
                fields[0] = (lines.Length - i).ToString();
                lines[i] = string.Join(",", fields);
            }

            // Перезаписываем файл CSV с обновленными данными
            File.WriteAllLines("Questions.csv", lines);
            MessageBox.Show("Вопрос успешно удален");
        }
    }
}
