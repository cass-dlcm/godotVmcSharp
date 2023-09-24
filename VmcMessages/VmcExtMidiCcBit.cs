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
    public class VmcExtMidiCcBit : VmcMessage
    {
        public int Knob { get; }
        public int Active { get; }
        public VmcExtMidiCcBit(OscMessage m) : base(m.Address)
        {
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "knob", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "active", 'i', m.Data[1].Type));
                return;
            }
            if ((int)m.Data[1].Value < 0 || (int)m.Data[1].Value > 1)
            {
                GD.Print($"Invalid value for \"active\" argument of {Addr}. Expected 0 or 1, received {(int)m.Data[1].Value}.");
                return;
            }
            Knob = (int)m.Data[0].Value;
            Active = (int)m.Data[1].Value;
        }

        public VmcExtMidiCcBit(int knob, int active) : base(new OscAddress("/VMC/Ext/Midi/CC/Bit"))
        {
            if (active < 0 || active > 1)
            {
                GD.Print($"Invalid value for \"active\" argument of {Addr}. Expected 0 or 1, received {active}.");
                return;
            }
            Knob = knob;
            Active = active;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{
                new OscArgument(Knob, 'i'),
                new OscArgument(Active, 'i')
            });
        }
    }
}