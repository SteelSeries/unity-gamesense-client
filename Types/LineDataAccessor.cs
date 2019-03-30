/*
 * LineDataAccessor.cs
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

        [System.Serializable] public class LineDataAccessor {//: UnityEngine.ScriptableObject {
            public enum Type {
                UpdateValue = 0,
                FrameKey,
                GoLispExpr
            }

            public Type type;
            public string value;

            private static LineDataAccessor _new() {
                return new LineDataAccessor();// CreateInstance< LineDataAccessor >();
            }

            public LineDataAccessor() {
                type = Type.UpdateValue;
            }

            public static LineDataAccessor UpdateValue() {
                return _new();
            }

            public static LineDataAccessor ContextFrameKey( string key ) {
                var lda = _new();
                lda.type = Type.FrameKey;
                lda.value = key;
                return lda;
            }

            public static LineDataAccessor GoLispExpression( string expression ) {
                var lda = _new();
                lda.type = Type.GoLispExpr;
                lda.value = expression;
                return lda;
            }
        }

    }

}
