using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreenScript : View
{
    public void OnHelpClicked()
    {
        Debug.Log("Help!");
    }

    public void OnPlayClicked()
    {
        Debug.Log("Play"); 
        ViewHandler.Instance.Show(ViewNames.MeteorSmash.LEVEL_SELECTION_SCREEN);
    }
}
