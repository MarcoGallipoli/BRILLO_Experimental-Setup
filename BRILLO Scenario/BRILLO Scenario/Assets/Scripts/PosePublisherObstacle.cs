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
using System.Collections.Generic;
using System.Threading;

namespace RosSharp.RosBridgeClient
{
    public class PosePublisherObstacle : UnityPublisher<MessageTypes.Geometry.PoseArray>
    {
        public List<Transform> ObstacleTransform;
        // this variable represents the frame from which it is established the position
        public  Transform ReferenceTransform;
        private Transform SingleTransform;


        private Vector3 positionUnity;
        private Vector3 positionUnity2Ros;
        private Quaternion rotationUnity;

        private Quaternion rotationRos;
        private Quaternion rotationUnity2Ros;
        public string FrameId = "world"; // this name refers to the reference frame

        public float Interval = 1.0f; //seconds
        private float deltaTime = 0f;
        private float previousRealTime;


        private MessageTypes.Geometry.PoseArray message;
        private MessageTypes.Geometry.Pose posa;
        //private MessageTypes.Geometry.Point position;
        //private MessageTypes.Geometry.Quaternion orientation;

        protected override void Start()
        {
            int ObstacleLength = ObstacleTransform.Count;
            for (int i = 0; i < ObstacleLength; i++)
            {
                ObstacleTransform[i].transform.parent = ReferenceTransform.transform; //this line of code defines the parent of the child, in this way we realise the transformation of frames
            }
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            int ObstacleLength = ObstacleTransform.Count;
            message = new MessageTypes.Geometry.PoseArray
            {
                header = new MessageTypes.Std.Header { frame_id = FrameId },
                poses = new MessageTypes.Geometry.Pose[ObstacleLength] 
            };
        }

        private void FixedUpdate()
        {
            int ObstacleLength = ObstacleTransform.Count;
            for (int i = 0; i < ObstacleLength; i++)
            {
                ObstacleTransform[i].transform.parent = ReferenceTransform.transform; //this line of code defines the parent of the child, in this way we realise the transformation of frames
            }
            deltaTime = Time.realtimeSinceStartup - previousRealTime;

            if (deltaTime > Interval)
            {
                UpdateMessage();
                deltaTime = 0;
                previousRealTime = Time.realtimeSinceStartup;
            }


        }



        private void UpdateMessage()
        {
            message.header.Update();
            for (int i = 0; i < ObstacleTransform.Count; i++)
            {
                UpdatePose(i);
            }
            Publish(message);
        }

        public void UpdatePose(int k)
        {
            posa = new MessageTypes.Geometry.Pose();

            SingleTransform = ObstacleTransform[k];
            // Translation
            positionUnity = ObstacleTransform[k].localPosition;
            positionUnity2Ros.x = positionUnity.x; // the relative position of unity is translated in the reference of ros
            positionUnity2Ros.y = positionUnity.z; // the relative position of unity is translated in the reference of ros
            positionUnity2Ros.z = positionUnity.y; // the relative position of unity is translated in the reference of ros
            // Rotation
            rotationUnity = SingleTransform.rotation;
            rotationRos = Unity2RosRotation(rotationUnity, ReferenceTransform.rotation); // the rotation of unity is translated in the reference of ros
            
            
                ////now it is possible to publish the message
            GetGeometryPoint(positionUnity2Ros, posa.position);
                //position.x = positionUnity2Ros.x;
            //Debug.Log(position.x);

            GetGeometryQuaternion(rotationRos, posa.orientation);
            message.poses[k] = posa;


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
