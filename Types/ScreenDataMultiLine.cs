/*
 * ScreenDataMultiLine.cs
 *
 * authors: Ellie Quirini (gabriella.quirini@steelseries.com)
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
        [FullSerializer.fsObject(Converter = typeof(ScreenDataMultiLineConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "ScreenDataMultiLine", menuName = "GameSense/Screen Data/Multi-Line")]
        public class ScreenDataMultiLine : AbstractGenericScreenData {

            public ScreenData[] lines;

            private static ScreenDataMultiLine _new() {
                ScreenDataMultiLine fd = UnityEngine.ScriptableObject.CreateInstance<ScreenDataMultiLine>();
                return fd;
            }

            public static ScreenDataMultiLine Create( ScreenData[] linesIn ) {
                ScreenDataMultiLine fd = _new();
                fd.lines = linesIn;
                return fd;
            }
        }

        class ScreenDataMultiLineConverter : FullSerializer.fsDirectConverter<ScreenDataMultiLine> {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary<string, FullSerializer.fsData> data, ref ScreenDataMultiLine model ) {
                return FullSerializer.fsResult.Fail("Not implemented");
            }

            protected override FullSerializer.fsResult DoSerialize( ScreenDataMultiLine model, System.Collections.Generic.Dictionary<string, FullSerializer.fsData> serialized ) {
                SerializeMember<ScreenData[]>(serialized, null, "lines", model.lines);

                return FullSerializer.fsResult.Success;
            }
        }
    }
}
