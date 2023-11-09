using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


namespace RosSharp.RosBridgeClient
{
    public class ScenePublisherDesired : UnityPublisher<MessageTypes.Std.Int32>
    {
        public int ActualSceneNumber;
        private int SceneNumber;

        private MessageTypes.Std.Int32 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            SceneNumber = ActualSceneNumber;

            message = new MessageTypes.Std.Int32
            {
                data = SceneNumber
            };
        }

        private void Update()
        {
            message.data = SceneNumber;
            //Debug.Log("Desired Scene" + message.data);

            Publish(message);
            //SceneNumber = DefaultSceneNumber;
        }

        public void ChangeScene1()
        {
            SceneNumber = 1;
        }

        public void ChangeScene2()
        {
            SceneNumber = 2;
        }

        public void ChangeScene0()
        {
            SceneNumber = 0;
        }
    }
}
