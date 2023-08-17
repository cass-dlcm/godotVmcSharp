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
    public class VmcExtRemote : VmcMessage
    {
        public string service { get; }
        public string json { get; }

        public VmcExtRemote(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 2)
            {
                GD.Print($"Invalid number of arguments for {addr} message. Expected 2 but received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "service", 's', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "json", 's', m.Data[1].Type));
                return;
            }
            service = (string)m.Data[0].Value;
            json = (string)m.Data[1].Value;
        }

        public VmcExtRemote(string _service, string _json) : base(new godotOscSharp.Address("/VMC/Ext/Remote"))
        {
            service = _service;
            json = _json;
        }
    }
}