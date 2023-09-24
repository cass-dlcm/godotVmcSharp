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
        public readonly int IsTopMost;
        public readonly int IsTransparent;
        public readonly int WindowClickThrough;
        public readonly int HideBorder;

        public VmcExtSettingWin(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 4)
            {
                GD.Print($"Invalid number of arguments for {Addr}. Expecting 4, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "isTopMost", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "isTransparent", 'i', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "windowClickThrough", 'i', m.Data[2].Type));
                return;
            }
            if (m.Data[3].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "hideBorder", 'i', m.Data[3].Type));
                return;
            }
            if ((int)m.Data[0].Value < 0 || (int)m.Data[0].Value > 1)
            {
                GD.Print($"Invalid value for \"isTopMost\" 'i' argument of {Addr}. Expected 0 or 1, received {(int)m.Data[0].Value}");
                return;
            }
            if ((int)m.Data[1].Value < 0 || (int)m.Data[1].Value > 1)
            {
                GD.Print($"Invalid value for \"isTransparent\" 'i' argument of {Addr}. Expected 0 or 1, received {(int)m.Data[1].Value}");
                return;
            }
            if ((int)m.Data[2].Value < 0 || (int)m.Data[2].Value > 1)
            {
                GD.Print($"Invalid value for \"windowClickThrough\" 'i' argument of {Addr}. Expected 0 or 1, received {(int)m.Data[2].Value}");
                return;
            }
            if ((int)m.Data[3].Value < 0 || (int)m.Data[3].Value > 1)
            {
                GD.Print($"Invalid value for \"hideBorder\" 'i' argument of {Addr}. Expected 0 or 1, received {(int)m.Data[3].Value}");
                return;
            }
            IsTopMost = (int)m.Data[0].Value;
            IsTransparent = (int)m.Data[1].Value;
            WindowClickThrough = (int)m.Data[2].Value;
            HideBorder = (int)m.Data[3].Value;
        }

        public VmcExtSettingWin(int isTopMost, int isTransparent, int windowClickThrough, int hideBorder) : base(new OscAddress("/VMC/Ext/Setting/Win"))
        {
            if (isTopMost < 0 || isTopMost > 1)
            {
                GD.Print($"Invalid value for \"isTopMost\" 'i' argument of {Addr}. Expected 0 or 1, received {isTopMost}");
                return;
            }
            if (isTransparent < 0 || isTransparent > 1)
            {
                GD.Print($"Invalid value for \"isTransparent\" 'i' argument of {Addr}. Expected 0 or 1, received {isTransparent}");
                return;
            }
            if (windowClickThrough < 0 || windowClickThrough > 1)
            {
                GD.Print($"Invalid value for \"windowClickThrough\" 'i' argument of {Addr}. Expected 0 or 1, received {windowClickThrough}");
                return;
            }
            if (hideBorder < 0 || hideBorder > 1)
            {
                GD.Print($"Invalid value for \"hideBorder\" 'i' argument of {Addr}. Expected 0 or 1, received {hideBorder}");
                return;
            }
            IsTopMost = isTopMost;
            IsTransparent = isTransparent;
            WindowClickThrough = windowClickThrough;
            HideBorder = hideBorder;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{
                new OscArgument(IsTopMost, 'i'),
                new OscArgument(IsTransparent, 'i'),
                new OscArgument(WindowClickThrough, 'i'),
                new OscArgument(HideBorder, 'i')
            });
        }
    }
}