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
    public class VmcExtMidiNote : VmcMessage
    {
        public int Active { get; }
        public int Channel { get; }
        public int Note { get; }
        public float Velocity { get; }

        public VmcExtMidiNote(OscMessage m) : base(m.Address)
        {
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "active", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "channel", 'i', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "note", 'i', m.Data[2].Type));
                return;
            }
            if (m.Data[3].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "velocity", 'f', m.Data[4].Type));
                return;
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 1)
            {
                GD.Print($"Invalid value for \"active\" 'i' argument of {addr}. Expected 0 or 1, received {(int)m.Data[0].Value}");
                return;
            }
            Active = (int)m.Data[0].Value;
            Channel = (int)m.Data[1].Value;
            Note = (int)m.Data[2].Value;
            Velocity = (int)m.Data[3].Value;
        }

        public VmcExtMidiNote(int active, int channel, int note, float velocity) : base(new OscAddress("/VMC/Ext/Midi/Note"))
        {
            if (active < 0 || active > 1)
            {
                GD.Print($"Invalid value for \"active\" 'i' argument of {addr}. Expected 0 or 1, received {Active}.");
                return;
            }
            Active = active;
            Channel = channel;
            Note = note;
            Velocity = velocity;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(addr, new System.Collections.Generic.List<OscArgument>{
                new OscArgument(Active, 'i'),
                new OscArgument(Channel, 'i'),
                new OscArgument(Note, 'i'),
                new OscArgument(Velocity, 'f')
            });
        }
    }
}