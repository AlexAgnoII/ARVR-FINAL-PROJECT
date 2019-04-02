using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionScreenScript : View
{
   public void OnBackToMenuClicked()
    {
        Debug.Log("hello");
        ViewHandler.Instance.HideCurrentView();
    }
}
