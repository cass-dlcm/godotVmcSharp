using Godot;
using godotOscSharp;

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
            if (blendShape == "Joy" || blendShape == "Angry" || blendShape == "Sorrow" || blendShape == "Fun" ||
            blendShape == "A" || blendShape == "I" || blendShape == "U" || blendShape == "E" || blendShape == "O" ||
            blendShape == "Blink_L" || blendShape == "Blink_R")
            {
                name = blendShape;
                value = (float)m.Data[1].Value;
                return;
            } else if (blendShape == "happy" || blendShape == "angry" || blendShape == "sad" || blendShape == "relaxed" ||
            blendShape == "aa" || blendShape == "ih" || blendShape == "ou" || blendShape == "ee" || blendShape == "oh" ||
            blendShape == "blinkLeft" || blendShape == "blinkRight")
            {
                name = blendShape;
                value = (float)m.Data[1].Value;
                return;
            } else if (blendShape == "browInnerUp" ||
            blendShape == "browDownLeft" || blendShape == "browDownRight" ||
            blendShape == "browOuterUpLeft" || blendShape == "browOuterUpRight" ||
            blendShape == "eyeLookUpLeft" || blendShape == "eyeLookUpRight" ||
            blendShape == "eyeLookDownLeft" || blendShape == "eyeLookDownRight" ||
            blendShape == "eyeLookInLeft" || blendShape == "eyeLookInRight" ||
            blendShape == "eyeLookOutLeft" || blendShape == "eyeLookOutRight" ||
            blendShape == "eyeBlinkLeft" || blendShape == "eyeBlinkRight" ||
            blendShape == "eyeSquintLeft" || blendShape == "eyeSquintRight" ||
            blendShape == "eyeWideLeft" || blendShape == "eyeWideRight" ||
            blendShape == "cheekPuff" ||
            blendShape == "cheekSquintLeft" || blendShape == "cheekSquintRight" ||
            blendShape == "noseSneerLeft" || blendShape == "noseSneerRight" ||
            blendShape == "jawOpen" || blendShape == "jawForward" ||
            blendShape == "jawLeft" || blendShape == "jawRight" ||
            blendShape == "mouthFunnel" || blendShape == "mouthPucker" ||
            blendShape == "mouthLeft" || blendShape == "mouthRight" ||
            blendShape == "mouthRollUpper" || blendShape == "mouthRollLower" ||
            blendShape == "mouthShrugUpper" || blendShape == "mouthShrugLower" ||
            blendShape == "mouthClose" ||
            blendShape == "mouthSmileLeft" || blendShape == "mouthSmileRight" ||
            blendShape == "mouthFrownLeft" || blendShape == "mouthFrownRight" ||
            blendShape == "mouthDimpleLeft" || blendShape == "mouthDimpleRight" ||
            blendShape == "mouthUpperUpLeft" || blendShape == "mouthUpperUpRight" ||
            blendShape == "mouthLowerDownLeft" || blendShape == "mouthLowerDownRight" ||
            blendShape == "mouthPressLeft" || blendShape == "mouthPressRight" ||
            blendShape == "mouthStretchLeft" || blendShape == "mouthStretchRight" ||
            blendShape == "tongueOut")
            {
                name = blendShape;
                value = (float)m.Data[1].Value;
                return;
            } else
            {
                GD.Print($"Invalid argument for /VMC/Ext/Blend/Val. BlendShape \"{blendShape}\" not in list.");
                return;
            }
        }
    }
}