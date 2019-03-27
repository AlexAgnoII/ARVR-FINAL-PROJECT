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

    //Attributes used on ball throwing (DEFAULT VALUES)
    [SerializeField] private float maxTime = 1f; 
    [SerializeField] private float minSwipeDistance = 10f;
    [SerializeField] private float throwForceXY = 250f;
    [SerializeField] private float throwForceZ = 5f;
    private float startTime;
    private float endTime;
    private Vector3 startPos;
    private Vector3 endPos;
    private float swipeDistance;
    private float swipeTime;

    private bool thrown, 
                 holding, 
                 curve;
    private const float SPEED_1_LIMIT = 325f; //PC:25 MOBILE:325
    private const float SPEED_2_LIMIT = 680f; //PC:50 MOBILE: 680
    private const float SPEED_3_LIMIT = 820f; //PC:85 MOBILE: 820+

    private const float SPEED_1 = 100f;
    private const float SPEED_2 = 250f;
    private const float SPEED_3 = 300f;

    /*
     - remove all labeled with test on real deployment
     - dont forget to change hasInput and releasedInput for good touch performance.
         */

    // Start is called before the first frame update
    void Start()
    {
        this.meteorPlaceHolder.gameObject.SetActive(true);
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
            this.ShowMeteor();
            this.UpdateHoldingObject(); //display object being held by hand.

            //test (REMOVE WEN DONE).
            this.ResetMeteor();
        }
        //test else
        else
        {
            this.thrown = false;
        }
        /* real else.
        else if(!thrown)
        {
            this.HideMeteor();
        }
        */
        //if not yet thrown, keep doing this.
        if (!thrown) {  

            if(this.hasInput())
            {
                this.holding = true;            
                this.startTime = Time.time;
                this.startPos = Input.mousePosition;
            }
            else if (this.releasdInput())
            {
                this.endTime = Time.time;
                this.endPos = Input.mousePosition;

                this.swipeDistance = (this.endPos - this.startPos).magnitude;
                this.swipeTime = this.endTime - this.startTime;

                if(this.swipeTime < this.maxTime && this.swipeDistance > this.minSwipeDistance)
                {
                    this.ThrowMeteor();
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

        float speed = this.ThrowSpeedHandler();
        
        this.meteorRB.isKinematic = false;                   //old speed factor.
        this.meteorRB.AddRelativeForce(-direction.x * speed, // * this.throwForceXY,
                                       -direction.y * speed, // * this.throwForceXY,
                                       throwForceZ / this.swipeTime);

        this.thrown = true;
        this.meteor.parent = null;

        Parameters param = new Parameters();
        param.PutExtra("speed", this.swipeDistance);
        EventBroadcaster.Instance.PostEvent("SPEED", param);
    }

    private float ThrowSpeedHandler()
    {

        if (this.swipeDistance < SPEED_1_LIMIT)
        {
            Debug.Log("speed 1: " + this.swipeDistance);
            return SPEED_1;
        }
        else if (this.swipeDistance < SPEED_2_LIMIT)
        {
            Debug.Log("speed 2: " + this.swipeDistance);
            return SPEED_2;
        }
        else if (this.swipeDistance < SPEED_3_LIMIT)
        {
            Debug.Log("speed 3: " + this.swipeDistance);
            return SPEED_3;
        }

        else
        {
            Debug.Log("Reached level 3 limit so give speed 3: " + this.swipeDistance);
            return SPEED_3;
        }

    }

    //switcing input for debugging.
    private bool hasInput()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began; //FOR MOBILE
        //return Input.GetMouseButton(0); //FOR PC
    }

    private bool releasdInput()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended; //FOR MOBILE
        //return Input.GetMouseButtonUp(0); //FOR PC
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
        this.meteor.position = new Vector3(touchPosition.x, touchPosition.y, touchPosition.z);

        //Debug.Log(touchPosition);
    }

    //Keeps the meteor to have a forward look similar to where the camera is looking.
    private void UpdateForwardLook()
    {
        this.meteorPlaceHolder.position = Camera.main.transform.position; // + Camera.main.transform.forward * 0.0f; //removed this for now.
        this.meteor.forward = Camera.main.transform.forward;
    }

    private void ShowMeteor()
    {
        if (!this.meteor.gameObject.activeSelf) { 
            this.meteor.gameObject.SetActive(true);
           //Debug.Log("showing meteor");
        }

    }

    private void HideMeteor()
    {
        if (this.meteor.gameObject.activeSelf) { 
            this.meteor.gameObject.SetActive(false);
            //Debug.Log("hiding meteor.");
        }


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
        this.meteor.parent = this.meteorPlaceHolder;
    }

    //sets all boolean values to its defaults.
    private void ResetBoolean()
    {
        this.thrown = false;
        this.curve = false;
        this.holding = false;
    }


}
