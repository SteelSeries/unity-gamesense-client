/*
 * LineData.cs
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

using UnityEngine;
using System.Collections.Generic;

namespace SteelSeries {

    namespace GameSense {

        [System.Serializable] public class LineDataText {
            public bool has_text { get; private set; }
            public string prefix;
            public string suffix;
            public bool bold;
            public System.UInt32 wrap;

            public LineDataText() {
                has_text = true;
                bold = false;
                wrap = 0;
            }

            private static LineDataText _new() {
                return new LineDataText();
            }

            public static LineDataText Create( string prefix, string suffix, bool bold, System.UInt32 wrap ) {
                var ldtxt = _new();
                ldtxt.prefix = prefix;
                ldtxt.suffix = suffix;
                ldtxt.bold = bold;
                ldtxt.wrap = wrap;
                return ldtxt;
            }
        }

        [System.Serializable] public class LineDataProgressBar {
            public const bool has_progress_bar = true;
            public static LineDataProgressBar Create() {
                return new LineDataProgressBar();
            }
        }


        [FullSerializer.fsObject(Converter = typeof(LineDataConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "LineData", menuName = "GameSense/Screen Data/Frame Data/Line Data")]
        [System.Serializable] public class LineData : ScriptableObject {
            public enum Type {
                Text = 0,
                ProgressBar
            }

            [UnityEngine.SerializeField]
            private LineDataText _txt;
            [UnityEngine.SerializeField]
            private LineDataProgressBar _pb;
            public LineDataAccessor accessor;

            public Type type;

            public LineDataText AsText {
                get { return _txt; }
            }

            public LineDataProgressBar AsProgressBar {
                get { return _pb; }
            }

            private static LineData _new() {
                return CreateInstance< LineData >();
            }

            public LineData() {
                type = Type.Text;
            }

            public void Set( LineDataText data ) {
                type = Type.Text;
                _txt = data;
                accessor = null;
            }

            public void Set( LineDataProgressBar data ) {
                type = Type.ProgressBar;
                _pb = data;
                accessor = null;
            }

            public void Set( LineDataText data, LineDataAccessor accessor ) {
                type = Type.Text;
                _txt = data;
                this.accessor = accessor;
            }

            public void Set( LineDataProgressBar data, LineDataAccessor accessor ) {
                type = Type.ProgressBar;
                _pb = data;
                this.accessor = accessor;
            }

            public static LineData Create( LineDataText data ) {
                var ld = _new();
                ld.Set( data );
                return ld;
            }

            public static LineData Create( LineDataProgressBar data ) {
                var ld = _new();
                ld.Set( data );
                return ld;
            }

            public static LineData Create( LineDataText data, LineDataAccessor accessor ) {
                var ld = _new();
                ld.Set( data, accessor );
                return ld;
            }

            public static LineData Create( LineDataProgressBar data, LineDataAccessor accessor ) {
                var ld = _new();
                ld.Set( data, accessor );
                return ld;
            }

            public void DecorateSerialized( Dictionary< string, FullSerializer.fsData > serialized ) {
                switch ( type ) {

                    case Type.Text:
                        serialized[ "has-text" ] = new FullSerializer.fsData( AsText.has_text );
                        serialized[ "prefix" ] = new FullSerializer.fsData( AsText.prefix );
                        serialized[ "suffix" ] = new FullSerializer.fsData( AsText.suffix );
                        serialized[ "bold" ] = new FullSerializer.fsData( AsText.bold );
                        serialized[ "wrap" ] = new FullSerializer.fsData( AsText.wrap );
                        break;

                    case Type.ProgressBar:
                        serialized[ "has-progress-bar" ] = new FullSerializer.fsData( LineDataProgressBar.has_progress_bar );
                        break;

                }

                if (accessor != null) {
                    switch (accessor.type) {

                        case LineDataAccessor.Type.FrameKey:
                            serialized["context-frame-key"] = new FullSerializer.fsData(accessor.value);
                            break;

                        case LineDataAccessor.Type.GoLispExpr:
                            serialized["arg"] = new FullSerializer.fsData(accessor.value);
                            break;

                    }
                }
            }
        }


        class LineDataConverter : Converter< LineData > {
            protected override FullSerializer.fsResult DoSerialize( LineData model, Dictionary< string, FullSerializer.fsData > serialized ) {
                model.DecorateSerialized( serialized );
                return FullSerializer.fsResult.Success;
            }
        }

    }

}
