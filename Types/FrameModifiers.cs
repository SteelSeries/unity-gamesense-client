/*
 * FrameModifiers.cs
 *
 * authors: sharkgoesmad
 *
 *
 * Copyright (c) 2019 SteelSeries
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

        [System.Serializable] public class FrameModifiers {

            [UnityEngine.SerializeField] private bool _repeats;
            [UnityEngine.SerializeField] private System.UInt32 _repeatCount;

            public System.UInt32 length_millis;
            public System.UInt32 repeatCount {
                get { return _repeatCount; }
                set { _repeats = true; _repeatCount = value; }
            }

            public bool repeats {
                get { return _repeats; }
                set {
                    if ( value == false ) {
                        _repeatCount = 0;
                    }
                    _repeats = value;
                }
            }

            public FrameModifiers( System.UInt32 lengthMs, System.UInt32 repeatCount ) {
                length_millis = lengthMs;
                this.repeatCount = repeatCount;
            }

            public FrameModifiers( System.UInt32 lengthMs, bool repeats ) {
                length_millis = lengthMs;
                this.repeats = repeats;
            }
        }

    }

}
