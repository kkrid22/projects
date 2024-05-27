using Silk.NET.Maths;
using Silk.NET.OpenGL;
using StbImageSharp;
using System.Globalization;

namespace Szeminarium1_24_02_17_2
{
    internal class ObjResourceReader
    {
        public static unsafe GlObject CreateTeapotWithColor(GL Gl, float[] faceColor)
        {
            uint vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);

            List<float[]> objVertices;
            List<faceData[]> objFaces;
            List<float[]> objNormals;
            List<float[]> objTexCoords;

            ReadObjDataForTeapot(out objVertices, out objFaces, out objNormals, out objTexCoords);

            List<float> glVertices = new List<float>();
            List<float> glColors = new List<float>();
            List<uint> glIndices = new List<uint>();
            CreateGlArraysFromObjArrays(faceColor, objVertices, objFaces, objNormals, glVertices, glColors, glIndices, objTexCoords);

            return CreateOpenGlObject(Gl, vao, glVertices, glColors, glIndices);
        }

        private static unsafe GlObject CreateOpenGlObject(GL Gl, uint vao, List<float> glVertices, List<float> glColors, List<uint> glIndices)
        {
            uint offsetPos = 0;
            uint offsetNormal = offsetPos + (3 * sizeof(float));
            uint offsetTexture = offsetNormal + (3 * sizeof(float));
            uint vertexSize = offsetTexture + (2 * sizeof(float));

            uint vertices = Gl.GenBuffer();
            Gl.BindBuffer(GLEnum.ArrayBuffer, vertices);
            Gl.BufferData(GLEnum.ArrayBuffer, (ReadOnlySpan<float>)glVertices.ToArray().AsSpan(), GLEnum.StaticDraw);
            Gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, vertexSize, (void*)offsetPos);
            Gl.EnableVertexAttribArray(0);

