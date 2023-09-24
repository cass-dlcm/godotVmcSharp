using System.Collections.Generic;
using Godot;

namespace godotVmcSharp
{
    class DeviceReceiver: Node3D
    {
        readonly Dictionary<string, Node3D> devices;
        public DeviceReceiver()
        {
            this.devices = new Dictionary<string, Node3D>{};
        }
        public void ProcessMessage(VmcExtDevicePos message)
        {
            if (!this.devices.ContainsKey(message.Serial))
            {
                this.devices.Add(message.Serial, new Node3D());
            }
            this.devices[message.Serial].Transform = message.Transform;
        }
    }
}