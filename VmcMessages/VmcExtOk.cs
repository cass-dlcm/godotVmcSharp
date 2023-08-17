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
        public int Loaded { get; }
        public int? CalibrationState { get; }
        public int? CalibrationMode { get; }
        public int? TrackingStatus { get; }

        public VmcExtOk(int loaded) : base(new OscAddress("/VMC/Ext/OK"))
        {
            if (loaded < 0 || loaded > 1)
            {
                GD.Print($"Invalid value for loaded status. Expected 0 or 1, received {loaded}.");
                return;
            }
            Loaded = loaded;
        }

        public VmcExtOk(int loaded, int calibrationState, int calibrationMode) : base(new OscAddress("/VMC/Ext/OK"))
        {
            if (loaded < 0 || loaded > 1)
            {
                GD.Print($"Invalid value for loaded status. Expected 0 or 1, received {loaded}.");
                return;
            }
            if (calibrationState < 0 || calibrationState > 3)
            {
                GD.Print($"Invalid value for calibration state. Expected 0-3, received {calibrationState}");
                return;
            }
            if (calibrationMode < 0 || calibrationMode > 2)
            {
                GD.Print($"Invalid value for calibration mode. Expected 0-2, received {calibrationMode}");
                return;
            }
            Loaded = loaded;
            CalibrationState = calibrationState;
            CalibrationMode = calibrationMode;
        }

        public VmcExtOk(int loaded, int calibrationState, int calibrationMode, int trackingStatus) : base(new OscAddress("/VMC/Ext/OK"))
        {
            if (loaded < 0 || loaded > 1)
            {
                GD.Print($"Invalid value for loaded status. Expected 0 or 1, received {loaded}.");
                return;
            }
            if (calibrationState < 0 || calibrationState > 3)
            {
                GD.Print($"Invalid value for calibration state. Expected 0-3, received {calibrationState}");
                return;
            }
            if (calibrationMode < 0 || calibrationMode > 2)
            {
                GD.Print($"Invalid value for calibration mode. Expected 0-2, received {calibrationMode}");
                return;
            }
            if (trackingStatus < 0 || trackingStatus > 1)
            {
                GD.Print($"Invalid value for tracking status. Expected 0-1, received {trackingStatus}");
                return;
            }
            Loaded = loaded;
            CalibrationState = calibrationState;
            CalibrationMode = calibrationMode;
            TrackingStatus = trackingStatus;
        }

        public VmcExtOk(OscMessage m) : base(m.Address)
        {
            switch (m.Data.Count)
            {
                case 1:
                    if (!OkParam0(m.Data[0]))
                    {
                        return;
                    }
                    Loaded = (int)m.Data[0].Value;
                    break;
                case 3:
                    if (!OkParam1And2(m.Data[0], m.Data[1], m.Data[2]))
                    {
                        return;
                    }
                    Loaded = (int)m.Data[0].Value;
                    CalibrationState = (int)m.Data[1].Value;
                    CalibrationMode = (int)m.Data[2].Value;
                    break;
                case 4:
                    if (!OkParam3(m.Data[0], m.Data[1], m.Data[2], m.Data[3]))
                    {
                        return;
                    }
                    Loaded = (int)m.Data[0].Value;
                    CalibrationState = (int)m.Data[1].Value;
                    CalibrationMode = (int)m.Data[2].Value;
                    TrackingStatus = (int)m.Data[3].Value;
                    break;
                default:
                    GD.Print($"Invalid number of arguments for /VMC/Ext/OK message. Expected 1, 3, or 4 but received {m.Data.Count}");
                    return;
            }
        }

        private bool OkParam0(OscArgument arg)
        {
            if (arg.Type != 'i')
            {
                GD.Print($"Invalid argument type for /VMC/Ext/OK message. Expected int in argument 0, received {arg.Type}");
                return false;
            }
            if ((int)arg.Value < 0 && (int)arg.Value > 1)
            {
                GD.Print($"Invalid value for loaded status. Expected 0-1, received {(int)arg.Value}");
                return false;
            }
            return true;
        }

        private bool OkParam1And2(OscArgument arg0, OscArgument arg1, OscArgument arg2)
        {
            if (!OkParam0(arg0))
            {
                return false;
            }
            if (arg1.Type != 'i')
            {
                GD.Print($"Invalid argument type for /VMC/Ext/OK message. Expected int in argument 1, received {arg1.Type}");
                return false;
            }
            if (arg2.Type != 'i')
            {
                GD.Print($"Invalid argument type for /VMC/Ext/OK message. Expected int in argument 2, received {arg2.Type}");
                return false;
            }
            if ((int)arg1.Value < 0 && (int)arg1.Value > 3)
            {
                GD.Print($"Invalid value for calibration state. Expected 0-3, received {(int)arg1.Value}");
                return false;
            }
            if ((int)arg2.Value < 0 && (int)arg2.Value > 2)
            {
                GD.Print($"Invalid value for calibration mode. Expected 0-2, received {(int)arg2.Value}");
                return false;
            }
            return true;
        }

        private bool OkParam3(OscArgument arg0, OscArgument arg1, OscArgument arg2, OscArgument arg)
        {
            if (!OkParam1And2(arg0, arg1, arg2))
            {
                return false;
            }
            if (arg.Type != 'i')
            {
                GD.Print($"Invalid argument type for /VMC/Ext/OK message. Expected int in argument 3, received {arg.Type}");
                return false;
            }
            if ((int)arg.Value < 0 && (int)arg.Value > 1)
            {
                GD.Print($"Invalid value for tracking status. Expected 0-1, received {(int)arg.Value}");
                return false;
            }
            return true;
        }

        public new OscMessage ToMessage()
        {
            if (CalibrationState == null)
            {
                return new OscMessage(addr, new List<OscArgument>{
                    new OscArgument(Loaded, 'i')
                });
            }
            if (TrackingStatus == null)
            {
                return new OscMessage(addr, new List<OscArgument>{
                    new OscArgument(Loaded, 'i'),
                    new OscArgument(CalibrationState, 'i'),
                    new OscArgument(CalibrationMode, 'i')
                });
            }
            return new OscMessage(addr, new List<OscArgument>{
                new OscArgument(Loaded, 'i'),
                new OscArgument(CalibrationState, 'i'),
                new OscArgument(CalibrationMode, 'i'),
                new OscArgument(TrackingStatus, 'i')
            });
        }
    }
}