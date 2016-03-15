/*
 * RateRange.cs
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

        [FullSerializer.fsObject(Converter = typeof(RateRangeConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "RateRanges", menuName = "GameSense/Rate/Ranges")]
        public class RateRange : AbstractRate {

            public Frequency[] frequency;
            public RepeatLimit[] repeatLimit;

            private static RateRange _new() {
                RateRange cr = UnityEngine.ScriptableObject.CreateInstance< RateRange >();
                return cr;
            }

            private static RateRange Create( System.UInt32 size ) {
                RateRange rr = _new();
                rr.frequency = new Frequency[ size ];
                rr.repeatLimit = new RepeatLimit[ size ];
                return rr;
            }

            public static RateRange Create( Frequency[] frequency, RepeatLimit[] repeatLimit = null ) {
                RateRange rr = _new();
                rr.frequency = frequency;
                rr.repeatLimit = repeatLimit;
                return rr;
            }

            public static RateRange Create( FreqRepeatLimitPair[] pairs ) {
                RateRange rr = Create( (System.UInt32)pairs.Length );

                System.UInt32 idx = 0;
                foreach ( FreqRepeatLimitPair pair in pairs ) {
                    rr.frequency[idx].low = pair.low;
                    rr.frequency[idx].high = pair.high;
                    rr.frequency[idx].frequency = pair.frequency;
                    rr.repeatLimit[idx].low = pair.low;
                    rr.repeatLimit[idx].high = pair.high;
                    rr.repeatLimit[idx].repeatLimit = pair.repeatLimit;
                    ++idx;
                }

                return rr;
            }

        }


        class RateRangeConverter : FullSerializer.fsDirectConverter< RateRange > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref RateRange model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( RateRange model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                // TODO check result of each
                SerializeMember< Frequency[] >( serialized, null, "frequency", model.frequency );
                if ( model.repeatLimit != null ) {
                    SerializeMember< RepeatLimit[] >( serialized, null, "repeat_limit", model.repeatLimit );
                }

                return FullSerializer.fsResult.Success;
            }

        }

    }

}