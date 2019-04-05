using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] private Material boxMaterial;
    private Rigidbody targetRB;
    private Collider targetCollider;
    private Renderer targetRenderer;
    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        this.targetRB = this.GetComponent<Rigidbody>();
        this.targetCollider = this.GetComponent<Collider>();
        this.targetRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
        this.done = false;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (!done) {
            if (collision.gameObject.CompareTag(TagNames.METEOR))
            {

                if(this.gameObject.CompareTag(TagNames.TARGET)) { 
                    this.targetRB.isKinematic = false;
                    this.targetCollider.enabled = false;
                    this.targetRB.AddForce(new Vector3(this.RandomValue(), 1000f, this.RandomValue()));
                    this.done = true;
                    StartCoroutine(EnableCollider());
                }
                else if (this.gameObject.CompareTag(TagNames.FAKE_TARGET))
                {
                    this.targetRenderer.material = boxMaterial;
                }
 
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
