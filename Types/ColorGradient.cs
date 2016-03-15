/*
 * ColorGradient.cs
 *
 * authors: Tomasz Rybiarczyk (tomasz.rybiarczyk@steelseries.com)
 *
 *
 * Copyright (c) 2016 SteelSeries
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

namespace SteelSeries {

    namespace GameSense {

        [UnityEngine.CreateAssetMenu(fileName = "ColorGradient", menuName = "GameSense/Color Effects/Gradient")]
        public class ColorGradient : AbstractColor_Nonrecursive {

            public Gradient gradient;

            public override ColorEffect ColorEffectType() {
                return ColorEffect.Gradient;
            }

            private static ColorGradient _new() {
                ColorGradient cg = UnityEngine.ScriptableObject.CreateInstance< ColorGradient >();
                return cg;
            }

            public static ColorGradient Create( Gradient gradient ) {
                ColorGradient cg = _new();
                cg.gradient = gradient;
                return cg;
            }

            public static ColorGradient Create( RGB zero, RGB hundred ) {
                ColorGradient cg = _new();
                cg.gradient = new Gradient( zero, hundred );
                return cg;
            }

        }

    }

}