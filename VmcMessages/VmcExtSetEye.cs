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
        public int enable { get; }
        public Godot.Vector3 position { get; }

        public VmcExtSetEye(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 4)
            {
                GD.Print($"Invalid number of arguments for {addr}. Expecting 4, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "enable", 'i', m.Data[0].Type));
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
            enable = (int)m.Data[0].Value;
            position = new Godot.Vector3((float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value);
        }

        public VmcExtSetEye(int _enable, Godot.Vector3 _position) : base(new godotOscSharp.Address("/VMC/Ext/Set/Eye"))
        {
            enable = _enable;
            position = _position;
        }
    }
}