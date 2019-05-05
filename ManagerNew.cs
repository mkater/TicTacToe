using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNew : MonoBehaviour
{
    public static ManagerNew theManager;

    int turn = 1;

    int[,] mapdata = new int[3, 3];
    Color[] colors = { Color.white, new Color(1, 0, 0), new Color(0, 1, 0) };

    bool canPlayMore = true;

    public ManagerNew()
    {
        theManager = this;
    }

    public int checkWin()
    {
        if (mapdata[0, 0] == 1 && mapdata[0, 0] == mapdata[0, 1] && mapdata[0, 0] == mapdata[0, 2])
        {
            return 1;
        }
        if (mapdata[1, 0] == 1 && mapdata[1, 0] == mapdata[1, 1] && mapdata[1, 0] == mapdata[1, 2])
        {
            return 1;
        }
        if (mapdata[2, 0] == 1 && mapdata[2, 0] == mapdata[2, 1] && mapdata[2, 0] == mapdata[2, 2])
        {
            return 1;
        }
        if (mapdata[0, 0] == 1 && mapdata[0, 0] == mapdata[1, 0] && mapdata[0, 0] == mapdata[2, 0])
        {
            return 1;
        }
        if (mapdata[0, 1] == 1 && mapdata[0, 1] == mapdata[1, 1] && mapdata[0, 1] == mapdata[2, 1])
        {
            return 1;
        }
        if (mapdata[0, 2] == 1 && mapdata[0, 2] == mapdata[1, 2] && mapdata[0, 2] == mapdata[2, 2])
        {
            return 1;
        }
        if (mapdata[0, 0] == 1 && mapdata[0, 0] == mapdata[1, 1] && mapdata[0, 0] == mapdata[2, 2])
        {
            return 1;
        }
        if (mapdata[0, 2] == 1 && mapdata[0, 2] == mapdata[1, 1] && mapdata[0, 2] == mapdata[2, 0])
        {
            return 1;
        }

        if (mapdata[0, 0] == 2 && mapdata[0, 0] == mapdata[0, 1] && mapdata[0, 0] == mapdata[0, 2])
        {
            return 2;
        }
        if (mapdata[1, 0] == 2 && mapdata[1, 0] == mapdata[1, 1] && mapdata[1, 0] == mapdata[1, 2])
        {
            return 2;
        }
        if (mapdata[2, 0] == 2 && mapdata[2, 0] == mapdata[2, 1] && mapdata[2, 0] == mapdata[2, 2])
        {
            return 2;
        }
        if (mapdata[0, 0] == 2 && mapdata[0, 0] == mapdata[1, 0] && mapdata[0, 0] == mapdata[2, 0])
        {
            return 2;
        }
        if (mapdata[0, 1] == 2 && mapdata[0, 1] == mapdata[1, 1] && mapdata[0, 1] == mapdata[2, 1])
        {
            return 2;
        }
        if (mapdata[0, 2] == 2 && mapdata[0, 2] == mapdata[1, 2] && mapdata[0, 2] == mapdata[2, 2])
        {
            return 2;
        }
        if (mapdata[0, 0] == 2 && mapdata[0, 0] == mapdata[1, 1] && mapdata[0, 0] == mapdata[2, 2])
        {
            return 2;
        }
        if (mapdata[0, 2] == 2 && mapdata[0, 2] == mapdata[1, 1] && mapdata[0, 2] == mapdata[2, 0])
        {
            return 2;
        }

        bool good = false;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (mapdata[i, j] == 0)
                {
                    good = true;
                }
            }
        }

        if (!good)
        {
            return 3;
        }

        return 0;
    }


    public Color PlacePiece(int row, int column)
    {
        if (mapdata[column - 1, row - 1] == 0 && canPlayMore)
        {
            mapdata[column - 1, row - 1] = turn;

            if (turn == 1)
            {
                turn = 2;
                GameObject.FindObjectOfType<TextUpdate>().pushText("Red plays at: " + row + " " + column + "\n");

                int win = checkWin();
                if (win == 1)
                {
                    canPlayMore = false;
                    GameObject.FindObjectOfType<TextUpdate>().pushText("Red wins!\n");
                }
                if (win == 2)
                {
                    canPlayMore = false;
                    GameObject.FindObjectOfType<TextUpdate>().pushText("Green wins!\n");
                }
                if (win == 3)
                {
                    canPlayMore = false;
                    GameObject.FindObjectOfType<TextUpdate>().pushText("Tie!\n");
                }

                return colors[1];
            }
            else
            {
                turn = 1;
                GameObject.FindObjectOfType<TextUpdate>().pushText("Green plays at: " + row + " " + column + "\n");

                int win = checkWin();
                if (win == 1)
                {
                    canPlayMore = false;
                    GameObject.FindObjectOfType<TextUpdate>().pushText("Red wins!\n");
                }
                if (win == 2)
                {
                    canPlayMore = false;
                    GameObject.FindObjectOfType<TextUpdate>().pushText("Green wins!\n");
                }
                if (win == 3)
                {
                    canPlayMore = false;
                    GameObject.FindObjectOfType<TextUpdate>().pushText("Tie!\n");
                }
                return colors[2];
            }
        }
        else
        {
            if (canPlayMore)
                GameObject.FindObjectOfType<TextUpdate>().pushText("Error play!! at: " + row + " " + column + "\n");
            return colors[mapdata[column - 1, row - 1]];
        }
    }

    public void Update()
    {
        
        //enable to play with AI  - Note: to swap plays you have to change the turn ==, like:   turn == 2   
      if (turn == 2)
        {
            //play AI!
            Vector2 thePlay = GameObject.FindObjectOfType<AI>().play(mapdata, turn);

            Color c = PlacePiece((int)thePlay.y, (int)thePlay.x);

            string s = ((int)thePlay.y)+""+ ((int)thePlay.x);

            GameObject.Find(s).GetComponent<SpriteRenderer>().color = c;
            
        }
    }
}
