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
        public int Enable { get; }
        public int Port { get; }
        public string IpAddress { get; }

        public VmcExtRcv(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count > 3 || m.Data.Count < 2)
            {
                GD.Print($"Invalid number of arguments for {Addr}. Expecting 2 or 3, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "enable", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "port", 'i', m.Data[0].Type));
                return;
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 1)
            {
                GD.Print($"Invalid value for \"enable\" argument of {Addr}. Expected 0 or 1, received {(int)m.Data[0].Value}.");
                return;
            }
            if ((int)m.Data[1].Value < 0 || (int)m.Data[1].Value > 65535)
            {
                GD.Print($"Invalid value for \"port\" argument of {Addr}. Expected 0-65535, received {(int)m.Data[1].Value}.");
                return;
            }
            Enable = (int)m.Data[0].Value;
            Port = (int)m.Data[1].Value;
            if (m.Data.Count != 3)
            {
                IpAddress = "";
                return;
            }
            if (m.Data[2].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "IpAddress", 's', m.Data[2].Type));
                return;
            }
            IpAddress = (string)m.Data[2].Value;
        }

        public VmcExtRcv(int enable, int port) : base(new OscAddress("/VMC/Ext/Rcv"))
        {
            if (enable < 0 || enable > 1)
            {
                GD.Print($"Invalid value for \"enable\" argument of {Addr}. Expected 0 or 1, received {enable}.");
                return;
            }
            if (port < 0 || port > 65535)
            {
                GD.Print($"Invalid value for \"port\" argument of {Addr}. Expected 0-65535, received {port}.");
                return;
            }
            Enable = enable;
            Port = port;
            IpAddress = "";
        }

        public VmcExtRcv(int enable, int port, string ipAddress) : base(new OscAddress("/VMC/Ext/Rcv"))
        {
            if (enable < 0 || enable > 1)
            {
                GD.Print($"Invalid value for \"enable\" argument of {Addr}. Expected 0 or 1, received {enable}.");
                return;
            }
            if (port < 0 || port > 65535)
            {
                GD.Print($"Invalid value for \"port\" argument of {Addr}. Expected 0-65535, received {port}.");
                return;
            }
            Enable = enable;
            Port = port;
            IpAddress = ipAddress;
        }

        public new OscMessage ToMessage()
        {
            if (IpAddress == "")
            {
                return new OscMessage(Addr, new List<OscArgument>{
                    new OscArgument(Enable, 'i'),
                    new OscArgument(Port, 'i'),
                });
            }
            return new OscMessage(Addr, new List<OscArgument>{
                new OscArgument(Enable, 'i'),
                new OscArgument(Port, 'i'),
                new OscArgument(IpAddress, 's'),
            });
        }
    }
}