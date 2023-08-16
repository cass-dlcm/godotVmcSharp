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
    public class VmcExtCon : VmcMessage
    {
        public int active { get; }
        public string name { get; }
        public int isLeft { get; }
        public int isTouch { get; }
        public int isAxis { get; }
        public Godot.Vector3 axis { get; }
        
        public VmcExtCon(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 8)
            {
                GD.Print($"Invalid number of arguments for /VMC/Ext/Con. Expected 8, received {m.Data.Count}.");
                return;
            }
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
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsLeft", 'i', m.Data[2].Type));
            }
            if (m.Data[3].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsTouch", 'i', m.Data[3].Type));
            }
            if (m.Data[4].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsAxis", 'i', m.Data[4].Type));
            }
            if (m.Data[5].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.x", 'f', m.Data[5].Type));
            }
            if (m.Data[6].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.y", 'f', m.Data[6].Type));
            }
            if (m.Data[7].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.z", 'f', m.Data[7].Type));
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 2)
            {
                GD.Print($"Invalid value for \"active\" 'i' argument of /VMC/Ext/Con. Expected 0-2, received {(int)m.Data[0].Value}");
                return;
            }
            active = (int)m.Data[0].Value;
            name = (string)m.Data[1].Value;
            isLeft = (int)m.Data[2].Value;
            isTouch = (int)m.Data[3].Value;
            isAxis = (int)m.Data[4].Value;
            axis = new Godot.Vector3((float)m.Data[5].Value, (float)m.Data[6].Value, (float)m.Data[7].Value);
        }
    }
}