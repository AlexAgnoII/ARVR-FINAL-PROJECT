using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private GameObject gameThrowHandler;
    [SerializeField] private GameObject placementHandler;

    private Vector3 targetPosition;
    private bool targetPositionAvailable;

    private float maxDistance = 8f;
    private float distance = 0f;

    //proceed displaying text.
    private bool proceed = true;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.MeteorSmash.ON_SPAWN_TARGET_DONE, this.AllowThrowing);
        this.targetPositionAvailable = false; 

    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.MeteorSmash.ON_SPAWN_TARGET_DONE);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPositionAvailable)
        {
            this.CheckDistance();
        }
    }

    private void CheckDistance()
    {

        Vector3 vectorDistance = this.targetPosition - Camera.main.transform.position;
        vectorDistance.y = 0f;
        this.distance =  vectorDistance.magnitude;

        if(this.distance < this.maxDistance)
        {
            if (gameThrowHandler.activeSelf)
                EventBroadcaster.Instance.PostEvent(EventNames.MeteorSmash.ON_PLAYER_INVALID_DISTANCE);
            
            this.DeactivateGameHandler();
            EventBroadcaster.Instance.PostEvent(EventNames.MeteorSmash.ON_PLAYER_LET_GO); //if player was holding the meteor for too long and he/she went too near.
        }

        else
        {
            if (!this.gameThrowHandler.activeSelf)
                EventBroadcaster.Instance.PostEvent(EventNames.MeteorSmash.ON_PLAYER_VALID_DISTANCE);

            this.ActivateGameThrowingHandler();
        }
    }

    private void AllowThrowing(Parameters param)
    {
        this.ActivateGameThrowingHandler();
        this.DeactivatePlacementHander();

        this.targetPosition = new Vector3(param.GetFloatExtra(EventNames.MeteorSmash.VALUE_TARGET_POSITION_X, -1),
                                          param.GetFloatExtra(EventNames.MeteorSmash.VALUE_TARGET_POSITION_Y, -1),
                                          param.GetFloatExtra(EventNames.MeteorSmash.VALUE_TARGET_POSITION_Z, -1));

        this.targetPositionAvailable = true;
        /*
        //TEST PURPOSES.
        Parameters paramCoor = new Parameters();
        string coordinates = " " + this.targetPosition.x + " " + this.targetPosition.y + " " + this.targetPosition.z;
        paramCoor.PutExtra(EventNames.MeteorSmash.VALUE_TARGET_COORDINATES, coordinates);
        EventBroadcaster.Instance.PostEvent(EventNames.MeteorSmash.ON_PRINT_TARGET_POSITION, paramCoor);
        */

    }


    private void ActivateGameThrowingHandler()
    {
        if(!this.gameThrowHandler.activeSelf)
            this.gameThrowHandler.SetActive(true);
    }

    private void DeactivateGameHandler()
    {
        if(this.gameThrowHandler.activeSelf)
            this.gameThrowHandler.SetActive(false);
    }

    private void ActivatePlacementHandler()
    {
        this.placementHandler.SetActive(true);
    }

    private void DeactivatePlacementHander()
    {
        this.placementHandler.SetActive(false);
    }
}
