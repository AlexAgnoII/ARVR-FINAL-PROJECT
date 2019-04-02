using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScriptScreen : View
{
    [SerializeField] Text scoreText;

    public void Start()
    {
        this.scoreText.text = "Score: " + ScoreStorage.Score;
    }

    
}
