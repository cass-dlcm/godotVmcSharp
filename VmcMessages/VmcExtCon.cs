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
        public readonly int Active;
        public readonly string Name;
        public readonly int IsLeft;
        public readonly int IsTouch;
        public readonly int IsAxis;
        public readonly Vector3 Axis;

        public VmcExtCon(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 8)
            {
                GD.Print($"Invalid number of arguments for /VMC/Ext/Con. Expected 8, received {m.Data.Count}.");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "active", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "name", 's', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "IsLeft", 'i', m.Data[2].Type));
            }
            if (m.Data[3].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "IsTouch", 'i', m.Data[3].Type));
            }
            if (m.Data[4].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "IsAxis", 'i', m.Data[4].Type));
            }
            if (m.Data[5].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "Axis.x", 'f', m.Data[5].Type));
            }
            if (m.Data[6].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "Axis.y", 'f', m.Data[6].Type));
            }
            if (m.Data[7].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "Axis.z", 'f', m.Data[7].Type));
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 2)
            {
                GD.Print($"Invalid value for \"active\" 'i' argument of /VMC/Ext/Con. Expected 0-2, received {(int)m.Data[0].Value}");
                return;
            }
            Active = (int)m.Data[0].Value;
            Name = (string)m.Data[1].Value;
            IsLeft = (int)m.Data[2].Value;
            IsTouch = (int)m.Data[3].Value;
            IsAxis = (int)m.Data[4].Value;
            Axis = new Vector3((float)m.Data[5].Value, (float)m.Data[6].Value, (float)m.Data[7].Value);
        }

        public VmcExtCon(int active, string name, int isLeft, int isTouch, int isAxis, Vector3 axis) : base(new OscAddress("/VMC/Ext/Con"))
        {
            if (active < 0 || active > 2)
            {
                GD.Print($"Invalid value for \"active\" 'i' argument of {Addr}. Expected 0-2, received {active}");
                return;
            }
            Active = active;
            Name = name;
            IsLeft = isLeft;
            IsTouch = isTouch;
            IsAxis = isAxis;
            Axis = axis;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{
                new OscArgument(Active, 'i'),
                new OscArgument(Name, 's'),
                new OscArgument(IsLeft, 'i'),
                new OscArgument(IsTouch, 'i'),
                new OscArgument(IsAxis, 'i'),
                new OscArgument(Axis.X, 'i'),
                new OscArgument(Axis.Y, 'i'),
                new OscArgument(Axis.Z, 'i'),
            });
        }
    }
}