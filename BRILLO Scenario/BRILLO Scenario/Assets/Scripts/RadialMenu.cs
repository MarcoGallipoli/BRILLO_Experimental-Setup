using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class RadialMenu : MonoBehaviour
{
    [Header("Scene")]
    public Transform selectionTransform = null;
    public Transform cursorTransform = null;

    /* How to add a new sector:
    1) Add a variable to the Header(“Events”) 
    2) Modify the list of sections in the method “CreateAndSetupSections” 
    3) Determine the degreeIncrement by the ratio: 360/n_sectors 
    4) Modify the index of the last sector in the method “SetSelectedEvent” 
     */
    [Header("Events")] // the name of the sections
    public RadialSection top = null;
    public RadialSection bottom_right= null;
    public RadialSection bottom_left= null;
    
    // it must be written with a rotation in clockwise

    private Vector2 touchPosition = Vector2.zero;
    private List<RadialSection> radialSections = null;
    private RadialSection highlightedSection = null;

    private readonly float degreeIncrement = 120.0f; // determined by the ratio 360/n_sector
   

    private void Awake()
    {
        CreateAndSetupSections();
    }

    private void CreateAndSetupSections()
    {
        // Creation of the list of the sections
        radialSections = new List<RadialSection>()
        {
            // it must be written with a rotation in clockwise
            top,
            bottom_right,
            bottom_left,
        };
    }
    // These methods are to activate and destroy the menu
    private void Start()
    {
        Show(false); // it is false because we want the activation only when the touchpad is touched
    }

    public void Show(bool value)
    {
        gameObject.SetActive(value);
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero + touchPosition; //this vector shows the position of the finger
        float rotation = GetDegree(direction); // from the position it is possible determine the angle with a rotation in clockwise

        SetCursorPosition();
        SetSelectionRotation(rotation);
        SetSelectedEvent(rotation);
    }

    private float GetDegree(Vector2 direction)
    {
        float value =Mathf.Atan2(direction.x,direction.y);
        value *= Mathf.Rad2Deg;

        if (value < 0)
            value += 360.0f;
        return value;
    }

    private void SetCursorPosition()
    {
        cursorTransform.localPosition = touchPosition; 
    }

    private void SetSelectionRotation(float newRotation)
    {
        float snappedRotation = SnapRotation(newRotation);
        selectionTransform.localEulerAngles = new Vector3(0, 0, -snappedRotation); 
    }
    
    private float SnapRotation(float rotation)
    {
        return GetnearestIncrement(rotation) * degreeIncrement;
    }

    private int GetnearestIncrement(float rotation)
    {
        return Mathf.RoundToInt(rotation/degreeIncrement); 
    }
    // These methods allows the activation of a specified event

    private void SetSelectedEvent(float currentRotation)
    {
        int index = GetnearestIncrement(currentRotation);
        // index gives us the chosen section
        if (index == 3) // it is a circle, so the last one is the first
            index = 0; 
        highlightedSection = radialSections[index]; // only the chosen section is activated
    }

    public void SetTouchPosition ( Vector2 newValue)
    {
        touchPosition = newValue;
    }

    public void ActivateHighlightedSection()
    {
        highlightedSection.onPress.Invoke(); // when the touchpad is pressed the section is activated

    }
}
