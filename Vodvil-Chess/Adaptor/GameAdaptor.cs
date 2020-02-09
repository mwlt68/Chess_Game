using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vodvil_Chess.Pieces;
using Vodvil_Chess.Manager;
using Vodvil_Chess.Other;

namespace Vodvil_Chess.Adaptor
{
    public class GameAdaptor
    {

        public GameAdaptor(Pieces.Piece.Party focusParty, PlayerManager.PlayerType whitePlayer, PlayerManager.PlayerType blackPlayer)
        {
            GameManager gameManager = new GameManager();
            gameManager.GameStart(focusParty,whitePlayer,blackPlayer);
            PieceImg pieceImg = new PieceImg();
            if (!pieceImg.readImgs())
            {
                return;
            }
            DisplayPiecesToMySquare(Board.board);

        }
        
        public static void DisplayPiecesToMySquare(Pieces.Piece[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i,j] != null)
                    {

                        callPaintInitFunc( board,  i,  j);
                    }
                    else
                        Game.mySquares[i, j].image = null;

                }
            }
            Display();
        }
        private static void Display()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Game.mySquares[i, j].pb.BackColor = getColor(i,j);
                    Game.mySquares[i, j].pb.Invalidate();
                }
            }
        }
        private static void callPaintInitFunc(Pieces.Piece[,] board,int i,int j)
        {
            string pieceName = "";
            pieceName += board[i, j].GetType().Name;
            if (board[i,j].party==Piece.Party.white)
                pieceName += "W";
            else
                pieceName += "B";
            Game.mySquares[i, j].image = findImgWithTag(pieceName);
        }
        private static Image findImgWithTag(string tag)
        {
            foreach (var item in PieceImg.pieceImgs)
            {
                if (item.Tag.Equals(tag))
                {
                    return item;
                }
            }
            Console.WriteLine("findImgWithTag have an error !");
            return null;
        }
        private static Color getColor(int i ,int j)    // this func get original color of picturebox(part of board)
        {
            if (Game.mySquares[i,j].color==MySquare.ColorType.white)
            {
                return Color.WhiteSmoke;
            }
            else if (Game.mySquares[i, j].color == MySquare.ColorType.black)
            {
                return Color.DarkTurquoise;

            }
            else if(Game.mySquares[i, j].color == MySquare.ColorType.red)
            {
                return Color.Red;
            }
            else
            {
                Console.WriteLine("getColor function have an error at GameAdaptor Classs !");
                return Color.Yellow;
            }
        }
    }
}
