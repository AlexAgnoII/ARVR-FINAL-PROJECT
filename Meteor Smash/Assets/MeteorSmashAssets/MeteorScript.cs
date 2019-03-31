using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private bool detectionDone = false;
    private const float SECONDS_BEFORE_SUCCESS_HIT = 2.1f;
    private const float SECONDS_BEFORE_FAILED_HIT = 1.5f;

    [SerializeField] private GameObject meteorGroundParticleEffect;
    [SerializeField] private GameObject meteorAirParticleEffect;
    [SerializeField] private GameObject targetParticleEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if(!this.detectionDone)
        {
            Debug.Log("HIT: " + collision.gameObject.tag);
            this.detectionDone = true;
            //Did it hit our target?
            if (collision.gameObject.tag.Equals(TagNames.TARGET))
            {
                this.ActivateTargetEffeect(collision.transform);
                StartCoroutine(this.SendSignal(EventNames.MeteorSmash.ON_METEOR_HIT_TARGET, true));
            }

            else 
            {
                if (collision.gameObject.tag.Equals(TagNames.DEAD_ZONE_GROUND))
                    this.ActivateMeteorGroundEffect();
                else if ((collision.gameObject.tag.Equals(TagNames.DEAD_ZONE_AIR)))
                    this.ActivateMeteorAirEffect();

                StartCoroutine(this.SendSignal(EventNames.MeteorSmash.ON_METEOR_HIT_NOTHING, true));
            }

            this.HideMeteor();
        }
    }

    private void HideMeteor()
    {
        //set meteor scale to 0
        this.transform.localScale = new Vector3(0,0,0);
        this.transform.GetComponent<Rigidbody>().isKinematic = true;

        //remove particle inside meteor.
    }

    private void ActivateMeteorGroundEffect()
    {
        Instantiate(this.meteorGroundParticleEffect, this.transform.position, Quaternion.identity);
    }

    private void ActivateMeteorAirEffect()
    {
        Instantiate(this.meteorAirParticleEffect, this.transform.position, Quaternion.identity);
    }

    private void ActivateTargetEffeect(Transform targetPosition)
    {
        Instantiate(this.targetParticleEffect, targetPosition.position, Quaternion.identity);
    }

    private IEnumerator SendSignal(String eventToSend, Boolean didHit)
    {
        float seconds = 0.0f;

        if (didHit)
        {
            seconds = SECONDS_BEFORE_SUCCESS_HIT;
        }
        else seconds = SECONDS_BEFORE_FAILED_HIT;

        yield return new WaitForSeconds(seconds);
        EventBroadcaster.Instance.PostEvent(eventToSend);
        this.detectionDone = false;
    }
}
