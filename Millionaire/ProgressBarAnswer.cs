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
using System.Windows.Shapes;

namespace Millionaire
{
    public partial class ProgressBarAnswer : Form
    {        
        public ProgressBarAnswer(int NumberQuestion)
        {

            InitializeComponent();
            ReadFile(NumberQuestion);
            Answer(NumberQuestion);

            
        }
        private void ReadFile(int NumberQuestion)
        {
            using (var reader = new StreamReader("Questions.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    if (values[0] == NumberQuestion.ToString())
                    {
                        string question = values[2].Replace("\"", "").Replace("^", "");
                        string[] answers = new string[] { values[3].Replace("\"", "").Replace("^", ""), values[4].Replace("\"", "").Replace("^", ""), values[5].Replace("\"", "").Replace("^", ""), values[6].Replace("\"", "").Replace("^", "") };

                        textBox1.Text = question;
                        if (answers.Length > 0) textBox1.Text = answers[0];
                        if (answers.Length > 1) textBox2.Text = answers[1];
                        if (answers.Length > 2) textBox3.Text = answers[2];
                        if (answers.Length > 3) textBox4.Text = answers[3];
                    }
                }
            }
        }
        private void Answer(int NumberQuestion)
        {
            string answerRight = null;
            using (var reader = new StreamReader("Questions.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    if (values[0] == NumberQuestion.ToString())
                    {
                        string[] answers = new string[] { values[3], values[4], values[5], values[6] };

                        for (int i = 0; i < 4; i++)
                        {
                            char[] charArray = answers[i].ToCharArray();
                            if (charArray[0] == '^')
                            {
                                answerRight = answers[i].Replace("\"", "").Replace("^", "");
                            }
                        }
                    }
                }
            }
            Random random = new Random();
            int randomValue1 = random.Next(0, 40);
            int randomValue2 = random.Next(0, 40 - randomValue1);
            int randomValue3 = random.Next(0, 40 - randomValue1 - randomValue2);
            int randomValue4 = 40 - randomValue1 - randomValue2 - randomValue3;
            if (answerRight != textBox1.Text)
            {
                progressBar1.Value = randomValue1;
            }
            else
            {
                progressBar1.Value = 60;
            }

            if (answerRight != textBox2.Text)
            {
                progressBar2.Value = randomValue2;
            }
            else
            {
                progressBar2.Value = 60;
            }

            if (answerRight != textBox3.Text)
            {
                progressBar3.Value = randomValue3;
            }
            else
            {
                progressBar3.Value = 60;
            }

            if (answerRight != textBox4.Text)
            {
                progressBar4.Value = randomValue4;
            }
            else
            {
                progressBar4.Value = 60;
            }
        }
    }
}
