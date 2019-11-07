/*
 * FrameDataImage.cs
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

        public abstract class AbstractImageDataSource : ScriptableObject {
            /// <summary>
            /// Method that mandates the return of a packed binary image in a byte array, where
            /// each bit corresponds to a single black/white pixel, specified right to left,
            /// top to bottom.
            /// </summary>
            /// <param name="deviceZone">Device-zone object that can be used for target display size reference</param>
            /// <returns>Packed binary image in a byte array</returns>
            public abstract System.Byte[] GetData( DeviceZone.SpecificScreen_Zone deviceZone );
        }

        public sealed class ImageDataBitVector : AbstractImageDataSource {
            public System.Byte[] data;

            /// <summary>
            /// Construct image data source from the supplied bit vector whose bit length
            /// must match the screen area (width * height).
            /// </summary>
            /// <param name="packedBinaryImage">Bit vector</param>
            /// <returns>ImageDataBitVector instance</returns>
            public static ImageDataBitVector Create( System.Byte[] packedBinaryImage ) {
                var id = CreateInstance< ImageDataBitVector >();
                id.data = packedBinaryImage;
                return id;
            }

            /// <summary>
            /// Return stored bit vector.
            /// </summary>
            /// <param name="deviceZone">Device-zone object</param>
            /// <returns>Packed binary image in a byte array</returns>
            public override System.Byte[] GetData( DeviceZone.SpecificScreen_Zone deviceZone ) {
                return data;
            }
        }

        [FullSerializer.fsObject(Converter = typeof(FrameDataImageConverter))]
        [UnityEngine.CreateAssetMenu(fileName = "FrameDataImage", menuName = "GameSense/Screen Data/Frame Data/Image")]
        [System.Serializable] public class FrameDataImage : AbstractFrameData {
            public System.Byte[] data { get; private set; }
            public DeviceZone.SpecificScreen_Zone deviceZone;
            public AbstractImageDataSource imageSource;
            public FrameModifiers frameModifiers;

            public static FrameDataImage Create( DeviceZone.SpecificScreen_Zone deviceZone, AbstractImageDataSource imageSource, FrameModifiers frameModifiers ) {
                var fd = CreateInstance< FrameDataImage >();
                fd.deviceZone = deviceZone;
                fd.imageSource = imageSource;
                fd.frameModifiers = frameModifiers;
                return fd;
            }

            /// <summary>
            /// Obtain image data from image source. This should be called in context of the main thread.
            /// </summary>
            public override void Preprocess() {
                if ( deviceZone == null ) {
                    Debug.LogError( "Device-zone object not specified" );
                    return;
                }

                if ( imageSource == null ) {
                    Debug.LogError( "Nil image data source" );
                    return;
                }

                data = imageSource.GetData( deviceZone );
            }
        }


        class FrameDataImageConverter : Converter< FrameDataImage > {
            protected override FullSerializer.fsResult DoSerialize( FrameDataImage model, Dictionary< string, FullSerializer.fsData > serialized ) {

                if ( model.deviceZone == null ) {
                    return FullSerializer.fsResult.Fail( "Nil device-zone" );
                }

                if ( model.data == null ) {
                    return FullSerializer.fsResult.Fail( "Nil image data" );
                }

                if ( model.data.Length != model.deviceZone.TargetScreenBufferSize() ) {
                    return FullSerializer.fsResult.Fail( "Image data size mismatch" );
                }

                SerializeMember( serialized, null, "image-data",  model.data );
                SerializeMember( serialized, null, "length-millis", model.frameModifiers.length_millis );

                if ( model.frameModifiers.repeats && model.frameModifiers.repeatCount != 0 ) {
                    SerializeMember( serialized, null, "repeats",  model.frameModifiers.repeatCount );
                } else {
                    SerializeMember( serialized, null, "repeats",  model.frameModifiers.repeats );
                }

                return FullSerializer.fsResult.Success;
            }
        }

    }

}

