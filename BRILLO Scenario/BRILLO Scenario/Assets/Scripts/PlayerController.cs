using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;

    void Update()
    {
        // From the position of the finger on the touchpad, it determines the direction of the movement
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y)); // From the position of the finger on the touchpad, it determines the direction of the movement
        transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction,Vector3.up);
    }
}
