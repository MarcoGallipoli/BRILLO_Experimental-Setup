using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PoseSubscriberGlass : UnitySubscriber<MessageTypes.Geometry.Pose>
    {
        // the two reference to manipulate
        public Transform PublishedTransform;
        // this variable represents the frame from which it is established the position
        public Transform ReferenceTransform;

        // The message received by ROS is inside these two variables
        private Vector3 positionRos;
        private Quaternion rotationRos;

        private Vector3 positionUnity;
        private Quaternion rotationUnity;
        private Quaternion rotationRos2Unity;



        private bool isMessageReceived;

        protected override void Start()
        {
            PublishedTransform.transform.parent = ReferenceTransform.transform; //this line of code defines the parent of the child, in this way we realise the transformation of frames
            base.Start();
        }

        private void FixedUpdate()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        //protected override void ReceiveMessage(MessageTypes.Geometry.PoseStamped message)
        //Attualmente, ROS non legge il tipo di messaggio STAMPED, ma solo POSE. Quindi modifichiamo le parti di PoseStamped in Pose
        protected override void ReceiveMessage(MessageTypes.Geometry.Pose message)
        {
            positionRos = GetPosition(message);
            //rotationRos = GetRotation(message);
            
            isMessageReceived = true;
        }
        
        
        private void ProcessMessage()
        {   
            //translation
            positionUnity = positionRos.Ros2Unity(); //Ros2Unity changes the coordinates od the vector3 ( xUnity = -yRos, yUnity = zRos , zUnity = xRos )
            //The height of the glass is the Y variable and it is fixed
            positionUnity.y = PublishedTransform.localPosition.y;
            PublishedTransform.localPosition = positionUnity;

            //PublishedTransform.localPosition = positionUnity;
            //rotation
            //rotationUnity = Ros2UnityRotation(rotationRos, ReferenceTransform.rotation);
            //PublishedTransform.rotation = rotationUnity;

        }

        private Quaternion Ros2UnityRotation(Quaternion rotationRos, Quaternion ReferenceRotation)
        {
            rotationRos2Unity = UpdateRotation(rotationRos);
            rotationUnity = ReferenceRotation * rotationRos2Unity; //this operation represents the relative rotation between two different frames
            return rotationUnity;
        }

        private Quaternion UpdateRotation(Quaternion quaternionPREV)
        {
            return new Quaternion(
                 -quaternionPREV.x,
                 -quaternionPREV.z,
                 -quaternionPREV.y,
                  quaternionPREV.w
                );
        }

        private Vector3 GetPosition(MessageTypes.Geometry.Pose message)
        {
            return new Vector3(
                //inoltre, dobbiamo cambiare message.pose.position in message.position (sempre per la questione del Pose al posto di PoseStamped)
               // (float)message.pose.position.x,
               // (float)message.pose.position.y,
               // (float)message.pose.position.z);
                 (float)message.position.x,
                 (float)message.position.y,
                 (float)message.position.z);
        }

        private Quaternion GetRotation(MessageTypes.Geometry.Pose message)
        {
            return new Quaternion(
                //(float)message.pose.orientation.x,
                //(float)message.pose.orientation.y,
                //(float)message.pose.orientation.z,
                //(float)message.pose.orientation.w);
                (float)message.orientation.x,
                (float)message.orientation.y,
                (float)message.orientation.z,
                (float)message.orientation.w
                );
        }
    }

   
}