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
                    new VmcExtCon(m);
                    break;
                case "/VMC/Ext/Key":
                    new VmcExtKey(m);
                    break;
                case "/VMC/Ext/Midi/Note":
                    new VmcExtMidiNote(m);
                    break;
                case "/VMC/Ext/Midi/CC/Val":
                    new VmcExtMidiCcVal(m);
                    break;
                case "/VMC/Ext/Midi/CC/Bit":
                    new VmcExtMidiCcBit(m);
                    break;
                case "/VMC/Ext/Hmd/Pos":
                    new VmcExtDevicePos(m);
                    break;
                case "/VMC/Ext/Con/Pos":
                    new VmcExtDevicePos(m);
                    break;
                case "/VMC/Ext/Tra/Pos":
                    new VmcExtDevicePos(m);
                    break;
                case "/VMC/Ext/Hmd/Pos/Local":
                    new VmcExtDevicePos(m);
                    break;
                case "/VMC/Ext/Con/Pos/Local":
                    new VmcExtDevicePos(m);
                    break;
                case "/VMC/Ext/Tra/Pos/Local":
                    new VmcExtDevicePos(m);
                    break;
                case "/VMC/Ext/Rcv":
                    new VmcExtRcv(m);
                    break;
                case "/VMC/Ext/Light":
                    new VmcExtLight(m);
                    break;
                case "/VMC/Ext/VRM":
                    new VmcExtVrm(m);
                    break;
                case "/VMC/Ext/Remote":
                    new VmcExtRemote(m);
                    break;
                case "/VMC/Ext/Opt":
                    new VmcExtOpt(m);
                    break;
            }
        }
    }
}
