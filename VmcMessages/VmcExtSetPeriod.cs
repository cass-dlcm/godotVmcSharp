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
using System.Collections.Generic;

namespace godotVmcSharp
{
    public class VmcExtSetPeriod : VmcMessage
    {
        public int status { get; }
        public int root { get; }
        public int bone { get; }
        public int blendShape { get; }
        public int camera { get; }
        public int devices { get; }

        public VmcExtSetPeriod(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 6)
            {
                GD.Print($"Invalid number of arguments for {addr}. Expecting 6, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "status", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "root", 'i', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "bone", 'i', m.Data[2].Type));
                return;
            }
            if (m.Data[3].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "blendShape", 'i', m.Data[3].Type));
                return;
            }
            if (m.Data[4].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "camera", 'i', m.Data[4].Type));
                return;
            }
            if (m.Data[5].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "devices", 'i', m.Data[5].Type));
                return;
            }
            status = (int)m.Data[0].Value;
            root = (int)m.Data[1].Value;
            bone = (int)m.Data[2].Value;
            blendShape = (int)m.Data[3].Value;
            camera = (int)m.Data[4].Value;
            devices = (int)m.Data[5].Value;
        }

        public VmcExtSetPeriod(int _status, int _root, int _bone, int _blendShape, int _camera, int _devices) : base(new godotOscSharp.Address("/VMC/Ext/Set/Period"))
        {
            status = _status;
            root = _root;
            bone = _bone;
            blendShape = _blendShape;
            camera = _camera;
            devices = _devices;
        }

        public godotOscSharp.OscMessage ToMessage()
        {
            return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{
                new godotOscSharp.OscArgument(status, 'i'),
                new godotOscSharp.OscArgument(root, 'i'),
                new godotOscSharp.OscArgument(bone, 'i'),
                new godotOscSharp.OscArgument(blendShape, 'i'),
                new godotOscSharp.OscArgument(camera, 'i'),
                new godotOscSharp.OscArgument(devices, 'i'),
            });
        }
    }
}