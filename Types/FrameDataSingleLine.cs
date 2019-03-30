/*
 * FrameDataSingleLine.cs
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

        [FullSerializer.fsObject(Converter = typeof(FrameDataSingleLineConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "FrameDataSingleLine", menuName = "GameSense/Screen Data/Frame Data/Single Line")]
        [System.Serializable] public class FrameDataSingleLine : AbstractFrameData {
            public LineData lineData;
            public EventIconId iconId;
            public FrameModifiers frameModifiers;

            public FrameDataSingleLine( LineData lineData, FrameModifiers frameModifiers, EventIconId iconId = EventIconId.Default ) {
                this.lineData = lineData;
                this.frameModifiers = frameModifiers;
                this.iconId = iconId;
            }
        }

        class FrameDataSingleLineConverter : FullSerializer.fsDirectConverter< FrameDataSingleLine > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref FrameDataSingleLine model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( FrameDataSingleLine model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {

                //switch ( model.lineData.type ) {

                //    case LineData.Type.Text:
                //        LineDataText ldtxt = model.lineData.AsText;
                //        SerializeMember( serialized, null, "has-text", ldtxt.has_text );
                //        SerializeMember( serialized, null, "prefix", ldtxt.prefix );
                //        SerializeMember( serialized, null, "suffix", ldtxt.suffix );
                //        SerializeMember( serialized, null, "bold", ldtxt.bold );
                //        SerializeMember( serialized, null, "wrap", ldtxt.wrap );
                //        break;

                //    case LineData.Type.ProgressBar:
                //        SerializeMember( serialized, null, "has-progress-bar",  LineDataProgressBar.has_progress_bar );
                //        break;

                //}
                model.lineData.DecorateSerialized( serialized );

                SerializeMember( serialized, null, "icon-id", ( System.UInt32 )model.iconId );

                //switch ( model.lineData.accessor.type ) {

                //    case LineDataAccessor.Type.FrameKey:
                //        SerializeMember( serialized, null, "context-frame-key", model.lineData.accessor.value );
                //        break;
                    
                //    case LineDataAccessor.Type.GoLispExpr:
                //        SerializeMember( serialized, null, "arg", model.lineData.accessor.value );
                //        break;

                //}

                SerializeMember( serialized, null, "length-millis",  model.frameModifiers.length_millis );

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
