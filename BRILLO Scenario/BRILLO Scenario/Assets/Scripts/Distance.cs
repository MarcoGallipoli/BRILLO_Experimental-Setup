using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Distance : MonoBehaviour
{
    public Transform Glass;
    public Transform Gripper;

    public GameObject theDisplay;
    
    private float distance;
    private string Stringa;
    private LineRenderer Line;

    void Start()
    {
        Line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Gripper.position, Glass.position);
        Debug.DrawLine(Gripper.position, Glass.position, Color.black);
        Line.SetPosition(0, new Vector3(Glass.position.x, Glass.position.y, Glass.position.z));
        Line.SetPosition(1, new Vector3(Gripper.position.x, Gripper.position.y, Gripper.position.z));
        
                Stringa = (Math.Round(distance, 2) + "m");
        theDisplay.GetComponent<TMP_Text>().text = Stringa;
    }

    //private void OnDrawGizmos()
    //{
    //    GUI.color = Color.black;
    //    Handles.Label(Gripper.position - (Gripper.position - Glass.position) / 2, distanceBetweenObjects.ToString());
    //}
}
