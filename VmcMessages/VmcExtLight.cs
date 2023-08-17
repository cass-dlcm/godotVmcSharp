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
using godotOscSharp;
using System.Collections.Generic;

namespace godotVmcSharp
{
    public class VmcExtLight : VmcMessage
    {
        public string Name { get; }
        public Transform3D Transform { get; }
        public Color Color { get; }

        public VmcExtLight(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 12)
            {
                GD.Print($"Invalid number of arguments for {base.Addr}. Expected 12, received {m.Data.Count}.");
                return;
            }
            if (m.Data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "name", 's', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "p.x", 'f', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "p.y", 'f', m.Data[2].Type));
                return;
            }
            if (m.Data[3].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "p.z", 'f', m.Data[3].Type));
                return;
            }
            if (m.Data[4].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "q.x", 'f', m.Data[4].Type));
                return;
            }
            if (m.Data[5].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "q.y", 'f', m.Data[5].Type));
                return;
            }
            if (m.Data[6].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "q.z", 'f', m.Data[6].Type));
                return;
            }
            if (m.Data[7].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "q.w", 'f', m.Data[7].Type));
                return;
            }
            if (m.Data[8].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "color.red", 'f', m.Data[8].Type));
                return;
            }
            if (m.Data[9].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "color.blue", 'f', m.Data[9].Type));
                return;
            }
            if (m.Data[10].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "color.green", 'f', m.Data[10].Type));
                return;
            }
            if (m.Data[11].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "color.alpha", 'f', m.Data[11].Type));
                return;
            }
            Name = (string)m.Data[0].Value;
            Transform = new Transform3D(new Basis(new Quaternion((float)m.Data[4].Value, (float)m.Data[5].Value, (float)m.Data[6].Value, (float)m.Data[7].Value)), new Vector3((float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value));
            Color = new Color((float)m.Data[8].Value, (float)m.Data[9].Value, (float)m.Data[10].Value, (float)m.Data[11].Value);
        }

        public VmcExtLight(string name, Transform3D transform, Color color) : base(new OscAddress("/VMC/Ext/Light"))
        {
            Name = name;
            Transform = transform;
            Color = color;
        }

        public new OscMessage ToMessage()
        {
            var quat = Transform.Basis.GetRotationQuaternion();
            return new OscMessage(Addr, new List<OscArgument>{
                new OscArgument(Name, 's'),
                new OscArgument(Transform.Origin.X, 'f'),
                new OscArgument(Transform.Origin.Y, 'f'),
                new OscArgument(Transform.Origin.Z, 'f'),
                new OscArgument(quat.X, 'f'),
                new OscArgument(quat.Y, 'f'),
                new OscArgument(quat.Z, 'f'),
                new OscArgument(quat.W, 'f'),
                new OscArgument(Color.R, 'f'),
                new OscArgument(Color.G, 'f'),
                new OscArgument(Color.B, 'f'),
                new OscArgument(Color.A, 'f')
            });
        }
    }
}