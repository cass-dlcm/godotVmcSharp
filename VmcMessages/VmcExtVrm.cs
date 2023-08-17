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
    public class VmcExtVrm : VmcMessage
    {
        public string path { get; }
        public string title { get; }
        public string? hash { get; }

        public VmcExtVrm(godotOscSharp.OscMessage m) : base(m.Address)
        {
            switch (m.Data.Count)
            {
                case 2:
                    if (m.Data[0].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(addr, "path", 's', m.Data[0].Type));
                        return;
                    }
                    if (m.Data[1].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(addr, "title", 's', m.Data[1].Type));
                        return;
                    }
                    path = (string)m.Data[0].Value;
                    title = (string)m.Data[1].Value;
                case 3:
                    if (m.Data[0].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(addr, "path", 's', m.Data[0].Type));
                        return;
                    }
                    if (m.Data[1].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(addr, "title", 's', m.Data[1].Type));
                        return;
                    }
                    if (m.Data[2].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(addr, "hash", 's', m.Data[1].Type));
                        return;
                    }
                    path = (string)m.Data[0].Value;
                    title = (string)m.Data[1].Value;
                    hash = (string)m.Data[2].Value;
                default:
                    GD.Print($"Invalid number of arguments for {addr} message. Expected 2 or 3 but received {m.Data.Count}");
                    return;
            }
        }
    }
}