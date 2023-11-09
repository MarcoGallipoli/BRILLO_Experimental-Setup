using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using System.Threading;

public class ActivateTutorial : MonoBehaviour
{
    public GameObject[] tutorial;
    private bool active;
    private bool button;
    //public SteamVR_Input_Sources hand;
    //public SteamVR_Action_Boolean Button;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in tutorial)
        {
            active = false;
            go.SetActive(active);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Button != null)
        //{
        //    button = Button.GetState(hand);
        //    if (button == true)
        //    {
                
        //        Control();
        //    }
            //else
            //{
            //    if (button == true)
            //        Deactivate();
            //}

        //}

    }
    public void Control ()
    {
        foreach (GameObject go in tutorial)
        {
            if (active == false)
            {
                active = true;
                go.SetActive(active);
            }
            else
            {
                active = false;
                go.SetActive(active);
            }
        }
    }

    public void Deactivate()
    {
        foreach (GameObject go in tutorial)
        {
            
                active = false;
                go.SetActive(active);
            
        }
    }

    public void Activate()
    {
        foreach (GameObject go in tutorial)
        {
            
                active = true;
                go.SetActive(active);
            
        }
        
    }
}
