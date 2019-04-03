﻿/*
 * ContextFrameObject.cs
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

        [System.Serializable] public abstract class ValueVariant {
            public abstract FullSerializer.fsData Serialize();
        }


        [FullSerializer.fsObject(Converter = typeof(ObjectVariantConverter))]
        [System.Serializable] public abstract class Variant< T > : ValueVariant {
            public T value { get; set; }
            public Variant() { value = default( T ); }
            public Variant( T v ) { value = v; }
        }


        public class BoolVariant : Variant< bool > {
            public BoolVariant( bool value ) : base( value ) { }
            public override FullSerializer.fsData Serialize() { return new FullSerializer.fsData( value ); }
        }


        public class IntegerVariant : Variant< int > {
            public IntegerVariant( int value ) : base( value ) { }
            public override FullSerializer.fsData Serialize() { return new FullSerializer.fsData( value ); }
        }


        public class FloatVariant : Variant< double > {
            public FloatVariant( double value ) : base( value ) { }
            public override FullSerializer.fsData Serialize() { return new FullSerializer.fsData( value ); }
        }


        public class StringVariant : Variant< string > {
            public StringVariant( string value ) : base( value ) { }
            public override FullSerializer.fsData Serialize() { return new FullSerializer.fsData( value ); }
        }


        public class ArrayVariant : Variant< ValueVariant[] > {
            public ArrayVariant( ValueVariant[] value ) : base( value ) { }

            public override FullSerializer.fsData Serialize() {
                var lst = new List< FullSerializer.fsData >();

                foreach (var v in value ) {
                    lst.Add( v.Serialize() );
                }

                return new FullSerializer.fsData( lst );
            }
        }


        [FullSerializer.fsObject(Converter = typeof(ObjectVariantConverter))]
        [System.Serializable] public class ObjectVariant : ValueVariant {
            public Dictionary< string, ValueVariant > props { get; }

            public ValueVariant this[ string key ] {
                get { return props[ key ]; }
                set { props[ key ] = value; }
            }

            public ObjectVariant() {
                props = new Dictionary< string, ValueVariant >();
            }

            public ObjectVariant( Dictionary< string, ValueVariant > properties ) {
                props = properties;
            }

            public Dictionary< string, ValueVariant >.Enumerator GetEnumerator() {
                return props.GetEnumerator();
            }

            public override FullSerializer.fsData Serialize() {
                var converter = new ObjectVariantConverter();
                FullSerializer.fsData fsData;

                converter.TrySerialize( this, out fsData, typeof( ObjectVariant ) );

                return fsData;
            }
        }


        [FullSerializer.fsObject(Converter = typeof(ContextFrameObjectConverter))]
        [System.Serializable] public class ContextFrameObject : AbstractContextFrame {
            public ObjectVariant frame;

            public ContextFrameObject() {
                frame = new ObjectVariant();
            }

            public ValueVariant this[ string key ] {
                get { return frame[ key ]; }
                set { frame[ key ] = value; }
            }
        }


        class ObjectVariantConverter : FullSerializer.fsDirectConverter< ObjectVariant > {
            protected override FullSerializer.fsResult DoDeserialize( Dictionary< string, FullSerializer.fsData > data, ref ObjectVariant model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( ObjectVariant model, Dictionary< string, FullSerializer.fsData > serialized ) {

                foreach ( var prop in model ) {
                    serialized[ prop.Key ] = prop.Value.Serialize();
                }

                return FullSerializer.fsResult.Success;
            }
        }


        class ContextFrameObjectConverter : FullSerializer.fsDirectConverter< ContextFrameObject > {
            protected override FullSerializer.fsResult DoDeserialize( Dictionary< string, FullSerializer.fsData > data, ref ContextFrameObject model ) {
                return FullSerializer.fsResult.Fail( "Not implemented" );
            }

            protected override FullSerializer.fsResult DoSerialize( ContextFrameObject model, Dictionary< string, FullSerializer.fsData > serialized ) {
                FullSerializer.fsData data;
                ObjectVariantConverter converter = new ObjectVariantConverter();
                
                converter.TrySerialize( model.frame, out data, typeof( ObjectVariant ) );

                var props = data.AsDictionary;
                foreach ( var prop in props ) {
                    serialized.Add( prop.Key, prop.Value );
                }


                return FullSerializer.fsResult.Success;
            }
        }

    }

}