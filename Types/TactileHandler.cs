/*
 * TactileHandler.cs
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

        // TODO
        // deivce-type: tactile
        // mode: "vibrate"
        // pattern: pattern data - always an array, delay optional or 0 for no delay
        // rate: same as color range
        // zone: required "one"

        [FullSerializer.fsObject(Converter = typeof(TactileHandlerConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "TactileHandler", menuName = "GameSense/Handlers/Tactile Handler")]
        [System.Serializable] public class TactileHandler : AbstractHandler {

            private TactilePatternType _tactilePatternType;
            public TactilePatternType TactilePatternType { get { return _tactilePatternType; } }
            private RateMode _rateMode;
            public RateMode RateMode { get {return _rateMode;} }

            // device - zone
            public DeviceZone.AbstractTactileDevice_Zone deviceZone;

            // mode
            public TactileMode mode;

            // pattern
            public AbstractTactilePattern pattern;
            public TactilePatternSimple pattern_simple { get { return pattern as TactilePatternSimple; } set { pattern = value; _tactilePatternType = TactilePatternType.Simple; } }
            public TactilePatternCustom pattern_custom { get { return pattern as TactilePatternCustom; } set { pattern = value; _tactilePatternType = TactilePatternType.Custom; } }
            public TactilePatternRange pattern_range { get { return pattern as TactilePatternRange; } set { pattern = value; _tactilePatternType = TactilePatternType.Range; } }

            // rate
            public AbstractRate rate;
            public RateStatic rate_static { get {return rate as RateStatic;} set {rate = value; _rateMode = RateMode.Static;} }
            public RateRange rate_range { get {return rate as RateRange;} set {rate = value; _rateMode = RateMode.Range;} }

            private static TactileHandler _new() {
                TactileHandler hh = UnityEngine.ScriptableObject.CreateInstance< TactileHandler >();
                return hh;
            }

            private static TactileHandler Create( DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode ) {
                TactileHandler hh = _new();
                hh.deviceZone = dz;
                hh.mode = mode;
                return hh;
            }

            public static TactileHandler Create( DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectSimple[] pattern ) {
                TactileHandler hh = Create( dz, mode );
                hh.pattern_simple = TactilePatternSimple.Create( pattern );
                return hh;
            }
            public static TactileHandler Create( DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectCustom[] pattern ) {
                TactileHandler hh = Create( dz, mode );
                hh.pattern_custom = TactilePatternCustom.Create( pattern );
                return hh;
            }
            public static TactileHandler Create(DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectRange[] pattern)
            {
                TactileHandler hh = Create(dz, mode);
                hh.pattern_range = TactilePatternRange.Create( pattern );
                return hh;
            }

            public static TactileHandler Create( DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectSimple[] pattern, RateStatic rate ) {
                TactileHandler hh = Create( dz, mode, pattern );
                hh.rate_static = rate;
                return hh;
            }
            public static TactileHandler Create( DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectCustom[] pattern, RateStatic rate ) {
                TactileHandler hh = Create( dz, mode, pattern );
                hh.rate_static = rate;
                return hh;
            }
            public static TactileHandler Create(DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectRange[] pattern, RateStatic rate)
            {
                TactileHandler hh = Create(dz, mode, pattern);
                hh.rate_static = rate;
                return hh;
            }

            public static TactileHandler Create( DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectSimple[] pattern, RateRange rate ) {
                TactileHandler hh = Create( dz, mode, pattern );
                hh.rate_range = rate;
                return hh;
            }
            public static TactileHandler Create( DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectCustom[] pattern, RateRange rate ) {
                TactileHandler hh = Create( dz, mode, pattern );
                hh.rate_range = rate;
                return hh;
            }
            public static TactileHandler Create(DeviceZone.AbstractTactileDevice_Zone dz, TactileMode mode, TactileEffectRange[] pattern, RateRange rate)
            {
                TactileHandler hh = Create(dz, mode, pattern);
                hh.rate_range = rate;
                return hh;
            }
        }


        class TactileHandlerConverter : FullSerializer.fsDirectConverter< TactileHandler > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref TactileHandler model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( TactileHandler model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                // TODO check result of each
                SerializeMember< string >( serialized, null, "device-type", model.deviceZone.device );
                SerializeMember< string >( serialized, null, "zone", ((DeviceZone.AbstarctGenericTactile_Zone)model.deviceZone).zone );
                SerializeMember< TactileMode >( serialized, null, "mode", model.mode );

                switch ( model.pattern.PatternType() ) {
                    case TactilePatternType.Simple: SerializeMember< TactileEffectSimple[] >( serialized, null, "pattern", model.pattern_simple.pattern ); break;
                    case TactilePatternType.Custom: SerializeMember< TactileEffectCustom[] >( serialized, null, "pattern", model.pattern_custom.pattern ); break;
                    case TactilePatternType.Range: SerializeMember< TactileEffectRange[] >( serialized, null, "pattern", model.pattern_range.pattern ); break;
                }

                switch ( model.RateMode ) {
                    case RateMode.Static: SerializeMember< RateStatic >( serialized, null, "rate", model.rate_static ); break;
                    case RateMode.Range: SerializeMember< RateRange >( serialized, null, "rate", model.rate_range ); break;
                }

                return FullSerializer.fsResult.Success;
            }
        }
    }
}