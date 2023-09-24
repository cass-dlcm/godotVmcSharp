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
    public class VmcExtVrm : VmcMessage
    {
        public readonly string Path;
        public readonly string Title;
        public readonly string Hash;

        public VmcExtVrm(OscMessage m) : base(m.Address)
        {
            switch (m.Data.Count)
            {
                case 2:
                    if (m.Data[0].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(Addr, "path", 's', m.Data[0].Type));
                        return;
                    }
                    if (m.Data[1].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(Addr, "title", 's', m.Data[1].Type));
                        return;
                    }
                    Path = (string)m.Data[0].Value;
                    Title = (string)m.Data[1].Value;
                    Hash = "";
                    break;
                case 3:
                    if (m.Data[0].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(Addr, "path", 's', m.Data[0].Type));
                        return;
                    }
                    if (m.Data[1].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(Addr, "title", 's', m.Data[1].Type));
                        return;
                    }
                    if (m.Data[2].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(Addr, "hash", 's', m.Data[1].Type));
                        return;
                    }
                    Path = (string)m.Data[0].Value;
                    Title = (string)m.Data[1].Value;
                    Hash = (string)m.Data[2].Value;
                    break;
                default:
                    GD.Print($"Invalid number of arguments for {Addr} message. Expected 2 or 3 but received {m.Data.Count}");
                    return;
            }
        }

        public VmcExtVrm(string path, string title) : base(new OscAddress("/VMC/Ext/VRM"))
        {
            Path = path;
            Title = title;
            Hash = "";
        }

        public VmcExtVrm(string path, string title, string hash) : base(new OscAddress("/VMC/Ext/VRM"))
        {
            Path = path;
            Title = title;
            Hash = hash;
        }

        public new OscMessage ToMessage()
        {
            if (Hash == null)
            {
                return new OscMessage(Addr, new List<OscArgument>{
                    new OscArgument(Path, 's'),
                    new OscArgument(Title, 's')
                });
            }
            return new OscMessage(Addr, new List<OscArgument>{
                new OscArgument(Path, 's'),
                new OscArgument(Title, 's'),
                new OscArgument(Hash, 's')
            });
        }
    }
}