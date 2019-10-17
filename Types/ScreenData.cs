/*
 * ScreenData.cs
 *
 * authors: Ellie Quirini (gabriella.quirini@steelseries.com)
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
        [FullSerializer.fsObject(Converter = typeof(ScreenDataConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "ScreenData", menuName = "GameSense/Screen Data/Single Line")]
        public class ScreenData : AbstractGenericScreenData {
            public bool has_progress_bar;       // has_progress_bar or has_text mandatory
            public bool has_text;
            public string prefix;               // optional
            public string suffix;               // optional
            public bool bold;                   // optional
            public int wrap;                    // optional
            public string arg;                  // optional
            public string context_frame_key;    // optional - overridden by 'arg'

            private static ScreenData _new() {
                ScreenData fd = UnityEngine.ScriptableObject.CreateInstance<ScreenData>();
                return fd;
            }

            public static ScreenData Create( bool HasProgressBar, bool hasText ) {
                ScreenData fd = _new();
                fd.has_progress_bar = HasProgressBar;
                fd.has_text = hasText;
                return fd;
            } 
        }

        class ScreenDataConverter : FullSerializer.fsDirectConverter<ScreenData> {
            protected override FullSerializer.fsResult DoDeserialize( System.Collections.Generic.Dictionary<string, FullSerializer.fsData> data, ref ScreenData model ) {
                return FullSerializer.fsResult.Fail("Not implemented");
            }

            protected override FullSerializer.fsResult DoSerialize( ScreenData model, System.Collections.Generic.Dictionary<string, FullSerializer.fsData> serialized ) {
                SerializeMember<bool>(serialized, null, "has-progress-bar", model.has_progress_bar);
                SerializeMember<bool>(serialized, null, "has-text", model.has_text);

                // add conditionals as to not clutter messages with defaults
                if (!string.IsNullOrEmpty(model.prefix)) {
                    SerializeMember<string>(serialized, null, "prefix", model.prefix);
                }
                if (!string.IsNullOrEmpty(model.suffix)) {
                    SerializeMember<string>(serialized, null, "suffix", model.suffix);
                }
                if (model.bold) {
                    SerializeMember<bool>(serialized, null, "bold", model.bold);
                }
                if (model.length_millis != 0) {
                    SerializeMember<int>(serialized, null, "length-millis", model.length_millis);
                }
                if (model.icon != EventIconId.Default) {
                    SerializeMember<EventIconId>(serialized, null, "icon-id", model.icon);
                }
                if (model.repeats != false) {
                    SerializeMember<dynamic>(serialized, null, "repeats", model.repeats);
                }
                if (model.wrap != 0) {
                    SerializeMember<int>(serialized, null, "wrap", model.wrap);
                }
                if (!string.IsNullOrEmpty(model.arg)) {
                    SerializeMember<string>(serialized, null, "arg", model.arg);
                }
                if (!string.IsNullOrEmpty(model.context_frame_key)) {
                    SerializeMember<string>(serialized, null, "context-frame-key", model.context_frame_key);
                }

                return FullSerializer.fsResult.Success;
            }
        }

    }

}
