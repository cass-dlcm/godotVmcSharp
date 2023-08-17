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

using godotOscSharp;
using Godot;

namespace godotVmcSharp
{
    public class VmcExtT : VmcMessage
    {
        public float time { get; }
        public VmcExtT(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 1)
            {
                GD.Print($"Invalid number of arguments for /VMC/Ext/T message. Expected 1 but received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'f')
            {
                InvalidArgumentType.GetErrorString(m.Address.ToString(), "time", 'f', m.Data[0].Type);
                return;
            }
            time = (float)m.Data[0].Value;
        }

        public VmcExtT(float _time) : base(new godotOscSharp.OscAddress("/VMC/Ext/T"))
        {
            time = _time;
        }
    }
}