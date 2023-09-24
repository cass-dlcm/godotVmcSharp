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
    public class VmcExtSetCalibExec : VmcMessage
    {
        public int Mode { get; }

        public VmcExtSetCalibExec(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 1)
            {
                GD.Print($"Invalid number of arguments for {Addr}. Expecting 1, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "mode", 'i', m.Data[0].Type));
                return;
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 2)
            {
                GD.Print($"Invalid value for argument \"mode\" of \"/VMC/Ext/Set/Calib/Exec\". Expected in range 0-2, received {(int)m.Data[0].Value}");
                return;
            }
            Mode = (int)m.Data[0].Value;
        }

        public VmcExtSetCalibExec(int mode) : base(new OscAddress("/VMC/Ext/Set/Calib/Exec"))
        {
            if (mode < 0 || mode > 2)
            {
                GD.Print($"Invalid value for argument \"mode\" of \"/VMC/Ext/Set/Calib/Exec\". Expected in range 0-2, received {mode}");
                return;
            }
            Mode = mode;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{new OscArgument(Mode, 'i')});
        }
    }
}