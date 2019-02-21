using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static int highscore = 0;

    public static int HighScore
    {
        get
        {
            return highscore;
        }
        set
        {
            highscore = value;
        }
    }
}
