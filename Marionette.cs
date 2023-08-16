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

using System;
using System.Net;
using System.Collections.Generic;
using Godot;

namespace godotVmcSharp
{
    public class Marionette
    {
        private godotOscSharp.OscReceiver receiver;
        private godotOscSharp.OscSender sender;
        public Marionette(int port)
        {
            receiver = new godotOscSharp.OscReceiver(port);
            receiver.MessageReceived += (sender, e) =>
            {
                if (sender == null) {
                    sender = new godotOscSharp.OscSender(IPAddress.Parse(e.IPAddress), port);
                }
                GD.Print($"Received a message from {e.IPAddress}:{e.Port}");
                ProcessMessage(e.Message);
            };
            receiver.ErrorReceived += (sender, e) =>
            {
                GD.Print($"Error: {e.ErrorMessage}");
            };
        }
        private void ProcessMessage(godotOscSharp.OscMessage m)
        {
            switch (m.Address.ToString())
            {
                case "/VMC/Ext/OK":
                    new VmcExtOk(m);
                    break;
                case "/VMC/Ext/T":
                    new VmcExtT(m);
                    break;
                case "/VMC/Ext/Root/Pos":
                    new VmcExtRootPos(m);
                    break;
                case "/VMC/Ext/Bone/Pos":
                    new VmcExtBonePos(m);
                    break;
                case "/VMC/Ext/Blend/Val":
                    new VmcExtBlendVal(m);
                    break;
                case "/VMC/Ext/Blend/Apply":
                    new VmcMessage(m.Address);
                    break;
                case "/VMC/Ext/Cam":
                    new VmcExtCam(m);
                    break;
                case "/VMC/Ext/Con":
                    ControllerInput(m.Data);
                    break;
                case "/VMC/Ext/Key":
                    KeyInput(m.Data);
                    break;
                case "/VMC/Ext/Midi/Note":
                    MidiNote(m.Data);
                    break;
                case "/VMC/Ext/Midi/CC/Val":
                    MidiValue(m.Data);
                    break;
                case "/VMC/Ext/Midi/CC/Bit":
                    MidiButton(m.Data);
                    break;
//                case "/VMC/Ext/Hmd/Pos":
//                    if (m.Data.Count == 8)
//                    {
//                        GD.Print(Transform8(m.Data, m.Address.ToString(), new List<string>{"serial", "p.x", "p.y", "p.z", "q.x", "q.y", "q.z", "q.w"}).ToString());
//                    } else {
//                        GD.Print($"Invalid number of arguments for \"{m.Address.ToString()}\". Expected 8, received {m.Data.Count}.");
//                    }
//                    break;
//                case "/VMC/Ext/Con/Pos":
//                    if (m.Data.Count == 8)
//                    {
//                        GD.Print(Transform8(m.Data, m.Address.ToString(), new List<string>{"serial", "p.x", "p.y", "p.z", "q.x", "q.y", "q.z", "q.w"}).ToString());
//                    } else {
//                        GD.Print($"Invalid number of arguments for \"{m.Address.ToString()}\". Expected 8, received {m.Data.Count}.");
//                    }
//                    break;
//                case "/VMC/Ext/Tra/Pos":
//                    if (m.Data.Count == 8)
//                    {
//                        GD.Print(Transform8(m.Data, m.Address.ToString(), new List<string>{"serial", "p.x", "p.y", "p.z", "q.x", "q.y", "q.z", "q.w"}).ToString());
//                    } else {
//                        GD.Print($"Invalid number of arguments for \"{m.Address.ToString()}\". Expected 8, received {m.Data.Count}.");
//                    }
//                    break;
//                case "/VMC/Ext/Hmd/Pos/Local":
//                    if (m.Data.Count == 8)
//                    {
//                        GD.Print(Transform8(m.Data, m.Address.ToString(), new List<string>{"serial", "p.x", "p.y", "p.z", "q.x", "q.y", "q.z", "q.w"}).ToString());
//                    } else {
//                        GD.Print($"Invalid number of arguments for \"{m.Address.ToString()}\". Expected 8, received {m.Data.Count}.");
//                    }
//                    break;
//                case "/VMC/Ext/Con/Pos/Local":
//                    if (m.Data.Count == 8)
//                    {
//                        GD.Print(Transform8(m.Data, m.Address.ToString(), new List<string>{"serial", "p.x", "p.y", "p.z", "q.x", "q.y", "q.z", "q.w"}).ToString());
//                    } else {
//                        GD.Print($"Invalid number of arguments for \"{m.Address.ToString()}\". Expected 8, received {m.Data.Count}.");
//                    }
//                    break;
//                case "/VMC/Ext/Tra/Pos/Local":
//                    if (m.Data.Count == 8)
//                    {
//                        GD.Print(Transform8(m.Data, m.Address.ToString(), new List<string>{"serial", "p.x", "p.y", "p.z", "q.x", "q.y", "q.z", "q.w"}).ToString());
//                    } else {
//                        GD.Print($"Invalid number of arguments for \"{m.Address.ToString()}\". Expected 8, received {m.Data.Count}.");
//                    }
//                    break;
                case "/VMC/Ext/Rcv":
                    ValidateReceiveEnable(m.Data);
                    break;
            }
        }
        private void ControllerInput(List<godotOscSharp.OscArgument> data)
        {
            var addr = "VMC/Ext/Con";
            if (data.Count != 8)
            {
                GD.Print($"Invalid number of arguments for /VMC/Ext/Con. Expected 8, received {data.Count}.");
                return;
            }
            if (data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "active", 'i', data[0].Type));
                return;
            }
            if (data[1].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "name", 's', data[1].Type));
                return;
            }
            if (data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsLeft", 'i', data[2].Type));
            }
            if (data[3].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsTouch", 'i', data[3].Type));
            }
            if (data[4].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "IsAxis", 'i', data[4].Type));
            }
            if (data[5].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.x", 'f', data[5].Type));
            }
            if (data[6].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.y", 'f', data[6].Type));
            }
            if (data[7].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "Axis.z", 'f', data[7].Type));
            }
            switch ((int)data[0].Value)
            {
                case 0:
                    GD.Print("controller release");
                    break;
                case 1:
                    GD.Print("controller press");
                    break;
                case 2:
                    GD.Print("controller change axis");
                    break;
                default:
                    GD.Print($"Invalid value for \"active\" 'i' argument of /VMC/Ext/Con. Expected 0-2, received {(int)data[0].Value}");
                    return;
            }
            GD.Print($"\t\"Name\": \"{(string)data[1].Value}\",\n\t\"IsLeft\": {(int)data[2].Value},\n\t\"IsTouch\": {(int)data[3].Value},\n\t\"IsAxis\": {(int)data[4].Value}\n\t\"Axis\"\n\t\t\"x\": {(float)data[5].Value}\n\t\t\"y\": {(float)data[6].Value}\n\t\t\"z\": {(float)data[7].Value}\n");
        }
        private void KeyInput(List<godotOscSharp.OscArgument> data)
        {
            var addr = "/VMC/Ext/Key";
            if (data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "active", 'i', data[0].Type));
                return;
            }
            if (data[1].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "name", 's', data[1].Type));
                return;
            }
            if (data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "keycode", 'i', data[2].Type));
            }
            switch ((int)data[0].Value)
            {
                case 0:
                    GD.Print($"Key {(string)data[1].Value} ({(int)data[2].Value}) is released");
                    break;
                case 1:
                    GD.Print($"Key {(string)data[1].Value} ({(int)data[2].Value}) is pressed");
                    break;
                default:
                    GD.Print($"Invalid value for \"active\" 'i' argument of {addr}. Expected 0 or 1, received {(int)data[0].Value}");
                    break;
            }
        }
        private void MidiNote(List<godotOscSharp.OscArgument> data)
        {
            var addr = "/VMC/Ext/Midi/Note";
            if (data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "active", 'i', data[0].Type));
                return;
            }
            if (data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "channel", 'i', data[1].Type));
                return;
            }
            if (data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "note", 'i', data[2].Type));
                return;
            }
            if (data[3].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "velocity", 'f', data[4].Type));
                return;
            }
            switch ((int)data[0].Value)
            {
            case 0:
                GD.Print($"Note event released on channel {(int)data[1].Value} for note {(int)data[2].Value} with velocity {(float)data[3].Value}.");
                break;
            case 1:
                GD.Print($"Note event pressed on channel {(int)data[1].Value} for note {(int)data[2].Value} with velocity {(float)data[3].Value}.");
                break;
            default:
                GD.Print($"Invalid value for \"active\" 'i' argument of {addr}. Expected 0 or 1, received {(int)data[0].Value}");
                return;
            }
        }
        private void MidiValue(List<godotOscSharp.OscArgument> data)
        {
            var addr = "/VMC/Ext/Midi/CC/Val";
            if (data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "knob", 'i', data[0].Type));
                return;
            }
            if (data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "value", 'f', data[1].Type));
                return;
            }
            GD.Print($"CC value input on knob {(int)data[0].Value} with value {(float)data[1].Value}.");
        }
        private void MidiButton(List<godotOscSharp.OscArgument> data)
        {
            var addr = "/VMC/Ext/Midi/CC/Bit";
            if (data.Count != 2)
            {
                GD.Print($"Invalid argument count for \"{addr}\". Expected 2, received {data.Count}");
                return;
            }
            if (data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "knob", 'i', data[0].Type));
                return;
            }
            if (data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "active", 'i', data[1].Type));
                return;
            }
            switch ((int)data[1].Value)
            {
                case 0:
                    GD.Print("Button released on knob {(int)data[0].Value}");
                    break;
                case 1:
                    GD.Print("Button pressed on knob {(int)data[0].Value}");
                    break;
                default:
                    GD.Print($"Invalid value for \"active\" argument of {addr}. Expected 0 or 1, received {(int)data[1].Value}.");
                    return;
            }
        }
        private bool ValidateReceiveEnable(List<godotOscSharp.OscArgument> data)
        {
            var addr = "/VMC/Ext/Rcv";
            switch (data.Count)
            {
                case 2:
                    break;
                case 3:
                    if (data[2].Type != 's')
                    {
                        GD.Print(InvalidArgumentType.GetErrorString(addr, "IpAddress", 's', data[2].Type));
                        return false;
                    }
                    break;
                default:
                    return false;
            }
            if (data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "enable", 'i', data[0].Type));
                return false;
            }
            if (data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "port", 'i', data[0].Type));
                return false;
            }
            if ((int)data[1].Value < 0 || (int)data[1].Value > 65535)
            {
                GD.Print($"Invalid value for \"port\" argument of {addr}. Expected 0-65535, received {(int)data[1].Value}.");
                return false;
            }
            return true;
        }
    }
}
