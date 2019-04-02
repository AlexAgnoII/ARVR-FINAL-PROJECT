using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameThrowScreenScript : View
{
    [SerializeField] private Text testText;


    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_GAME_WON, this.ShowGameWon);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_PLAYER_VALID_DISTANCE, this.PrintValidDistance);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_PLAYER_INVALID_DISTANCE, this.PrintInvalidDistance);
    }

    private void PrintInvalidDistance()
    {
        this.testText.text = "Too near, move back!";
    }

    private void PrintValidDistance()
    {
        this.testText.text = "";
    }

    private void PrintTargetPosition(Parameters param)
    {
        String coordinates = param.GetStringExtra(EventNames.MeteorSmash.VALUE_TARGET_COORDINATES, "Error.");
        this.testText.text = coordinates;
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_GAME_WON);
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_PLAYER_VALID_DISTANCE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_PLAYER_INVALID_DISTANCE);

    }

    private void ShowGameWon()
    {
        ViewHandler.Instance.Show(ViewNames.MeteorSmash.WIN_SCREEN);
    }

}
