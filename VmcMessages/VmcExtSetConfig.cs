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
    public class VmcExtSetConfig : VmcMessage
    {
        public readonly string Path;

        public VmcExtSetConfig(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 1)
            {
                GD.Print($"Invalid number of arguments for {Addr}. Expecting 1, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "path", 's', m.Data[0].Type));
                return;
            }
            Path = (string)m.Data[0].Value;
        }

        public VmcExtSetConfig(string path) : base(new OscAddress("/VMC/Ext/Set/Config"))
        {
            Path = path;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{new OscArgument(Path, 's')});
        }
    }
}