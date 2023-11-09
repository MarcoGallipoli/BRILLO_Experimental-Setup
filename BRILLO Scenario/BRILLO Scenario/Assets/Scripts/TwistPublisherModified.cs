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

using UnityEngine;
using Valve.VR;

namespace RosSharp.RosBridgeClient
{
    public class TwistPublisherModified : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        
        private Transform ReferenceTransform;
        public Transform ReferenceRightArm;
        public Transform ReferenceLeftArm;

        private Transform PublishedTransform;
        public Transform RightHand;
        public Transform LeftHand;

        private MessageTypes.Geometry.Twist message;
        private float previousRealTime;
        private Vector3 previousPosition = Vector3.zero;
        private Quaternion previousRotation = Quaternion.identity;

        public GameObject ChosenArmObject;
        private ChosenArm ChosenArmScript;
        private int Arm = 0;
        private int PreviousArm = 0;

        private Vector3 linearVelocity;
        private Vector3 positionUnity;
        private Vector3 positionUnity2Ros;
        private Vector3 velocityUnity2Ros;
        private Vector3 EulerAnglesUnity;
        private Vector3 EulerAnglesRos;
        private Vector3 AngularVelocity;
        private Quaternion TransitionQuaternionUnity;

        public string FrameId = "world"; // this name refers to the reference frame


        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void Update()
        {
            ChosenArmScript = ChosenArmObject.GetComponent<ChosenArm>();
            Arm = ChosenArmScript.ChosenArmNumber;

            if (Arm != PreviousArm)
            {
                if (Arm == 1)
                {
                    ReferenceTransform = ReferenceRightArm;
                    PublishedTransform = RightHand;
                    PublishedTransform.transform.parent = ReferenceTransform; //this line of code defines the parent of the child, in this way we realise the transformation of frames
                }
                else
                {
                    ReferenceTransform = ReferenceLeftArm;
                    PublishedTransform = LeftHand;
                    PublishedTransform.transform.parent = ReferenceTransform.transform;
                }
                PreviousArm = Arm;
            }
            if (Arm != 0)
                UpdateMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist();
            message.linear = new MessageTypes.Geometry.Vector3();
            message.angular = new MessageTypes.Geometry.Vector3();
        }
        private void UpdateMessage()
        {
            float deltaTime = Time.realtimeSinceStartup - previousRealTime;

            positionUnity = PublishedTransform.localPosition;
            

            linearVelocity = (positionUnity - previousPosition) / deltaTime;
            // TransitionQuaternionUnity = (PublishedTransform.localRotation * Quaternion.Inverse(previousRotation));
            //TransitionQuaternionUnity.ToAngleAxis(out float angle, out Vector3 axis);
            //w = (angle / deltaTime) * axis;
            //EulerAnglesUnity = TransitionQuaternionUnity.eulerAngles / deltaTime;

            //EulerAnglesUnity = PublishedTransform.localRotation.eulerAngles;

            // Translation
            velocityUnity2Ros.x = linearVelocity.x; // the relative position of unity is translated in the reference of ros
            velocityUnity2Ros.y = linearVelocity.z; // the relative position of unity is translated in the reference of ros
            velocityUnity2Ros.z = linearVelocity.y; // the relative position of unity is translated in the reference of ros
            // Debug.Log(velocityUnity2Ros);
            // Rotation
            EulerAnglesRos.x = -EulerAnglesUnity.x;
            EulerAnglesRos.y = -EulerAnglesUnity.y;
            EulerAnglesRos.z = EulerAnglesUnity.z;
            //Debug.Log( w);
            //Debug.Log("In Unity" + EulerAnglesUnity);
            //Debug.Log("In ROS" + EulerAnglesRos);


            message.linear = GetGeometryVector3(velocityUnity2Ros); ;
            message.angular = GetGeometryVector3(EulerAnglesRos);

            Publish(message);

            previousRealTime = Time.realtimeSinceStartup;
            previousPosition = positionUnity;
            //previousRotation = PublishedTransform.localRotation;
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();
            geometryVector3.x = vector3.x;
            geometryVector3.y = vector3.y;
            geometryVector3.z = vector3.z;

            return geometryVector3;
        }

        //public Quaternion Unity2RosRotation(Quaternion rotationUnity, Quaternion ReferenceRotation)
        //{
        //    rotationUnity2Ros = rotationUnity; 
        //    rotationRos = Quaternion.Inverse(ReferenceRotation) * rotationUnity2Ros; //this operation represents the relative rotation between two different frames, it must be realised after the "UpdateRotation"
        //    return UpdateRotation(rotationRos);
        //}

        //public Quaternion UpdateRotation(Quaternion quaternionPREV)
        //{
        //    return new Quaternion(
        //        -quaternionPREV.x,
        //         -quaternionPREV.z,
        //         -quaternionPREV.y,
        //         quaternionPREV.w
        //        );
        //}
    }
}
