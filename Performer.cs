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

using System.Net;
using Godot;
using godotOscSharp;

namespace godotVmcSharp
{
    public class Performer
    {
        private OscReceiver receiver;
        private OscSender sender;
        private CameraReceiver cam;
        private DeviceReceiver devices;
        private DirectionalLightReceiver lights;
        public Performer(IPAddress host, int port)
        {
            receiver = new OscReceiver(port);
            sender = new OscSender(host, port);
            receiver.MessageReceived += (sender, e) =>
            {
                GD.Print($"Received a message from {e.IPAddress}:{e.Port}");
                ProcessMessage(e.Message);
            };
            receiver.ErrorReceived += (sender, e) =>
            {
                GD.Print($"Error: {e.ErrorMessage}");
            };
            devices = new DeviceReceiver();
            lights = new DirectionalLightReceiver();
        }
        private void ProcessMessage(OscMessage m)
        {
            switch (m.Address.ToString())
            {
                case "/VMC/Ext/Hmd/Pos":
                case "/VMC/Ext/Con/Pos":
                case "/VMC/Ext/Tra/Pos":
                    this.devices.ProcessMessage(new VmcExtDevicePos(m));
                    break;
                case "/VMC/Ext/Set/Period":
                    new VmcExtSetPeriod(m);
                    break;
                case "/VMC/Ext/Midi/CC/Val":
                    new VmcExtMidiCcVal(m);
                    break;
                case "/VMC/Ext/Cam":
                    this.cam.ProcessMessage(new VmcExtCam(m));
                    break;
                case "/VMC/Ext/Blend/Val":
                    new VmcExtBlendVal(m);
                    break;
                case "/VMC/Ext/Blend/Apply":
                    new VmcMessage(m.Address);
                    break;
                case "/VMC/Ext/Set/Eye":
                    new VmcExtSetEye(m);
                    break;
                case "/VMC/Ext/Set/Req":
                    new VmcMessage(m.Address);
                    break;
                case "/VMC/Ext/Set/Res":
                    new VmcExtSetRes(m);
                    break;
                case "/VMC/Ext/Set/Calib/Ready":
                    new VmcMessage(m.Address);
                    break;
                case "/VMC/Ext/Set/Calib/Exec":
                    new VmcExtSetCalibExec(m);
                    break;
                case "/VMC/Ext/Set/Config":
                    new VmcExtSetConfig(m);
                    break;
                case "/VMC/Ext/Light":
                    this.lights.ProcessMessage(new VmcExtLight(m));
                    break;
                case "/VMC/Ext/Set/Shortcut":
                    new VmcExtSetShortcut(m);
                    break;
            }
        }
    }
}