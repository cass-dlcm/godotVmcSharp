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
    public class VmcExtSettingColor : VmcMessage
    {
        public Godot.Color color { get; }

        public VmcExtSettingColor(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 4)
            {
                GD.Print($"Invalid number of arguments for {addr}. Expecting 4, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "r", 'f', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "g", 'f', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "b", 'f', m.Data[2].Type));
                return;
            }
            if (m.Data[3].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "a", 'f', m.Data[3].Type));
                return;
            }
            color = new Godot.Color((float)m.Data[0].Value, (float)m.Data[1].Value, (float)m.Data[2].Value, (float)m.Data[3].Value);
        }

        public VmcExtSettingColor(Godot.Color _color) : base(new godotOscSharp.Address("/VMC/Ext/Setting/Color"))
        {
            color = _color;
        }

        public godotOscSharp.OscMessage ToMessage()
        {
            return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                new godotOscSharp.OscArgument(color.R, 'f'),
                new godotOscSharp.OscArgument(color.G, 'f'),
                new godotOscSharp.OscArgument(color.B, 'f'),
                new godotOscSharp.OscArgument(color.A, 'f')
            });
        }
    }
}