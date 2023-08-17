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
        public int knob { get; }
        public float value { get; }
        public VmcExtMidiCcVal(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "knob", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "value", 'f', m.Data[1].Type));
                return;
            }
            knob = (int)m.Data[0].Value;
            value = (int)m.Data[1].Value;
        }

        public VmcExtMidiCcVal(int _knob, float _value) : base(new godotOscSharp.Address("/VMC/Ext/Midi/CC/Val"))
        {
            knob = _knob;
            value = _value;
        }
    }
}