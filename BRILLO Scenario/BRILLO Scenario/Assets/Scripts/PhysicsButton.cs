using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    // these two parameters are used to defined when the button is pressed by the user
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    private bool _isPressed;
    private Vector3 _starPos;
    private ConfigurableJoint _joint;

    public UnityEvent onPressed, onReleased;
    
    void Start()
    {
        _starPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if (_isPressed == false && GetValue() + threshold >= 1)
            //Pressed();
        {
            Pressed();
        }
        if (_isPressed == true && GetValue() - threshold <= 0)
            //Release();
        {
            Release();

        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_starPos, transform.localPosition) / _joint.linearLimit.limit;
        
        if (Mathf.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }
    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
    }
    private void Release()
    {
        _isPressed = false;
        onReleased.Invoke();
    }

}
