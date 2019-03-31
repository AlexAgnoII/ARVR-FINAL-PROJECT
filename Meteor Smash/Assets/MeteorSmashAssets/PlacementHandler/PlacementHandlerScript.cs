using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class PlacementHandlerScript : MonoBehaviour
{
    /*
     - remove comment of test code, and comment working code for testing on pc.
     - remove comment for real working code for mobile deployment.
         */

    [SerializeField] private List<GameObject>  targetGroupPHList; //target group prefab to spawn.
    [SerializeField] private GameObject placementIndicator; //placement indicator prefab.
    [SerializeField] private ARSessionOrigin arOrigin; //ar camera.

    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool hasSpawnedTargetGroup = false;
    private float secondsBeforePlaying = 1.6f;

    private void Update()
    {
        this.MobilePlacement();
        //this.PcPlacement();
    }

    private void MobilePlacement()
    {
        /* REAL WORKING CODE: for mobile */
        if (!hasSpawnedTargetGroup) { 
            this.UpdatePlacementPose();
            this.UpdatePlacementIndicator();

            if(placementPoseIsValid &&
               Input.touchCount > 0 &&
               Input.GetTouch(0).phase == TouchPhase.Began)
            {
                this.spawnTargetGroup();
                this.spawnSuccess();
            }
        }
    }

    private void PcPlacement()
    {
        /* TEST CODE: For pc. */
        if (!hasSpawnedTargetGroup)
        {
            Instantiate(targetGroupPHList[LevelDecider.Level], this.placementIndicator.transform.position, Quaternion.identity);
            this.spawnSuccess();
            //Debug.Log("SPAWN");
        }
    }

    private void spawnSuccess()
    {
        this.placementIndicator.SetActive(false);
        this.hasSpawnedTargetGroup = true;
        this.StartCoroutine(SendGoSignal());
    }

    //sends signal to be recieved by game handler.
    private IEnumerator SendGoSignal()
    {
        yield return new WaitForSeconds(this.secondsBeforePlaying);
        EventBroadcaster.Instance.PostEvent(EventNames.MeteorSmash.ON_SPAWN_TARGET_DONE);
    }

    private void spawnTargetGroup()
    {
        Instantiate(targetGroupPHList[LevelDecider.Level], this.placementPose.position, this.placementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {

        if(this.placementPoseIsValid)
        {
            this.placementIndicator.SetActive(true);
            this.placementIndicator.transform.SetPositionAndRotation(this.placementPose.position, this.placementPose.rotation);
        }

        //replace this with changing of placementIndicator mesh
        else
        {
            this.placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        //checks if u hit something in the real world from ur camera.
        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        this.arOrigin.Raycast(screenCenter, hits,TrackableType.Planes);

        this.placementPoseIsValid = hits.Count > 0;

        //if we found a valid plane, get the pose.
        if(this.placementPoseIsValid)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraLook = new Vector3(cameraForward.x, 0.0f, cameraForward.z).normalized;

            this.placementPose = hits[0].pose;
            this.placementPose.rotation = Quaternion.LookRotation(cameraLook);

        }
    }
}
