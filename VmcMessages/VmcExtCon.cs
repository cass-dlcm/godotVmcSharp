namespace godotVmcSharp
{
    public class VmcExtCon : VmcMessage
    {
        public int active { get; }
        public string name { get; }
        public int isLeft { get; }
        public int isTouch { get; }
        public int isAxis { get; }
        public Godot.Vector3 axis { get; }
        
        public VmcExtCon(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 8)
            {
                GD.Print($"Invalid number of arguments for /VMC/Ext/Con. Expected 8, received {m.Data.Count}.");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "active", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "name", 's', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsLeft", 'i', m.Data[2].Type));
            }
            if (m.Data[3].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsTouch", 'i', m.Data[3].Type));
            }
            if (m.Data[4].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsAxis", 'i', m.Data[4].Type));
            }
            if (m.Data[5].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.x", 'f', m.Data[5].Type));
            }
            if (m.Data[6].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.y", 'f', m.Data[6].Type));
            }
            if (m.Data[7].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.z", 'f', m.Data[7].Type));
            }
            if ((int)m.Data[0] < 0 || (int)m.Data[0] > 2)
            {
                GD.Print($"Invalid value for \"active\" 'i' argument of /VMC/Ext/Con. Expected 0-2, received {(int)m.Data[0].Value}");
                return;
            }
            active = (int)m.Data[0];
            name = (string)m.Data[1];
            isLeft = (int)m.Data[2];
            isTouch = (int)m.Data[3];
            isAxis = (int)m.Data[4];
            axis = new Godot.Vector3((float)m.Data[5], (float)m.Data[6], (float)m.Data[7]);
        }
    }
}