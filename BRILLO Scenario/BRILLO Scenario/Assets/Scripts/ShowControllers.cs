using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowControllers : MonoBehaviour
{
    public bool showController = true;

    

    // Update is called once per frame
    void Update()
    {
        foreach (var hand in Player.instance.hands)
        {
            if (showController)
            {
                hand.ShowController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);
            }
            else
            {
                hand.HideController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
            
        }

    }

    public void Hide()
    {
        foreach (var hand in Player.instance.hands)
        {
            hand.HideController();
        }
    }

    public void Show()
    {
        foreach (var hand in Player.instance.hands)
        {
            hand.ShowController();
        }
    }
}
