using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using System.Diagnostics;
using System.Threading;


namespace RosSharp.RosBridgeClient
{
    public class JoyPublisherModified : UnityPublisher<MessageTypes.Sensor.Joy>
    {

        public string FrameId = "Unity";
        private decimal Elapsed_Time;

        private MessageTypes.Sensor.Joy message;

        //Hand to get (right hand or left hand or any hand),
        public SteamVR_Input_Sources hand;

        

        public GameObject player;

        private bool button;
        private int nButtons = 5;
        private SteamVR_Action_Boolean Button;
        public SteamVR_Action_Boolean Button1;
        public SteamVR_Action_Boolean Button2;
        public SteamVR_Action_Boolean Button3;
        public SteamVR_Action_Boolean Button4;
        public SteamVR_Action_Boolean Button5;
        private List<SteamVR_Action_Boolean> ButtonList = null;

        protected override void Start()
        {
            ButtonList = new List<SteamVR_Action_Boolean>
            {
                Button1,
                Button2,
                Button3,
                Button4,
                Button5,
            };

            base.Start();
            InitializeMessage();

        }

        private void Update()
        {
            UpdateMessage();
        }      


        private void InitializeMessage()
        {
            //nButtons al posto di JoyAxis e JoyButton Readers
            message = new MessageTypes.Sensor.Joy();
            message.header.frame_id = FrameId;
            message.axes = new float[nButtons];
            message.buttons = new int[nButtons];
        }

        private void UpdateMessage()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i<nButtons; i++)
            {
                Button = ButtonList[i];
            
            if (Button!=null){
                button = Button.GetState(hand);
            }
                message.buttons[i] = (button ? 1 : 0);
            }
            message.header.Update();
            
            Publish(message);
            stopwatch.Stop();

            Elapsed_Time = stopwatch.ElapsedMilliseconds;
            if (Elapsed_Time != 0)
                //UnityEngine.Debug.Log(Elapsed_Time);
            stopwatch.Restart();
        }
    }
}