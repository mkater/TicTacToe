using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    //0 - empty, 1 - player 1, 2 - player 2
    public Vector2 play(int[,] board, int player)
    {
        /*print current map in console, for example on using the 2d arrays and debugging*/
        print("Starting map print");
        for (int i = 0; i < 3; i++)
        {
            print(board[0, i] + " " + board[1, i] + " " + board[2, i]);
        }
        print("Ending map print");

        return startGame(board, player);    //calls first move function, which takes in the board and whichever player is going
    }

    public Vector2 secondMove(int[,] board, int player)
    {
        int win = (int)goforWin(board, player).x;   //takes in the values of goforwin and block functions
        int block = (int)goForBlock(board).x;       //if they trigger/return a value, the value will be between 1-3
        if (win == 4 && block == 4)                 //if its 4, it means none of the events triggered
        {
            return checkCornerStrategy(board, player);      //then we go to check corner strategy
        }
        else if (win == 4 && block != 4)        //if block triggers
        {
            return goForBlock(board);       //we go to that function
        }
        else
        {
            return goforWin(board, player);     //otherwise we go to goforwin function.
        }
    }
    
    public Vector2 thirdMove(int[,] board, int player)
    {
        int win = (int)goforWin(board, player).x;       //same as secondMove function
        int block = (int)goForBlock(board).x;
        if (win == 4 && block == 4)     //if neither trigger
        {
            if(board[0,0] == 0) //we go in order placing the square if it is empty
            {
                return new Vector2(1, 1);
            }
            else if(board[0,2] == 0)        //starting with corners
            {
                return new Vector2(1, 3);
            }
            else if(board[2,0] ==0)
            {
                return new Vector2(3, 1);
            }
            else if(board[2,2] == 0)
            {
                return new Vector2(3, 3);
            }
            else if(board[1,0] ==0)     //and then doing the middle pieces
            {
                return new Vector2(2, 1);
            }
            else if(board[0,1] ==0)
            {
                return new Vector2(1, 2);
            }
            else if(board[1,2] ==0)
            {
                return new Vector2(2, 3);
            }
            else
            {
                return new Vector2(3, 2);
            }
        }
        else if(win == 4 && block !=4)  //otherwise check for block
        {
           return goForBlock(board);
        }
        else  //then check for win
        {
            return goforWin(board, player);
        }
    }
    public Vector2 checkCornerStrategy(int[,] board, int player)
    {
        /*This function checks some of the strange corner cases where the program needs to think two moves ahead.*/
        if(((board[0,0] == board[2,2]) && board [2,2] !=0 && board[0,1] == 0) || ((board[0,2] == board[2,0]) && board[2,0] != 0 && board[0, 1] == 0))
        {
            return new Vector2(1, 2);     
        }
        else if(((board[2,1] == board[0,2]) && board [0,2] !=0 && board[2,2] == 0) || ((board[1,2] == board[2,2]) && board[2,2] !=0 && board[2, 2] == 0) || ((board[2, 1] == board[1, 2]) && board[1, 2] != 0 && board[2, 2] ==0))
        {
            return new Vector2(3, 3);
        }
        else
        {
           return thirdMove(board, player); //if they don't exist we just do normal third move
        }
    }

    public Vector2 goforWin(int[,] board, int player)
        /**** This function checks each of the 8 spots (middle not necessary as its always chosen first) to see if the winning move can be made.
         * It takes in the board state and which player is going, to check if two spots in a row are followed by an empty spot, and if those 
         * two in a row matches the player whose turn it is.  If so, it takes the empty spot to win the game.*/
    {
        if (((board[0, 0] == board[0, 1] && board[0, 1] == player) && board[0, 2] == 0) || ((board[1, 2] == board[2, 2] && board[2, 2] == player) && board[0, 2] == 0) || ((board[2, 0] == board[1, 1] && board[1, 1] == player) && board[0, 2] == 0))
        {
            return new Vector2(1, 3);
        }
        else if (((board[0, 0] == board[0, 2] && board[0, 2] == player) && board[0, 1] == 0) || ((board[1, 1] == board[2, 1] && board[2, 1] == player) && board[0, 1] == 0))
        {
            return new Vector2(1, 2);
        }
        else if (((board[0, 2] == board[0, 1] && board[0, 1] == player) && board[0, 0] == 0) || ((board[1, 0] == board[2, 0] && board[2, 0] == player) && board[0, 0] == 0) || ((board[2, 2] == board[1, 1] && board[1, 1] == player) && board[0, 0] == 0))
        {
            return new Vector2(1, 1);
        }
        else if (((board[0, 0] == board[2, 0] && board[2, 0] == player) && board[1, 0] == 0) || ((board[1, 1] == board[1, 2] && board[1, 2] == player) && board[1, 0] == 0))
        {
            return new Vector2(2, 1);
        }
        else if (((board[0, 2] == board[2, 2] && board[2, 2] == player) && board[1, 2] == 0) || ((board[1, 1] == board[1, 0] && board[1, 0] == player) && board[1, 2] == 0))
        {
            return new Vector2(2, 3);
        }
        else if (((board[0, 0] == board[1, 0] && board[1, 0] == player) && board[2, 0] == 0) || ((board[2, 1] == board[2, 2] && board[2, 2] == player) && board[2, 0] == 0) || ((board[0, 2] == board[1, 1] && board[1, 1] == player) && board[2, 0] == 0))
        {
            return new Vector2(3, 1);
        }
        else if (((board[2, 0] == board[2, 2] && board[2, 2] == player) && board[2, 1] == 0) || ((board[1, 1] == board[0, 1] && board[0, 1] == player) && board[2, 1] == 0))
        {
            return new Vector2(3, 2);
        }
        else if (((board[0, 0] == board[1, 1] && board[1, 1] == player) && board[2, 2] == 0) || ((board[2, 1] == board[2, 0] && board[2, 0] == player) && board[2, 2] == 0) || ((board[0, 2] == board[1, 2] && board[1, 2] == player) && board[2, 2] == 0))
        {
            return new Vector2(3, 3);
        }
        else
        {
            return new Vector2(4, 4);
        }
    }
    public Vector2 goForBlock(int[,] board)
    {
        /*Similar to goforWin, this function checks each of the 8 spots to see if the player needs to block.  The difference here is that it 
         * checks to see if two in a row are of the same player, but not necessarily the player whose turn it is.  Thus, if goforWin does
         * not return a board vector but goforBlock does, that means there is two in a row of the other player's pieces followed by an
         * empty, and thus a block must be made to prevent the opponent from winning.*/
            if (((board[0, 0] == board[0, 1] && board[0,1] !=0) && board[0, 2] == 0) || ((board[1, 2] == board[2, 2] && board[2,2] !=0) && board[0, 2] == 0) || ((board[2, 0] == board[1, 1] && board[1,1] !=0) && board[0, 2] == 0))
            {
                return new Vector2(1, 3);
            }
            else if (((board[0, 0] == board[0, 2] && board[0,2] !=0) && board[0, 1] == 0) || ((board[1, 1] == board[2, 1] && board[2,1] !=0) && board[0, 1] == 0))
            {
                return new Vector2(1, 2);
            }
            else if (((board[0, 2] == board[0, 1] && board[0,1] !=0) && board[0, 0] == 0) || ((board[1, 0] == board[2, 0] && board[2,0] !=0) && board[0, 0] == 0) || ((board[2, 2] == board[1, 1] && board[1,1] !=0) && board[0, 0] == 0))
            {
                return new Vector2(1, 1);
            }
            else if (((board[0, 0] == board[2, 0] && board[2,0] !=0) && board[1, 0] == 0) || ((board[1, 1] == board[1, 2] && board[1,2] !=0)  && board[1, 0] == 0))
            {
            return new Vector2(2, 1);
             }
           else if (((board[0, 2] == board[2, 2] && board[2,2] !=0) && board[1, 2] == 0) || ((board[1, 1] == board[1, 0] && board[1,0] !=0) && board[1, 2] == 0))
            {
                return new Vector2(2, 3);
            }
            else if (((board[0, 0] == board[1, 0] && board[1,0] !=0) && board[2, 0] == 0) || ((board[2, 1] == board[2, 2] && board[2,2] !=0) && board[2, 0] == 0) || ((board[0, 2] == board[1, 1] && board[1,1] !=0)  && board[2, 0] == 0))
            {
                return new Vector2(3, 1);
            }
           else  if (((board[2, 0] == board[2, 2] && board[2,2] !=0) && board[2, 1] == 0) || ((board[1, 1] == board[0, 1] && board[0,1] !=0) && board[2, 1] == 0))
            {
                return new Vector2(3, 2);
            }
           else  if (((board[0, 0] == board[1, 1] && board[1,1] !=0) && board[2, 2] == 0) || ((board[2, 1] == board[2, 0] && board[2,0] !=0)  && board[2, 2] == 0) || ((board[0, 2] == board[1, 2] && board[1,2] !=0) && board[2, 2] == 0))
            {
                return new Vector2(3, 3);
            }
            else
            {
            return new Vector2 (4,4);
            }
    }
    
    public Vector2 startGame(int[,] board, int player)
    {
        if (board[1, 1] == 0)       //takes middle spot if available
        {
            return new Vector2(2, 2); //values are between 1 to 3 x and 1 to 3 y
        }
        else
        {
            return secondMove(board, player);   //otherwise, goes to secondMove function
        }
    }
}

