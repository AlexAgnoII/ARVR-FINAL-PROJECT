using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A test script for the spawn animation of the levels.
public class SpawnTargetGroupAnimScript : MonoBehaviour
{
    [SerializeField] Transform targetGroupPos;
    private Vector3 targetPos = new Vector3(0.0f, 0.0f, 0.0f);
    private int endNum = 0;
    private bool hasFinished = false;
    private float speed = 4.5f;

    // Update is called once per frame
    void Update()
    {
        this.targetGroupPos.localPosition = Vector3.Lerp(this.targetGroupPos.localPosition, this.targetPos, speed * Time.deltaTime);
    }
}
