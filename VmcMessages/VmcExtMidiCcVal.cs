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
    public class VmcExtMidiCcVal : VmcMessage
    {
        public readonly int Knob;
        public readonly float Value;
        public VmcExtMidiCcVal(OscMessage m) : base(m.Address)
        {
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "knob", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "value", 'f', m.Data[1].Type));
                return;
            }
            Knob = (int)m.Data[0].Value;
            Value = (int)m.Data[1].Value;
        }

        public VmcExtMidiCcVal(int knob, float value) : base(new OscAddress("/VMC/Ext/Midi/CC/Val"))
        {
            Knob = knob;
            Value = value;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{
                new OscArgument(Knob, 'i'),
                new OscArgument(Value, 'f')
            });
        }
    }
}