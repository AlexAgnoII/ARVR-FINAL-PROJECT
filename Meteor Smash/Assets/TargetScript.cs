using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    private Rigidbody targetRB;
    private Collider targetCollider;
    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        this.targetRB = this.GetComponent<Rigidbody>();
        this.targetCollider = this.GetComponent<Collider>();
        this.done = false;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (!done) { 
           if(collision.gameObject.CompareTag(TagNames.METEOR))
            {
                this.targetRB.isKinematic = false;
                this.targetCollider.enabled = false;
                this.targetRB.AddForce(new Vector3(this.RandomValue(), 1000f, this.RandomValue()));
                this.done = true;
                StartCoroutine(EnableCollider());
            }
        }
    }

    private float RandomValue()
    {
        return Random.Range(-80f, 80f);
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);
        this.targetCollider.enabled = true;
    }
}
