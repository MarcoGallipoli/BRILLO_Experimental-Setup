using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFollowing : MonoBehaviour
{
    public Transform CameraTransform; //this frame has the z axis that goes into the camera and y axis that goes up
    public Transform MenuTransform;

    public float distance;
    private Vector3 relativeposition;
    private Quaternion relativerotation;


    void Start()
    {
        relativeposition.x = 0;
        relativeposition.y = 0;
        relativeposition.z = -distance; 
        // constant rotation of the menu
        relativerotation.x = 0;
        relativerotation.y = 90;
        relativerotation.z = 90;
        relativerotation.w = 0;

    }


    // Update is called once per frame
    void Update()
    {
        // Update of the position of the menu
        MenuTransform.position = CameraTransform.position + relativeposition;
        MenuTransform.rotation = relativerotation;
    }
}
