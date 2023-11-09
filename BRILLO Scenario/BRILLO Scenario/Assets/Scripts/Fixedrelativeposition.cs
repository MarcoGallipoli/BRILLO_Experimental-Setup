using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixedrelativeposition : MonoBehaviour
{
    public Transform Child;
    public Transform Master;

    private Vector3 relativeDistance;
    private Quaternion relativeRotation;


    // Start is called before the first frame update
    void Start()
    {
        relativeDistance = -Master.position + Child.position;
        relativeRotation = Quaternion.Inverse(Master.rotation) * Child.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Child.position = relativeDistance + Master.position;
        Child.rotation = Master.rotation * relativeRotation;
    }
}
