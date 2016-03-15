/*
 * ColorHandler.cs
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

        [FullSerializer.fsObject(Converter = typeof(ColorHandlerConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "ColorHandler", menuName = "GameSense/Handlers/Color Handler")]
        public class ColorHandler : AbstractHandler {

            // books
            private ColorEffect _colorEffect;
            public ColorEffect ColorEffect { get {return _colorEffect;} }
            private RateMode _rateMode;
            public RateMode RateMode { get {return _rateMode;} }

            // device type
            public DeviceZone.AbstractIlluminationDevice_Zone deviceZone;

            // mode
            public IlluminationMode mode;   // anything other then Color makes sense only for perkey devices

            // color
            public AbstractColor color;
            public ColorStatic color_static { get {return color as ColorStatic;} set {color = value; _colorEffect = ColorEffect.Static;} }
            public ColorGradient color_gradient { get {return color as ColorGradient;} set {color = value; _colorEffect = ColorEffect.Gradient;} }
            public ColorRanges color_range { get {return color as ColorRanges;} set {color = value; _colorEffect = ColorEffect.Range;} }

            // rate
            public AbstractRate rate;
            public RateStatic rate_static { get {return rate as RateStatic;} set {rate = value; _rateMode = RateMode.Static;} }
            public RateRange rate_range { get {return rate as RateRange;} set {rate = value; _rateMode = RateMode.Range;} }


            private static ColorHandler _new() {
                ColorHandler ch = UnityEngine.ScriptableObject.CreateInstance< ColorHandler >();
                return ch;
            }

            private static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode ) {
                ColorHandler ch = _new();
                ch.deviceZone = dz;
                ch.mode = mode;
                ch._rateMode = RateMode.None;
                return ch;
            }

            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorStatic color) {
                ColorHandler ch = Create( dz, mode );
                ch.color_static = color;
                return ch;
            }
            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorStatic color, RateStatic rate ) {
                ColorHandler ch = Create( dz, mode );
                ch.color_static = color;
                ch.rate_static = rate;
                return ch;
            }
            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorStatic color, RateRange rate ) {
                ColorHandler ch = Create( dz, mode );
                ch.color_static = color;
                ch.rate_range = rate;
                return ch;
            }

            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorGradient color) {
                ColorHandler ch = Create( dz, mode );
                ch.color_gradient = color;
                return ch;
            }
            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorGradient color, RateStatic rate ) {
                ColorHandler ch = Create( dz, mode );
                ch.color_gradient = color;
                ch.rate_static = rate;
                return ch;
            }
            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorGradient color, RateRange rate ) {
                ColorHandler ch = Create( dz, mode );
                ch.color_gradient = color;
                ch.rate_range = rate;
                return ch;
            }

            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorRanges color) {
                ColorHandler ch = Create( dz, mode );
                ch.color_range = color;
                return ch;
            }
            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorRanges color, RateStatic rate ) {
                ColorHandler ch = Create( dz, mode );
                ch.color_range = color;
                ch.rate_static = rate;
                return ch;
            }
            public static ColorHandler Create( DeviceZone.AbstractIlluminationDevice_Zone dz, IlluminationMode mode, ColorRanges color, RateRange rate ) {
                ColorHandler ch = Create( dz, mode );
                ch.color_range = color;
                ch.rate_range = rate;
                return ch;
            }

        }


        class ColorHandlerConverter : FullSerializer.fsDirectConverter< ColorHandler > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref ColorHandler model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( ColorHandler model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                // TODO check result of each
                SerializeMember< string >( serialized, null, "device-type", model.deviceZone.device );

                if ( model.deviceZone.HasCustomZone() ) {
                    SerializeMember< byte[] >( serialized, null, "custom-zone-keys", ((DeviceZone.AbstractIlluminationDevice_CustomZone)model.deviceZone).zone );
                } else {
                    SerializeMember< string >( serialized, null, "zone", ((DeviceZone.AbstractIlluminationDevice_StandardZone)model.deviceZone).zone );
                }

                switch ( model.mode ) {
                    case IlluminationMode.Color: SerializeMember< string >( serialized, null, "mode", "color" ); break;
                    case IlluminationMode.Percent: SerializeMember< string >( serialized, null, "mode", "percent" ); break;
                    case IlluminationMode.Count: SerializeMember< string >( serialized, null, "mode", "count" ); break;
                }

                switch ( model.color.ColorEffectType() ) {
                    case ColorEffect.Static: SerializeMember< ColorStatic >( serialized, null, "color", model.color_static ); break;
                    case ColorEffect.Gradient: SerializeMember< ColorGradient >( serialized, null, "color", model.color_gradient ); break;
                    case ColorEffect.Range: SerializeMember< ColorRange[] >( serialized, null, "color", model.color_range.ranges ); break;
                }

                SerializeMember< AbstractRate >( serialized, null, "rate", model.rate );

                return FullSerializer.fsResult.Success;
            }
        }
    }
}
