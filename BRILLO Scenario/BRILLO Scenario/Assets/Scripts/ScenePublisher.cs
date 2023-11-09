using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


namespace RosSharp.RosBridgeClient
{
    public class ScenePublisher : UnityPublisher<MessageTypes.Std.Int32>
    {
        public int ActualSceneNumber;

        private MessageTypes.Std.Int32 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Int32
            {
                data = ActualSceneNumber
            };
        }

        private void Update()
        {
            message.data = ActualSceneNumber;
            Publish(message);
            //Debug.Log("Actual Scene" + message.data);
        }
    }
}
