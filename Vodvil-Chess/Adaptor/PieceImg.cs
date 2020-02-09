using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vodvil_Chess.Adaptor
{
    public class PieceImg
    {
        public static List<Image> pieceImgs = new List<Image>(12);
        String curDirectory = "";
        public bool readImgs()
        {
            curDirectory = Path.GetDirectoryName(Application.ExecutablePath); 
            pieceImgs.Add(GetImage("PawnW"));
            pieceImgs.Add(GetImage("PawnB"));
            pieceImgs.Add(GetImage("BishopW"));
            pieceImgs.Add(GetImage("BishopB"));
            pieceImgs.Add(GetImage("RookW"));
            pieceImgs.Add(GetImage("RookB"));
            pieceImgs.Add(GetImage("KingW"));
            pieceImgs.Add(GetImage("KingB"));
            pieceImgs.Add(GetImage("KnightW"));
            pieceImgs.Add(GetImage("KnightB"));
            pieceImgs.Add(GetImage("QueenW"));
            pieceImgs.Add(GetImage("QueenB"));
            return checkAllImgs();
        }
        private bool checkAllImgs()
        {
            foreach (var item in pieceImgs)
            {
                if (item==null)
                {
                    return false;
                }
            }
            return true;
        }
        private Image GetImage(string name)
        {
            try
            {
                String path = curDirectory + "\\images\\" + name + ".PNG";
                Image i = Image.FromFile(path);
                i.Tag = name;
                return i;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot find "+name+ ".PNG !");
                return null;
            }
        }
        public static Image GetUndoRedoImage(bool isUndo)
        {
            String exeDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            String undoPath = exeDirectory + "\\images\\" + "undo.png";
            String redoPath = exeDirectory + "\\images\\" + "redo.png";
            if (isUndo)
            {
                return GetImageFromPath(undoPath);
            }
            else
            {
                return GetImageFromPath(redoPath);
            }
        }
        public static Image GetImageFromPath(String path)
        {
            try
            {
                Image i = Image.FromFile(path);
                return i;
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot find " + path + " !");
                return null;
            }
        }
    }
}
