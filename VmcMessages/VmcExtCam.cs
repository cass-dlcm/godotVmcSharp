using Godot;

namespace godotVmcSharp
{
    class VmcExtCam : VmcMessage
    {

        public string name { get; }
        public Godot.Transform3D transform { get; }
        public float fov { get; }

        public VmcExtCam(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 9)
            {
                GD.Print($"Invalid number of arguments for /VMC/Ext/Cam. Expected 9, received {m.Data.Count}.");
                return;
            }
            if (m.Data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "name", 's', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "p.x", 'f', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "p.y", 'f', m.Data[2].Type));
                return;
            }
            if (m.Data[3].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "p.z", 'f', m.Data[3].Type));
                return;
            }
            if (m.Data[4].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "q.x", 'f', m.Data[4].Type));
                return;
            }
            if (m.Data[5].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "q.y", 'f', m.Data[5].Type));
                return;
            }
            if (m.Data[6].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "q.z", 'f', m.Data[6].Type));
                return;
            }
            if (m.Data[7].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "q.w", 'f', m.Data[7].Type));
                return;
            }
            if (m.Data[8].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString("VMC/Ext/Cam", "fov", 'f', m.Data[8].Type));
                return;
            }
            name = (string)m.Data[0].Value;
            transform = new Godot.Transform3D(new Godot.Basis(new Godot.Quaternion((float)m.Data[4].Value, (float)m.Data[5].Value, (float)m.Data[6].Value, (float)m.Data[7].Value)), new Godot.Vector3((float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value));
            fov = (float)m.Data[8].Value;
        }
    }
}


