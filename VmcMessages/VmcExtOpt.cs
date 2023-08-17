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
    public class VmcExtOpt : VmcMessage
    {
        public string option { get; }

        public VmcExtOpt(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 1)
            {
                GD.Print($"Invalid number of arguments for {addr}. Expecting 1, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "option", 's', m.Data[0].Type));
                return;
            }
            option = (string)m.Data[0].Value;
        }

        public VmcExtOpt(string _option) : base(new godotOscSharp.Address("/VMC/Ext/Opt"))
        {
            option = _option;
        }

        public godotOscSharp.OscMessage ToMessage()
        {
            return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{new godotOscSharp.OscArgument(option, 's')});
        }
    }
}