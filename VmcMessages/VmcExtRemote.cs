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
        public readonly string Service;
        public readonly string Json;

        public VmcExtRemote(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 2)
            {
                GD.Print($"Invalid number of arguments for {Addr} message. Expected 2 but received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "service", 's', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "json", 's', m.Data[1].Type));
                return;
            }
            Service = (string)m.Data[0].Value;
            Json = (string)m.Data[1].Value;
        }

        public VmcExtRemote(string service, string json) : base(new OscAddress("/VMC/Ext/Remote"))
        {
            Service = service;
            Json = json;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{
                new OscArgument(Service, 's'),
                new OscArgument(Json, 's')
            });
        }
    }
}