using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOnly : MonoBehaviour
{

    private float theTime;
    // Start is called before the first frame update
    void Start()
    {
        theTime = Time.time;
        Debug.Log(Time.time);
    }


    private void OnDisable()
    {
        Debug.Log(Time.time - this.theTime);
    }
}
