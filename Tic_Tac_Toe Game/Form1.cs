using System;
using System.Drawing;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        //private void PaintForm(object sender, PaintEventArgs e)
        //{
        //    Pen pen = new Pen(Color.White, 10)
        //    {
        //        StartCap = System.Drawing.Drawing2D.LineCap.Round,
        //        EndCap = System.Drawing.Drawing2D.LineCap.Round
        //    };

        //    int moveX = 130;

        //    // Vertical lines
        //    e.Graphics.DrawLine(pen, 300 + moveX, 100, 300 + moveX, 450);
        //    e.Graphics.DrawLine(pen, 400 + moveX, 100, 400 + moveX, 450);

        //    // Horizontal lines
        //    e.Graphics.DrawLine(pen, 100 + moveX, 210, 500 + moveX, 210);
        //    e.Graphics.DrawLine(pen, 200 + moveX, 350, 500 + moveX, 350);
        //}

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        enum enPlayer { Player1, Player2 }
        enum enWinner { Player1, Player2, Draw, GameInProgress }

        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;

        void EndGame()
        {
            
           // lblTurn.Text = "Game Over";
            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    //lblPlayer1Winner.BackColor = Color.GreenYellow;
                    lblWinner.Text = "Player 1 Wins!";
                    break;
                case enWinner.Player2:
                   //LblPlayer2Winner .BackColor = Color.GreenYellow;
                    lblWinner.Text = "Player 2 Wins!";
                    break;
                default:
                    lblWinner.Text = "Draw!";
                    break;
            }
            if (GameStatus.Winner == enWinner.Player1)
            {
                // التأكد من أن Tag يحتوي على قيمة عددية
                int timesWon = (lblTimesPlayer1Win.Tag != null) ? Convert.ToInt32(lblTimesPlayer1Win.Tag) : 0;

                // زيادة العدد بمقدار 1
                timesWon++;

                // تحديث قيمة Tag
                lblTimesPlayer1Win.Tag = timesWon;

                // تحديث نص التسمية ليظهر عدد مرات الفوز
                lblTimesPlayer1Win.Text = $"{timesWon}";
            }
            else if(GameStatus.Winner == enWinner.Player2)
            {
                // التأكد من أن Tag يحتوي على قيمة عددية
                int timesWon = (label11.Tag != null) ? Convert.ToInt32(lblTimesPlayer2Win.Tag) : 0;

                // زيادة العدد بمقدار 1
                timesWon++;

                // تحديث قيمة Tag
                lblTimesPlayer2Win.Tag = timesWon;

                // تحديث نص التسمية ليظهر عدد مرات الفوز
               label11.Text = $"{timesWon}";
            }
            MessageBox.Show("Game Over!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool CheckValue(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn2.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = btn2.BackColor = btn3.BackColor = Color.GreenYellow;
                GameStatus.Winner = (btn1.Tag.ToString() == "x") ? enWinner.Player1 : enWinner.Player2;
                GameStatus.GameOver = true;
                EndGame();
                return true;
            }
            return false;
        }

        public void CheckWinner()
        {
            if (CheckValue(btn1, btn2, btn3) || CheckValue(btn4, btn5, btn6) || CheckValue(btn7, btn8, btn9) ||
                CheckValue(btn1, btn4, btn7) || CheckValue(btn2, btn5, btn8) || CheckValue(btn3, btn6, btn9) ||
                CheckValue(btn1, btn5, btn9) || CheckValue(btn3, btn5, btn7))
            {
                return;
            }

            if (GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
        }

        public void ChangeImage(Button btn)
        {
            if (GameStatus.GameOver) return; // إذا انتهت اللعبة، لا تفعل شيئًا

            if (btn.Tag.ToString() == "?") // التأكد من أن الزر لم يُستخدم بعد
            {
                btn.Image = Resources.خلفيات_للكتابة_عليها_سادة_21;// مسح الصورة القديمة (اختياري)

                if (PlayerTurn == enPlayer.Player1)
                {
                    btn.Image = Resources.X;
                    btn.Tag = "x";
                    lblPlayer1Turn.BackColor = Color.DarkGray;
                    lblPlayer2Turn.BackColor = Color.GreenYellow;
                }
                else
                {
                    btn.Image = Resources.O;
                    btn.Tag = "o";
                    lblPlayer2Turn.BackColor = Color.DarkGray;
                    lblPlayer1Turn.BackColor = Color.GreenYellow;
                }

                GameStatus.PlayCount++;
                CheckWinner();

                // تبديل الدور فقط إذا لم تنتهِ اللعبة
                if (!GameStatus.GameOver)
                {
                    PlayerTurn = (PlayerTurn == enPlayer.Player1) ? enPlayer.Player2 : enPlayer.Player1;
                }
            }
            else
            {
                MessageBox.Show("This spot is already taken!", "Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn1_Click(object sender, EventArgs e) => ChangeImage(btn1);
        private void btn2_Click(object sender, EventArgs e) => ChangeImage(btn2);
        private void btn3_Click(object sender, EventArgs e) => ChangeImage(btn3);
        private void btn4_Click(object sender, EventArgs e) => ChangeImage(btn4);
        private void btn5_Click(object sender, EventArgs e) => ChangeImage(btn5);
        private void btn6_Click(object sender, EventArgs e) => ChangeImage(btn6);
        private void btn7_Click(object sender, EventArgs e) => ChangeImage(btn7);
        private void btn8_Click(object sender, EventArgs e) => ChangeImage(btn8);
        private void btn9_Click(object sender, EventArgs e) => ChangeImage(btn9);

        public void ResetButton(Button btn)
        {
            btn.Image = null;  // No need for an icon
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
        }

        public void ResetGame()
        {
            ResetButton(btn1);
            ResetButton(btn2);
            ResetButton(btn3);
            ResetButton(btn4);
            ResetButton(btn5);
            ResetButton(btn6);
            ResetButton(btn7);
            ResetButton(btn8);
            ResetButton(btn9);

            PlayerTurn = enPlayer.Player1;
            lblPlayer1Turn.BackColor = Color.GreenYellow;
            lblPlayer2Turn.BackColor = Color.DarkGray;

            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "notification";
            notifyIcon1.BalloonTipText = "the Game is End";
            notifyIcon1.ShowBalloonTip(3000);
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.ProgrammingAdvices.com");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = dateTimePicker1.Value.ToString("dddd,dd-MM-yyyy");
        }

        private int Counter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Counter++;
            lblTimmer.Text = Counter.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "notification";
            notifyIcon1.BalloonTipText = "the Game is start";
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void label11_Click_1(object sender, EventArgs e)
        {

        }

        private void lblTimesPlayer1Win_Click(object sender, EventArgs e)
        {

        }
    }
}
