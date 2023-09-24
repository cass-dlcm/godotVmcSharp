using System.Collections.Generic;
using Godot;

namespace godotVmcSharp
{
    class DirectionalLightReceiver
    {
        readonly Dictionary<string, DirectionalLight3D> lights;
        public DirectionalLightReceiver()
        {
            this.lights = new Dictionary<string, DirectionalLight3D>{};
        }
        public void ProcessMessage(VmcExtLight message)
        {
            if (!this.lights.ContainsKey(message.Name))
            {
                this.lights.Add(message.Name, new DirectionalLight3D());
            }
            this.lights[message.Name].Transform = message.Transform;
            this.lights[message.Name].LightColor = message.Color;
        }
    }
}