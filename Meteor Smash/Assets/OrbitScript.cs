using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript : MonoBehaviour
{

    [SerializeField] private float xSpread;
    [SerializeField] private float zSpread;
    [SerializeField] private float yOffset;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool rotateClockwise;

    private float timer = 0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        this.timer -= Time.deltaTime * this.rotationSpeed;
        this.Orbit();
    }

    private void Orbit()
    {

        float x;
        float z;
        if (this.rotateClockwise)
        {
             x = -Mathf.Cos(timer) * xSpread;
        }
        else
        {
            x = Mathf.Cos(timer) * xSpread;
        }

        z = Mathf.Sin(timer) * zSpread;
        Vector3 pos = new Vector3(x, yOffset, z);
        this.transform.position = pos + centerPosition.position;
    } 
}
