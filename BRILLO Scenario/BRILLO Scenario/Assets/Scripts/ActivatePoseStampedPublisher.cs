using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using System.Threading;

namespace RosSharp.RosBridgeClient
{
    public class ActivatePoseStampedPublisher : UnityPublisher<MessageTypes.Geometry.PoseStamped>
    {
        public Transform PublishedTransform;
        public string FrameId = "Unity";

        private MessageTypes.Geometry.PoseStamped message;

        public SteamVR_Input_Sources hand;
        public SteamVR_Action_Boolean Button1;
        public SteamVR_Action_Boolean Button2;


        private bool button;
        private bool button1;
        private bool button2;

        private bool Combination = false;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.PoseStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    frame_id = FrameId
                }
            };
        }


        private void UpdateMessage()
        {
            message.header.Update();
            //The activation of the UpdateMessage starts when the sequence is correct
            if (AnalysisCombination() == true)
            {
                GetGeometryPoint(PublishedTransform.position.Unity2Ros(), message.pose.position);
                GetGeometryQuaternion(PublishedTransform.rotation.Unity2Ros(), message.pose.orientation);

                Publish(message);
        }
    }

        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
        {
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }

        private bool AnalysisCombination()
        {
            // If the Button is pressed the variable button is true
            button1 = StateOfButton(hand, Button1);
            
            if (button1 == true)
            {
                button2 = StateOfButton(hand, Button2);
                
                if (button2 == true)
                {
                    Combination = true;
                }
            }
            else
                Combination = false;

            return Combination;
        }

        private bool StateOfButton(SteamVR_Input_Sources hand, SteamVR_Action_Boolean Button)
        {
            if (Button != null)
            {
                button = Button.GetState(hand);
            }
            return button;
        }
    }
    
}
