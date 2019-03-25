using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameThrowHandlerScript : MonoBehaviour
{

    [SerializeField] private Transform meteorPlaceHolder; //object to throw.
    private Transform meteor;
    private Vector3 meteorBasePosition;
    private Rigidbody meteorRB;

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




    



    // Start is called before the first frame update
    void Start()
    {
        this.meteor = this.meteorPlaceHolder.Find("Sphere");
        this.meteorRB = this.meteor.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateForwardLook();

        if(Input.GetMouseButton(0))
        {
            this.UpdateHoldingObject(); //display object being held by hand.
            this.meteorRB.isKinematic = true; // temp, to reset
            this.meteorRB.velocity = Vector3.zero; //temp, to reset
            
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
                ThrowMeteor();
            }
        }
        
    }

    private void ThrowMeteor()
    {
        Vector3 heading = this.startPos - this.endPos; // the value that points one object to the other
        Vector3 direction = heading / this.swipeDistance; //normalized direction.
        

        this.meteorRB.isKinematic = false;
        /*this.meteorRB.AddForce(-direction.x * this.throwForceXY,
                               -direction.y * this.throwForceXY,
                               (throwForceZ / this.swipeTime));
        */
        this.meteorRB.AddRelativeForce(-direction.x * this.throwForceXY,
                                       -direction.y * this.throwForceXY,
                                       (throwForceZ / this.swipeTime));
        


        Debug.Log(this.swipeTime);
    }

    //switcing input for debugging.
    private bool hasInput()
    {
        //return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        return Input.GetMouseButton(0);
    }

    private Vector3 getInputPosition()
    {
        //return new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, Camera.main.nearClipPlane + 0.7f);
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 0.7f);
    }

    //Updates the object positon when while being held by user.
    private void UpdateHoldingObject()
    {
        Vector3 touchNear = this.getInputPosition();
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touchNear);
        this.meteor.localPosition = new Vector3(touchPosition.x, touchPosition.y, touchPosition.z);

        //Debug.Log(touchPosition);
    }

    private void ResetMeteorPosition()
    {
        this.meteor.localPosition = this.meteorBasePosition;
    }


    //Keeps the object to be thrown stationary on a spot of the screen, and have the right orientation of forward throw.
    private void UpdateForwardLook()
    {
        this.meteorPlaceHolder.position = Camera.main.transform.position; // + Camera.main.transform.forward * 0.0f;
        this.meteor.forward = Camera.main.transform.forward;

    }


}
