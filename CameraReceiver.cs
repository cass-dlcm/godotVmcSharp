using Godot;

namespace godotVmcSharp
{
    class CameraReceiver
    {
        readonly Camera3D camera;

        public CameraReceiver(Camera3D camera)
        {
            this.camera = camera;
        }

        public void ProcessMessage(VmcExtCam message) {
            this.camera.Fov = message.Fov;
            this.camera.Transform = message.Transform;
        }
    }
}