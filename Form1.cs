using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XO_Game.Properties;

namespace XO_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        struct stGameStatus
        {
            public bool GameOver;
            public byte PlayCount;
            public enWinner Winner;
        }
        stGameStatus GameStatus;
        enum enPlayer {Player1=1,Player2=2};
        enum enWinner { Player1=1,Player2=2,Draw =3, InProgress =4};


        enPlayer PlayerTurn = enPlayer.Player1;

        void EndGame()
        {
            lblTurn.Text = "Game Over";
            switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;
                case enWinner.Player2:
                    lblWinner.Text = "player 2";
                    break;
                default:
                    lblWinner.Text = "Draw";
                    break;
            }
            MessageBox.Show("Game Over", "End", MessageBoxButtons.OK);
            DisableAllButtons();
        }

        void DisableAllButtons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }
        void EnableAllButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
        }

        public bool CheckValues(Button btn1 , Button btn2 ,Button btn3)
        {
            
            if(btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor= Color.GreenYellow;
                if(btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
               
            }
            GameStatus.GameOver = false;

            return false;
        }
        public void CheckWinner()
        {
            // check rows
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return;

            if (CheckValues(button7, button8, button9))
                return;

            // check columns

            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;
            // check diagonal 
            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button3, button5, button7))
                return;

        }

        public void ChangeImage(Button btn)
        {
            if(btn.Tag.ToString() == "?")
            {
                switch(PlayerTurn)
                {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        lblTurn.Text = "Player 2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lblTurn.Text = "Player 1";
                        GameStatus.PlayCount++;
                        btn.Tag = "O";
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if(GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }


        }

        private void ResetButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
        }
        private void RestartGame()
        {
            EnableAllButtons();
            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

            PlayerTurn =enPlayer.Player1;
            lblTurn.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.InProgress;
            lblWinner.Text = "In Progresss";

        }

        private void button_Click(object sender, EventArgs e)
        {
            
            ChangeImage((Button)sender);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.White;
            Pen whitePen = new Pen(white);

            whitePen.Width = 10;

            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            // draw horizental lines
            e.Graphics.DrawLine(whitePen, 260, 225, 625, 225);
            e.Graphics.DrawLine(whitePen, 260, 350, 625, 350);
            // draw vertical lines
            e.Graphics.DrawLine(whitePen, 365, 120, 365, 450);
            e.Graphics.DrawLine(whitePen, 520, 120, 520, 450);

        }


    }
}
