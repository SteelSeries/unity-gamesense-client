/*
 * FrameDataRange.cs
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

using System.Collections.Generic;

namespace SteelSeries {

    namespace GameSense {

        [FullSerializer.fsObject(Converter = typeof(FrameDataRangeConverter))]
        [System.Serializable] public class FrameDataRange {
            public System.Int32 low;
            public System.Int32 high;
            public AbstractFrameData[] datas;

            public FrameDataRange( System.Int32 low, System.Int32 high, AbstractFrameData[] datas ) {
                this.low = low;
                this.high = high;
                this.datas = datas;
            }

            /// <summary>
            /// Allow frames to do processing before while still on the main thread.
            /// </summary>
            public void Preprocess() {
                foreach ( var d in datas ) {
                    d.Preprocess();
                }
            }
        }

        class FrameDataRangeConverter : Converter< FrameDataRange > {
            protected override FullSerializer.fsResult DoSerialize( FrameDataRange model, Dictionary< string, FullSerializer.fsData > serialized ) {

                SerializeMember( serialized, null, "low", model.low );
                SerializeMember( serialized, null, "high", model.high );
                SerializeMember( serialized, null, "datas", model.datas );

                return FullSerializer.fsResult.Success;
            }
        }

    }

}
