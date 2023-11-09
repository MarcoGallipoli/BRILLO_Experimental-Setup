using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SimpleAttach : MonoBehaviour
{
    private Interactable interactable;
    private void Start()
    {
        //At the start of the programme, every interactable component is collocated in this list. They are recognised by the "interactable" component
        interactable = GetComponent<Interactable>(); 
    }

    private void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint(); // show the button to click to grab the object
    }

    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint(); // hide the button to click to grab the object
    }

    private void HandHoverUpdate(Hand hand)
    {
        // GRAB THE OBJECT
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);
        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
            hand.HoverLock(interactable);
            hand.HideGrabHint();

        }
        else if (isGrabEnding)
        {
            //Release
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
    }
}
