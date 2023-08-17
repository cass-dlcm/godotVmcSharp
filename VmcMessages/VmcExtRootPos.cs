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
using System.Threading;

namespace godotVmcSharp
{
    public class VmcExtRootPos : VmcMessage
    {
        public string Name { get; }
        public Transform3D Transform { get; }

        public Vector3? Scale { get; }
        public Vector3? Offset { get; }

        public VmcExtRootPos(OscMessage m) : base(m.Address)
        {
            switch (m.Data.Count)
            {
                case 8:
                    if (!Transform8(m.Data))
                    {
                        return;
                    }
                    Name = (string)m.Data[0].Value;
                    Transform = new Transform3D(new Basis(new Quaternion((float)m.Data[4].Value, (float)m.Data[5].Value, (float)m.Data[6].Value, (float)m.Data[7].Value)), new Vector3((float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value));
                    break;
                case 14:
                    if (!Transform14(m.Data))
                    {
                        return;
                    }
                    Name = (string)m.Data[0].Value;
                    Transform = new Transform3D(new Basis(new Quaternion((float)m.Data[4].Value, (float)m.Data[5].Value, (float)m.Data[6].Value, (float)m.Data[7].Value)), new Vector3((float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value));
                    Scale = new Vector3((float)m.Data[8].Value, (float)m.Data[9].Value, (float)m.Data[10].Value);
                    Offset = new Vector3((float)m.Data[11].Value, (float)m.Data[12].Value, (float)m.Data[13].Value);
                    break;
                default:
                    GD.Print($"Invalid number of arguments for {Addr}. Expected 8 or 14, received {m.Data.Count}.");
                    return;
            }
        }

        public VmcExtRootPos(string name, Transform3D transform) : base(new OscAddress("/VMC/Ext/Root/Pos"))
        {
            Name = name;
            Transform = transform;
        }

        public VmcExtRootPos(string name, Transform3D transform, Vector3 scale, Vector3 offset) : base(new OscAddress("/VMC/Ext/Root/Pos"))
        {
            Name = name;
            Transform = transform;
            Scale = scale;
            Offset = offset;
        }

        private bool Transform8(List<OscArgument> data)
        {
            if (data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "name", 's', data[0].Type));
                return false;
            }
            if (data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "p.x", 'f', data[1].Type));
                return false;
            }
            if (data[2].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "p.y", 'f', data[2].Type));
                return false;
            }
            if (data[3].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "p.z", 'f', data[3].Type));
                return false;
            }
            if (data[4].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "q.x", 'f', data[4].Type));
                return false;
            }
            if (data[5].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "q.y", 'f', data[5].Type));
                return false;
            }
            if (data[6].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "q.z", 'f', data[6].Type));
                return false;
            }
            if (data[7].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "q.w", 'f', data[7].Type));
                return false;
            }
            return true;
        }

        private bool Transform14(List<OscArgument> data)
        {
            if (!Transform8(data))
            {
                return false;
            }
            if (data[8].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "s.x", 'f', data[8].Type));
                return false;
            }
            if (data[9].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "s.y", 'f', data[9].Type));
                return false;
            }
            if (data[10].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "s.z", 'f', data[10].Type));
                return false;
            }
            if (data[11].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "o.x", 'f', data[11].Type));
                return false;
            }
            if (data[12].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "o.y", 'f', data[12].Type));
                return false;
            }
            if (data[13].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "o.z", 'f', data[13].Type));
                return false;
            }
            return true;
        }

        public new OscMessage ToMessage()
        {
            var quat = Transform.Basis.GetRotationQuaternion();
            if (!Scale.HasValue)
            {
                return new OscMessage(Addr, new List<OscArgument>{
                    new OscArgument(Name, 's'),
                    new OscArgument(Transform.Origin.X, 'f'),
                    new OscArgument(Transform.Origin.Y, 'f'),
                    new OscArgument(Transform.Origin.Z, 'f'),
                    new OscArgument(quat.X, 'f'),
                    new OscArgument(quat.Y, 'f'),
                    new OscArgument(quat.Z, 'f'),
                    new OscArgument(quat.W, 'f'),
                });
            }
            return new OscMessage(Addr, new List<OscArgument>{
                new OscArgument(Name, 's'),
                new OscArgument(Transform.Origin.X, 'f'),
                new OscArgument(Transform.Origin.Y, 'f'),
                new OscArgument(Transform.Origin.Z, 'f'),
                new OscArgument(quat.X, 'f'),
                new OscArgument(quat.Y, 'f'),
                new OscArgument(quat.Z, 'f'),
                new OscArgument(quat.W, 'f'),
                new OscArgument(Scale.Value.X, 'f'),
                new OscArgument(Scale.Value.Y, 'f'),
                new OscArgument(Scale.Value.Z, 'f'),
                new OscArgument(Offset.Value.X, 'f'),
                new OscArgument(Offset.Value.Y, 'f'),
                new OscArgument(Offset.Value.Z, 'f'),
            });
        }
    }
}