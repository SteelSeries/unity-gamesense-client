/*
 * GSClientEditor.cs
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

using UnityEditor;

namespace SteelSeries {

    namespace GameSense {

        // need this to support undo and redo ops in color editors
        class ColorPicker : UnityEngine.ScriptableObject {
            public UnityEngine.Color color;
        }


        [CustomEditor(typeof(ColorStatic), true)]
        public class ColorStaticEditor : Editor {

            ColorStatic cs;
            ColorPicker color;

            void OnEnable() {

                cs = (ColorStatic)target;

                color = UnityEngine.ScriptableObject.CreateInstance< ColorPicker >();
                color.color.r = cs.red / 255.0f;
                color.color.g = cs.green / 255.0f;
                color.color.b = cs.blue / 255.0f;
                color.color.a = 1.0f;

            }

            public override void OnInspectorGUI() {

                serializedObject.Update();

                Undo.RecordObject( color, "Color Change" );

                color.color = EditorGUILayout.ColorField( color.color );
                cs.red = (System.Byte)(color.color.r * 255);
                cs.green = (System.Byte)(color.color.g * 255);
                cs.blue = (System.Byte)(color.color.b * 255);

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty( cs );

            }
        }


        [CustomEditor(typeof(ColorGradient), true)]
        public class ColorGradientEditor : Editor {

            ColorGradient cg;
            ColorPicker colorZero;
            ColorPicker colorHundred;


            void OnEnable() {

                cg = (ColorGradient)target;

                colorZero = UnityEngine.ScriptableObject.CreateInstance< ColorPicker >();
                colorZero.color.r = cg.gradient.zero.red / 255.0f;
                colorZero.color.g = cg.gradient.zero.green / 255.0f;
                colorZero.color.b = cg.gradient.zero.blue / 255.0f;
                colorZero.color.a = 1.0f;

                colorHundred = UnityEngine.ScriptableObject.CreateInstance< ColorPicker >();
                colorHundred.color.r = cg.gradient.hundred.red / 255.0f;
                colorHundred.color.g = cg.gradient.hundred.green / 255.0f;
                colorHundred.color.b = cg.gradient.hundred.blue / 255.0f;
                colorHundred.color.a = 1.0f;

            }

            public override void OnInspectorGUI() {

                serializedObject.Update();

                Undo.RecordObject( colorZero, "Color Change" );
                Undo.RecordObject( colorHundred, "Color Change" );

                colorZero.color = EditorGUILayout.ColorField( colorZero.color );
                cg.gradient.zero.red = (System.Byte)(colorZero.color.r * 255);
                cg.gradient.zero.green = (System.Byte)(colorZero.color.g * 255);
                cg.gradient.zero.blue = (System.Byte)(colorZero.color.b * 255);

                colorHundred.color = EditorGUILayout.ColorField( colorHundred.color );
                cg.gradient.hundred.red = (System.Byte)(colorHundred.color.r * 255);
                cg.gradient.hundred.green = (System.Byte)(colorHundred.color.g * 255);
                cg.gradient.hundred.blue = (System.Byte)(colorHundred.color.b * 255);

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty( cg );

            }
        }


        [CustomEditor(typeof(DeviceZone.RGBPerkeyZoneCustom), true)]
        public class RGBPerkeyZoneCustomEditor : Editor {

            DeviceZone.AbstractIlluminationDevice_CustomZone zc;
            SerializedProperty zone;

            void OnEnable() {

                zc = (DeviceZone.AbstractIlluminationDevice_CustomZone)target;
                zone = serializedObject.FindProperty( "zone" );

            }

            public override void OnInspectorGUI() {

                serializedObject.Update();

                EditorGUILayout.PropertyField( zone, new UnityEngine.GUIContent( "HID codes", "Array of HID codes corresponding to keys" ), true );

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty( zc );

            }
        }


        [CustomEditor(typeof(GSClient))]
        public class GSClientEditor : Editor {

            SerializedProperty gameName;
            SerializedProperty gameDisplayName;
            SerializedProperty iconColor;
            SerializedProperty events;

            void OnEnable() {

                gameName = serializedObject.FindProperty( "GameName" );
                gameDisplayName = serializedObject.FindProperty( "GameDisplayName" );
                iconColor = serializedObject.FindProperty( "IconColor" );
                events = serializedObject.FindProperty( "Events" );

            }

            public override void OnInspectorGUI() {

                serializedObject.Update();

                Undo.RecordObject( target, "Inspector" );

                EditorGUILayout.PropertyField( gameName );
                EditorGUILayout.PropertyField( gameDisplayName );
                EditorGUILayout.PropertyField( iconColor );

                EditorGUILayout.PropertyField( events, true );

                serializedObject.ApplyModifiedProperties();

            }

        }

    }

}
