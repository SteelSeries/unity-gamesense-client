/*
 * ImageDataTexture2D.cs
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

namespace SteelSeries {

    namespace GameSense {

        [UnityEngine.CreateAssetMenu(fileName = "ImageDataTexture2D", menuName = "GameSense/Screen Data/Frame Data/Texture2D Image Data")]
        public class ImageDataTexture2D : AbstractImageDataSource {
            public Texture2D texture;

            /// <summary>
            /// Create image data source from texture resource.
            /// </summary>
            /// <param name="texture">Texture2D object</param>
            /// <returns>ImageDataTexture2D instance</returns>
            public static ImageDataTexture2D Create( Texture2D texture ) {
                var id = CreateInstance< ImageDataTexture2D >();
                id.texture = texture;
                return id;
            }

            /// <summary>
            /// Convert stored texture to a packed binary image. Per pixel instensity is computed and thresholded.
            /// This needs to be called in context of the main thread.
            /// </summary>
            /// <param name="deviceZone">Device-zone object</param>
            /// <returns>Packed binary image in a byte array</returns>
            public override System.Byte[] GetData( DeviceZone.SpecificScreen_Zone deviceZone ) {
                uint targetDevArea = deviceZone.width * deviceZone.height;

                if ( targetDevArea == 0 ) {
                    Debug.LogErrorFormat( "Using generic screen device for image data" );
                    return null;
                }

                if ( texture == null ) {
                    Debug.LogErrorFormat( "Texture is required" );
                    return null;
                }

                if ( !texture.isReadable ) {
                    Debug.LogErrorFormat( "Texture resource is not accessible" );
                    return null;
                }

                if ( !isFormatSupported( texture.format ) ) {
                    Debug.LogErrorFormat( "Texture format is not supported" );
                    return null;
                }

                int samplingW = System.Math.Min( ( int )deviceZone.width, texture.width );
                int samplingH = System.Math.Min( ( int )deviceZone.height, texture.height );
                uint destIdx = 0;
                System.Byte[] luma = new System.Byte[deviceZone.width * deviceZone.height];
                Color32[] colors = texture.GetPixels32();
                if ( colors == null ) {
                    Debug.LogErrorFormat( "Texture resource is not accessible" );
                    return null;
                }

                // sample bottom to top
                for (int row = samplingH - 1; row >= 0; --row ) {
                    uint rowIdx = ( uint )( row * texture.width );
                    for (uint col = 0; col < samplingW; ++col ) {

                        // Fast Luma approximation
                        // Y = ( 3R + 4G + B ) / 8
                        Color32 c = colors[ rowIdx + col ];
                        luma[ destIdx ] = ( byte )
                            ( ( ( ( uint )c.r << 1 ) + c.r +
                                ( ( uint )c.g << 2 ) +
                                   c.b ) >> 3 );

                        ++destIdx;

                    }
                }

                System.Byte[] data = new System.Byte[ deviceZone.TargetScreenBufferSize() ];
                uint idx = 0;
                const byte threshold = 0x80;
                for ( uint dataIdx = 0; dataIdx < data.Length; ++dataIdx ) {

                    // apply thresholding at 50% instensity
                    data[ dataIdx ] = ( byte )(
                        ( ( luma[ idx ] & threshold ) ) |
                        ( ( luma[ idx + 1 ] & threshold ) >> 1 ) |
                        ( ( luma[ idx + 2 ] & threshold ) >> 2 ) |
                        ( ( luma[ idx + 3 ] & threshold ) >> 3 ) |
                        ( ( luma[ idx + 4 ] & threshold ) >> 4 ) |
                        ( ( luma[ idx + 5 ] & threshold ) >> 5 ) |
                        ( ( luma[ idx + 6 ] & threshold ) >> 6 ) |
                        ( ( luma[ idx + 7 ] & threshold ) >> 7 ) );

                    idx += 8;

                }

                return data;
            }

            private bool isFormatSupported( TextureFormat format ) {
                return format == TextureFormat.RGBA32 ||
                    format == TextureFormat.ARGB32 ||
                    format == TextureFormat.RGB24;
            }
        }

    }

}
