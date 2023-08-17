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
    public class VmcExtSetPeriod : VmcMessage
    {
        public int Status { get; }
        public int Root { get; }
        public int Bone { get; }
        public int BlendShape { get; }
        public int Camera { get; }
        public int Devices { get; }

        public VmcExtSetPeriod(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 6)
            {
                GD.Print($"Invalid number of arguments for {Addr}. Expecting 6, received {m.Data.Count}");
                return;
            }
            if (m.Data[0].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "status", 'i', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "root", 'i', m.Data[1].Type));
                return;
            }
            if (m.Data[2].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "bone", 'i', m.Data[2].Type));
                return;
            }
            if (m.Data[3].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "blendShape", 'i', m.Data[3].Type));
                return;
            }
            if (m.Data[4].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "camera", 'i', m.Data[4].Type));
                return;
            }
            if (m.Data[5].Type != 'i')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "devices", 'i', m.Data[5].Type));
                return;
            }
            Status = (int)m.Data[0].Value;
            Root = (int)m.Data[1].Value;
            Bone = (int)m.Data[2].Value;
            BlendShape = (int)m.Data[3].Value;
            Camera = (int)m.Data[4].Value;
            Devices = (int)m.Data[5].Value;
        }

        public VmcExtSetPeriod(int status, int root, int bone, int blendShape, int camera, int devices) : base(new OscAddress("/VMC/Ext/Set/Period"))
        {
            Status = status;
            Root = root;
            Bone = bone;
            BlendShape = blendShape;
            Camera = camera;
            Devices = devices;
        }

        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument>{
                new OscArgument(Status, 'i'),
                new OscArgument(Root, 'i'),
                new OscArgument(Bone, 'i'),
                new OscArgument(BlendShape, 'i'),
                new OscArgument(Camera, 'i'),
                new OscArgument(Devices, 'i'),
            });
        }
    }
}