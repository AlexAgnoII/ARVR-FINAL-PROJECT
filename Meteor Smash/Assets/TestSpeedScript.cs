using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSpeedScript : MonoBehaviour
{
    [SerializeField] private Text speedText;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver("SPEED", ChangeText);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver("SPEED");
    }

    private void ChangeText(Parameters param)
    {
        float speed = param.GetFloatExtra("speed", -1);
        this.speedText.text = "Speed: " + speed;
    }
}
