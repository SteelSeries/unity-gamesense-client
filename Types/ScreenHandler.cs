/*
 * ScreenHandler.cs
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

        [FullSerializer.fsObject(Converter = typeof(ScreenHandlerConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "ScreenHandler", menuName = "GameSense/Handlers/Screen Handler")]
        [System.Serializable] public class ScreenHandler : AbstractHandler {
            private ScreenDataType _screenDataType { get; set; }
            public ScreenDataType screenDataType {
                get { return _screenDataType; }
            }

            // device - zone
            public DeviceZone.AbstractScreenDevice_Zone deviceZone;

            // mode
            public ScreenMode mode;

            // datas
            public AbstractScreenData datas;
            public ScreenDataStatic data_static {
                get { return datas as ScreenDataStatic; }
                set { datas = value; _screenDataType = ScreenDataType.Static; }
            }

            public ScreenDataRanges data_ranges {
                get { return datas as ScreenDataRanges; }
                set { datas = value; _screenDataType = ScreenDataType.Range; }
            }

            private static ScreenHandler _new() {
                ScreenHandler sh = CreateInstance< ScreenHandler >();
                return sh;
            }

            private static ScreenHandler Create( DeviceZone.AbstractScreenDevice_Zone dz, ScreenMode mode ) {
                ScreenHandler sh = _new();
                sh.deviceZone = dz;
                sh.mode = mode;
                return sh;
            }

            public static ScreenHandler Create( DeviceZone.AbstractScreenDevice_Zone dz, ScreenMode mode, AbstractFrameData[] datas ) {
                ScreenHandler sh = Create( dz, mode );
                sh.data_static = ScreenDataStatic.Create( datas );
                return sh;
            }

            public static ScreenHandler Create( DeviceZone.AbstractScreenDevice_Zone dz, ScreenMode mode, FrameDataRange[] datas ) {
                ScreenHandler sh = Create( dz, mode );
                sh.data_ranges = ScreenDataRanges.Create( datas );
                return sh;
            }

            /// <summary>
            /// Invoke Preprocess for each contained frame data.
            /// Needs to be called on the main thread (from Monobehaviour script).
            /// </summary>
            public override void Preprocess() {
                switch ( screenDataType ) {

                    case ScreenDataType.Static:
                        foreach ( var d in data_static.datas ) {
                            d.Preprocess();
                        }
                        break;

                    case ScreenDataType.Range:
                        foreach ( var d in data_ranges.datas ) {
                            d.Preprocess();
                        }
                        break;

                }
            }
        }


        class ScreenHandlerConverter : FullSerializer.fsDirectConverter< ScreenHandler > {
            protected override FullSerializer.fsResult DoDeserialize( Dictionary< string, FullSerializer.fsData > data, ref ScreenHandler model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( ScreenHandler model, Dictionary< string, FullSerializer.fsData > serialized ) {

                SerializeMember( serialized, null, "device-type", model.deviceZone.device );
                SerializeMember( serialized, null, "zone", ((DeviceZone.AbstractGenericScreen_Zone)model.deviceZone).zone );
                SerializeMember( serialized, null, "mode", model.mode );

                switch ( model.screenDataType ) {
                    case ScreenDataType.Static: SerializeMember( serialized, null, "datas", model.data_static.datas ); break;
                    case ScreenDataType.Range: SerializeMember( serialized, null, "datas", model.data_ranges.datas ); break;
                }

                return FullSerializer.fsResult.Success;
            }
        }

    }

}
