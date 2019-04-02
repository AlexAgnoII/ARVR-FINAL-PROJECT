using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameThrowScreenScript : View
{
    [SerializeField] private Text testText;
    [SerializeField] private GameObject heatPanel;


    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_GAME_WON, this.ShowGameWon);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_PLAYER_VALID_DISTANCE, this.PrintValidDistance);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_PLAYER_INVALID_DISTANCE, this.PrintInvalidDistance);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_PLAYER_HOLD_TOO_LONG, this.ShowHeatPanel);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_PLAYER_LET_GO, this.HideHeatPanel);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_REMOVE_TEXT_PLACEMENT, this.ClearTextField);
    }

    private void ClearTextField()
    {
        this.testText.text = "";
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

    private void ShowHeatPanel()
    {
        if (!this.heatPanel.activeSelf) { 
            this.heatPanel.SetActive(true);
            this.testText.text = "Let go, its too hot!";
        }
    }

    private void HideHeatPanel()
    {
        if (this.heatPanel.activeSelf) { 
            this.heatPanel.SetActive(false);
            this.testText.text = "";
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_GAME_WON);
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_PLAYER_VALID_DISTANCE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_PLAYER_INVALID_DISTANCE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_PLAYER_HOLD_TOO_LONG);
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_PLAYER_LET_GO);
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_REMOVE_TEXT_PLACEMENT);

    }

    private void ShowGameWon()
    {
        ViewHandler.Instance.Show(ViewNames.MeteorSmash.WIN_SCREEN);
    }

}
