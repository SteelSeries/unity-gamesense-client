/*
 * ScreenHandler.cs
 *
 * authors: Ellie Quirini (gabriella.quirini@steelseries.com)
 *
 * Copyright(c) 2016 SteelSeries
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

namespace SteelSeries
{

    namespace GameSense
    {
        [FullSerializer.fsObject(Converter = typeof(ScreenHandlerConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "ScreenHandler", menuName = "GameSense/Handlers/Screen Handler")]
        [System.Serializable] public class ScreenHandler : AbstractHandler {

            // device - zone
            public DeviceZone.AbstractScreenDevice_Zone deviceZone;

            // mode
            public ScreenMode mode;

            // datas
            public AbstractScreenData[] frameData;

            private static ScreenHandler _new() {
                ScreenHandler hh = UnityEngine.ScriptableObject.CreateInstance<ScreenHandler>();
                return hh;
            }

            public static ScreenHandler Create( DeviceZone.AbstractScreenDevice_Zone dz, ScreenMode mode, ScreenData[] data) {
                ScreenHandler hh = _new();
                hh.deviceZone = dz;
                hh.mode = mode;
                hh.frameData = data;
                return hh;
            }

        }

        class ScreenHandlerConverter : FullSerializer.fsDirectConverter<ScreenHandler> {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary<string, FullSerializer.fsData> data, ref ScreenHandler model ) {
                return FullSerializer.fsResult.Fail("Not implemented");
            }
            protected override FullSerializer.fsResult DoSerialize( ScreenHandler model, System.Collections.Generic.Dictionary<string, FullSerializer.fsData> serialized ) {
                SerializeMember<string>(serialized, null, "device-type", model.deviceZone.device);
                SerializeMember<string>(serialized, null, "zone", ((DeviceZone.AbstractGenericScreen_Zone)model.deviceZone).zone);
                SerializeMember<ScreenMode>(serialized, null, "mode", model.mode);
                SerializeMember<AbstractScreenData[]>(serialized, null, "datas", model.frameData);

                return FullSerializer.fsResult.Success;
            }
        }
    }
}
