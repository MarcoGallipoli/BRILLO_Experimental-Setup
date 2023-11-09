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
using System.ComponentModel;

namespace RosSharp.RosBridgeClient
{
    public class ObstacleSizePublisher : UnityPublisher<MessageTypes.Geometry.PoseArray>
    {
        public List<Transform> ObstacleTransform;

        private Transform SingleTransform;

        private Quaternion falserotation;
        private Vector3 sizeUnity2Ros;
        private Vector3 sizeUnity;

        public float Interval = 1.0f; //seconds
        private float deltaTime = 0f;
        private float previousRealTime;

        public string FrameId = "world"; // this name refers to the reference frame

        private MessageTypes.Geometry.PoseArray message;
        private MessageTypes.Geometry.Pose posa;



        protected override void Start()
        {

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
            sizeUnity = SingleTransform.localScale;
            sizeUnity2Ros.x = sizeUnity.x; // the relative position of unity is translated in the reference of ros
            sizeUnity2Ros.y = sizeUnity.z; // the relative position of unity is translated in the reference of ros
            sizeUnity2Ros.z = sizeUnity.y; // the relative position of unity is translated in the reference of ros
            
                ////now it is possible to publish the message
            GetGeometryPoint(sizeUnity2Ros, posa.position);
            falserotation.x = 0;
            falserotation.y = 0;
            falserotation.z = 0;
            falserotation.w = 1;
            
            GetGeometryQuaternion(falserotation, posa.orientation);
            message.poses[k] = posa;


        }
        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
        {
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x = 0;
            geometryQuaternion.y = 0;
            geometryQuaternion.z = 0;
            geometryQuaternion.w = 1;
        }

        
    }
}
