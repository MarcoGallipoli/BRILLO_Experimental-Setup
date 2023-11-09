using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using System.Threading;



namespace RosSharp.RosBridgeClient
{
    public class LinearVelocityPublisher : UnityPublisher<MessageTypes.Geometry.Vector3>
    {
        public Transform PublishedTransform;

        private MessageTypes.Geometry.Vector3 message;

        private float previousRealTime;        
        private Vector3 previousPosition = Vector3.zero;
        private int i;

        public SteamVR_Input_Sources hand;
        public SteamVR_Action_Boolean Button;
        private bool button = false;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
            i = 0;
        }

        private void Update()
        {
            UpdateMessage();
        }

        private void InitializeMessage()
        {
              message = new MessageTypes.Geometry.Vector3();
        }
        private void UpdateMessage()
        {
            
            float deltaTime = Time.realtimeSinceStartup - previousRealTime;
            Vector3 linearVelocity = (PublishedTransform.position - previousPosition)/deltaTime;
                
            message = GetGeometryVector3(linearVelocity.Unity2Ros()); ;
            
            previousRealTime = Time.realtimeSinceStartup;
            previousPosition = PublishedTransform.position;

            
            if (i != 0)
            {
                button = Button.GetState(hand);
                if (button == true)
                Publish(message);
    
            }
            else
            {
                MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();
                geometryVector3.x = 0;
                geometryVector3.y = 0;
                geometryVector3.z = 0;
                Publish(message);

            }
            i++;
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();
            geometryVector3.x = vector3.x;
            geometryVector3.y = vector3.y;
            geometryVector3.z = vector3.z;
            
            return geometryVector3;
        }

   
    }
}
