﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
namespace Millionaire
{
    public partial class Form1 : Form
    {
        private List<Question> questions = new List<Question>();
        private List<Price> prices = new List<Price>();
        int NumberQuestion = 1;
        int NumerSelected;

        SoundPlayer playerBegin;
        SoundPlayer playerTrue;
        SoundPlayer playerFalse;
        SoundPlayer playerCall;
        SoundPlayer playerWin;

        bool SaundMult = true;
        int countBegin = 0;
        public Form1()
        {
            InitializeComponent();
            LoadQuestions();
            ReadFile();
            NumerSelected = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = NumerSelected; 
            playerFalse = new SoundPlayer("false.wav");
            playerTrue = new SoundPlayer("true.wav");
            playerBegin = new SoundPlayer("begin.wav");
            playerCall = new SoundPlayer("zvonok.wav");
            playerWin = new SoundPlayer("win.wav");
            ToggleSound();


        }
        private void ToggleSound()
        {
            if (countBegin == 0)
            {
                if (SaundMult)
                {
                    playerBegin.Play();
                }
                else
                {
                    playerBegin.Stop();
                }
                countBegin++;
            }
        }
        private void ReadFile()
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
                        if (answers.Length > 0) button4.Text = answers[0];
                        if (answers.Length > 1) button5.Text = answers[1];
                        if (answers.Length > 2) button9.Text = answers[2];
                        if (answers.Length > 3) button10.Text = answers[3];
                    }
                }
            }
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
                    if (questionNumber % 5 == 0)
                    {
                        listBox1.Items.Add($"{questionNumber} -> {questionPrice} ₴  👑");
                    }
                    else
                    {
                        listBox1.Items.Add($"{questionNumber} -> {questionPrice} ₴");
                    }
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

        private async void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            button5.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;

            if (SaundMult)
                playerBegin.Stop();
            Button clickedButton = (Button)sender;
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

            if (answerRight != clickedButton.Text)
            {
                if (SaundMult)
                    playerFalse.Play();

                await BlinkButton(clickedButton, Color.Red);
                MessageBox.Show("Вы проиграли, но не расстраивайтесь. (╯︵╰,)\n" +
                    "Продолжайте играть и тренироваться, и вы обязательно достигнете успеха!");
                Application.Exit();
            }
            else
            {
                if (SaundMult)
                    playerTrue.Play();
            }

            await BlinkButton(clickedButton, Color.Green);

            NumberQuestion++;
            if (NumerSelected == 0)
            {
                if (SaundMult)
                    playerWin.Play();
                MessageBox.Show("Вы одержали победу!!! Ураааа");
            }
            NumerSelected--;
            listBox1.SelectedIndex = NumerSelected;
            ReadFile();
        }

        private async Task
        BlinkButton(Button button, Color blinkColor)
        {
            int count = 0;
            Color originalColor = button.BackColor;

            while (count < 3)
            {
                button.BackColor = blinkColor;
                await Task.Delay(500);
                button.BackColor = originalColor;
                await Task.Delay(500);
                count++;
            }
        }
        private void играToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добро пожаловать в бесплатную онлайн-версию знаменитого игрового шоу “Кто хочет стать миллионером?”. " +
                "В этой уникальной игре можно ощутить себя настоящим участником телепрограммы, завоевавшей миллионы поклонников по всему миру. " +
                "Бесплатная версия игры позволит в режиме реального времени стать главным героем нашумевшей передачи.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
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
            int twoButt = 0;
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            while (twoButt < 2)
            {
                int numToDisable = random.Next(1, 4);
                switch (numToDisable)
                {
                    case 1:
                        if (answerRight != button4.Text)
                        {
                            button4.Enabled = false;
                            if (flag1)
                            {
                                twoButt++;
                                flag1 = false;
                            }
                        }
                        break;
                    case 2:
                        if (answerRight != button5.Text)
                        {
                            button5.Enabled = false;
                            if (flag2)
                            {
                                twoButt++;
                                flag2 = false;
                            }
                        }
                        break;
                    case 3:
                        if (answerRight != button9.Text)
                        {
                            button9.Enabled = false;
                            if (flag3)
                            {
                                twoButt++;
                                flag3 = false;
                            }
                        }
                        break;
                    case 4:
                        if (answerRight != button10.Text)
                        {
                            button10.Enabled = false;
                            if (flag4)
                            {
                                twoButt++;
                                flag4 = false;
                            }
                        }
                        break;
                }


            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SaundMult)
                playerCall.Play();
            button2.Enabled = false;
            string answerRight = null;
            string[] answers = null;
            using (var reader = new StreamReader("Questions.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    if (values[0] == NumberQuestion.ToString())
                    {
                        answers = new string[] { values[3], values[4], values[5], values[6] };

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
            int randomNumber = random.Next(1, 101);
            if (randomNumber <= 80)
            {
                MessageBox.Show($"Оооо привет, да я слышал про это, думаю ответ - {answerRight}");
            }
            else
            {

                MessageBox.Show($"Оооо привет, ой я даже не знаю, возможно ответ - {answers[0]}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            ProgressBarAnswer answer = new ProgressBarAnswer(NumberQuestion);
            answer.ShowDialog();
        }

        private void администраторскийРежимToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string expectedPassword = "admin";
            string password = Microsoft.VisualBasic.Interaction.InputBox("Введите пароль:", "Пароль", "", -1, -1);
            if (password != expectedPassword)
            {
                MessageBox.Show("Пароль неверный!");
                return;
            }
            AdminMenu menu = new AdminMenu();
            menu.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SaundMult = !SaundMult;
            if (SaundMult)
            {
                button11.Image = Image.FromFile("on.bmp");
            }
            else
            {
                button11.Image = Image.FromFile("off.bmp");
            }
            ToggleSound();
        }

    }
}
