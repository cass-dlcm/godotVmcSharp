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
    public class VmcExtKey : VmcMessage
    {
        public int active { get; }
        public string name { get; }
        public int keycode { get; }
        public VmcExtKey(godotOscSharp.OscMessage m) : base(m.Address)
        {
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
                GD.Print(InvalidArgumentType.GetErrorString(addr, "keycode", 'i', m.Data[2].Type));
                return;
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 1)
            {
                GD.Print($"Invalid value for \"active\" 'i' argument of {addr}. Expected 0 or 1, received {(int)m.Data[0].Value}");
                return;
            }
            active = (int)m.Data[0].Value;
            name = (string)m.Data[1].Value;
            keycode = (int)m.Data[2].Value;
        }

        public VmcExtKey(int _active, string _name, int _keycode) : base(new godotOscSharp.Address("/VMC/Ext/Key"))
        {
            if (_active < 0 || _active > 1)
            {
                GD.Print($"Invalid value for \"active\" 'i' argument of {addr}. Expected 0 or 1, received {_active}");
                return;
            }
            active = _active;
            name = _name;
            keycode = _keycode;
        }

        public godotOscSharp.OscMessage ToMessage()
        {
            return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                new godotOscSharp.OscArgument(active, 'i'),
                new godotOscSharp.OscArgument(name, 's'),
                new godotOscSharp.OscArgument(keycode, 'i')
            });
        }
    }
}