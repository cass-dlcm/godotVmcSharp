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
    public class VmcExtRootPos : VmcMessage
    {
        public string name { get; private set; }
        public Godot.Transform3D transform { get; private set;}

        public Godot.Vector3? scale { get; private set; }
        public Godot.Vector3? offset { get; private set; }

        public VmcExtRootPos(godotOscSharp.OscMessage m) : base(m.Address)
        {
            switch (m.Data.Count)
            {
                case 8:
                    Transform8(m.Data);
                    break;
                case 14:
                    Transform14(m.Data);
                    break;
                default:
                    GD.Print($"Invalid number of arguments for {base.addr}. Expected 8 or 14, received {m.Data.Count}.");
                    break;
            }
        }

        private void Transform8(List<godotOscSharp.OscArgument> data)
        {
            if (data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "name", 's', data[0].Type));
                return;
            }
            if (data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "p.x", 'f', data[1].Type));
                return;
            }
            if (data[2].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "p.y", 'f', data[2].Type));
                return;
            }
            if (data[3].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "p.z", 'f', data[3].Type));
                return;
            }
            if (data[4].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "q.x", 'f', data[4].Type));
                return;
            }
            if (data[5].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "q.y", 'f', data[5].Type));
                return;
            }
            if (data[6].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "q.z", 'f', data[6].Type));
                return;
            }
            if (data[7].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "q.w", 'f', data[7].Type));
                return;
            }
            name = (string)data[0].Value;
            transform = new Godot.Transform3D(new Godot.Basis(new Godot.Quaternion((float)data[4].Value, (float)data[5].Value, (float)data[6].Value, (float)data[7].Value)), new Godot.Vector3((float)data[1].Value, (float)data[2].Value, (float)data[3].Value));
        }

        private void Transform14(List<godotOscSharp.OscArgument> data)
        {
            Transform8(data);
            if (transform == null)
            {
                return;
            }
            if (data[8].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "s.x", 'f', data[8].Type));
                return;
            }
            if (data[9].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "s.y", 'f', data[9].Type));
                return;
            }
            if (data[10].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "s.z", 'f', data[10].Type));
                return;
            }
            if (data[11].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "o.x", 'f', data[11].Type));
                return;
            }
            if (data[12].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "o.y", 'f', data[12].Type));
                return;
            }
            if (data[13].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "o.z", 'f', data[13].Type));
                return;
            }
            scale = new Godot.Vector3((float)data[8].Value, (float)data[9].Value, (float)data[10].Value);
            offset = new Godot.Vector3((float)data[11].Value, (float)data[12].Value, (float)data[13].Value);
        }
    }
}