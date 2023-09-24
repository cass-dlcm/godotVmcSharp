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

namespace godotVmcSharp
{
    public class VmcExtSetEye : VmcMessage
    {
        public readonly int Enable;
        public readonly Vector3 Position;

        public VmcExtSetEye(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 4)
            {
                GD.Print($"Invalid number of arguments for {Addr}. Expecting 4, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "enable", 'i', m.Data[0].Type));
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
            Enable = (int)m.Data[0].Value;
            Position = new Vector3((float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value);
        }

        public VmcExtSetEye(int enable, Vector3 position) : base(new OscAddress("/VMC/Ext/Set/Eye"))
        {
            Enable = enable;
            Position = position;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{
                new OscArgument(Enable, 'i'),
                new OscArgument(Position.X, 'f'),
                new OscArgument(Position.Y, 'f'),
                new OscArgument(Position.Z, 'f'),
            });
        }
    }
}