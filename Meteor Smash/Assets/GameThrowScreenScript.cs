using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameThrowScreenScript : View
{

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_GAME_WON, this.ShowGameWon);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_GAME_WON);
    }

    private void ShowGameWon()
    {
        ViewHandler.Instance.Show(ViewNames.MeteorSmash.WIN_SCREEN);
    }

}
