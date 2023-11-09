﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVisibility : MonoBehaviour
{
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in objects)
        {
            bool active = false;
            go.GetComponent<MeshRenderer>().enabled=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate ()
    {
        foreach (GameObject go in objects){
            bool active = true;
            go.SetActive(active);
    }
    }

    public void Deactivate()
    {
        foreach (GameObject go in objects)
        {
            bool active = false;
            go.SetActive(active);
        }
    }
}
