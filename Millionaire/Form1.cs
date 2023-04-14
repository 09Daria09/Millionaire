using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace Millionaire
{
    public partial class Form1 : Form
    {
        private List<Question> questions = new List<Question>();
        private List<Price> prices = new List<Price>();

        public Form1()
        {
            InitializeComponent();

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            using (TextFieldParser parser = new TextFieldParser("Questions.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    int questionNumber;
                    if (!int.TryParse(fields[0], out questionNumber))
                    {
                        MessageBox.Show("Error: Invalid question number format");
                        continue;
                    }

                    int questionPrice;
                    if (!int.TryParse(fields[1].Trim(), out questionPrice))
                    {
                        MessageBox.Show("Error: Invalid question price format");
                        continue;
                    }

                    string questionText = fields[2];
                    string[] answers = fields.Skip(3).ToArray();
                    Question question = new Question
                    {
                        Number = questionNumber,
                        Text = questionText,
                        Answers = answers
                    };

                    Price price = new Price
                    {
                        Value = questionPrice
                    };

                    questions.Add(question);
                    prices.Add(price);

                    listBox1.Items.Add($"{questionNumber} - {questionPrice} грн");
                }
            }
        }

        public class Question
        {
            public int Number { get; set; }
            public string Text { get; set; }
            public string[] Answers { get; set; }
        }

        public class Price
        {
            public int Value { get; set; }
        }
    }
}
