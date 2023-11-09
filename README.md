# BRILLO - Experimental Setup

The repository is composed of two main packages which can be customized. 
This work has been supported by the BRILLO project (Bartending Robot for Interactive Long-Lasting Operations) which has received funding from the PON I&C 2014-2020 MISE. 

## Running and Developing

### Local Work Station Requirements
- Windows 10/11
- HTC Vive Pro Set
- SteamVR
- At least Unity 2019.4.26f1
- Visual Studio 2022

### Remote Work Station Requirements
- ROS noetic
- USB camera

### Communication Requirements
- Ethernet connection

### How to setup the ROS package
1.

### How to setup the Unity package
1. Download the correct Unity version from the store
2. Download SteamVR
3. Setup the Virtual Control Room in SteamVR
4. Download the folder BRILLO Scenario
5. Open the project in the editor
6. Insert the correct IP in the scene objects referring to the ROS connection


### How to run the experimental setup
1. Open the project folder in ROS and launch the package
2. Once the package is running on ROS, you can launch the Unity project
3. Control in a simulated case the two KUKA's Lbr iiwa 14 R820 series, with Schunk EGL 90 PN grippers mounted on the end-effector.

### How to customize the experimental setup (Local Work Station)
1. Import the customized URDF file in the Unity editor
2. Choose the robotic system's characteristics (inertia, meshes, gravity)

### How to customize the experimental setup (Remote Work Station)
1. 
