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

using UnityEngine;
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
            SerializedProperty developer;
            SerializedProperty events;

            void OnEnable() {

                gameName = serializedObject.FindProperty( "GameName" );
                gameDisplayName = serializedObject.FindProperty( "GameDisplayName" );
                developer = serializedObject.FindProperty( "Developer" );
                events = serializedObject.FindProperty( "Events" );

            }

            public override void OnInspectorGUI() {

                serializedObject.Update();

                Undo.RecordObject( target, "Inspector" );

                EditorGUILayout.PropertyField( gameName );
                EditorGUILayout.PropertyField( gameDisplayName );
                EditorGUILayout.PropertyField( developer );

                EditorGUILayout.PropertyField( events, true );

                serializedObject.ApplyModifiedProperties();

            }

        }


        class GSClientEditorUtils {
            public static float GetPropertyHeight( SerializedProperty prop, GUIContent label ) {

                float height = 0;

                // get sibling
                SerializedProperty endProp = prop.GetEndProperty();

                // move onto the first child property
                prop.NextVisible( true );

                do {
                    height += EditorGUI.GetPropertyHeight( prop, label, true ) + EditorGUIUtility.standardVerticalSpacing;
                    prop.NextVisible( false );
                } while ( !SerializedProperty.EqualContents( prop, endProp ) );

                return height;
            }
        }


        [CustomPropertyDrawer( typeof( FrameModifiers ) )]
        public class FrameModifiersEditor : PropertyDrawer {

            SerializedProperty spLength;
            SerializedProperty spRepeats;
            SerializedProperty spRepeatCount;

            public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label) {

                spLength = prop.FindPropertyRelative( "length_millis" );
                spRepeats = prop.FindPropertyRelative( "_repeats" );
                spRepeatCount = prop.FindPropertyRelative( "_repeatCount" );

                if ( spLength == null )
                    return;

                EditorGUI.BeginProperty(pos, label, prop);

                var rectLength = new Rect( pos.x, pos.y, pos.width, EditorGUI.GetPropertyHeight( spLength ) );
                var rectRepeats = new Rect( pos.x,
                                            rectLength.y+rectLength.height + EditorGUIUtility.standardVerticalSpacing,
                                            pos.width/2,
                                            EditorGUI.GetPropertyHeight( spRepeats ) );
                var rectRepCount = new Rect( pos.x+pos.width/2,
                                             rectRepeats.y,
                                             pos.width/2,
                                             EditorGUI.GetPropertyHeight( spRepeatCount ) );
                
                EditorGUI.PropertyField( rectLength, spLength, new GUIContent( "Length (ms)" ) );
                EditorGUI.PropertyField( rectRepeats, spRepeats, new GUIContent( "Repeats" ) );
                
                if ( spRepeats.boolValue == true ) {
                    EditorGUI.PropertyField( rectRepCount, spRepeatCount, new GUIContent( "Repeat Count" ) );
                }

                EditorGUI.EndProperty();
            }

            public override float GetPropertyHeight( SerializedProperty prop, GUIContent label ) {
                return GSClientEditorUtils.GetPropertyHeight( prop, label );
            }
        }


        [CustomPropertyDrawer( typeof( LineDataAccessor ), true )]
        public class LineDataAccessorEditor : PropertyDrawer {

            LineDataAccessor.Type type;
            SerializedProperty spLdaType;
            SerializedProperty spLdaValue;


            public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label) {

                spLdaType = prop.FindPropertyRelative( "type" );
                spLdaValue = prop.FindPropertyRelative( "value" );

                if ( spLdaType == null )
                    return;

                if ( spLdaValue == null )
                    return;
               
                EditorGUI.BeginProperty(pos, label, prop);

                var rectType = new Rect( pos.x,
                                         pos.y,
                                         pos.width,
                                         EditorGUI.GetPropertyHeight( spLdaType ) );
                var rectKey = new Rect( pos.x,
                                        rectType.y+rectType.height + EditorGUIUtility.standardVerticalSpacing,
                                        pos.width,
                                        EditorGUI.GetPropertyHeight( spLdaValue )  );

                EditorGUI.PropertyField( rectType, spLdaType );

                type = ( LineDataAccessor.Type )spLdaType.enumValueIndex;               

                switch ( type ) {

                    case LineDataAccessor.Type.FrameKey:
                        EditorGUI.PropertyField( rectKey, spLdaValue, new GUIContent( "Key" ) );
                        break;

                    case LineDataAccessor.Type.GoLispExpr:
                        EditorGUI.PropertyField( rectKey, spLdaValue, new GUIContent( "Expression" ) );
                        break;

                }

                EditorGUI.EndProperty();
            }

            public override float GetPropertyHeight( SerializedProperty prop, GUIContent label ) {
                return GSClientEditorUtils.GetPropertyHeight( prop, label );
            }

        }


        [CustomPropertyDrawer( typeof( LineDataText ), true )]
        public class LineDataTextEditor : PropertyDrawer {

            SerializedProperty spPrefix;
            SerializedProperty spSuffix;
            SerializedProperty spBold;
            SerializedProperty spWrap;


            public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label) {

                spPrefix = prop.FindPropertyRelative( "prefix" );
                spSuffix = prop.FindPropertyRelative( "suffix" );
                spBold = prop.FindPropertyRelative( "bold" );
                spWrap = prop.FindPropertyRelative( "wrap" );

                if ( spPrefix == null )
                    return;

                if ( spSuffix == null )
                    return;

                if ( spBold == null )
                    return;

                if ( spWrap == null )
                    return;
               
                EditorGUI.BeginProperty(pos, label, prop);

                var rectPrefix = new Rect( pos.x,
                                           pos.y,
                                           pos.width,
                                           EditorGUI.GetPropertyHeight( spPrefix ) );
                var rectSuffix = new Rect( pos.x,
                                           rectPrefix.y + rectPrefix.height + EditorGUIUtility.standardVerticalSpacing,
                                           pos.width,
                                           EditorGUI.GetPropertyHeight( spSuffix ) );
                var rectBold = new Rect( pos.x,
                                         rectSuffix.y + rectSuffix.height + EditorGUIUtility.standardVerticalSpacing,
                                         pos.width,
                                         EditorGUI.GetPropertyHeight( spBold ) );
                var rectWrap = new Rect( pos.x,
                                         rectBold.y + rectBold.height + EditorGUIUtility.standardVerticalSpacing,
                                         pos.width,
                                         EditorGUI.GetPropertyHeight( spWrap ) );
                
                EditorGUI.PropertyField( rectPrefix, spPrefix, new GUIContent( spPrefix.displayName ) );
                EditorGUI.PropertyField( rectSuffix, spSuffix, new GUIContent( spSuffix.displayName ) );
                EditorGUI.PropertyField( rectBold, spBold, new GUIContent( spBold.displayName ) );
                EditorGUI.PropertyField( rectWrap, spWrap, new GUIContent( spWrap.displayName ) );

                EditorGUI.EndProperty();
            }

            public override float GetPropertyHeight( SerializedProperty prop, GUIContent label ) {
                return GSClientEditorUtils.GetPropertyHeight( prop, label );
            }
        }


        [CustomPropertyDrawer( typeof( LineDataProgressBar ), true )]
        public class LineDataProgressBarEditor : PropertyDrawer {

            public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label) {
               
                EditorGUI.BeginProperty(pos, label, prop);
                /* do not show anything */
                EditorGUI.EndProperty();

            }

            public override float GetPropertyHeight( SerializedProperty prop, GUIContent label ) {
                return 0;
            }
        }


        [CustomEditor( typeof( LineData ), true )]
        public class LineDataEditor : Editor {

            LineData ld;
            SerializedProperty spLdType;
            SerializedProperty spTxt;
            SerializedProperty spPb;
            SerializedProperty spLdAccessor;

            void OnEnable() {

                ld = ( LineData )target;

                spLdType = serializedObject.FindProperty( "type" );
                spTxt = serializedObject.FindProperty( "_txt" );
                spPb = serializedObject.FindProperty( "_pb" );
                spLdAccessor = serializedObject.FindProperty( "accessor" );

            }

            public override void OnInspectorGUI() {

                serializedObject.Update();

                EditorGUILayout.LabelField( "Line Data:" );

                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField( spLdType );
                
                switch ( ld.type ) {

                    case LineData.Type.Text:
                        EditorGUILayout.PropertyField( spTxt, false );
                        break;

                    case LineData.Type.ProgressBar:
                        EditorGUILayout.PropertyField( spPb, false );
                        break;
                }

                EditorGUI.indentLevel--;

                EditorGUILayout.Space();
                EditorGUILayout.LabelField( "Value Accessor:" );

                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField( spLdAccessor, true );
                EditorGUI.indentLevel--;

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty( ld );

            }
        }

    }

}
