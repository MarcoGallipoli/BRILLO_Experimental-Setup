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
    public class ScalePublisherObstacle : UnityPublisher<MessageTypes.Geometry.Vector3>
    {
        public GameObject Obstacle;

        public float Interval = 1.0f; //seconds
        private float deltaTime = 0f;
        private float previousRealTime;

        private Vector3 ScaleUnity;
        private Vector3 ScaleROS;

        private Renderer Rendering;

        private MessageTypes.Geometry.Vector3 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
            Rendering = Obstacle.GetComponent<MeshRenderer>();
            Rendering.enabled = false;
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Vector3
            {
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

            ScaleUnity = Obstacle.transform.localScale;
            ScaleROS.x = ScaleUnity.x; // the relative coordinate of unity is translated in the reference of ros
            ScaleROS.y = ScaleUnity.z; // the relative coordinate of unity is translated in the reference of ros
            ScaleROS.z = ScaleUnity.y; // the relative coordinate of unity is translated in the reference of ros
            //now it is possible to publish the message
            GetGeometryPoint(ScaleROS, message);

            Publish(message);
        }


        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Vector3 Vector)
        {
            Vector.x = position.x;
            Vector.y = position.y;
            Vector.z = position.z;
        }

   
    }
}
