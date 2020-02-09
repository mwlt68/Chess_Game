using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vodvil_Chess.Move;
using Vodvil_Chess.Other;
using Vodvil_Chess.Pieces;

namespace Vodvil_Chess.Manager
{
    public class History
    {
        public static List<History> rootHistory= new List<History>();
        public static int historyCounter =-1;

        public  Piece.Party currentPlayer;
        public  bool doesGameFinish = false;
        public  bool gameIsGoing = true;
        public  Coordinate upperPawnCoor;
        public Piece[,] hisBoard;
        public History(Piece [,]board)
        {
            checkNext();
            historyCounter++;
            hisBoard = SomeProcess.BoardCoppier(board);
            doesGameFinish = GameManager.doesGameFinish;
            gameIsGoing = GameManager.gameIsGoing;
            currentPlayer = PlayerManager.currentPlayer;
            if (GameManager.upperPawnCoor != null)
            {
                upperPawnCoor = new Coordinate(GameManager.upperPawnCoor.GetCorX, GameManager.upperPawnCoor.GetCorY);
            }
            else
                upperPawnCoor = null;
            rootHistory.Add(this);
        }
        public History()
        {

        }
        private void checkNext()
        {
            if (rootHistory == null)
            {
                return;
            }
            if (rootHistory.Count - 1 > historyCounter)
            {
                int removeSize;
                int count = rootHistory.Count;

                removeSize = count-(historyCounter);
                for (int i = 1; i < removeSize; i++)
                {
                    rootHistory.RemoveAt(count - i);
                }
                historyCounter = rootHistory.Count()-1;
            }
        }
        public  void  preClick()
        {
            
            if (historyCounter > 0)
            {
                historyCounter--;
                History history = rootHistory.ElementAt(historyCounter);
                modifyCurrentBoard(history);
            }
        }
        public  void  nextClick()
        {
            if (historyCounter  < rootHistory.Count()-1 )
            {
                historyCounter++;
                History history = rootHistory.ElementAt(historyCounter);
                modifyCurrentBoard(history);
            }
        }
        private void modifyCurrentBoard(History history)
        {
            Board.board= SomeProcess.BoardCoppier(history.hisBoard);
            GameManager.doesGameFinish = history.doesGameFinish;
            GameManager.gameIsGoing = history.gameIsGoing;
            PlayerManager.currentPlayer = history.currentPlayer;
            if (upperPawnCoor != null)
            {
                GameManager.upperPawnCoor = new Coordinate(history.upperPawnCoor.GetCorX, history.upperPawnCoor.GetCorY);
            }
            else
                GameManager.upperPawnCoor = null;
        }
    }

}
