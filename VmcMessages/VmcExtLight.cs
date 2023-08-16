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
        public string name { get; }
        public Godot.Transform3D transform { get; }
        public Godot.Color color { get; }

        public VmcExtLight(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 12)
            {
                GD.Print($"Invalid number of arguments for {base.addr}. Expected 12, received {m.Data.Count}.");
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
                GD.Print(InvalidArgumentType.GetErrorString(addr, "color.red", 'f', m.Data[8].Type));
                return;
            }
            if (m.Data[9].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "color.blue", 'f', m.Data[9].Type));
                return;
            }
            if (m.Data[10].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "color.green", 'f', m.Data[10].Type));
                return;
            }
            if (m.Data[11].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "color.alpha", 'f', m.Data[11].Type));
                return;
            }
            name = (string)m.Data[0].Value;
            transform = new Godot.Transform3D(new Godot.Basis(new Godot.Quaternion((float)m.Data[4].Value, (float)m.Data[5].Value, (float)m.Data[6].Value, (float)m.Data[7].Value)), new Godot.Vector3((float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value));
            color = new Godot.Color((float)m.Data[8].Value, (float)m.Data[9].Value, (float)m.Data[10].Value, (float)m.Data[11].Value);
        }

        public VmcExtLight(string _name, Godot.Transform3D _transform, Godot.Color _color) : base(new godotOscSharp.Address("/VMC/Ext/Light"))
        {
            name = _name;
            transform = _transform;
            color = _color;
        }

        public godotOscSharp.OscMessage ToMessage()
        {
            var quat = transform.Basis.GetRotationQuaternion();
            return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                new godotOscSharp.OscArgument(name, 's'),
                new godotOscSharp.OscArgument(transform.Origin.X, 'f'),
                new godotOscSharp.OscArgument(transform.Origin.Y, 'f'),
                new godotOscSharp.OscArgument(transform.Origin.Z, 'f'),
                new godotOscSharp.OscArgument(quat.X, 'f'),
                new godotOscSharp.OscArgument(quat.Y, 'f'),
                new godotOscSharp.OscArgument(quat.Z, 'f'),
                new godotOscSharp.OscArgument(quat.W, 'f'),
                new godotOscSharp.OscArgument(color.R, 'f'),
                new godotOscSharp.OscArgument(color.G, 'f'),
                new godotOscSharp.OscArgument(color.B, 'f'),
                new godotOscSharp.OscArgument(color.A, 'f')
            });
        }
    }
}