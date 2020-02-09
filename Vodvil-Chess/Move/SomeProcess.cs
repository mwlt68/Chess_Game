using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vodvil_Chess.Manager;
using Vodvil_Chess.Other;
using Vodvil_Chess.Pieces;

namespace Vodvil_Chess.Move
{
    public class SomeProcess
    {
        public static void ChangePosition(Coordinate currentCoor,Coordinate focusCoordinate)
        {
            Board.board[focusCoordinate.GetCorX, focusCoordinate.GetCorY] = Board.board[currentCoor.GetCorX, currentCoor.GetCorY];
            Board.board[focusCoordinate.GetCorX, focusCoordinate.GetCorY].didAnyMove = 1;
            Board.board[focusCoordinate.GetCorX, focusCoordinate.GetCorY].position = new Coordinate(focusCoordinate.GetCorX, focusCoordinate.GetCorY);
            Board.board[currentCoor.GetCorX, currentCoor.GetCorY] = null;
            GameManager.lastPiece = null;
            GameManager.lastMovableList = null;
        }
        public static void PutPiece(String name)
        {
            Piece.Party party = PlayerManager.currentPlayer;

            if (name.Equals("Queen"))
            {
                PieceSetter(new Queen(party));
            }
            else if (name.Equals("Rook"))
            {
                PieceSetter(new Rook(party));
            }
            else if (name.Equals("Bishop"))
            {
                PieceSetter(new Bishop(party));
            }
            else if (name.Equals("Knight"))
            {
                PieceSetter(new Knight(party));
            }
        }
        private static  void PieceSetter(Piece piece)
        {
            piece.didAnyMove = -1;
            Coordinate coordinate = GameManager.upperPawnCoor;
            piece.position = coordinate;
            Board.board[coordinate.GetCorX, coordinate.GetCorY] = piece;
        }
        public static void ChangePositionForRok(Coordinate currentCoor, Coordinate focusCoordinate)
        {
            // Current coordinates about king

            // King will pass focus coordinate
            ChangePosition(currentCoor, focusCoordinate);

            if (focusCoordinate.GetCorY > currentCoor.GetCorY )    // This means that the position of the rook is to the right of the king.
            {

                // Rook will pass left of focus coordinate
                Coordinate rookCoor = new Coordinate(focusCoordinate.GetCorX,focusCoordinate.GetCorY+1);
                Coordinate focusRookCoor= new Coordinate(focusCoordinate.GetCorX, focusCoordinate.GetCorY - 1);
                ChangePosition(rookCoor, focusRookCoor);
            }
            else                                                 // This means that the position of the rook is to the left of the king.
            {
                // Rook will pass right of focus coordinate
                Coordinate rookCoor = new Coordinate(focusCoordinate.GetCorX, focusCoordinate.GetCorY - 1);
                Coordinate focusRookCoor = new Coordinate(focusCoordinate.GetCorX, focusCoordinate.GetCorY + 1);
                ChangePosition(rookCoor, focusRookCoor);
            }
        }
        public static Piece[,] BoardCoppier(Piece[,]  board)
        {
            Piece[,] copy = new Piece[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null)
                        copy[i, j] = (Piece)board[i, j].Clone();
                    else
                        copy[i, j] = null;
                    if (copy[i, j] != null)
                    {
                        copy[i, j].position = new Coordinate(i, j);
                    }
                }
            }
            return copy;
        }
        public static bool  CheckPawnUpper(Coordinate nextCoordinate,Piece piece)
        {
            if (piece != null && piece is Pawn)
            {
                if (nextCoordinate.GetCorX == 7 || nextCoordinate.GetCorX == 0)
                {
                    GameManager.upperPawnCoor = new Coordinate(nextCoordinate.GetCorX,nextCoordinate.GetCorY);
                    GameManager.gameIsGoing = false;
                    return true;
                }
            }
            return false;
        }
        public static bool IsClickedCoorInLastMovableFloor(Coordinate clickedCoor)
        {
            foreach (var coor in GameManager.lastMovableList)
            {
                if (CoordinatesCompare(clickedCoor,coor))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool CoordinatesCompare(Coordinate clickedCoor,Coordinate coor)
        {
            if (clickedCoor.GetCorX.Equals(coor.GetCorX) && clickedCoor.GetCorY.Equals(coor.GetCorY))
            {
                return true;
            }
            return false;
        }

        public static List<Piece> GetPieceOfTeam(Piece[,] board, Piece.Party teamParty)
        {
            List<Piece> pieces = new List<Piece>();
            foreach (var piece in board)
            {
                if (piece != null && piece.party == teamParty)
                {
                    pieces.Add(piece);
                }
            }
            return pieces;
        }
        public static List<Coordinate> GetMovableCoorOfTeam( Piece[,] board,List<Piece> Pieces)
        {
            List<Coordinate> movableCoor = new List<Coordinate>();
            foreach (var piece in Pieces)
            {
                {
                    movableCoor.AddRange(piece.getMovablePos(board, false));
                }

            }
            return movableCoor;
        }
    }
}
