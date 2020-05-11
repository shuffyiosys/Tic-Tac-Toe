using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
  public partial class mainForm : Form
  {
    private Button[] buttons;

    private ticTacToeEngine gameEngine;

    private void newGame(int Players)
    {
      gameEngine = new ticTacToeEngine(Players);

      for (int i = 1; i < 10; i++)
      {
        buttons[i].Text = "";
        buttons[i].Enabled = true;
      }
    }

    private void winGame(int Player)
    {
      if (Player == -3)
      {
        MessageBox.Show("X wins!");
      }
      else if(Player == 3)
      {
        MessageBox.Show("O wins!");
      }
      else if (Player == 0xBAD)
      {
        MessageBox.Show("Bad game...");
      }
      else
      {
        MessageBox.Show("Draw");
      }

      for (int i = 1; i < 10; i++)
      {
        buttons[i].Enabled = false;
      }
    }

    private void playPiece(int Button)
    {
      buttons[Button].Text = gameEngine.placePiece(Button);
      buttons[Button].Enabled = false;
      evalGame();
    }

    private int evalGame()
    {
      int result = 0;
      int piece = 0;

      result = gameEngine.evaluateLines();

      if (gameEngine.Players != 2)
      {
        piece = gameEngine.runAI();

        if (piece == 0)
        {
          winGame(0xBAD);
        }
        else
        {
          buttons[piece].Text = gameEngine.placePiece(piece);
          buttons[piece].Enabled = false;
          result = gameEngine.evaluateLines();
        }
      }
      
      if (result != 0)
      {
        winGame(result);
        return result;
      }

      return result;
    }

    private void letTheComputerPlayToolStripMenuItem_Click(object sender, EventArgs e)
    {
      newGame(0);
      while (evalGame() == 0) ;
    }

    public mainForm()
    {
      InitializeComponent();
      gameEngine = new ticTacToeEngine(2);

      buttons = new Button[10];
      buttons[1] = bottomLeftSquareButtom;
      buttons[2] = bottomMiddleSquareButton;
      buttons[3] = bottomRightSquareButton;
      buttons[4] = midLeftSquareButton;
      buttons[5] = midMiddleSquareButton;
      buttons[6] = midRightSquareButton;
      buttons[7] = topLeftSquareButton;
      buttons[8] = topMiddleSquareButton;
      buttons[9] = topRightSquareButton;
    }

    private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
    {
      newGame(1);
    }

    private void newGame2PlayersToolStripMenuItem_Click(object sender, EventArgs e)
    {
      newGame(2);
    }

    private void newGame1PlayerOToolStripMenuItem_Click(object sender, EventArgs e)
    {
      newGame(1);
      gameEngine.voidTurn();
      evalGame();
    }

    private void quitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void topLeftSquareButton_Click(object sender, EventArgs e)
    {
      playPiece(7);
    }

    private void topMiddleSquareButton_Click(object sender, EventArgs e)
    {
      playPiece(8);
    }

    private void topRightSquareButton_Click(object sender, EventArgs e)
    {
      playPiece(9);
    }

    private void midLeftSquareButton_Click(object sender, EventArgs e)
    {
      playPiece(4);
    }

    private void midMiddleSquareButton_Click(object sender, EventArgs e)
    {
      playPiece(5);
    }

    private void midRightSquareButton_Click(object sender, EventArgs e)
    {
      playPiece(6);
    }

    private void bottomLeftSquareButtom_Click(object sender, EventArgs e)
    {
      playPiece(1);
    }

    private void bottomMiddleSquareButton_Click(object sender, EventArgs e)
    {
      playPiece(2);
    }

    private void bottomRightSquareButton_Click(object sender, EventArgs e)
    {
      playPiece(3);
    }
  }
}
