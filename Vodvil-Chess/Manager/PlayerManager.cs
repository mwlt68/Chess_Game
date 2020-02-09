using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vodvil_Chess.Bot;
using Vodvil_Chess.Move;
using Vodvil_Chess.Other;
using Vodvil_Chess.Pieces;

namespace Vodvil_Chess.Manager
{
    public class PlayerManager
    {
        public static Piece.Party currentPlayer;
        public enum PlayerType
        {
            Human,
            Bot,
        }
        public static PlayerType whitePlayer;
        public static PlayerType blackPlayer;
        public PlayerManager()
        {
            currentPlayer = Piece.Party.white;
            
        }
        public static bool ChangeCurrentPlayer()
        {
            if (currentPlayer == Piece.Party.white)
                currentPlayer = Piece.Party.black;
            else
                currentPlayer = Piece.Party.white;
            return Play();
        }
        public static bool Play()
        {
            if (currentPlayer==Piece.Party.white)
            {
                if (whitePlayer==PlayerType.Bot && !GameManager.doesGameFinish && GameManager.gameIsGoing)
                {
                    
                    RandomBot randomBot = new RandomBot();
                    Piece piece = randomBot.GetRandomPieceFromBoard(Board.board);
                    if (piece== null)
                    {
                        return false;
                    }
                    GameManager.lastPiece = piece;
                    Game.mySquares[piece.position.GetCorX, piece.position.GetCorY].ToMouseLeftClick();
                    Coordinate moveCoordinate = randomBot.GetRandomMovePiece(Board.board,piece);
                    List<Coordinate> coordinates = new List<Coordinate>();
                    coordinates.Add(moveCoordinate);
                    GameManager.lastMovableList = coordinates;
                    Game.mySquares[moveCoordinate.GetCorX, moveCoordinate.GetCorY].ToMouseRightClick();
                }
            }
            else
            {
                if (blackPlayer == PlayerType.Bot && !GameManager.doesGameFinish && GameManager.gameIsGoing)
                {
                    RandomBot randomBot = new RandomBot();
                    Piece piece = randomBot.GetRandomPieceFromBoard(Board.board);
                    if (piece == null)
                    {
                        return false;
                    }
                    GameManager.lastPiece = piece;
                    Game.mySquares[piece.position.GetCorX, piece.position.GetCorY].ToMouseLeftClick();
                    Coordinate moveCoordinate = randomBot.GetRandomMovePiece(Board.board, piece);
                    List<Coordinate> coordinates = new List<Coordinate>();
                    coordinates.Add(moveCoordinate);
                    GameManager.lastMovableList = coordinates;
                    Game.mySquares[moveCoordinate.GetCorX, moveCoordinate.GetCorY].ToMouseRightClick();
                }
            }
            return true;
        }
    }
}
