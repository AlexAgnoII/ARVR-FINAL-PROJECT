using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameThrowHandlerScript : MonoBehaviour
{
    //Attributes about our meteor.
    [SerializeField] private Transform meteorPlaceHolder; //object to throw.
    private Transform meteor;
    private Vector3 meteorBasePosition;
    private Rigidbody meteorRB;

    //Attributes used on ball throwing
    [SerializeField] private float maxTime;
    [SerializeField] private float minSwipeDistance;
    [SerializeField] private float throwForceXY;
    [SerializeField] private float throwForceZ;
    private float startTime;
    private float endTime;
    private Vector3 startPos;
    private Vector3 endPos;
    private float swipeDistance;
    private float swipeTime;

    private bool thrown, 
                 holding, 
                 curve;

    // Start is called before the first frame update
    void Start()
    {
        this.meteor = this.meteorPlaceHolder.Find("Sphere");
        this.meteorRB = this.meteor.GetComponent<Rigidbody>();
        this.ResetBoolean(); //sets all boolean values to its defaults.
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateForwardLook();

        if(holding)
        {
            this.UpdateHoldingObject(); //display object being held by hand.
        }

        //if not yet thrown, keep doing this.
        if (!thrown) {  

            if(Input.GetMouseButton(0))
            {
                this.holding = true;            
                this.startTime = Time.time;
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                this.endTime = Time.time;
                this.endPos = Input.mousePosition;

                this.swipeDistance = (this.endPos - this.startPos).magnitude;
                this.swipeTime = this.endTime - this.startTime;

                if(this.swipeTime < this.maxTime && this.swipeDistance > this.minSwipeDistance)
                {
                    this.ThrowMeteor();
                    this.thrown = true;
                }
                this.holding = false;
            }

        }

    }

    //throws the meteor on where the meteor is facing.
    private void ThrowMeteor()
    {
        Vector3 heading = this.startPos - this.endPos; // the value that points one object to the other
        Vector3 direction = heading / this.swipeDistance; //normalized direction.
        

        this.meteorRB.isKinematic = false;
        this.meteorRB.AddRelativeForce(-direction.x * this.throwForceXY,
                                       -direction.y * this.throwForceXY,
                                       (throwForceZ / this.swipeTime));
    }

    //switcing input for debugging.
    private bool hasInput()
    {
        //return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began; //FOR MOBILE
        return Input.GetMouseButton(0); //FOR PC
    }

    private Vector3 getInputPosition()
    {
        //return new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.nearClipPlane + 0.7f); //FOR MOBILE
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 0.7f); //FOR PC
    }

    //Updates the object positon when while being held by user.
    private void UpdateHoldingObject()
    {
        Vector3 touchNear = this.getInputPosition();
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touchNear);
        this.meteor.localPosition = new Vector3(touchPosition.x, touchPosition.y, touchPosition.z);
    }

    //Keeps the meteor to have a forward look similar to where the camera is looking.
    private void UpdateForwardLook()
    {
        this.meteorPlaceHolder.position = Camera.main.transform.position; // + Camera.main.transform.forward * 0.0f; //removed this for now.
        this.meteor.forward = Camera.main.transform.forward;
    }

    //Resets all values.
    private void ResetAll()
    {
        this.ResetMeteor();
        this.ResetBoolean();
    }

    //set meteor to its defaults.
    private void ResetMeteor()
    {
        this.meteorRB.isKinematic = true;
        this.meteorRB.velocity = Vector3.zero; 
    }

    //sets all boolean values to its defaults.
    private void ResetBoolean()
    {
        this.thrown = false;
        this.curve = false;
        this.holding = false;
    }


}
