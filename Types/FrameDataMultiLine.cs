/*
 * FrameDataMultiLine.cs
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

        [FullSerializer.fsObject(Converter = typeof(FrameDataMultiLineConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "FrameDataMultiLine", menuName = "GameSense/Screen Data/Frame Data/Multi Line")]
        [System.Serializable] public class FrameDataMultiLine : AbstractFrameData {
            public LineData[] lines;
            public EventIconId iconId;
            public FrameModifiers frameModifiers;

            private static FrameDataMultiLine _new() {
                var fd = CreateInstance< FrameDataMultiLine >();
                return fd;
            }

            public static FrameDataMultiLine Create( LineData[] lines, FrameModifiers frameModifiers, EventIconId iconId = EventIconId.Default ) {
                var fd = _new();
                fd.lines = lines;
                fd.frameModifiers = frameModifiers;
                fd.iconId = iconId;
                return fd;
            }
        }

        class FrameDataMultiLineConverter : Converter< FrameDataMultiLine > {
            protected override FullSerializer.fsResult DoSerialize( FrameDataMultiLine model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {

                SerializeMember( serialized, null, "lines", model.lines );
                SerializeMember( serialized, null, "icon-id", ( System.UInt32 )model.iconId );
                SerializeMember( serialized, null, "length-millis", model.frameModifiers.length_millis );

                if ( model.frameModifiers.repeats && model.frameModifiers.repeatCount != 0 ) {
                    SerializeMember( serialized, null, "repeats",  model.frameModifiers.repeatCount );
                } else {
                    SerializeMember( serialized, null, "repeats",  model.frameModifiers.repeats );
                }

                return FullSerializer.fsResult.Success;
            }
        }

    }

}
