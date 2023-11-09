/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

// Added allocation free alternatives
// UoK , 2019, Odysseas Doumas (od79@kent.ac.uk / odydoum@gmail.com)

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PosePublisherGripper : UnityPublisher<MessageTypes.Geometry.PoseStamped>
    {
        public Transform LeftGripper;
        public Transform RightGripper;
        // this variable represents the frame from which it is established the position
        private Transform ReferenceTransform;
        private Transform PublishedTransform;
        public Transform ReferenceRightArm;
        public Transform ReferenceLeftArm;

        public GameObject ChosenArmObject;
        private ChosenArm ChosenArmScript;
        private int Arm = 0;
        private int PreviousArm = 0;
        
        private Vector3 positionUnity;
        private Vector3 positionUnity2Ros;
        private Quaternion rotationUnity;
        private Quaternion rotationRos;
        private Quaternion rotationUnity2Ros;
        public string FrameId = "world"; // this name refers to the reference frame

        private MessageTypes.Geometry.PoseStamped message;

        protected override void Start()
        {

            base.Start();
            InitializeMessage();
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

        private void FixedUpdate()
        {
            ChosenArmScript = ChosenArmObject.GetComponent<ChosenArm>();
            Arm = ChosenArmScript.ChosenArmNumber;
            if (Arm != PreviousArm)
                {
                    if (Arm == 1)
                    {
                        ReferenceTransform = ReferenceRightArm;
                        PublishedTransform = RightGripper;
                        PublishedTransform.transform.parent = ReferenceTransform.transform; //this line of code defines the parent of the child, in this way we realise the transformation of frames
                    }
                    else
                    {
                        ReferenceTransform = ReferenceLeftArm;
                        PublishedTransform = LeftGripper;
                        PublishedTransform.transform.parent = ReferenceTransform.transform;
                    }
                    PreviousArm = Arm;
                }
            if (Arm != 0)
            {
                UpdateMessage();
            }
            
                
            
        }

        

        private void UpdateMessage()
        {
            message.header.Update();

            // Translation
            positionUnity = PublishedTransform.localPosition; 
            positionUnity2Ros.x = positionUnity.x; // the relative position of unity is translated in the reference of ros
            positionUnity2Ros.y = positionUnity.z; // the relative position of unity is translated in the reference of ros
            positionUnity2Ros.z = positionUnity.y; // the relative position of unity is translated in the reference of ros
            // Rotation
            rotationUnity = PublishedTransform.rotation; 
            rotationRos = Unity2RosRotation(rotationUnity,ReferenceTransform.rotation); // the rotation of unity is translated in the reference of ros

            //now it is possible to publish the message
            GetGeometryPoint(positionUnity2Ros, message.pose.position);
            //GetGeometryQuaternion(rotationTF, message.pose.orientation);
            GetGeometryQuaternion(rotationRos, message.pose.orientation);
            Publish(message);
        }


        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
        {
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
        }

        public Quaternion Unity2RosRotation(Quaternion rotationUnity, Quaternion ReferenceRotation)
        {
            rotationUnity2Ros = rotationUnity; 
            rotationRos = Quaternion.Inverse(ReferenceRotation) * rotationUnity2Ros; //this operation represents the relative rotation between two different frames, it must be realised after the "UpdateRotation"
            return UpdateRotation(rotationRos);
        }

        public Quaternion UpdateRotation(Quaternion quaternionPREV)
        {
            return new Quaternion(
                -quaternionPREV.x,
                 -quaternionPREV.z,
                 -quaternionPREV.y,
                 quaternionPREV.w
                );
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }

        


    }
}
