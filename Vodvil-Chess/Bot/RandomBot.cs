using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vodvil_Chess.Other;
using Vodvil_Chess.Pieces;

namespace Vodvil_Chess.Bot
{
    public class RandomBot
    {
        Random random = new Random();
        public RandomBot()
        {

        }
        public Piece GetRandomPieceFromBoard(Piece[,] board)
        {
            List<Piece> teamPieces= new List<Piece>();
            List<Piece> includeMovableFloorPieces = new List<Piece>();
            teamPieces = GetPieceOfTeam(board);
            includeMovableFloorPieces = CheckMovableCoorOfTeamPieces(teamPieces, board);
            int count = includeMovableFloorPieces.Count();
            if (count==0)
            {
                return null;
            }
            return includeMovableFloorPieces.ElementAt(random.Next(count));
        }
        public Coordinate GetRandomMovePiece(Piece[,] board,Piece piece)
        {
            List<Coordinate> movableCoorOfPiece = piece.getMovablePos(board,true);
            if (movableCoorOfPiece != null)
            {
                int count = movableCoorOfPiece.Count();
                return movableCoorOfPiece.ElementAt(random.Next(count));
            }
            return null;
        }
        private List<Piece> GetPieceOfTeam(Piece[,] pieces)
        {
            Piece.Party party= Manager.PlayerManager.currentPlayer;
            List<Piece> OpposingTeam = new List<Piece>();
            foreach (var piece in pieces)
            {
                if (piece != null && piece.party == party)
                {
                    OpposingTeam.Add(piece);
                }
            }
            return OpposingTeam;
        }
        private List<Piece> CheckMovableCoorOfTeamPieces(List<Piece> teamPieces,Piece[,] board)
        {
            Piece.Party party = Manager.PlayerManager.currentPlayer;
            List<Piece> movablePieces = new List<Piece>();
            foreach (var piece in teamPieces)
            {
                List<Coordinate> coordinates = piece.getMovablePos(board, true);
                if (coordinates == null || coordinates.Count()==0)
                {

                }
                else
                {
                    movablePieces.Add(piece) ;
                }
                
            }
            return movablePieces;
        }
        
    }
}
