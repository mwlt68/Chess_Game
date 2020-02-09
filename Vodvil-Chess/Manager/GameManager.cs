using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vodvil_Chess.Manager;
using Vodvil_Chess.Other;
using Vodvil_Chess.Pieces;

namespace Vodvil_Chess
{
    public class GameManager
    {
        public static List<Coordinate> lastMovableList = new List<Coordinate>();
        public static Piece lastPiece;
        public static Piece.Party focusParty;
        public static bool doesGameFinish = false;
        public static bool gameIsGoing = true;
        public static Coordinate upperPawnCoor;

        public GameManager()
        {

        }
        public void GameStart(Piece.Party party,PlayerManager.PlayerType whitePlayer, PlayerManager.PlayerType blackPlayer)
        {
            focusParty = party;
            Board board = new Board(party);
            board.fillNullToBoard();
            board.initPieces();
            History history = new History(Board.board);
            PlayerManager playerManager = new PlayerManager();
            PlayerManager.blackPlayer = blackPlayer;
            PlayerManager.whitePlayer= whitePlayer;
            PlayerManager.Play();
        }

    }
}