            Gl.EnableVertexAttribArray(2);
            Gl.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, vertexSize, (void*)offsetNormal);

            uint colors = Gl.GenBuffer();

            //// set texture
            //// create texture
            uint texture = Gl.GenTexture();
            // activate texture 0
            Gl.ActiveTexture(TextureUnit.Texture0);
            // bind texture
            Gl.BindTexture(TextureTarget.Texture2D, texture);

            var skyboxImageResult = ReadTextureImage("Skull.jpg");
            var textureBytes = (ReadOnlySpan<byte>)skyboxImageResult.Data.AsSpan();
            // Here we use "result.Width" and "result.Height" to tell OpenGL about how big our texture is.
            Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)skyboxImageResult.Width,
                (uint)skyboxImageResult.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, textureBytes);
            Gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            Gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            Gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            Gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            // unbinde texture
            Gl.BindTexture(TextureTarget.Texture2D, 0);

            Gl.EnableVertexAttribArray(3);
            Gl.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, vertexSize, (void*)offsetTexture);


            uint indices = Gl.GenBuffer();
            Gl.BindBuffer(GLEnum.ElementArrayBuffer, indices);
            Gl.BufferData(GLEnum.ElementArrayBuffer, (ReadOnlySpan<uint>)glIndices.ToArray().AsSpan(), GLEnum.StaticDraw);

            // release array buffer
            Gl.BindBuffer(GLEnum.ArrayBuffer, 0);
            uint indexArrayLength = (uint)glIndices.Count;
            return new GlObject(vao, vertices, colors, indices, indexArrayLength, Gl, texture);
        }

        public static void CreateGlArraysFromObjArrays(float[] faceColor, List<float[]> objVertices, List<faceData[]> objFaces,
                                                 List<float[]> objNormals, List<float> glVertices, List<float> glColors,
                                                 List<uint> glIndices, List<float[]> objTexCoords)
        {
            Dictionary<string, int> glVertexIndices = new Dictionary<string, int>();
            foreach (var objFace in objFaces)
            {
                for (int i = 0; i < 3; ++i)
                {
                    int vertexIndex = objFace[i].VertexIndex;
                    int texCoordIndex = objFace[i].TextureIndex;
                    int normalIndex = objFace[i].NormalIndex;

                    var objVertex = objVertices[vertexIndex-1];
                    var texCoord = texCoordIndex != -1 ? objTexCoords[texCoordIndex - 1] : new float[] { 0, 0 };
                    var normal = normalIndex != -1 ? objNormals[normalIndex - 1] : new float[] { 0, 0, 0 };

                    List<float> glVertex = new List<float>();
                    glVertex.AddRange(objVertex);
                    glVertex.AddRange(normal);
                    glVertex.AddRange(texCoord);
                   

                    var glVertexStringKey = string.Join(" ", glVertex);
                    if (!glVertexIndices.ContainsKey(glVertexStringKey))
                    {
                        glVertices.AddRange(glVertex);
                        glColors.AddRange(faceColor);
                        glVertexIndices.Add(glVertexStringKey, glVertexIndices.Count);
                    }
                    glIndices.Add((uint)glVertexIndices[glVertexStringKey]);
                }
            }
        }

        private static unsafe void ReadObjDataForTeapot(out List<float[]> objVertices, out List<faceData[]> objFaces, out List<float[]> objNormals, out List<float[]> objTexCoords)
        {
            objVertices = new List<float[]>();
            objTexCoords = new List<float[]>();
            objNormals = new List<float[]>();
            objFaces = new List<faceData[]>(); // Modified to store the complete vertex/texCoord/normal triplet indices

            using (Stream objStream = typeof(ObjResourceReader).Assembly.GetManifestResourceStream("Szeminarium1_24_02_17_2.Resources.skully.obj"))
            using (StreamReader objReader = new StreamReader(objStream))
            {
                while (!objReader.EndOfStream)
                {
                    var line = objReader.ReadLine();

                    if (String.IsNullOrEmpty(line) || line.Trim().StartsWith("#"))
                        continue;

                    var lineClassifier = line.Substring(0, line.IndexOf(' '));
                    var lineData = line.Substring(lineClassifier.Length).Trim().Split(' ');

                    switch (lineClassifier)
                    {
                        case "v":
                            float[] vertex = new float[3];
                            for (int i = 0; i < 3; ++i)
                                vertex[i] = float.Parse(lineData[i], CultureInfo.InvariantCulture);
                            objVertices.Add(vertex);
                            break;

                        case "vt":
                            float[] texCoord = new float[2];
                            for (int i = 0; i < 2; ++i)
                                texCoord[i] = float.Parse(lineData[i], CultureInfo.InvariantCulture);

                            texCoord[1] = -texCoord[1];
                            objTexCoords.Add(texCoord);
                            break;

                        case "vn":
                            float[] normal = new float[3];
                            for (int i = 0; i < 3; ++i)
                                normal[i] = float.Parse(lineData[i], CultureInfo.InvariantCulture);
                            objNormals.Add(normal);
                            break;

                        case "f":
                            if (lineData.Length == 4)
                            {
                                faceData[] faceIndices = new faceData[4]; // Store vertex/texCoord/normal triplet indices

                                for (int i = 0; i < faceIndices.Length; ++i)
                                {
                                    if (lineData[i].IndexOf("/") != -1)
                                    {
                                        var lineDataWithNormal = lineData[i].Split("/");
                                        faceIndices[i].VertexIndex = lineDataWithNormal.Length > 0 && !string.IsNullOrWhiteSpace(lineDataWithNormal[0]) ? int.Parse(lineDataWithNormal[0], CultureInfo.InvariantCulture): -1;

                                        faceIndices[i].TextureIndex = lineDataWithNormal.Length > 1 && !string.IsNullOrWhiteSpace(lineDataWithNormal[1]) ? int.Parse(lineDataWithNormal[1], CultureInfo.InvariantCulture) : -1;

                                        faceIndices[i].NormalIndex = lineDataWithNormal.Length > 2 && !string.IsNullOrWhiteSpace(lineDataWithNormal[2]) ? int.Parse(lineDataWithNormal[2], CultureInfo.InvariantCulture) : -1;
                                    }
                                    else
                                    {
                                        faceIndices[i].VertexIndex = int.Parse(lineData[i], CultureInfo.InvariantCulture);
                                        faceIndices[i].TextureIndex = -1;
                                        faceIndices[i].NormalIndex = -1;
                                    }
                                }

                                faceData[] firstTriangle = new faceData[3];
                                faceData[] secondTriangle = new faceData[3];

                                firstTriangle[0] = faceIndices[0];
                                firstTriangle[1] = faceIndices[1];
                                firstTriangle[2] = faceIndices[2];

                                secondTriangle[0] = faceIndices[0];
                                secondTriangle[1] = faceIndices[2];
                                secondTriangle[2] = faceIndices[3];

                                objFaces.Add(firstTriangle);
                                objFaces.Add(secondTriangle);
                            }
                            else if (lineData.Length == 3) 
                            {
                                faceData[] faceIndices = new faceData[3]; // Store vertex/texCoord/normal triplet indices
                                
                                for (int i = 0; i < faceIndices.Length; ++i)
                                {
                                    if (lineData[i].IndexOf("/") != -1)
                                    {
                                        var lineDataWithNormal = lineData[i].Split("/");
                                        faceIndices[i].VertexIndex = lineDataWithNormal.Length > 0 && !string.IsNullOrWhiteSpace(lineDataWithNormal[0]) ? int.Parse(lineDataWithNormal[0], CultureInfo.InvariantCulture) : -1;

                                        faceIndices[i].TextureIndex = lineDataWithNormal.Length > 1 && !string.IsNullOrWhiteSpace(lineDataWithNormal[1]) ? int.Parse(lineDataWithNormal[1], CultureInfo.InvariantCulture) : -1;

                                        faceIndices[i].NormalIndex = lineDataWithNormal.Length > 2 && !string.IsNullOrWhiteSpace(lineDataWithNormal[2]) ? int.Parse(lineDataWithNormal[2], CultureInfo.InvariantCulture) : -1;
                                    }
                                    else
                                    {
                                        faceIndices[i].VertexIndex = int.Parse(lineData[i], CultureInfo.InvariantCulture);
                                        faceIndices[i].TextureIndex = -1;
                                        faceIndices[i].NormalIndex = -1;
                                    }
                                }
                                objFaces.Add(faceIndices);
                            }
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        private static unsafe ImageResult ReadTextureImage(string textureResource)
        {
            ImageResult result;
            using (Stream textureStream
                = typeof(GlCube).Assembly.GetManifestResourceStream("Szeminarium1_24_02_17_2.Resources." + textureResource))
            {
                result = ImageResult.FromStream(textureStream, ColorComponents.RedGreenBlueAlpha);
            }
            return result;
        }
    }
}
