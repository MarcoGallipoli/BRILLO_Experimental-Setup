using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using System.Diagnostics;

namespace RosSharp.RosBridgeClient
{
    public class ElapsedTime : UnityPublisher<MessageTypes.Std.Float32>
    {
        private float Message;
        private float Time;
        private MessageTypes.Std.Float32 message;
        
        private int i;

        protected override void Start()
        {
      
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Float32
            {
                data = Message
            };
        }

        private void Update()
        {

            if(Message != 0f)
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                message.data = Message;
                Publish(message);
                watch.Stop();
                Time = watch.ElapsedMilliseconds+Time;
                UnityEngine.Debug.Log("Execution Time: " + Time + " ms");
                Message = 0f;
                Time = 0f;
            }

        }

        public void TestTime()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Message = 1f;
            watch.Stop();
            Time = watch.ElapsedMilliseconds;

        }
    }
}
