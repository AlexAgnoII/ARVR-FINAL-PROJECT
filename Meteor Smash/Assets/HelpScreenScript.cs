using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreenScript : View
{
    public void OnBackToMenuButtonClicked()
    {
        ViewHandler.Instance.HideCurrentView();
    }
}
