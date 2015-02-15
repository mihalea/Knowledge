using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Teste
{
    public partial class TestingChamber : Form
    {
        int currentDomain;
        List<Question> lq = new List<Question>();
        List<int> showed = new List<int>();
        List<PictureBox> h = new List<PictureBox>(); //heart sprites
        int q; //Current Question
        int score;
        int totalQue;
        int hearts;
        int questionNumber;

        public TestingChamber(int domain)
        {
            InitializeComponent();
            currentDomain = domain;
            totalQue = -1;
        }

        private void TestingChamber_Load(object sender, EventArgs e)
        {
            StreamReader input = new StreamReader(@"intrebari/" + currentDomain.ToString()+ ".dat");
            string str = input.ReadLine();


            int toRead = totalQue = Convert.ToInt32(str);

            for (int i = 0; i < toRead; i++)
            {
                Question temp = new Question();

                temp.setQuestion(input.ReadLine());
                for (int j = 0; j < 4; j++)
                {
                    temp.addAnswer(input.ReadLine());
                }

                temp.setCorrect(input.ReadLine());

                lq.Add(temp);

            }

            //Load hearts
            for (int i = 0; i < 3; i++)
            {
                PictureBox p = new PictureBox();
                p.BackgroundImage = Image.FromFile(@"images\heart.png");
                p.BackgroundImageLayout = ImageLayout.Stretch;
                p.Size = new Size(20, 20);
                p.Location = new Point(24+i*25, 240);
                p.Enabled = true;
                p.Show();

                h.Add(p);
            }

            

            startRound();
        }

        private void startRound()
        {
            questionNumber = 0;
            score = 0;
            hearts = 3;
            showed.Clear();
            showQue();
            UpdateHearts();
            pictureBox1.Show();
            pictureBox2.Show();
            pictureBox3.Show();
        }

        private bool wasShowed(int q)
        {
            foreach (int index in showed)
            {
                if (q == index)
                    return true;
            }

            return false;
        }

        private void UpdateHearts()
        {
            if (hearts < 3)
            {
                pictureBox3.Hide();

                if (hearts < 2)
                {
                    pictureBox2.Hide();

                    if (hearts < 1)
                    {
                        pictureBox1.Hide();
                    }
                }
            }

        }

        private void showQue()
        {
            
            if (showed.Count == totalQue)
            {
                MessageBox.Show("There are no more questions available!");
                //Application.Exit();
                return;
            }

            if (hearts == 0)
            {
                DialogResult result = MessageBox.Show("Your score is " + score.ToString()
                    + Environment.NewLine +"Do you want to try again?" , 
                    "Game Over!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    startRound();
                    return;
                }
                else
                {
                    MenuWindow menu = new MenuWindow();
                    menu.Location = this.Location;
                    this.Visible = false;
                    menu.ShowDialog();
                    this.Close();
                }
            }

            questionNumber++;

            this.Text = "Question number " + questionNumber.ToString();

            Random rdm = new Random();
            q = rdm.Next(0, totalQue);

            while (wasShowed(q))
            {
                q = rdm.Next(0, totalQue);
            }

            showed.Add(q);

            Question currentQ = lq.ElementAt<Question>(q);

            lbQuestion.Text = currentQ.Intrebare;
            lbxAns.Items.Clear();

            for (int i = 0; i < 4; i++)
            {

                lbxAns.Items.Add( (i+1).ToString() + ") " + currentQ.ans.ElementAt<String>(i));
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbxAns.SelectedIndex != lq.ElementAt<Question>(q).correctAnswer)
            {
                hearts--;
                this.Text = "Wrong!";
            }
            else
            {
                score += 10;
                this.Text = "Correct!";
            }

            UpdateHearts();
            System.Threading.Thread.Sleep(1000);
            lbxAns.BackColor = SystemColors.Window;

            
            showQue();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            btnSkip.Enabled = false;
            showQue();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            Random rdm = new Random();

            if (rdm.Next(0,100) < 75)
            {
                MessageBox.Show("Your friend thinks the correct answer is: " +
                    (lq.ElementAt<Question>(q).correctAnswer+1).ToString());
            }
            else
            {
                int r = rdm.Next(0, 4);

                while (r == lq.ElementAt<Question>(q).correctAnswer)
                {
                    r = rdm.Next(0, 4);
                }

                MessageBox.Show("Your friend thinks the correct answer is: " +
                    (r+1).ToString());
            }

            btnCall.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Location = this.Location;
            this.Visible = false;
            menu.ShowDialog();
            this.Close();
        }

    }
}
