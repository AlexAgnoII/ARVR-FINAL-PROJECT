using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private bool detectionDone = false;
    private const float SECONDS_BEFORE_SUCCESS_HIT = 2.1f;
    private const float SECONDS_BEFORE_FAILED_HIT = 1.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if(!this.detectionDone)
        {
            Debug.Log("HIT: " + collision.gameObject.tag);
            this.detectionDone = true;
            //Did it hit our target?
            if (collision.gameObject.tag.Equals(TagNames.TARGET))
            {
                
                StartCoroutine(this.SendSignal(EventNames.MeteorSmash.ON_METEOR_HIT_TARGET, true));
            }

            else if (collision.gameObject.tag.Equals(TagNames.DEAD_ZONE))
            {
                StartCoroutine(this.SendSignal(EventNames.MeteorSmash.ON_METEOR_HIT_NOTHING, false));
            }
        }
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
