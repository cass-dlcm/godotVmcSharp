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
    public class VmcExtSettingWin : VmcMessage
    {
        public int isTopMost { get; }
        public int isTransparent { get; }
        public int windowClickThrough { get; }
        public int hideBorder { get; }

        public VmcExtSettingWin(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 4)
            {
                GD.Print($"Invalid number of arguments for {addr}. Expecting 4, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "isTopMost", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "isTransparent", 'i', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "windowClickThrough", 'i', m.Data[2].Type));
                return;
            }
            if (m.Data[3].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "hideBorder", 'i', m.Data[3].Type));
                return;
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 1)
            {
                GD.Print($"Invalid value for \"isTopMost\" 'i' argument of {addr}. Expected 0 or 1, received {(int)m.Data[0].Value}");
                return;
            }
            if ((int)m.Data[1].Value < 0 || (int)m.Data[1].Value > 1)
            {
                GD.Print($"Invalid value for \"isTransparent\" 'i' argument of {addr}. Expected 0 or 1, received {(int)m.Data[1].Value}");
                return;
            }
            if ((int)m.Data[2].Value < 0 || (int)m.Data[2].Value > 1)
            {
                GD.Print($"Invalid value for \"windowClickThrough\" 'i' argument of {addr}. Expected 0 or 1, received {(int)m.Data[2].Value}");
                return;
            }
            if ((int)m.Data[3].Value < 0 || (int)m.Data[3].Value > 1)
            {
                GD.Print($"Invalid value for \"hideBorder\" 'i' argument of {addr}. Expected 0 or 1, received {(int)m.Data[3].Value}");
                return;
            }
            isTopMost = (int)m.Data[0].Value;
            isTransparent = (int)m.Data[1].Value;
            windowClickThrough = (int)m.Data[2].Value;
            hideBorder = (int)m.Data[3].Value;
        }

        public VmcExtSettingWin(int _isTopMost, int _isTransparent, int _windowClickThrough, int _hideBorder) : base(new godotOscSharp.Address("/VMC/Ext/Setting/Win"))
        {
            if (_isTopMost < 0 || _isTopMost > 1)
            {
                GD.Print($"Invalid value for \"isTopMost\" 'i' argument of {addr}. Expected 0 or 1, received {_isTopMost}");
                return;
            }
            if (_isTransparent < 0 || _isTransparent > 1)
            {
                GD.Print($"Invalid value for \"isTransparent\" 'i' argument of {addr}. Expected 0 or 1, received {_isTransparent}");
                return;
            }
            if (_windowClickThrough < 0 || _windowClickThrough > 1)
            {
                GD.Print($"Invalid value for \"windowClickThrough\" 'i' argument of {addr}. Expected 0 or 1, received {_windowClickThrough}");
                return;
            }
            if (_hideBorder < 0 || _hideBorder > 1)
            {
                GD.Print($"Invalid value for \"hideBorder\" 'i' argument of {addr}. Expected 0 or 1, received {_hideBorder}");
                return;
            }
            isTopMost = _isTopMost;
            isTransparent = _isTransparent;
            windowClickThrough = _windowClickThrough;
            hideBorder = _hideBorder;
        }
    }
}