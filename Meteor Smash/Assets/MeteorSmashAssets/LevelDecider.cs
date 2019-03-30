using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDecider : MonoBehaviour
{

    private static int level = 0;

    public static int Level {

        get
        {
            return level;
        }

        set
        {
            level = value;
        }

    }
}
