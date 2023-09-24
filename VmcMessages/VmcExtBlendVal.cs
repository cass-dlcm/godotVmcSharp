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
    public class VmcExtBlendVal : VmcMessage
    {
        public readonly string Name;
        public readonly float Value;
        public VmcExtBlendVal(OscMessage m) : base(m.Address)
        {
            if (m.Data.Count != 2)
            {
                GD.Print($"Invalid number of arguments for /VMC/Ext/Blend/Val. Expected 2, received {m.Data.Count}.");
                return;
            }
            if (m.Data[0].Type != 's')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "name", 's', m.Data[0].Type));
                return;
            }
            if (m.Data[1].Type != 'f')
            {
                GD.Print(InvalidArgumentType.GetErrorString(Addr, "value", 'f', m.Data[1].Type));
                return;
            }
            var blendShape = (string)m.Data[0].Value;
            if (IsVrm0BlendShape(blendShape))
            {
                Name = blendShape;
                Value = (float)m.Data[1].Value;
                return;
            }
            if (IsVrm1Expression(blendShape))
            {
                Name = blendShape;
                Value = (float)m.Data[1].Value;
                return;
            }
            if (IsArkitBlendShape(blendShape))
            {
                Name = blendShape;
                Value = (float)m.Data[1].Value;
                return;
            }
            GD.Print($"Invalid argument for {Addr}. BlendShape \"{blendShape}\" not in list.");
        }

        public VmcExtBlendVal(string name, float value) : base(new OscAddress("VMC/Ext/Blend/Val"))
        {
            if (IsVrm0BlendShape(name))
            {
                Name = name;
                Value = value;
                return;
            }
            if (IsVrm1Expression(name))
            {
                Name = name;
                Value = value;
                return;
            }
            if (IsArkitBlendShape(name))
            {
                Name = name;
                Value = value;
                return;
            }
            GD.Print($"Invalid argument for {Addr}. BlendShape \"{name}\" not in list.");
        }

        private bool IsVrm0BlendShape(string name)
        {
            return name == "Joy" || name == "Angry" || name == "Sorrow" || name == "Fun" ||
               name == "A" || name == "I" || name == "U" || name == "E" || name == "O" ||
               name == "Blink_L" || name == "Blink_R";
        }

        private bool IsVrm1Expression(string name)
        {
            return name == "happy" || name == "angry" || name == "sad" || name == "relaxed" ||
               name == "aa" || name == "ih" || name == "ou" || name == "ee" || name == "oh" ||
               name == "blinkLeft" || name == "blinkRight";
        }

        private bool IsArkitBlendShape(string name)
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
        public new OscMessage ToMessage()
        {
            return new OscMessage(Addr, new System.Collections.Generic.List<OscArgument> {
                new OscArgument(Name, 's'),
                new OscArgument(Value, 'f')
            });
        }
    }
}