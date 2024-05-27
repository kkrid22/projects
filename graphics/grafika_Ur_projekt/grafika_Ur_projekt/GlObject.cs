using Silk.NET.Maths;
using Silk.NET.OpenGL;
using StbImageSharp;

namespace grafika_Ur_projekt
{
    internal class GlObject
    {
        public uint? Texture { get; private set; }
        public uint Vao { get; }
        public uint Vertices { get; }
        public uint Colors { get; }
        public uint Indices { get; }
        public uint IndexArrayLength { get; }

        private GL Gl;

        private double Time { get; set; } = 0;

        public double ownRevolution { get; set; } = 0;
        public double globalRevolution { get; set; } = 0;
        public double OrbAngleOwnRevolution { get; set; } = 0;
        public double OrbAngleRevolutionOnGlobalY { get; private set; } = 0;

        public float scale { get; set; } = 1f;

        public GlObject(uint vao, uint vertices, uint colors, uint indeces, uint indexArrayLength, GL gl, uint? texture)
        {
            this.Vao = vao;
            this.Vertices = vertices;
            this.Colors = colors;
            this.Indices = indeces;
            this.IndexArrayLength = indexArrayLength;
            this.Gl = gl;
            Texture = texture;
        }

        /// <summary>
        /// Creates a sphere at the origin with given <paramref name="radius"/> using <paramref name="minResolution"/> number of units in the u-v plane. 
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="minResolution"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static unsafe GlObject CreateSphere(float radius, uint minResolution,GL Gl, string path)
        {
            Func<double, double, Vector3D<float>> get3DPointFromUV = (double u, double v) => GetSphereVertexPostion(radius, u, v);
            Func<Vector3D<float>, Vector3D<float>> sphereNormalCalculator = (Vector3D<float> v) => Vector3D.Normalize(v);

            Dictionary<string, int> vertexDescription2IndexTable = new Dictionary<string, int>();
            List<float> vertexDescriptionList = new List<float>();
            List<float> colorsList = new List<float>();
            List<uint> triangleIndices = new List<uint>();

            CreateTrianglesOfUVMesh(minResolution, get3DPointFromUV, sphereNormalCalculator, vertexDescription2IndexTable, vertexDescriptionList, colorsList, triangleIndices);

           // Texture planetTexture = LoadTexture(planetName);

            uint vao = Gl.GenVertexArray();
            Gl.BindVertexArray(vao);

            uint offsetPos = 0;
            uint offsetNormal = offsetPos + (3 * sizeof(float));
            uint offsetTexture = offsetNormal + (3 * sizeof(float));
            uint vertexSize = offsetTexture + (2 * sizeof(float));

            
            uint vertices = Gl.GenBuffer();
            Gl.BindBuffer(GLEnum.ArrayBuffer, vertices);
            Gl.BufferData(GLEnum.ArrayBuffer, (ReadOnlySpan<float>)vertexDescriptionList.ToArray().AsSpan(), GLEnum.StaticDraw);
            Gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, vertexSize, (void*)offsetPos);
            Gl.EnableVertexAttribArray(0);

            Gl.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, vertexSize, (void*)offsetNormal);
            Gl.EnableVertexAttribArray(2);

            uint colors = Gl.GenBuffer();;

            // set texture
            // create texture
            uint texture = Gl.GenTexture();
            // activate texture 0
            Gl.ActiveTexture(TextureUnit.Texture0);
            // bind texture
            Gl.BindTexture(TextureTarget.Texture2D, texture);

