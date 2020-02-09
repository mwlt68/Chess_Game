using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vodvil_Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void HumanVsHumanBtnClick(object sender, EventArgs e)
        {
            Game game = new Game(this,Pieces.Piece.Party.white,Manager.PlayerManager.PlayerType.Human, Manager.PlayerManager.PlayerType.Human);
            game.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GetPartyFromGroupBox() != Pieces.Piece.Party.none)
            {
                Game game;
                if (GetPartyFromGroupBox()== Pieces.Piece.Party.white)
                {
                     game= new Game(this, Pieces.Piece.Party.white, Manager.PlayerManager.PlayerType.Human, Manager.PlayerManager.PlayerType.Bot);
                }
                else
                {
                    game = new Game(this, Pieces.Piece.Party.black, Manager.PlayerManager.PlayerType.Bot, Manager.PlayerManager.PlayerType.Human);
                }
                game.Show();
                this.Hide();
            }
        }
        private Pieces.Piece.Party GetPartyFromGroupBox()
        {
            if (radioButton1.Checked)
            {
                return Pieces.Piece.Party.white;
            }
            else if (radioButton2.Checked)
            {
                return Pieces.Piece.Party.black;
            }
            else
            {
                MessageBox.Show("Please ,chose a party !");
                return Pieces.Piece.Party.none;
            }
        }
    }
}
