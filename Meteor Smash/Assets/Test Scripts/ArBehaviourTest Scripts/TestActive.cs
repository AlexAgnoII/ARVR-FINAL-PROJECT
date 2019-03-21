using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestActive : MonoBehaviour
{
    //Test of physics on ARfoundation and its behaviour when being rendered / unrendered.
    [SerializeField] private GameObject gameObjectToTest;
    [SerializeField] private Text activeDisplayText;
    [SerializeField] private Text renderDisplayText;

    [SerializeField] private Text buttonActiveText;
    [SerializeField] private Text buttonRenderText;

    private bool activeDone = false;
    private bool renderDone = false;

    void Update()
    {
        if(this.gameObjectToTest.activeSelf)
        {

            if (activeDone == false) { 
                this.displayActiveStatus("true");
                activeDone = true;
            }
        }
        else
        {
            if (activeDone == true) { 
                this.displayActiveStatus("false");
                activeDone = false;
            }
        }

        if(this.gameObjectToTest.GetComponent<MeshRenderer>().isVisible)
        {
            if (renderDone == false) {
                this.displayRenderedStatus("true");
                renderDone = true;
            }
        }

        else
        {
            if(renderDone == true) { 
                this.displayRenderedStatus("false");
                renderDone = false;
            }
        }
    }

    private void displayActiveStatus(string word)
    {
       activeDisplayText.text = "Active: " + word;
        Debug.Log("ACTIVE FUNCTION");
    }

    private void displayRenderedStatus(string word)
    {
        renderDisplayText.text = "Rendered: " + word;
        Debug.Log("RENDER FUNCTION");
    }

    public void changeActiveStatus()
    {
        if (this.gameObjectToTest.activeSelf)
        {
            this.gameObjectToTest.SetActive(false);
            this.buttonActiveText.text = "Active";
        }
        else { 
            this.gameObjectToTest.SetActive(true);
            this.buttonActiveText.text = "!Active";
        }
    }

    public void changeRenderStatus()
    {
        if (this.gameObjectToTest.GetComponent<MeshRenderer>().isVisible)
        {
            this.gameObjectToTest.GetComponent<MeshRenderer>().enabled = false;
            this.buttonRenderText.text = "Render";
        }
        else {
            this.gameObjectToTest.GetComponent<MeshRenderer>().enabled = true;
            this.buttonRenderText.text = "!Render";
        }
    }

    public void resetGameObject()
    {
        this.gameObjectToTest.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObjectToTest.transform.localPosition = new Vector3(0.0f, 5.0f, 0.0f);
    }
}