            var objectImageResult = ReadTextureImage(path);
            var textureBytes = (ReadOnlySpan<byte>)objectImageResult.Data.AsSpan();
            // Here we use "result.Width" and "result.Height" to tell OpenGL about how big our texture is.
            Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)objectImageResult.Width,
                (uint)objectImageResult.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, textureBytes);
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
            Gl.BufferData(GLEnum.ElementArrayBuffer, (ReadOnlySpan<uint>)triangleIndices.ToArray().AsSpan(), GLEnum.StaticDraw);

            // release array buffer
            Gl.BindBuffer(GLEnum.ArrayBuffer, 0);
            uint indexArrayLength = (uint)triangleIndices.Count;

            return new GlObject(vao, vertices, colors, indices, indexArrayLength, Gl, texture);
        }
        
        private static unsafe void CreateTrianglesOfUVMesh(uint minResolution, Func<double, double, Vector3D<float>> get3DPointFromUV, Func<Vector3D<float>, Vector3D<float>> sphereNormalCalculator, Dictionary<string, int> vertexDescription2IndexTable, List<float> vertexDescriptionList, List<float> colorsList, List<uint> triangleIndices)
        {
            double uvStep = 1d / minResolution;
            for (double i = 0; i < minResolution; i++)
            {
                double u = (double)i * uvStep;
                for (int j = 0; j < minResolution; j++)
                {
                    double v = (double)j * uvStep;
                    var vertex = get3DPointFromUV(u, v);

                    var vertexRight = get3DPointFromUV(u + uvStep, v);
                    var vertexBottom = get3DPointFromUV(u, v + uvStep);
                    var vertexDiagonal = get3DPointFromUV(u + uvStep, v + uvStep);

                    int vertexIndex = GetIndexForVertex(vertexDescription2IndexTable, vertexDescriptionList, vertex, colorsList, sphereNormalCalculator,(float)u,(float)v);

                    int vertexIndexRight = GetIndexForVertex(vertexDescription2IndexTable, vertexDescriptionList, vertexRight, colorsList, sphereNormalCalculator, (float)(u + uvStep), (float)v);

                    int vertexIndexBottom = GetIndexForVertex(vertexDescription2IndexTable, vertexDescriptionList, vertexBottom, colorsList, sphereNormalCalculator, (float)u, (float)(v + uvStep));

                    int vertexIndexDiagonal = GetIndexForVertex(vertexDescription2IndexTable, vertexDescriptionList, vertexDiagonal, colorsList, sphereNormalCalculator, (float)(u + uvStep), (float)(v + uvStep));

                    triangleIndices.Add((uint)vertexIndex);
                    triangleIndices.Add((uint)vertexIndexBottom);
                    triangleIndices.Add((uint)vertexIndexRight);

                    triangleIndices.Add((uint)vertexIndexBottom);
                    triangleIndices.Add((uint)vertexIndexDiagonal);
                    triangleIndices.Add((uint)vertexIndexRight);
                }
            }
        }

        private static unsafe int GetIndexForVertex(Dictionary<string, int> vertexDescription2IndexTable, List<float> vertexDescriptionList, Vector3D<float> vertex, List<float> colorsList, Func<Vector3D<float>, Vector3D<float>> calculateNormalAtVertex, float u, float v)
        {
            int vertexIndex = -1;
            var normal = calculateNormalAtVertex(vertex);

            string key = $"{vertex.X}, {vertex.Y}, {vertex.Z}; {normal.X}, {normal.Y}, {normal.Z}; u:{u}; v:{v}";
            if (!vertexDescription2IndexTable.ContainsKey(key))
            {
                vertexDescriptionList.Add((float)vertex.X);
                vertexDescriptionList.Add((float)vertex.Y);
                vertexDescriptionList.Add((float)vertex.Z);
                vertexDescriptionList.Add((float)normal.X);
                vertexDescriptionList.Add((float)normal.Y);
                vertexDescriptionList.Add((float)normal.Z);
                vertexDescriptionList.Add((float)-v);
                vertexDescriptionList.Add((float)-u);


                colorsList.AddRange(new float[] { 1f, 0f, 0f, 1f });

                vertexIndex = vertexDescription2IndexTable.Count;
                vertexDescription2IndexTable.Add(key, vertexDescription2IndexTable.Count);
            }
            else
            {
                vertexIndex = vertexDescription2IndexTable[key];
            }

            return vertexIndex;
        }

        private static Vector3D<float> GetSphereVertexPostion(float radius, double u, double v)
        {
            return new Vector3D<float>(
                                    (float)(radius * Math.Cos(GetAlphaFromU(u)) * Math.Cos(GetBetaFromV(v))),
                                    (float)(radius * Math.Sin(GetAlphaFromU(u))),
                                    (float)(radius * Math.Cos(GetAlphaFromU(u)) * Math.Sin(GetBetaFromV(v))));
        }

        private static double GetBetaFromV(double v)
        {
            return v * 2 * Math.PI;
        }
        private static double GetAlphaFromU(double u)
        {
            return u * Math.PI - Math.PI / 2;
        }
        internal void SetScale(float scale)
        {
            this.scale = scale;
        }
        internal void SetOwnRevolution(double speed)
        {
            this.ownRevolution = speed;
        }
        internal void SetGlobalRevolution(double speed)
        {
            this.globalRevolution = speed;
        }
        internal void AdvanceTime(double deltaTime)
        {
            // set a simulation time
            Time += deltaTime;
            //valtozoval kell szorozni es nem csak siman time-al
            OrbAngleOwnRevolution = Time * 10 * ownRevolution;

            OrbAngleRevolutionOnGlobalY = -Time * globalRevolution;
        }
        internal void ReleaseGlObject()
        {
            // always unbound the vertex buffer first, so no halfway results are displayed by accident
            Gl.DeleteBuffer(Vertices);
            Gl.DeleteBuffer(Colors);
            Gl.DeleteBuffer(Indices);
            Gl.DeleteVertexArray(Vao);
        }
        private static unsafe ImageResult ReadTextureImage(string textureResource)
        {
            ImageResult result;
            using (Stream skyeboxStream
                = typeof(GlCube).Assembly.GetManifestResourceStream("grafika_Ur_projekt.Resources." + textureResource))
            {
                result = ImageResult.FromStream(skyeboxStream, ColorComponents.RedGreenBlueAlpha);
            }
            return result;
        }

    }

}
