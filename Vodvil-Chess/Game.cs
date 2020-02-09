using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vodvil_Chess.Adaptor;
using Vodvil_Chess.Manager;
using Vodvil_Chess.Move;
using Vodvil_Chess.Other;
using Vodvil_Chess.Pieces;

namespace Vodvil_Chess
{
    public partial class Game : Form
    {
        Random random = new Random();
        int heightOfForm=0;
        int Val = 50;
        Form parent;
        public static MySquare[,] mySquares = new MySquare[8, 8];
        private PlayerManager.PlayerType whitePlayer;
        private PlayerManager.PlayerType blackPlayer;
        private Pieces.Piece.Party focusParty;
        public Game(Form parent, Pieces.Piece.Party focusParty,PlayerManager.PlayerType whitePlayer, PlayerManager.PlayerType blackPlayer)
        {
            InitializeComponent();
            this.parent = parent;
            this.whitePlayer = whitePlayer;
            this.blackPlayer = blackPlayer;
            this.focusParty = focusParty;
            
        }
        private void Game_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            setFormStartProperties();
            GameAdaptor gameAdaptor = new GameAdaptor(focusParty, whitePlayer,blackPlayer);
        }
        private void setFormStartProperties()
        {
            Val=this.Size.Height/15;
            setBoardFLP();
            createSquaresOfBoard();
            comboBox1.Location = new Point(150,50);
            comboBox1.Visible = false;
            SetUndoRedo();
        }

        private void SetUndoRedo()
        {
            int PictureBox1XPos = BoardFLP.Location.X - BoardFLP.Size.Width + 15;
            int PictureBox1YPos = BoardFLP.Location.Y;
            int PictureBox2XPos = PictureBox1XPos;
            int PictureBox2YPos = PictureBox1YPos + pictureBox1.Height +15;
            pictureBox1.Location= new Point(0,0);
            pictureBox2.Location= new Point(0,115);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BackColor = Color.Beige;
            pictureBox2.BackColor = Color.Azure;
            pictureBox1.Image = PieceImg.GetUndoRedoImage(true);     // This image for undo
            pictureBox2.Image = PieceImg.GetUndoRedoImage(false);     // This image for redo
        }

        private void setBoardFLP()
        {
            heightOfForm = this.Size.Height - Val;
            BoardFLP.Size = new Size(heightOfForm, heightOfForm);  // This adjustment is for the board to be square.
            int different =(this.Size.Width - BoardFLP.Size.Width)/2;
            BoardFLP.Location=new Point(different,0);
        }
        private void createSquaresOfBoard()
        {
            int usableSize = heightOfForm-Val;
            int squareSize = usableSize / 8;  // 9 because we need 9*9 square
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    createSquareObj(i, j, squareSize);
                }
            }
        }
        private void createSquareObj(int i, int j,int squareSize)
        {
            MySquare mySquare;
            if ((i+j) % 2 == 0)        // This for white square
            {
                mySquare = new MySquare(this,new Point(i,j),MySquare.ColorType.white, squareSize,BoardFLP,comboBox1);
            }
            else                        // This for black square
            {
               mySquare = new MySquare(this,new Point(i, j), MySquare.ColorType.black, squareSize,BoardFLP,comboBox1);
            }
            mySquares[i,j]=(mySquare);
        }

        private void GameClose(object sender, FormClosedEventArgs e)
        {
            
            parent.Close();
        }

        private void BoardFLP_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SomeProcess.PutPiece(comboBox1.SelectedItem.ToString());
            PawnUpperLaterProcess();
        }
        private void PawnUpperLaterProcess()
        {
            GameManager.gameIsGoing = true;
            GameManager.upperPawnCoor = null;
            comboBox1.Visible = false;
            bool result=PlayerManager.ChangeCurrentPlayer();
            if (!result)
            {
                GameManager.doesGameFinish = true;
                MessageBox.Show("The game finished by scoreless !");
            }
            History history = new History(Board.board);
            GameAdaptor.DisplayPiecesToMySquare(Board.board);
        }
        public void CheckBotPawnUpper()
        {
            if (PlayerManager.currentPlayer == Piece.Party.white)
            {
                if (PlayerManager.whitePlayer == PlayerManager.PlayerType.Bot)
                {
                    SomeProcess.PutPiece(ChoseComboBoxItemRandom());
                    PawnUpperLaterProcess();
                }
                else
                {
                    MessageBox.Show("Please,Chose a piece !");
                }


            }
            else
            {
                if (PlayerManager.blackPlayer == PlayerManager.PlayerType.Bot)
                {
                    SomeProcess.PutPiece(ChoseComboBoxItemRandom());
                    PawnUpperLaterProcess();
                }
                else{
                    MessageBox.Show("Please,Chose a piece !");
                }
            }
        }
        private  String ChoseComboBoxItemRandom()
        {
            int rndVal = random.Next(comboBox1.Items.Count);
            return comboBox1.Items[rndVal].ToString();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (GameManager.gameIsGoing)
            {
                History history = new History();
                history.preClick();
                GameAdaptor.DisplayPiecesToMySquare(Board.board);
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (GameManager.gameIsGoing)
            {
                History history = new History();
                history.nextClick();
                GameAdaptor.DisplayPiecesToMySquare(Board.board);
            }

        }
    }
}
