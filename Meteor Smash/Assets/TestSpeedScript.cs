using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSpeedScript : MonoBehaviour
{
    [SerializeField] private Text speedText;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_SPEED_PRINT, ChangeText);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_SHOW_USER_MISSED_MSG, ShowMissedMsg);
    }

    private void ShowMissedMsg()
    {
        this.speedText.text = "You missed! Try again!";
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_SPEED_PRINT);
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_SHOW_USER_MISSED_MSG, ShowMissedMsg);
    }

    private void ChangeText(Parameters param)
    {
        float speed = param.GetFloatExtra(EventNames.MeteorSmash.SPEED_VALUE_TO_PRINT, -1);
        this.speedText.text = "S p e e d : " + speed;
    }
}
