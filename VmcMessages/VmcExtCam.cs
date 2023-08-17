/*
    godotVmcSharp
    Copyright (C) 2023  Cassandra de la Cruz-Munoz

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as published
    by the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
    */

using Godot;

namespace godotVmcSharp
{
    class VmcExtCam : VmcMessage
    {

        public string Name { get; }
        public Transform3D Transform { get; }
        public float Fov { get; }

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
            Name = (string)m.Data[0].Value;
            Transform = new Godot.Transform3D(new Godot.Basis(new Godot.Quaternion((float)m.Data[4].Value, (float)m.Data[5].Value, (float)m.Data[6].Value, (float)m.Data[7].Value)), new Godot.Vector3((float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value));
            Fov = (float)m.Data[8].Value;
        }

        public VmcExtCam(string name, Transform3D transform, float fov) : base(new godotOscSharp.Address("/VMC/Ext/Cam"))
        {
            Name = name;
            Transform = transform;
            Fov = fov;
        }

        public new godotOscSharp.OscMessage ToMessage()
        {
            var quat = Transform.Basis.GetRotationQuaternion();
            return new godotOscSharp.OscMessage(addr, new System.Collections.Generic.List<godotOscSharp.OscArgument>{
                new godotOscSharp.OscArgument(Name, 's'),
                new godotOscSharp.OscArgument(Transform.Origin.X, 'f'),
                new godotOscSharp.OscArgument(Transform.Origin.Y, 'f'),
                new godotOscSharp.OscArgument(Transform.Origin.Z, 'f'),
                new godotOscSharp.OscArgument(quat.X, 'f'),
                new godotOscSharp.OscArgument(quat.Y, 'f'),
                new godotOscSharp.OscArgument(quat.Z, 'f'),
                new godotOscSharp.OscArgument(quat.W, 'f'),
                new godotOscSharp.OscArgument(Fov, 'f')
            });
        }
    }
}
