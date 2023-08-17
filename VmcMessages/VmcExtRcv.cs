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
    public class VmcExtRcv : VmcMessage
    {
        public int enable { get; }
        public int port { get; }
        public string? ipAddress { get; }

        public VmcExtRcv(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count > 3 || m.Data.Count < 2)
            {
                GD.Print($"Invalid number of arguments for {addr}. Expecting 2 or 3, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "enable", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "port", 'i', m.Data[0].Type));
                return;
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 1)
            {
                GD.Print($"Invalid value for \"enable\" argument of {addr}. Expected 0 or 1, received {(int)m.Data[0].Value}.");
                return;
            }
            if ((int)m.Data[1].Value < 0 || (int)m.Data[1].Value > 65535)
            {
                GD.Print($"Invalid value for \"port\" argument of {addr}. Expected 0-65535, received {(int)m.Data[1].Value}.");
                return;
            }
            enable = (int)m.Data[0].Value;
            port = (int)m.Data[1].Value;
            if (m.Data.Count != 3)
            {
                return;
            }
            if (m.Data[2].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IpAddress", 's', m.Data[2].Type));
                return;
            }
            ipAddress = (string)m.Data[2].Value;
        }

        public VmcExtRcv(int _enable, int _port) : base(new godotOscSharp.Address("/VMC/Ext/Rcv"))
        {
            if (_enable < 0 || _enable > 1)
            {
                GD.Print($"Invalid value for \"enable\" argument of {addr}. Expected 0 or 1, received {_enable}.");
                return;
            }
            if (_port < 0 || _port > 65535)
            {
                GD.Print($"Invalid value for \"port\" argument of {addr}. Expected 0-65535, received {_port}.");
                return;
            }
            enable = _enable;
            port = _port;
        }

        public VmcExtRcv(int _enable, int _port, string _ipAddress) : base(new godotOscSharp.Address("/VMC/Ext/Rcv"))
        {
            if (_enable < 0 || _enable > 1)
            {
                GD.Print($"Invalid value for \"enable\" argument of {addr}. Expected 0 or 1, received {_enable}.");
                return;
            }
            if (_port < 0 || _port > 65535)
            {
                GD.Print($"Invalid value for \"port\" argument of {addr}. Expected 0-65535, received {_port}.");
                return;
            }
            enable = _enable;
            port = _port;
            ipAddress = _ipAddress;
        }

        public godotOscSharp.OscMessage ToMessage()
        {
            if (ipAddress == null)
            {
                return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                    new godotOscSharp.OscArgument(enable, 'i'),
                    new godotOscSharp.OscArgument(port, 'i'),
                });
            }
            return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                new godotOscSharp.OscArgument(enable, 'i'),
                new godotOscSharp.OscArgument(port, 'i'),
                new godotOscSharp.OscArgument(ipAddress, 's'),
            });
        }
    }
}