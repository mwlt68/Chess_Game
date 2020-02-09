using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vodvil_Chess.Adaptor;
using Vodvil_Chess.Move;
using Vodvil_Chess.Other;
using Vodvil_Chess.Pieces;

namespace Vodvil_Chess.Manager
{
    public class MySquare : PictureBox 
    {
        public enum ColorType
        {
            white = 0,
            black = 1,
            red = 2,
        }
        public Image image;
        public Point position;
        public ColorType color;
        private Size size1;
        public PictureBox pb;
        private ComboBox comboBox;
        private Game game;
        public Size Size1 { get => size1; set => size1 = value; }

        public MySquare(Game game,Point position, ColorType color, int size, FlowLayoutPanel panel, ComboBox comboBox)
        {
            this.position = position;
            this.color = color;
            this.Size1 = new Size(size, size);
            createPictureBox(panel);
            this.comboBox = comboBox;
            this.game = game;
        }
        private void createPictureBox(FlowLayoutPanel panel)
        {
            pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Size = this.Size1;
            pb.BackColor = getColor();
            pb.MouseClick += new MouseEventHandler(SquareClick);
            pb.Paint += new PaintEventHandler(myPainter); 
            panel.Controls.Add(pb);
            
        }

        private Color getColor()
        {
            if (this.color == ColorType.black)
                return Color.DarkTurquoise;
            else
                return Color.WhiteSmoke;
        }
        void myPainter(object sender,PaintEventArgs e)
        {
            if (image != null)
            {
                Graphics graphics = e.Graphics;
                graphics.DrawImage(image,0,0);
            }
        }
        #region ClickEvent
        void SquareClick(object sender, MouseEventArgs eventArgs)
        {
            if (GameManager.gameIsGoing)
            {
                if (eventArgs.Button == MouseButtons.Left)
                {
                    ToMouseLeftClick();
                }
                if (eventArgs.Button == MouseButtons.Right)
                {
                    ToMouseRightClick();
                }
            }
        }
        private void ChangeColorTypeOfMovableFloor()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Game.mySquares[i, j].color = ColorType.white;
                    }
                    else
                        Game.mySquares[i, j].color = ColorType.black;
                }
            }
            if (GameManager.lastMovableList != null)
            {
                foreach (var floor in GameManager.lastMovableList)
                {
                    Game.mySquares[floor.GetCorX, floor.GetCorY].color = ColorType.red;
                }
            }
            
        }

        public  void ToMouseLeftClick()
        {
            Piece piece = Board.board[position.X, position.Y];
            if (piece == null || PlayerManager.currentPlayer != piece.party || GameManager.doesGameFinish)
            {
                return;
            }
            if ( piece != null)
            {
                GameManager.lastMovableList=piece.getMovablePos(Board.board,true);
                GameManager.lastPiece = piece;
            }
            else
            {
                if (GameManager.lastMovableList != null)
                    GameManager.lastMovableList.Clear();
                GameManager.lastPiece = null;
            }
            ChangeColorTypeOfMovableFloor();
            GameAdaptor.DisplayPiecesToMySquare(Board.board);
        }
        public void ToMouseRightClick()
        {
            if (GameManager.doesGameFinish)
            {
                return;
            }
            if (GameManager.lastMovableList != null && GameManager.lastMovableList.Count > 0)   // İf there are movable floor
            {
                Coordinate thisCoor = new Coordinate(position.X, position.Y);
                Piece.Party curParty = GameManager.lastPiece.party;
                if (SomeProcess.IsClickedCoorInLastMovableFloor(thisCoor))
                {
                    if (GameManager.lastPiece is King  && 
                        Math.Abs((position.Y-GameManager.lastPiece.position.GetCorY))>1)    // If there is Rok 
                    {
                        SomeProcess.ChangePositionForRok(GameManager.lastPiece.position, thisCoor);
                    }
                    else
                    {
                        SomeProcess.ChangePosition(GameManager.lastPiece.position, thisCoor);
                    }
                    bool controlPawnUpper=SomeProcess.CheckPawnUpper(thisCoor, Board.board[thisCoor.GetCorX,thisCoor.GetCorY]);
                    if (!controlPawnUpper)
                    {
                        ChangeColorTypeOfMovableFloor();
                        GameAdaptor.DisplayPiecesToMySquare(Board.board);
                        GameFinishControl(curParty);
                        bool result = PlayerManager.ChangeCurrentPlayer();
                        if (!result)
                        {
                            GameManager.doesGameFinish = true;
                            MessageBox.Show("The game finished by scoreless !");
                        }
                        AddHistory();
                    }
                    else
                    {
                        comboBox.Visible = true;
                        game.CheckBotPawnUpper();
                        ChangeColorTypeOfMovableFloor();
                        GameAdaptor.DisplayPiecesToMySquare(Board.board);
                        
                    }
                    
                }
            }

        }
        private void AddHistory()
        {
            History history = new History(Board.board);
        }
        private void GameFinishControl(Piece.Party currentParty)
        {
            // 1 win White
            // 0 scoreless
            // -1 win Black
            //  2 game doesnt finish
            FinishControl finishControl = new FinishControl();
            int result =finishControl.GameFinishControl(Board.board, currentParty);
            switch (result)
            {
                case -1:
                    GameManager.doesGameFinish = true;
                    MessageBox.Show("Black team winner !");
                    break;
                case 0:
                    GameManager.doesGameFinish = true;
                    MessageBox.Show("The game finished by scoreless !");
                    break;
                case 1:
                    GameManager.doesGameFinish = true;
                    MessageBox.Show("White team winner !");
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
