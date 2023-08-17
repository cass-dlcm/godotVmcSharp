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
using System.Collections.Generic;

namespace godotVmcSharp
{
    public class VmcExtOk : VmcMessage
    {
        public int loaded { get; private set; }
        public int? calibrationState { get; private set; }
        public int? calibrationMode { get; private set; }
        public int? trackingStatus { get; private set; }

        public VmcExtOk(int _loaded) : base(new godotOscSharp.Address("/VMC/Ext/OK"))
        {
            if (_loaded < 0 || _loaded > 1)
            {
                GD.Print($"Invalid value for loaded status. Expected 0 or 1, received {_loaded}.");
                return;
            }
            loaded = _loaded;
        }

        public VmcExtOk(int _loaded, int _calibrationState, int _calibrationMode) : base(new godotOscSharp.Address("/VMC/Ext/OK"))
        {
            if (_loaded < 0 || _loaded > 1)
            {
                GD.Print($"Invalid value for loaded status. Expected 0 or 1, received {_loaded}.");
                return;
            }
            if (_calibrationState < 0 || _calibrationState > 3)
            {
                GD.Print($"Invalid value for calibration state. Expected 0-3, received {_calibrationState}");
                return;
            }
            if (_calibrationMode < 0 || _calibrationMode > 2)
            {
                GD.Print($"Invalid value for calibration mode. Expected 0-2, received {_calibrationMode}");
                return;
            }
            loaded = _loaded;
            calibrationState = _calibrationState;
            calibrationMode = _calibrationMode;
        }

        public VmcExtOk(int _loaded, int _calibrationState, int _calibrationMode, int _trackingStatus) : base(new godotOscSharp.Address("/VMC/Ext/OK"))
        {
            if (_loaded < 0 || _loaded > 1)
            {
                GD.Print($"Invalid value for loaded status. Expected 0 or 1, received {_loaded}.");
                return;
            }
            if (_calibrationState < 0 || _calibrationState > 3)
            {
                GD.Print($"Invalid value for calibration state. Expected 0-3, received {_calibrationState}");
                return;
            }
            if (_calibrationMode < 0 || _calibrationMode > 2)
            {
                GD.Print($"Invalid value for calibration mode. Expected 0-2, received {_calibrationMode}");
                return;
            }
            if (_trackingStatus < 0 || _trackingStatus > 1)
            {
                GD.Print($"Invalid value for tracking status. Expected 0-1, received {_trackingStatus}");
                return;
            }
            loaded = _loaded;
            calibrationState = _calibrationState;
            calibrationMode = _calibrationMode;
            trackingStatus = _trackingStatus;
        }

        public VmcExtOk(godotOscSharp.OscMessage m) : base(m.Address)
        {
            switch (m.Data.Count)
            {
                case 1:
                    OkParam0(m.Data[0]);
                    break;
                case 3:
                    OkParam1And2(m.Data[0], m.Data[1], m.Data[2]);
                    break;
                case 4:
                    OkParam3(m.Data[0], m.Data[1], m.Data[2], m.Data[3]);
                    break;
                default:
                    GD.Print($"Invalid number of arguments for /VMC/Ext/OK message. Expected 1, 3, or 4 but received {m.Data.Count}");
                    break;
            }
        }

        private void OkParam0(godotOscSharp.OscArgument arg)
        {
            if (arg.Type != 'i')
            {
                GD.Print($"Invalid argument type for /VMC/Ext/OK message. Expected int in argument 0, received {arg.Type}");
                return;
            }
            if ((int)arg.Value < 0 && (int)arg.Value > 1)
            {
                GD.Print($"Invalid value for loaded status. Expected 0-1, received {(int)arg.Value}");
                return;
            }
            loaded = (int)arg.Value;
        }

        private void OkParam1And2(godotOscSharp.OscArgument arg0, godotOscSharp.OscArgument arg1, godotOscSharp.OscArgument arg2)
        {
            OkParam0(arg0);
            if (arg1.Type != 'i')
            {
                GD.Print($"Invalid argument type for /VMC/Ext/OK message. Expected int in argument 1, received {arg1.Type}");
                return;
            }
            if (arg2.Type != 'i')
            {
                GD.Print($"Invalid argument type for /VMC/Ext/OK message. Expected int in argument 2, received {arg2.Type}");
                return;
            }
            if ((int)arg1.Value < 0 && (int)arg1.Value > 3)
            {
                GD.Print($"Invalid value for calibration state. Expected 0-3, received {(int)arg1.Value}");
                return;
            }
            if ((int)arg2.Value < 0 && (int)arg2.Value > 2)
            {
                GD.Print($"Invalid value for calibration mode. Expected 0-2, received {(int)arg2.Value}");
                return;
            }
            calibrationState = (int)arg1.Value;
            calibrationMode = (int)arg2.Value;
        }

        private void OkParam3(godotOscSharp.OscArgument arg0, godotOscSharp.OscArgument arg1, godotOscSharp.OscArgument arg2, godotOscSharp.OscArgument arg)
        {
            OkParam1And2(arg0, arg1, arg2);
            if (arg.Type != 'i')
            {
                GD.Print($"Invalid argument type for /VMC/Ext/OK message. Expected int in argument 3, received {arg.Type}");
                return;
            }
            if ((int)arg.Value < 0 && (int)arg.Value > 1)
            {
                GD.Print($"Invalid value for tracking status. Expected 0-1, received {(int)arg.Value}");
                return;
            }
            trackingStatus = (int)arg.Value;
        }

        public godotOscSharp.OscMessage ToMessage()
        {
            if (calibrationState == null)
            {
                return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                    new godotOscSharp.OscArgument(loaded, 'i')
                });
            }
            if (trackingStatus == null)
            {
                return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                    new godotOscSharp.OscArgument(loaded, 'i'),
                    new godotOscSharp.OscArgument(calibrationState, 'i'),
                    new godotOscSharp.OscArgument(calibrationMode, 'i')
                });
            }
            return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                new godotOscSharp.OscArgument(loaded, 'i'),
                new godotOscSharp.OscArgument(calibrationState, 'i'),
                new godotOscSharp.OscArgument(calibrationMode, 'i'),
                new godotOscSharp.OscArgument(trackingStatus, 'i')
            });
        }
    }
}