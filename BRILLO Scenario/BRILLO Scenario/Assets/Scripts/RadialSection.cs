using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using Valve.VR;

[Serializable]
public class RadialSection
{
    public GameObject Command = null; //text of the command
    public UnityEvent onPress = new UnityEvent(); // Event to activate on press
}
