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
    public class VmcExtBlendVal : VmcMessage
    {
        public string name { get; }
        public float value { get; }
        public VmcExtBlendVal(godotOscSharp.OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 2) {
                GD.Print($"Invalid number of arguments for /VMC/Ext/Blend/Val. Expected 2, received {m.Data.Count}.");
                return;
            }
            if (m.Data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "name", 's', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(addr, "value", 'f', m.Data[1].Type));
                return;
            }
            var blendShape = (string)m.Data[0].Value;
            if (isVrm0BlendShape(blendShape))
            {
                name = blendShape;
                value = (float)m.Data[1].Value;
                return;
            }
            if (isVrm1Expression(blendShape))
            {
                name = blendShape;
                value = (float)m.Data[1].Value;
                return;
            }
            if (isArkitBlendShape(blendShape))
            {
                name = blendShape;
                value = (float)m.Data[1].Value;
                return;
            }
            GD.Print($"Invalid argument for {addr}. BlendShape \"{blendShape}\" not in list.");
        }

        public VmcExtBlendVal(string _name, float _value) : base(new godotOscSharp.Address("VMC/Ext/Blend/Val"))
        {
            if (isVrm0BlendShape(_name))
            {
                name = _name;
                value = _value;
                return;
            }
            if (isVrm1Expression(_name))
            {
                name = _name;
                value = _value;
                return;
            }
            if (isArkitBlendShape(_name))
            {
                name = _name;
                value = _value;
                return;
            }
            GD.Print($"Invalid argument for {addr}. BlendShape \"{_name}\" not in list.");
        }

        private bool isVrm0BlendShape(string name)
        {
            return name == "Joy" || name == "Angry" || name == "Sorrow" || name == "Fun" ||
               name == "A" || name == "I" || name == "U" || name == "E" || name == "O" ||
               name == "Blink_L" || name == "Blink_R";
        }

        private bool isVrm1Expression(string name)
        {
            return name == "happy" || name == "angry" || name == "sad" || name == "relaxed" ||
               name == "aa" || name == "ih" || name == "ou" || name == "ee" || name == "oh" ||
               name == "blinkLeft" || name == "blinkRight";
        }

        private bool isArkitBlendShape(string name)
        {
            return name == "browInnerUp" ||
                name == "browDownLeft" || name == "browDownRight" ||
                name == "browOuterUpLeft" || name == "browOuterUpRight" ||
                name == "eyeLookUpLeft" || name == "eyeLookUpRight" ||
                name == "eyeLookDownLeft" || name == "eyeLookDownRight" ||
                name == "eyeLookInLeft" || name == "eyeLookInRight" ||
                name == "eyeLookOutLeft" || name == "eyeLookOutRight" ||
                name == "eyeBlinkLeft" || name == "eyeBlinkRight" ||
                name == "eyeSquintLeft" || name == "eyeSquintRight" ||
                name == "eyeWideLeft" || name == "eyeWideRight" ||
                name == "cheekPuff" ||
                name == "cheekSquintLeft" || name == "cheekSquintRight" ||
                name == "noseSneerLeft" || name == "noseSneerRight" ||
                name == "jawOpen" || name == "jawForward" ||
                name == "jawLeft" || name == "jawRight" ||
                name == "mouthFunnel" || name == "mouthPucker" ||
                name == "mouthLeft" || name == "mouthRight" ||
                name == "mouthRollUpper" || name == "mouthRollLower" ||
                name == "mouthShrugUpper" || name == "mouthShrugLower" ||
                name == "mouthClose" ||
                name == "mouthSmileLeft" || name == "mouthSmileRight" ||
                name == "mouthFrownLeft" || name == "mouthFrownRight" ||
                name == "mouthDimpleLeft" || name == "mouthDimpleRight" ||
                name == "mouthUpperUpLeft" || name == "mouthUpperUpRight" ||
                name == "mouthLowerDownLeft" || name == "mouthLowerDownRight" ||
                name == "mouthPressLeft" || name == "mouthPressRight" ||
                name == "mouthStretchLeft" || name == "mouthStretchRight" ||
                name == "tongueOut";
        }
        public godotOscSharp.OscMessage ToMessage()
        {
            return new godotOscSharp.OscMessage(addr, new List<godotOscSharp.OscArgument>{new godotOscSharp.OscArgument(name, 's'), new godotOscSharp.OscArgument(value, 'f')});
        }
    }
}