using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using System.Diagnostics;

namespace RosSharp.RosBridgeClient
{
    public class ElapsedTimeButton : UnityPublisher<MessageTypes.Sensor.Joy>
    {
        private float Message;
        private float Time;
        private MessageTypes.Sensor.Joy message;
        public string FrameId = "Unity";

        private int i;
        
        //Hand to get (right hand or left hand or any hand),
        public SteamVR_Input_Sources hand;



        public GameObject player;

        private bool button;
        private int nButtons = 1;
        public SteamVR_Action_Boolean Button;

        protected override void Start()
        {
      
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Sensor.Joy();
            message.header.frame_id = FrameId;
            message.axes = new float[nButtons];
            message.buttons = new int[nButtons];
        }

        private void Update()
        {    
            if (Button != null)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                watch.Start();
                button = Button.GetState(hand);
                if (button != false)
                {
                    message.buttons[0] = (button ? 1 : 0);
                    message.header.Update();
                    Publish(message);
                    watch.Stop();
                    Time = watch.ElapsedMilliseconds;
                    UnityEngine.Debug.Log("Execution Time: " + Time + " ms");
                    Time = 0f;
                }
                else
            {
                watch.Stop();
            }
                
            }
        }

    }
}
