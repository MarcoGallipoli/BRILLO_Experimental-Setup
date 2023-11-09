using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class InfoBanner : UnitySubscriber<MessageTypes.Std.String>
    {
        private string Stringa;
        private MessageTypes.Std.String message;
        private bool isMessageReceived;

        public GameObject theDisplay;
        public GameObject Help;


        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Std.String message)
        {
            Stringa = message.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            ProcessHelp();

            theDisplay.GetComponent<TMP_Text>().text =Stringa;
        }

        public void ProcessHelp()
        {
            if (Stringa == "IIWA SM - APPROACH")
            {
                Help.GetComponent<TMP_Text>().text = "Move the gripper in the final position and press the button 'right menu'";
            }
            if (Stringa == "IIWA SM - PLANNING")
            {
                Help.GetComponent<TMP_Text>().text = "Wait...";
            }
            if (Stringa == "IIWA SM - PLAN FOUND")
            {
                Help.GetComponent<TMP_Text>().text = "Press the button 'right menu' to continue";
            }
            if (Stringa == "IIWA SM - SIMULATE")
            {
                //Help.GetComponent<TMP_Text>().text = "Press the button 'right menu' to continue";
            }
            if (Stringa == "IIWA COMMANDER - Robot simulated paused")
            {
                Help.GetComponent<TMP_Text>().text = "Press both the triggers to visualize the movement. Press the button 'right menu' to continue or the button 'left menu' to go back";
            }
            if (Stringa == "IIWA COMMANDER - Robot simulated in motion")
            {
                Help.GetComponent<TMP_Text>().text = "Release both the triggers to stop the movement";
            }
            if (Stringa == "IIWA COMMANDER - Robot real in motion")
            {
                Help.GetComponent<TMP_Text>().text = "Release both the triggers to stop the movement";
            }
            if (Stringa == "IIWA COMMANDER - Robot real paused")
            {
                Help.GetComponent<TMP_Text>().text = "Press both the triggers to move the real robot. Press the button 'left menu' to go back";
            }
            
            if (Stringa == "IIWA SM - TELEMANIP")
            {
                Help.GetComponent<TMP_Text>().text = "Press the button 'right menu' to continue";
            }
            if (Stringa == "IIWA SM - TELEMANIP EXECUTE")
            {
                Help.GetComponent<TMP_Text>().text = "Press both the triggers to start the direct telemanipulation. Press the button 'left menu' to go back";
            }

        }
    }
}
