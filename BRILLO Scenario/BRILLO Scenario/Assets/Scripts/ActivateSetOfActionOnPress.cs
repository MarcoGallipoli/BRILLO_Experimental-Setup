using UnityEngine;
using System.Collections;

namespace Valve.VR
{
    public class ActivateSetOfActionOnPress : MonoBehaviour
    {
        public SteamVR_ActionSet actionSet = SteamVR_Input.GetActionSet("default");

        public SteamVR_Input_Sources forSources = SteamVR_Input_Sources.Any;

        public bool disableAllOtherActionSets = false;

        public int initialPriority = 0;

        public void Activate()
        {
            actionSet.Activate(forSources, initialPriority, disableAllOtherActionSets);
        }

        public void DeActivate()
        {
            actionSet.Deactivate(forSources);
        }

    }
}