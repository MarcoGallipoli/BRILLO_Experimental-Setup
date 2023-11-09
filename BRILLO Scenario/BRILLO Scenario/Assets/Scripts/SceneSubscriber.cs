using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RosSharp.RosBridgeClient
{
    public class SceneSubscriber : UnitySubscriber<MessageTypes.Std.Int32>
    {
        private int ROSSceneNumber;
        private int ActualSceneNumber = 0;
        private string SceneNumber;

        public GameObject[] Welcome;
        public GameObject[] Approach;
        public GameObject[] Telemanip;
        public GameObject[] Approch_Telemanip;
        public GameObject[] Glasses;
        public GameObject[] Gripper_Right;
        public GameObject[] Gripper_Left;
        public GameObject[] Arm_Right_Simulate;
        public GameObject[] Arm_Right_Real;
        public GameObject[] Arm_Left_Real;
        public GameObject[] Arm_Left_Simulate;
        public GameObject[] Button;


        public GameObject ChosenArmObject;
        private ChosenArm ChosenArmScript;
        private int Chosen;

        private Renderer Rendering;


        private bool isMessageReceived;

        private MessageTypes.Std.Int32 message;

        protected override void Start()
        {
            Chosen = 0;
            foreach (GameObject go in Welcome)
            {
                bool active = true;
                go.SetActive(active);
            }
            foreach (GameObject go in Approach)
            {
                bool active = false;
                go.SetActive(active);
            }
            foreach (GameObject go in Telemanip)
            {
                bool active = false;
                go.SetActive(active);
            }
            foreach (GameObject go in Approch_Telemanip)
            {
                bool active = false;
                go.SetActive(active);
            }
            
            foreach (GameObject go in Glasses)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                Rendering.enabled = false;
            }
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
            foreach (GameObject go in Button)
            {
                go.GetComponent<PhysicsButton>().enabled = true;
            }
            foreach (GameObject go in Arm_Right_Real)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                Rendering.enabled = true;
            }
            foreach (GameObject go in Arm_Right_Simulate)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                Rendering.enabled = false;
            }
            foreach (GameObject go in Arm_Left_Real)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                Rendering.enabled = true;
            }
            foreach (GameObject go in Arm_Left_Simulate)
            {
                Rendering = go.GetComponent<MeshRenderer>();
                Rendering.enabled = false;
            }
            base.Start();
        }

        private void Update()
        {
            ChosenArmScript = ChosenArmObject.GetComponent<ChosenArm>();
            Chosen = ChosenArmScript.ChosenArmNumber;
            if (isMessageReceived)
                ProcessMessage();
            if (ActualSceneNumber==0)
            {
                foreach (GameObject go in Arm_Left_Real)
                {
                    if (Chosen == 2)
                    {
                        Rendering = go.GetComponent<MeshRenderer>();
                        Rendering.enabled = true;
                    }
                    else
                    {
                        Rendering = go.GetComponent<MeshRenderer>();
                        Rendering.enabled = false;
                    };
                }
                foreach (GameObject go in Arm_Right_Real)
                {
                    if (Chosen == 1)
                    {
                        Rendering = go.GetComponent<MeshRenderer>();
                        Rendering.enabled = true;
                    }
                    else
                    {
                        Rendering = go.GetComponent<MeshRenderer>();
                        Rendering.enabled = false;
                    }
                }
            }
        }

        protected override void ReceiveMessage(MessageTypes.Std.Int32 message)
        {
            ROSSceneNumber = message.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
           

            if (ActualSceneNumber != ROSSceneNumber)
            {
                //SceneNumber = ("Scene " + ROSSceneNumber);
                //SceneManager.LoadScene(SceneNumber);
                switch (ROSSceneNumber)
                {
                    case 0:
                        foreach (GameObject go in Welcome)
                        {
                            bool active = true;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Approach)
                        {
                            bool active = false;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Telemanip)
                        {
                            bool active = false;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Approch_Telemanip)
                        {
                            bool active = false;
                            go.SetActive(active);
                        }
                        
                        foreach (GameObject go in Glasses)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = false;
                        }
                        foreach (GameObject go in Arm_Right_Real)
                        {
                            if (Chosen == 1)
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = true;
                            }
                            else
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                        }
                        foreach (GameObject go in Arm_Right_Simulate)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = false;
                        }
                        foreach (GameObject go in Arm_Left_Real)
                        {
                            if (Chosen == 2)
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = true;
                            }
                            else
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            };
                        }
                        foreach (GameObject go in Arm_Left_Simulate)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = false;
                        }
                        foreach (GameObject go in Button)
                        {
                            go.GetComponent<PhysicsButton>().enabled = true;
                        }
                        foreach (GameObject go in Gripper_Right)
                        {
                            if (Chosen == 1)
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                            else
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }

                        }
                        foreach (GameObject go in Gripper_Left)
                        {
                            if (Chosen == 2)
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                            else
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                        }
                        ActualSceneNumber = 0;
                        break;
                    case 1:
                        foreach (GameObject go in Welcome)
                        {
                            bool active = false;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Approach)
                        {
                            bool active = true;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Telemanip)
                        {
                            bool active = false;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Approch_Telemanip)
                        {
                            bool active = true;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Glasses)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = true;
                        }
                        foreach (GameObject go in Arm_Right_Real)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = true;
                        }
                        foreach (GameObject go in Arm_Right_Simulate)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = true;
                        }
                        foreach (GameObject go in Arm_Left_Real)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = true;
                        }
                        foreach (GameObject go in Arm_Left_Simulate)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = true;
                        }
                        foreach (GameObject go in Button)
                        {
                            go.GetComponent<PhysicsButton>().enabled = false;
                        }
                        foreach (GameObject go in Gripper_Right)
                        {
                            if(Chosen == 1)
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = true;
                            }
                            else
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            };
                        }
                        foreach (GameObject go in Gripper_Left)
                        {
                            if (Chosen == 2)
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = true;
                            }
                            else
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                        }
                        ActualSceneNumber = 1;
                        break;
                    case 2:
                        foreach (GameObject go in Welcome)
                        {
                            bool active = false;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Approach)
                        {
                            bool active = false;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Telemanip)
                        {
                            bool active = true;
                            go.SetActive(active);
                        }
                        foreach (GameObject go in Approch_Telemanip)
                        {
                            bool active = true;
                            go.SetActive(active);
                        }
                        
                        foreach (GameObject go in Glasses)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = true;
                        }
                        foreach (GameObject go in Gripper_Right)
                        {
                            if (Chosen == 1)
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                            else
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                        }
                        foreach (GameObject go in Gripper_Left)
                        {
                            if (Chosen == 2)
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                            else
                            {
                                Rendering = go.GetComponent<MeshRenderer>();
                                Rendering.enabled = false;
                            }
                        }
                        foreach (GameObject go in Arm_Right_Real)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = true;
                        }
                        foreach (GameObject go in Arm_Right_Simulate)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = false;
                        }
                        foreach (GameObject go in Arm_Left_Real)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = true;
                        }
                        foreach (GameObject go in Arm_Left_Simulate)
                        {
                            Rendering = go.GetComponent<MeshRenderer>();
                            Rendering.enabled = false;
                        }
                        foreach (GameObject go in Button)
                        {
                            go.GetComponent<PhysicsButton>().enabled = false;
                        }
                        ActualSceneNumber = 2;
                        break;
                }
            }
            
        }

        
    }
}
