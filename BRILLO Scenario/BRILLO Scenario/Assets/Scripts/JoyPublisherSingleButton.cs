using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


namespace RosSharp.RosBridgeClient
{
    public class JoyPublisherSingleButton : UnityPublisher<MessageTypes.Sensor.Joy>
    {

        public string FrameId = "Unity";

        private MessageTypes.Sensor.Joy message;

        //Hand to get (right hand or left hand or any hand),
        public SteamVR_Input_Sources hand;

        

        public GameObject player;

        private bool button;
        private int nButtons = 5;
        public SteamVR_Action_Boolean Button;
        
       

        protected override void Start()
        {
           

            base.Start();
            InitializeMessage();

        }

        private void Update()
        {
            UpdateMessage();
        }      


        private void InitializeMessage()
        {
            message = new MessageTypes.Sensor.Joy();
            message.header.frame_id = FrameId;
            message.axes = new float[nButtons];
            message.buttons = new int[nButtons];
        }

        private void UpdateMessage()
        {
            
            if (Button!=null){
                button = Button.GetState(hand);
            }
                message.buttons[0] = (button ? 1 : 0);
            
            message.header.Update();
            
            Publish(message);
        }
    }
}