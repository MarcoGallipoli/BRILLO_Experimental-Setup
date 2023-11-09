using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;


namespace RosSharp.RosBridgeClient
{
    public class ChosenArm : UnityPublisher<MessageTypes.Std.Int32>
    {
        public int ChosenArmNumber = 0;
        public GameObject[] Gripper_Left;
        public GameObject[] Gripper_Right;
        public GameObject[] Right_Arm;
        public GameObject[] Left_Arm;


        public GameObject[] Right_Obstacle;
        public GameObject[] Left_Obstacle;


        
        private Renderer Rendering;

        private MessageTypes.Std.Int32 message;

        protected override void Start()
        {
            foreach (GameObject go in Gripper_Left)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                Rendering.enabled = false;
            }
            foreach (GameObject go in Gripper_Right)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                Rendering.enabled = false;
            }
            foreach (GameObject go in Right_Arm)
            {
                go.GetComponent<JointStateWriter>().enabled = false;
            }
            foreach (GameObject go in Left_Arm)
            {
                go.GetComponent<JointStateWriter>().enabled = false;
            }

            foreach (GameObject go in Right_Obstacle)
            {
                go.GetComponent<PosePublisherObstacle>().enabled = (false);
                go.GetComponent<ObstacleSizePublisher>().enabled = (false);
            }
            foreach (GameObject go in Left_Obstacle)
            {
                go.GetComponent<PosePublisherObstacle>().enabled = (false);
                go.GetComponent<ObstacleSizePublisher>().enabled = (false);
            }
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Int32
            {
                data = ChosenArmNumber
            };
        }

        private void Update()
        {
            message.data = ChosenArmNumber;
            Publish(message);
            //Debug.Log("Actual Scene" + message.data);
        }

        public void RightArm()
        {
            ChosenArmNumber = 1;
            foreach (GameObject element in Gripper_Right)
            {
                Rendering = element.GetComponent<MeshRenderer>();
                    Rendering.enabled = false;
            }
            foreach (GameObject element in Gripper_Left)
            {
                Rendering = element.GetComponent<MeshRenderer>();
                    Rendering.enabled = false;
            }
            foreach (GameObject go in Right_Arm)
            {
                go.GetComponent<JointStateWriter>().enabled = true;
            }
            foreach (GameObject go in Left_Arm)
            {
                go.GetComponent<JointStateWriter>().enabled = false;
            }
            
            foreach (GameObject go in Right_Obstacle)
            {
                go.GetComponent<PosePublisherObstacle>().enabled = (true);
                go.GetComponent<ObstacleSizePublisher>().enabled = (true);
            }
            foreach (GameObject go in Left_Obstacle)
            {
                go.GetComponent<PosePublisherObstacle>().enabled = (false);
                go.GetComponent<ObstacleSizePublisher>().enabled = (false);
            }
            
        }

        public void LeftArm()
        {
            ChosenArmNumber = 2;
            foreach (GameObject go in Gripper_Right)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                Rendering.enabled = false;
            }
            foreach (GameObject go in Gripper_Left)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                    Rendering.enabled = false;
            }
            foreach (GameObject go in Right_Arm)
            {
                go.GetComponent<JointStateWriter>().enabled = false;
            }
            foreach (GameObject go in Left_Arm)
            {
                go.GetComponent<JointStateWriter>().enabled = true;
            }
            
            foreach (GameObject go in Right_Obstacle)
            {
                go.GetComponent<PosePublisherObstacle>().enabled = (false);
                go.GetComponent<ObstacleSizePublisher>().enabled = (false);
            }
            foreach (GameObject go in Left_Obstacle)
            {
                go.GetComponent<PosePublisherObstacle>().enabled = (true);
                go.GetComponent<ObstacleSizePublisher>().enabled = (true);
            }
            
        }
    }
}
