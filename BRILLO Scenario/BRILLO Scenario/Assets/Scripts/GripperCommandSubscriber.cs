using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


namespace RosSharp.RosBridgeClient
{
    public class GripperCommandSubscriber : UnitySubscriber<MessageTypes.Std.Float32>
    {
        public  Transform FingerA; // frame of the finger A
        public  Transform FingerB; // frame of the finger B

        private Vector3 FingerAnew; // new position of finger A
        private Vector3 FingerBnew; // new position of finger B

        public  Transform center; // frame which refers to the center of the gripper

        private float percentage;
        private float previouspercentage = 0.0f; // useful in case of the activation of the if structure in the process message
        private bool isMessageReceived ;

        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            FingerA.transform.parent = center.transform; //this line of code defines the parent of the child, in this way we realise the transformation of frames
            FingerB.transform.parent = center.transform; //this line of code defines the parent of the child, in this way we realise the transformation of frames
            if (isMessageReceived)
                ProcessMessage();
        }
        // this method receives a float message from ROS, between 1 and 0
        protected override void ReceiveMessage(MessageTypes.Std.Float32 message)
        {
            percentage = message.data;
            isMessageReceived = true;
        }
        // this method defines the distance between the two fingers
        private void ProcessMessage()
        {
            // these two vairables allows us to modify the values of the coordinates and after that to update the local position of the fingers
            FingerAnew = FingerA.localPosition;
            FingerBnew = FingerB.localPosition;

            FingerAnew.x = FingerAnew.x - (FingerAnew.x * percentage);
            FingerBnew.x = FingerBnew.x - (FingerBnew.x * percentage);

            FingerA.localPosition = FingerAnew;
            FingerB.localPosition = FingerBnew;
        }
    }
}
