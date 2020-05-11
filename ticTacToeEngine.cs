using System;
using System.Linq;

namespace TicTacToe
{
  class ticTacToeEngine
  {
    private int[][] gameBoard = new int[3][];
    private int players;
    private int turn;

    /* Combination of winning lines:
       0: Top row
       1: Middle row
       2: Bottom row
       3: Left column
       4: Middle column
       5: Right column
       6: \ diagonal
       7: / diagonal*/
    private int[] lineResults = new int[8];

    /* Public methods */
    public ticTacToeEngine()
    {
      gameBoard[0] = new int[3];
      gameBoard[1] = new int[3];
      gameBoard[2] = new int[3];
      lineResults = new int[8];
      this.players = 1;
      this.turn = -1;
    }

    public ticTacToeEngine(int Players)
    {
      gameBoard[0] = new int[3];
      gameBoard[1] = new int[3];
      gameBoard[2] = new int[3];
      lineResults = new int[8];
      this.players = Players;
      this.turn = -1;
    }

    /* @brief Runs the TicTacToe AI routine
       
       @return Piece that the AI wants to play.
     */
    public int runAI()
    {
      Random rand = new Random();
      int piece = 0;

      //Check for 2 in a row condition (it must be filled regardless)
      piece = checkTwoInARow();
      if (piece != 0)
      {
        return piece;
      }

      //Check anchors
      piece = checkAnchor();
      if (piece != 0)
      {
        return piece;
      }

      /* Take a random piece inside. */
      piece = checkInside();

      return piece;
    }

    /* @brief Places a piece on the internal game board.
     
       @param Button Piece to play on the game board.
       @return X or O string to put on the GUI button. */
    public string placePiece(int Button)
    {
      string playerLetter = "";
      Button--;

      if (gameBoard[Button / 3][Button % 3] == 0)
      {
        gameBoard[Button / 3][Button % 3] = turn;
      }
      else
      {
        return playerLetter;
      }

      if (turn == 1)
      {
        turn = -1;
        return "O";
      }
      else
      {
        turn = 1;
        return "X";
      }
    }

    /* @brief Skips a turn. */
    public void voidTurn()
    {
      if (turn == 1)
      {
        turn = -1;
      }
      else
      {
        turn = 1;
      }
    }

    /* @brief Evaluates if there's a 3-in-a-row. Also used for the AI.
       
       @return 3 or -3 if there's a line. 0 otherwise. */
    public int evaluateLines()
    {
      //Evaluate rows and cols
      for (int i = 0; i < 3; i++)
      {
        lineResults[i] = gameBoard[i][0] + gameBoard[i][1] + gameBoard[i][2];
        lineResults[i + 3] = gameBoard[0][i] + gameBoard[1][i] + gameBoard[2][i];
      }

      //Evaluate diagnols
      lineResults[6] = gameBoard[0][0] + gameBoard[1][1] + gameBoard[2][2];
      lineResults[7] = gameBoard[0][2] + gameBoard[1][1] + gameBoard[2][0];

      for (int i = 0; i < 8; i++)
      {
        if (Math.Abs(lineResults[i]) == 3)
        {
          return lineResults[i];
        }
      }

      return 0;
    }

    /* @brief Accessor for the players variable*/
    public int Players
    {
      get { return players; }
    }

    /* Private methods */

    /* @brief Checks if there's a two in a row right now. It must be filled no matter what.
     * 
     * @return Piece to play.
     */
    private int checkTwoInARow()
    {
      /* First check if we can win. */
      for (int i = 0; i < 8; i++)
      {
        if (lineResults[i] == 2)
        {
          return fillLine(i);
        }
      }

      /* Then check if opponent can win.*/
      for (int i = 0; i < 8; i++)
      {
        if (lineResults[i] == -2)
        {
          return fillLine(i);
        }
      }

      return 0;
    }

    /* @brief Checks a line and puts a piece to fill it
     * 
     * @return Piece to play
     */
    private int fillLine(int Line)
    {
      if (Line < 3)
      {
        if (gameBoard[Line][0] == 0)
        {
          return ((Line * 3) + 1);
        }
        else if (gameBoard[Line][1] == 0)
        {
          return ((Line * 3) + 2);
        }
        else if (gameBoard[Line][2] == 0)
        {
          return ((Line * 3) + 3);
        }
      }
      else if (Line < 6)
      {
        if (gameBoard[0][Line - 3] == 0)
        {
          return ((Line - 3) + 1);
        }
        else if (gameBoard[1][Line - 3] == 0)
        {
          return ((Line - 3) + 4);
        }
        else if (gameBoard[2][Line - 3] == 0)
        {
          return ((Line - 3) + 7);
        }
      }
      else if (Line == 6)
      {
        if (gameBoard[0][0] == 0)
        {
          return 1;
        }
        else if (gameBoard[1][1] == 0)
        {
          return 5;
        }
        else if (gameBoard[2][2] == 0)
        {
          return 9;
        }
      }
      else if (Line == 7)
      {
        if (gameBoard[0][2] == 0)
        {
          return 3;
        }
        else if (gameBoard[1][1] == 0)
        {
          return 5;
        }
        else if (gameBoard[2][0] == 0)
        {
          return 7;
        }
      }

      return 0;
    }

    /* @brief Checks the corners (I call these anchors) to resolve where to play.
     * 
     * @return Piece to play
     */
    private int checkAnchor()
    {
      int i = 0;
      int[] available_anchors = new int[4];
      Random rand = new Random();

      //Evaluate anchors
      if (gameBoard[0][0] == 0)
      {
        available_anchors[i++] = 1;
      }
      if (gameBoard[0][2] == 0)
      {
        available_anchors[i++] = 3;
      }
      if (gameBoard[2][0] == 0)
      {
        available_anchors[i++] = 7;
      }
      if (gameBoard[2][2] == 0)
      {
        available_anchors[i++] = 9;
      }

      //If an achor is occupied, take the opposite one.
      for (int j = 0; j < 4; j++)
      {
        switch (available_anchors[j])
        {
          case 1:
            if (!available_anchors.Contains(9))
            {
              return 1;
            }
            break;
          case 3:
            if (!available_anchors.Contains(7))
            {
              return 3;
            }
            break;
          case 7:
            if (!available_anchors.Contains(3))
            {
              return 7;
            }
            break;
          case 9:
            if (!available_anchors.Contains(1))
            {
              return 9;
            }
            break;
          default:
            break;
        }
      }
      
      //Otherwise, return a random anchor. Should return 0 if none are available.
      return available_anchors[rand.Next(i)];
    }

    /* @brief Checks if any of the inside pieces are availalbe, choose one at random
     * 
     * This is done last, and more than likely it will not be run.
     * 
     * @return Piece to play
     */
    private int checkInside()
    {
      int i = 0;
      int[] available_places = new int[5];
      Random rand = new Random();

      if (gameBoard[0][1] == 0)
      {
        available_places[i++] = 2;
      }
      if (gameBoard[2][1] == 0)
      {
        available_places[i++] = 8;
      }

      for (int j = 4; j < 7; j++)
      {
        if (gameBoard[1][j-4] == 0)
        {
          available_places[i++] = j;
        }
      }

      //return a random place (what else can we do?)
      return available_places[rand.Next(i)];
    }
  }
}
