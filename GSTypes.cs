/*
 * GSTypes.cs
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

        public enum IconColor {
            Orange      = 0,
            Gold        = 1,
            Yellow      = 2,
            Green       = 3,
            Teal        = 4,
            LightBlue   = 5,
            Blue        = 6,
            Purple      = 7,
            Fuschia     = 8,
            Pink        = 9,
            Red         = 10,
            Silver      = 11
        }


        public enum EventIconId {
            Default     = 0,    // Blank display, no icon
            Health      = 1,    // Health
            Armor       = 2,    // Armor
            Ammo        = 3,    // Ammo/Ammunition
            Money       = 4,    // Money
            Flashbang   = 5,    // Flash/Flashbang/Explosion
            Kills       = 6,    // Kills
            Headshot    = 7,    // Headshot
            Helmet      = 8,    // Helmet
            Hunger      = 10,   // Hunger
            Air         = 11,   // Air/Breath
            Compass     = 12,   // Compass
            Tool        = 13,   // Tool/Pickaxe
            Mana        = 14,   // Mana/Potion
            Clock       = 15,   // Clock
            Lightning   = 16,   // Lightning
            Item        = 17    // Item/Backpack
        }


        [System.Serializable] public struct EventData {
            public System.Int32 value;
        }




        // ******************** RATE ********************

        public enum RateMode {
            None = 0,
            Static,
            Range
        }


        [System.Serializable] public struct Frequency {
            public System.UInt32 low;
            public System.UInt32 high;
            public System.UInt32 frequency;

            public Frequency( System.UInt32 low, System.UInt32 high, System.UInt32 frequency ) {
                this.low = low; this.high = high; this.frequency = frequency;
            }
        }


        [FullSerializer.fsObject(Converter = typeof(RepeatLimitConverter))]
        [System.Serializable] public struct RepeatLimit {
            public System.UInt32 low;
            public System.UInt32 high;
            public System.UInt32 repeatLimit;

            public RepeatLimit( System.UInt32 low, System.UInt32 high, System.UInt32 repeatLimit ) {
                this.low = low; this.high = high; this.repeatLimit = repeatLimit;
            }
        }


        [System.Serializable] public struct FreqRepeatLimitPair {
            public System.UInt32 low;
            public System.UInt32 high;
            public System.UInt32 frequency;
            public System.UInt32 repeatLimit;

            public FreqRepeatLimitPair( System.UInt32 low, System.UInt32 high, System.UInt32 frequency, System.UInt32 repeatLimit ) {
                this.low = low; this.high = high; this.frequency = frequency; this.repeatLimit = repeatLimit;
            }
        }


        class RepeatLimitConverter : FullSerializer.fsDirectConverter< RepeatLimit > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref RepeatLimit model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( RepeatLimit model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                // TODO check result of each
                SerializeMember< System.UInt32 >( serialized, null, "low", model.low );
                SerializeMember< System.UInt32 >( serialized, null, "high", model.high );
                SerializeMember< System.UInt32 >( serialized, null, "repeat_limit", model.repeatLimit );

                return FullSerializer.fsResult.Success;
            }
        }




        // ******************** DEVICE - ZONE ********************

        // https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/standard-zones.md
        namespace DeviceZone {

            class IlluminationZone {
                public const string FunctionKeys = "function-keys";
                public const string MainKeyboard = "main-keyboard";
                public const string Keypad = "keypad";
                public const string NumberKeys = "number-keys";
                public const string MacroKeys = "macro-keys";

                public const string Wheel = "wheel";
                public const string Logo = "logo";
                public const string Base = "base";

                public const string Earcups = "earcups";

                public const string One = "one";
                public const string Two = "two";
                public const string Three = "three";
                public const string Four = "four";
                public const string Five = "five";

                // only for per key devices
                public const string RowQ = "q-row";
                public const string RowA = "a-row";
                public const string RowZ = "z-row";
                public const string AllMacroKeys = "all-macro-keys";
                public const string NavCluster = "nav-cluster";
                public const string Arrows = "arrows";
                public const string KeypadNumbers = "keypad-nums";
                public const string All = "all";
            }


            class TactileZone {
                public const string One = "one";
            }


            abstract public class AbstractDevice_Zone : UnityEngine.ScriptableObject {
                protected string _device;
                public string device { get { return _device; } }

                public AbstractDevice_Zone( string device ) { _device = device; }
            }


            abstract public class AbstractIlluminationDevice_Zone : AbstractDevice_Zone {
                public AbstractIlluminationDevice_Zone( string device ) : base( device ) { }

                public abstract bool HasCustomZone();
            }


            abstract public class AbstractIlluminationDevice_StandardZone : AbstractIlluminationDevice_Zone {
                protected string _zone;
                public string zone { get { return _zone; } }

                public AbstractIlluminationDevice_StandardZone( string device, string zone ) : base( device ) { _zone = zone; }

                public override bool HasCustomZone() { return false; }
            }

            abstract public class AbstractIlluminationDevice_CustomZone : AbstractIlluminationDevice_Zone {
                public byte[] zone;

                public AbstractIlluminationDevice_CustomZone( string device, byte[] zone ) : base( device ) { this.zone = zone; }

                public override bool HasCustomZone() { return true; }
            }

            // keyboard
            public class Keyboard : AbstractIlluminationDevice_StandardZone { public Keyboard( string zone ) : base( "keyboard", zone ) {} };

            // mouse
            public class Mouse : AbstractIlluminationDevice_StandardZone { public Mouse( string zone ) : base( "mouse", zone ) {} };

            // headset
            public class Headset : AbstractIlluminationDevice_StandardZone { public Headset( string zone ) : base( "headset", zone ) {} };

            // indicator
            public class Indicator : AbstractIlluminationDevice_StandardZone { public Indicator( string zone ) : base( "indicator", zone ) {} };

            // perkey (M800 and the likes)
            public class RGBPerkey : AbstractIlluminationDevice_StandardZone { public RGBPerkey( string zone ) : base( "rgb-per-key-zones", zone ) {} };


            abstract public class AbstractTactileDevice_Zone : AbstractDevice_Zone {
                public AbstractTactileDevice_Zone( string device ) : base( device ) { }
            }

            // tactile generic
            public class AbstarctGenericTactile_Zone : AbstractTactileDevice_Zone {
                protected string _zone;
                public string zone { get { return _zone; } }
                public AbstarctGenericTactile_Zone( string device, string zone ) : base( device ) { _zone = zone; }
            }


            // concrete devices
            // TODO
        }


        [FullSerializer.fsObject(Converter = typeof(RateStaticConverter))]
        public abstract class AbstractRate : UnityEngine.ScriptableObject { }


        public abstract class AbstractHandler : UnityEngine.ScriptableObject { }




        // ******************** COLORS ********************

        public enum ColorEffect {
            Static = 0,
            Gradient,
            Range
        }


        public enum RangeColorEffect {
            Static = 0,
            Gradient
        }


        public enum IlluminationMode {
            Color = 0,
            Percent,
            Count
        }


        // TODO copy constructors
        [System.Serializable] public struct RGB {
            public System.Byte red;
            public System.Byte green;
            public System.Byte blue;

            public RGB( System.Byte r, System.Byte g, System.Byte b ) {
                red = r; green = g; blue = b;
            }
        }


        [System.Serializable] public struct Gradient {

            public RGB zero;
            public RGB hundred;

            public Gradient( RGB zero, RGB hundred ) {
                this.zero = zero; this.hundred = hundred;
            }

            public Gradient( System.Byte r0, System.Byte g0, System.Byte b0, System.Byte r1, System.Byte g1, System.Byte b1 ) {
                zero = new RGB( r0, g0, b0 );
                hundred = new RGB( r1, g1, b1 );
            }
        }


        public abstract class AbstractColor : UnityEngine.ScriptableObject {
            public abstract ColorEffect ColorEffectType();
        }
        public abstract class AbstractColor_Nonrecursive : AbstractColor { }


        [FullSerializer.fsObject(Converter = typeof(ColorRangeConverter))]
        [System.Serializable] public class ColorRange {

            // books
            private RangeColorEffect _effect;
            public RangeColorEffect ColorEffect { get {return _effect;} }

            public System.UInt32 low;

            public System.UInt32 high;

            public AbstractColor_Nonrecursive color;
            public ColorStatic color_static { get {return color as ColorStatic;} set {color = value; _effect = RangeColorEffect.Static;} }
            public ColorGradient color_gradient { get {return color as ColorGradient;} set {color = value; _effect = RangeColorEffect.Gradient;} }

            public ColorRange( System.UInt32 low, System.UInt32 high, ColorStatic color ) {
                this.low = low; this.high = high; color_static = color;
            }


            public ColorRange( System.UInt32 low, System.UInt32 high, ColorGradient color ) {
                this.low = low; this.high = high; color_gradient = color;
            }

            public ColorRange( System.UInt32 low, System.UInt32 high, RGB zero, RGB hundred ) {
                this.low = low; this.high = high; color_gradient = ColorGradient.Create( zero, hundred );
            }
        }


        class ColorRangeConverter : FullSerializer.fsDirectConverter< ColorRange > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref ColorRange model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( ColorRange model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                // TODO check result of each
                SerializeMember< System.UInt32 >( serialized, null, "low", model.low );
                SerializeMember< System.UInt32 >( serialized, null, "high", model.high );

                SerializeMember< AbstractColor_Nonrecursive >( serialized, null, "color", model.color );

                return FullSerializer.fsResult.Success;
            }
        }




        // ******************** TACTILE ********************

        public enum TactileMode {
            vibrate
        }


        public enum TactileEffectType {
            ti_predefined_strongclick_100 = 0,
            ti_predefined_strongclick_60,
            ti_predefined_strongclick_30,
            ti_predefined_sharpclick_100,
            ti_predefined_sharpclick_60,
            ti_predefined_sharpclick_30,
            ti_predefined_softbump_100,
            ti_predefined_softbump_60,
            ti_predefined_softbump_30,
            ti_predefined_doubleclick_100,
            ti_predefined_doubleclick_60,
            ti_predefined_tripleclick_100,
            ti_predefined_softfuzz_60,
            ti_predefined_strongbuzz_100,
            ti_predefined_buzzalert750ms,
            ti_predefined_buzzalert1000ms,
            ti_predefined_strongclick1_100,
            ti_predefined_strongclick2_80,
            ti_predefined_strongclick3_60,
            ti_predefined_strongclick4_30,
            ti_predefined_mediumclick1_100,
            ti_predefined_mediumclick2_80,
            ti_predefined_mediumclick3_60,
            ti_predefined_sharptick1_100,
            ti_predefined_sharptick2_80,
            ti_predefined_sharptick3_60,
            ti_predefined_shortdoubleclickstrong1_100,
            ti_predefined_shortdoubleclickstrong2_80,
            ti_predefined_shortdoubleclickstrong3_60,
            ti_predefined_shortdoubleclickstrong4_30,
            ti_predefined_shortdoubleclickmedium1_100,
            ti_predefined_shortdoubleclickmedium2_80,
            ti_predefined_shortdoubleclickmedium3_60,
            ti_predefined_shortdoublesharptick1_100,
            ti_predefined_shortdoublesharptick2_80,
            ti_predefined_shortdoublesharptick3_60,
            ti_predefined_longdoublesharpclickstrong1_100,
            ti_predefined_longdoublesharpclickstrong2_80,
            ti_predefined_longdoublesharpclickstrong3_60,
            ti_predefined_longdoublesharpclickstrong4_30,
            ti_predefined_longdoublesharpclickmedium1_100,
            ti_predefined_longdoublesharpclickmedium2_80,
            ti_predefined_longdoublesharpclickmedium3_60,
            ti_predefined_longdoublesharptick1_100,
            ti_predefined_longdoublesharptick2_80,
            ti_predefined_longdoublesharptick3_60,
            ti_predefined_buzz1_100,
            ti_predefined_buzz2_80,
            ti_predefined_buzz3_60,
            ti_predefined_buzz4_40,
            ti_predefined_buzz5_20,
            ti_predefined_pulsingstrong1_100,
            ti_predefined_pulsingstrong2_60,
            ti_predefined_pulsingmedium1_100,
            ti_predefined_pulsingmedium2_60,
            ti_predefined_pulsingsharp1_100,
            ti_predefined_pulsingsharp2_60,
            ti_predefined_transitionclick1_100,
            ti_predefined_transitionclick2_80,
            ti_predefined_transitionclick3_60,
            ti_predefined_transitionclick4_40,
            ti_predefined_transitionclick5_20,
            ti_predefined_transitionclick6_10,
            ti_predefined_transitionhum1_100,
            ti_predefined_transitionhum2_80,
            ti_predefined_transitionhum3_60,
            ti_predefined_transitionhum4_40,
            ti_predefined_transitionhum5_20,
            ti_predefined_transitionhum6_10,
            ti_predefined_transitionrampdownlongsmooth1_100to0,
            ti_predefined_transitionrampdownlongsmooth2_100to0,
            ti_predefined_transitionrampdownmediumsmooth1_100to0,
            ti_predefined_transitionrampdownmediumsmooth2_100to0,
            ti_predefined_transitionrampdownshortsmooth1_100to0,
            ti_predefined_transitionrampdownshortsmooth2_100to0,
            ti_predefined_transitionrampdownlongsharp1_100to0,
            ti_predefined_transitionrampdownlongsharp2_100to0,
            ti_predefined_transitionrampdownmediumsharp1_100to0,
            ti_predefined_transitionrampdownmediumsharp2_100to0,
            ti_predefined_transitionrampdownshortsharp1_100to0,
            ti_predefined_transitionrampdownshortsharp2_100to0,
            ti_predefined_transitionrampuplongsmooth1_0to100,
            ti_predefined_transitionrampuplongsmooth2_0to100,
            ti_predefined_transitionrampupmediumsmooth1_0to100,
            ti_predefined_transitionrampupmediumsmooth2_0to100,
            ti_predefined_transitionrampupshortsmooth1_0to100,
            ti_predefined_transitionrampupshortsmooth2_0to100,
            ti_predefined_transitionrampuplongsharp1_0to100,
            ti_predefined_transitionrampuplongsharp2_0to100,
            ti_predefined_transitionrampupmediumsharp1_0to100,
            ti_predefined_transitionrampupmediumsharp2_0to100,
            ti_predefined_transitionrampupshortsharp1_0to100,
            ti_predefined_transitionrampupshortsharp2_0to100,
            ti_predefined_transitionrampdownlongsmooth1_50to0,
            ti_predefined_transitionrampdownlongsmooth2_50to0,
            ti_predefined_transitionrampdownmediumsmooth1_50to0,
            ti_predefined_transitionrampdownmediumsmooth2_50to0,
            ti_predefined_transitionrampdownshortsmooth1_50to0,
            ti_predefined_transitionrampdownshortsmooth2_50to0,
            ti_predefined_transitionrampdownlongsharp1_50to0,
            ti_predefined_transitionrampdownlongsharp2_50to0,
            ti_predefined_transitionrampdownmediumsharp1_50to0,
            ti_predefined_transitionrampdownmediumsharp2_50to0,
            ti_predefined_transitionrampdownshortsharp1_50to0,
            ti_predefined_transitionrampdownshortsharp2_50to0,
            ti_predefined_transitionrampuplongsmooth1_0to50,
            ti_predefined_transitionrampuplongsmooth2_0to50,
            ti_predefined_transitionrampupmediumsmooth1_0to50,
            ti_predefined_transitionrampupmediumsmooth2_0to50,
            ti_predefined_transitionrampupshortsmooth1_0to50,
            ti_predefined_transitionrampupshortsmooth2_0to50,
            ti_predefined_transitionrampuplongsharp1_0to50,
            ti_predefined_transitionrampuplongsharp2_0to50,
            ti_predefined_transitionrampupmediumsharp1_0to50,
            ti_predefined_transitionrampupmediumsharp2_0to50,
            ti_predefined_transitionrampupshortsharp1_0to50,
            ti_predefined_transitionrampupshortsharp2_0to50,
            ti_predefined_longbuzzforprogrammaticstopping_100,
            ti_predefined_smoothhum1nokickorbrakepulse_50,
            ti_predefined_smoothhum2nokickorbrakepulse_40,
            ti_predefined_smoothhum3nokickorbrakepulse_30,
            ti_predefined_smoothhum4nokickorbrakepulse_20,
            ti_predefined_smoothhum5nokickorbrakepulse_10
        }


        public enum TactilePatternType {
            Simple = 0,
            Custom,
            Range
        }


        [FullSerializer.fsObject(Converter = typeof(TactileEffectSimpleConverter))]
        [System.Serializable] public class TactileEffectSimple {
            public TactileEffectType type;
            public System.UInt32 delay_ms;

            public TactileEffectSimple( TactileEffectType effect, System.UInt32 delay = 0 ) {
                type = effect; delay_ms = delay;
            }
        }


        [FullSerializer.fsObject(Converter = typeof(TactileEffectCustomConverter))]
        [System.Serializable] public class TactileEffectCustom {
            public const string type = "custom";
            public System.UInt32 length_ms;
            public System.UInt32 delay_ms;

            public TactileEffectCustom( System.UInt32 length, System.UInt32 delay = 0 ) {
                length_ms = length; delay_ms = delay;
            }
        }


        [FullSerializer.fsObject(Converter = typeof(TactileEffectRangeConverter))]
        [System.Serializable] public class TactileEffectRange
        {
            private TactilePatternType _tactilePatternType;
            public TactilePatternType TactilePatternType { get { return _tactilePatternType; } }

            public System.UInt32 low;
            public System.UInt32 high;

            public TactilePattern_Nonrecursive pattern;
            public TactilePatternSimple pattern_simple { get { return pattern as TactilePatternSimple; } set { pattern = value; _tactilePatternType = TactilePatternType.Simple; } }
            public TactilePatternCustom pattern_custom { get { return pattern as TactilePatternCustom; } set { pattern = value; _tactilePatternType = TactilePatternType.Custom; } }


            private TactileEffectRange(System.UInt32 low, System.UInt32 high)
            {
                this.low = low; this.high = high; _tactilePatternType = TactilePatternType.Simple; pattern = null;
            }


            public TactileEffectRange(System.UInt32 low, System.UInt32 high, TactileEffectSimple[] pattern) : this( low, high ) {
                pattern_simple = TactilePatternSimple.Create( pattern );
            }
            public TactileEffectRange(System.UInt32 low, System.UInt32 high, TactileEffectCustom[] pattern) : this( low, high ) {
                pattern_custom = TactilePatternCustom.Create( pattern );
            }
        }


        class TactileEffectSimpleConverter : FullSerializer.fsDirectConverter< TactileEffectSimple > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref TactileEffectSimple model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( TactileEffectSimple model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                // TODO check result of each
                SerializeMember< TactileEffectType >( serialized, null, "type", model.type );
                SerializeMember< System.UInt32 >( serialized, null, "delay-ms", model.delay_ms );

                return FullSerializer.fsResult.Success;
            }
        }


        class TactileEffectCustomConverter : FullSerializer.fsDirectConverter< TactileEffectCustom > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary< string, FullSerializer.fsData > data, ref TactileEffectCustom model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( TactileEffectCustom model, System.Collections.Generic.Dictionary< string, FullSerializer.fsData > serialized ) {
                // TODO check result of each
                SerializeMember< string >( serialized, null, "type", TactileEffectCustom.type );
                SerializeMember< System.UInt32 >( serialized, null, "length-ms", model.length_ms );
                SerializeMember< System.UInt32 >( serialized, null, "delay-ms", model.delay_ms );

                return FullSerializer.fsResult.Success;
            }
        }


        class TactileEffectRangeConverter : FullSerializer.fsDirectConverter< TactileEffectRange > {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary<string, FullSerializer.fsData> data, ref TactileEffectRange model ) {
                return FullSerializer.fsResult.Fail("Not implemented");
            }

            protected override FullSerializer.fsResult DoSerialize( TactileEffectRange model, System.Collections.Generic.Dictionary<string, FullSerializer.fsData> serialized ) {
                // TODO check result of each
                SerializeMember<System.UInt32>( serialized, null, "low", model.low );
                SerializeMember<System.UInt32>( serialized, null, "high", model.high );

                switch ( model.TactilePatternType ) {
                    case TactilePatternType.Simple: SerializeMember< TactileEffectSimple[] >( serialized, null, "pattern", model.pattern_simple.pattern ); break;
                    case TactilePatternType.Custom: SerializeMember< TactileEffectCustom[] >( serialized, null, "pattern", model.pattern_custom.pattern ); break;
                }

                return FullSerializer.fsResult.Success;
            }
        }


        public abstract class AbstractTactilePattern : UnityEngine.ScriptableObject {

            public abstract TactilePatternType PatternType();

        }

        public abstract class TactilePattern_Nonrecursive: AbstractTactilePattern { }

    }

}
