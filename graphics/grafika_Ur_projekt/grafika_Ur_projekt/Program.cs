using grafika_Ur_projekt;
using ImGuiNET;
using Silk.NET.Core;
using Silk.NET.GLFW;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenAL;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Vulkan;
using Silk.NET.Windowing;
using System.Numerics;

namespace grafika_Ur_projekt
{
    internal static class Program
    {
        private static CameraDescriptor camera;
        private static bool firstMouse = true;
        private static System.Numerics.Vector2 lastMousePosition;
        private static float deltaTimes;
        private static HashSet<Silk.NET.GLFW.Keys> pressedKeys = new HashSet<Silk.NET.GLFW.Keys>();

        private static IWindow window;
        private static IInputContext inputContext;
        private static GL Gl;
       

        private static ImGuiController controller;

        //object related variables
        private const float radius = 1f;
        private const uint minResolution = 50;

        private static GlObject sun;
        private static GlObject[] planets = new GlObject[9];
        private static GlObject spaceship;
        private static GlCube skyBox;

        private static string[] textureNames = ["mercury.jpg", "venus.jpg", "earth.jpg", "mars.jpg", "jupiter.jpg", "saturn.jpg", "uranus.jpg", "neptune.jpg"];

        private static float[] distance = [0.39f, 0.72f, 1f, 1.52f, 5.2f, 9.54f, 19.2f, 30.06f];
        private static float avarageDistance = 2f * 4.22687f;//atlag
        private static double[] orbitalSpeed = [0.2409f, 0.616f, 1f, 1.9f, 12f, 29.5f, 84f, 165f];//how fast does it go aroung the sun 
                                                                                //relative to earth both
        private static float[] ownRotation = [58.67f, 3.583f, 1f, 1.04f, 0.42f, 0.46f, 0.71f, 0.67f];//how long for a day
        private static float[] scaleToEachOther = [0.33333336f, 0.99f, 1f, 0.573f, 11f, 9f, 4f, 3.9888f];
        private static float avarageScale = 3.8606417f; //atlag

        //shader related variables
        private static uint program;
        private static float Shininess = 0f;
        private const string ModelMatrixVariableName = "uModel";
        private const string NormalMatrixVariableName = "uNormal";
        private const string ViewMatrixVariableName = "uView";
        private const string ProjectionMatrixVariableName = "uProjection";

        private const string TextureUniformVariableName = "uTexture";

        private const string LightColorVariableName = "lightColor";
        private const string LightPositionVariableName = "lightPos";
        private const string ViewPosVariableName = "viewPos";
        private const string ShininessVariableName = "shininess";

        static void Main(string[] args)
        {
            WindowOptions windowOptions = WindowOptions.Default;
            windowOptions.Title = "Ur Projekt";
            windowOptions.Size = new Vector2D<int>(1000,1000);
            windowOptions.PreferredDepthBufferBits = 24;

            window = Window.Create(windowOptions);

            window.Load += Window_Load;
            window.Update += Window_Update;
            window.Render += Window_Render;
            window.Closing += Window_Closing;

            window.Run();
        }
        // Different windows
        private static void Window_Load()
        { 

            inputContext = window.CreateInput();
            foreach (var keyboard in inputContext.Keyboards)
            {
                keyboard.KeyDown += Keyboard_KeyDown;
               
            }
            foreach (var keyboard in inputContext.Keyboards)
            {
                keyboard.KeyUp += Keyboard_KeyUp;
            }

            var mouse = inputContext.Mice[0];
            mouse.MouseMove += Mouse_MouseMove;
            mouse.MouseDown += Mouse_MouseDown;

            Gl = window.CreateOpenGL();

            controller = new ImGuiController(Gl, window, inputContext);

            window.FramebufferResize += s =>
            {
                Gl.Viewport(s);
            };

            Gl.ClearColor(System.Drawing.Color.White);

            SetUpObjects();

            LinkProgram();

            //Gl.Enable(EnableCap.CullFace);

            Gl.Enable(EnableCap.DepthTest);
            Gl.DepthFunc(DepthFunction.Lequal);
        }
       private static void Window_Update(double deltaTime)
        {
            deltaTimes = (float)deltaTime;
            ProcessInput();
            CameraLookAt();
            sun.AdvanceTime(deltaTime/100);
            for (int i = 0; i < 8; i++)                
            {
                planets[i].AdvanceTime(deltaTime / 100) ;
            }

            controller.Update((float)deltaTime);
        }
        private static unsafe void Window_Render(double deltaTime)
        {
            // GL here
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Clear(ClearBufferMask.DepthBufferBit);

            Gl.UseProgram(program);
            camera.UpdateCameraVectors();

            SetViewMatrix();
            SetProjectionMatrix();

            SetLightColor();
            SetLightPosition();
            SetViewerPosition();
            SetShininess();

            RotateSun();
            RotatePlanets();

            DrawSkyBox();
            DrawObject();
            controller.Render();

        }

        //initialize the different components

        private static unsafe void SetUpObjects()
        {
            InitializeCamera();

            skyBox = GlCube.CreateInteriorCube(Gl, "");

            for (int i = 0; i < 8; i++)
            {
                planets[i] = GlObject.CreateSphere(radius,minResolution,Gl,"planets." + textureNames[i]);
            }
            sun = GlObject.CreateSphere(radius,minResolution,Gl, "planets.sun.jpg");
            
            sun.SetScale(3f);
            sun.SetOwnRevolution(1f / 27f);
            
            for (int i = 0; i < 8; i++)
            {
                distance[i] = distance[i] * avarageDistance;
                scaleToEachOther[i] = scaleToEachOther[i] / (avarageScale);
            }

            for (int i = 0; i < 8; i++)
            {
                planets[i].SetScale(scaleToEachOther[i]);
            }

            for (int i = 0; i < 8; i++)
            {
                planets[i].SetGlobalRevolution( 1/ orbitalSpeed[i]);
            }

            for (int i = 0; i < 8; i++) 
            {
                planets[i].SetOwnRevolution(1f / ownRotation[i]);   
            }

            spaceship = ObjResourceReader.CreateObjectWithColor(Gl, [1f,0f,0f,1f],"spaceship.spaceship.obj", "spaceship.spaceship_color.jpg");
        }

        private static void InitializeCamera()
        {
            camera = new CameraDescriptor(new Vector3D<float>(0.0f, 0.0f, 3.0f), Vector3D<float>.UnitY, -90.0f, 0.0f);
        }

        private static void LinkProgram()
        {
            uint vshader = Gl.CreateShader(ShaderType.VertexShader);
            uint fshader = Gl.CreateShader(ShaderType.FragmentShader);

            Gl.ShaderSource(vshader, ReadShader("VertexShader.vert"));
            Gl.CompileShader(vshader);
            Gl.GetShader(vshader, ShaderParameterName.CompileStatus, out int vStatus);
            if (vStatus != (int)GLEnum.True)
                throw new Exception("Vertex shader failed to compile: " + Gl.GetShaderInfoLog(vshader));

            Gl.ShaderSource(fshader, ReadShader("FragmentShader.frag"));
            Gl.CompileShader(fshader);

            program = Gl.CreateProgram();
            Gl.AttachShader(program, vshader);
            Gl.AttachShader(program, fshader);
            Gl.LinkProgram(program);
            Gl.GetProgram(program, GLEnum.LinkStatus, out var status);
            if (status == 0)
            {
                Console.WriteLine($"Error linking shader {Gl.GetProgramInfoLog(program)}");
            }
            Gl.DetachShader(program, vshader);
            Gl.DetachShader(program, fshader);
            Gl.DeleteShader(vshader);
            Gl.DeleteShader(fshader);
        }

        // Drawing the different objects

        private static unsafe void DrawObject() 
        {
            var translation = Matrix4X4.CreateTranslation(0f, 0f, 0f);
            var scale = Matrix4X4.CreateScale(0.1f);
            var modelMatrixForCenterCube = scale * translation;
            SetModelMatrix(modelMatrixForCenterCube);
            Gl.BindVertexArray(spaceship.Vao);

            int textureLocation = Gl.GetUniformLocation(program, TextureUniformVariableName);
            if (textureLocation == -1)
            {
                throw new Exception($"{TextureUniformVariableName} uniform not found on shader.");
            }
            // set texture 0
            Gl.Uniform1(textureLocation, 0);

            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)GLEnum.Linear);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)GLEnum.Linear);
            Gl.BindTexture(TextureTarget.Texture2D, spaceship.Texture.Value);

            Gl.DrawElements(GLEnum.Triangles, spaceship.IndexArrayLength, GLEnum.UnsignedInt, null);
            Gl.BindVertexArray(0);

            CheckError();
            Gl.BindTexture(TextureTarget.Texture2D, 0);
            CheckError();
        }

        private static unsafe void DrawSkyBox()
        {
            Matrix4X4<float> modelMatrix = Matrix4X4.CreateScale(430f);
            SetModelMatrix(modelMatrix);
            Gl.BindVertexArray(skyBox.Vao);

            int textureLocation = Gl.GetUniformLocation(program, TextureUniformVariableName);
            if (textureLocation == -1)
            {
                throw new Exception($"{TextureUniformVariableName} uniform not found on shader.");
            }
            // set texture 0
            Gl.Uniform1(textureLocation, 0);

            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)GLEnum.Linear);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)GLEnum.Linear);
            Gl.BindTexture(TextureTarget.Texture2D, skyBox.Texture.Value);

            Gl.DrawElements(GLEnum.Triangles, skyBox.IndexArrayLength, GLEnum.UnsignedInt, null);
            Gl.BindVertexArray(0);

            CheckError();
            Gl.BindTexture(TextureTarget.Texture2D, 0);
            CheckError();
        }

        private static unsafe void RotatePlanets()
        {
            for (int i = 0; i < 8; i++)
            {
                //megprobalunk bentmarad a 0,1 intervallumlban
                float t = distance[i] / (float)Math.Sqrt(2);
           
                Matrix4X4<float> scaleOrb = Matrix4X4.CreateScale(planets[i].scale);
                Matrix4X4<float> trans = Matrix4X4.CreateTranslation(t, 0f, t);
                Matrix4X4<float> rotLocY = Matrix4X4.CreateRotationY((float)planets[i].OrbAngleOwnRevolution);
                Matrix4X4<float> rotGlobY = Matrix4X4.CreateRotationY((float)planets[i].OrbAngleRevolutionOnGlobalY);//nap koruli forgas akar lenni

                Matrix4X4<float> orb = scaleOrb * rotLocY * trans * rotGlobY;

                SetModelMatrix(orb);
                Gl.BindVertexArray(planets[i].Vao);
                
                int textureLocation = Gl.GetUniformLocation(program, TextureUniformVariableName);
                if (textureLocation == -1)
                {
                    throw new Exception($"{TextureUniformVariableName} uniform not found on shader.");
                }
                // set texture 0
                Gl.Uniform1(textureLocation, 0);

                Gl.ActiveTexture(TextureUnit.Texture0);
                Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)GLEnum.Linear);
                Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)GLEnum.Linear);
                Gl.BindTexture(TextureTarget.Texture2D, planets[i].Texture.Value);

                Gl.DrawElements(GLEnum.Triangles, planets[i].IndexArrayLength, GLEnum.UnsignedInt, null);
                Gl.BindVertexArray(0);

                CheckError();
                Gl.BindTexture(TextureTarget.Texture2D, 0);
                CheckError();
            }
        }

        private static unsafe void RotateSun()
        {
            Matrix4X4<float> scaleorb = Matrix4X4.CreateScale(sun.scale);
            Matrix4X4<float> trans = Matrix4X4.CreateTranslation(0f, 0f, 0f);
            Matrix4X4<float> rotGlobY = Matrix4X4.CreateRotationY((float)sun.OrbAngleOwnRevolution);

            Matrix4X4<float> orb = scaleorb * trans * rotGlobY;

            SetModelMatrix(orb);
            Gl.BindVertexArray(sun.Vao);

            int textureLocation = Gl.GetUniformLocation(program, TextureUniformVariableName);
            if (textureLocation == -1)
            {
                throw new Exception($"{TextureUniformVariableName} uniform not found on shader.");
            }
            // set texture 0
            Gl.Uniform1(textureLocation, 0);

            Gl.ActiveTexture(TextureUnit.Texture0);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)GLEnum.Linear);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)GLEnum.Linear);
            Gl.BindTexture(TextureTarget.Texture2D, sun.Texture.Value);

            Gl.DrawElements(GLEnum.Triangles, sun.IndexArrayLength, GLEnum.UnsignedInt, null);
            Gl.BindVertexArray(0);

            CheckError();
            //Gl.BindTexture(TextureTarget.Texture2D, 0);
            CheckError();

        }

        // input managing functions
        private static void ProcessInput()
        {
            if (pressedKeys.Contains(Keys.W))
                camera.ProcessKeyboard(Keys.W, deltaTimes);
            if (pressedKeys.Contains(Keys.S))
                camera.ProcessKeyboard(Keys.S, deltaTimes);
            if (pressedKeys.Contains(Keys.A))
                camera.ProcessKeyboard(Keys.A, deltaTimes);
            if (pressedKeys.Contains(Keys.D))
                camera.ProcessKeyboard(Keys.D, deltaTimes);
        }
        private static void Mouse_MouseDown(IMouse mouse, Silk.NET.Input.MouseButton button)
        {
            if (button == Silk.NET.Input.MouseButton.Left)
            {
                firstMouse = false;
            }
        }
        private static void CameraLookAt()
        {
            if (pressedKeys.Contains(Keys.Up))
            {
                camera.ProcessMouseMovement(0, -2); // Look up
            }
            if (pressedKeys.Contains(Keys.Down))
            {
                camera.ProcessMouseMovement(0, 2); // Look down
            }
            if (pressedKeys.Contains(Keys.Left))
            {
                camera.ProcessMouseMovement(-2, 0); // Look left
            }
            if (pressedKeys.Contains(Keys.Right))
            {
                camera.ProcessMouseMovement(2, 0); // Look right
            }
        }
        private static void Keyboard_KeyDown(IKeyboard keyboard, Key key, int arg3)
        {
            pressedKeys.Add((Keys)key);

            if ((Keys)key == Keys.Escape)
            {
                window.Close();
            }
        }

        private static void Keyboard_KeyUp(IKeyboard keyboard, Key key, int arg3)
        {
            pressedKeys.Remove((Keys)key);
        }

        private static void Mouse_MouseMove(IMouse mouse, Vector2 position)
        {
            var delta = position - lastMousePosition;

            if (pressedKeys.Contains(Keys.Up))
            {
                position.Y -= 1f;
            }
            if (pressedKeys.Contains(Keys.Down)) // Down
            {
                position.Y += 1.0f;
            }
            if (pressedKeys.Contains(Keys.Left)) // Left
            {
                position.X -= 1.0f;
            }
            if (pressedKeys.Contains(Keys.Right)) // Right
            {
                position.X += 1.0f;
            }

            position.X = Math.Clamp(position.X, 0, window.Size.X);
            position.Y = Math.Clamp(position.Y, 0, window.Size.Y);

            // Set the new cursor position
            mouse.Position = position;

            lastMousePosition = position;
        }

        //camera managing functions

        private static unsafe void SetModelMatrix(Matrix4X4<float> modelMatrix)
        {
            int location = Gl.GetUniformLocation(program, ModelMatrixVariableName);
            if (location == -1)
            {
                throw new Exception($"{ModelMatrixVariableName} uniform not found on shader.");
            }

            Gl.UniformMatrix4(location, 1, false, (float*)&modelMatrix);
            CheckError();

            var modelMatrixWithoutTranslation = new Matrix4X4<float>(modelMatrix.Row1, modelMatrix.Row2, modelMatrix.Row3, modelMatrix.Row4);
            modelMatrixWithoutTranslation.M41 = 0;
            modelMatrixWithoutTranslation.M42 = 0;
            modelMatrixWithoutTranslation.M43 = 0;
            modelMatrixWithoutTranslation.M44 = 1;

            Matrix4X4<float> modelInvers;
            Matrix4X4.Invert<float>(modelMatrixWithoutTranslation, out modelInvers);
            Matrix3X3<float> normalMatrix = new Matrix3X3<float>(Matrix4X4.Transpose(modelInvers));
            location = Gl.GetUniformLocation(program, NormalMatrixVariableName);
            if (location == -1)
            {
                throw new Exception($"{NormalMatrixVariableName} uniform not found on shader.");
            }

            Gl.UniformMatrix3(location, 1, false, (float*)&normalMatrix);
            CheckError();
        }

        private static unsafe void SetProjectionMatrix()
        {
            var projectionMatrix = camera.GetProjectionMatrix(1024f, 768f);
            int location = Gl.GetUniformLocation(program, ProjectionMatrixVariableName);

            if (location == -1)
            {
                throw new Exception($"{ViewMatrixVariableName} uniform not found on shader.");
            }

            Gl.UniformMatrix4(location, 1, false, (float*)&projectionMatrix);
            CheckError();
        }


        private static unsafe void SetLightPosition() 
        {
            int location = Gl.GetUniformLocation(program, LightPositionVariableName);

            if (location == -1)
            {
                throw new Exception($"{LightPositionVariableName} uniform not found on shader.");
            }

            Gl.Uniform3(location, 0f, 0f, 0f);//its the sun the middle of everything

            CheckError();
        }

        private static unsafe void SetLightColor() 
        {
            int location = Gl.GetUniformLocation(program, LightColorVariableName);

            if (location == -1)
            {
                throw new Exception($"{LightColorVariableName} uniform not found on shader.");
            }

            Gl.Uniform3(location, 1f, 1f, 1f);
            CheckError();
        }

        private static unsafe void SetViewerPosition() 
        {
            int location = Gl.GetUniformLocation(program, ViewPosVariableName);

            if (location == -1)
            {
                throw new Exception($"{ViewPosVariableName} uniform not found on shader.");
            }

            Gl.Uniform3(location, camera.Position.X, camera.Position.Y, camera.Position.Z);
            CheckError();
        }

        private static unsafe void SetShininess() 
        {
            int location = Gl.GetUniformLocation(program, ShininessVariableName);

            if (location == -1)
            {
                throw new Exception($"{ShininessVariableName} uniform not found on shader.");
            }

            Gl.Uniform1(location, Shininess);
            CheckError();
        }

        private static unsafe void SetViewMatrix()
        {
            var viewMatrix = camera.GetViewMatrix();

            int location = Gl.GetUniformLocation(program, ViewMatrixVariableName);

            if (location == -1)
            {
                throw new Exception($"{ViewMatrixVariableName} uniform not found on shader.");
            }

            Gl.UniformMatrix4(location, 1, false, (float*)&viewMatrix);
            CheckError();
        }

        // other functions

        public static void CheckError()
        {
            var error = (Silk.NET.GLFW.ErrorCode)Gl.GetError();
            if (error != Silk.NET.GLFW.ErrorCode.NoError)
                throw new Exception("GL.GetError() returned " + error.ToString());
        }

        private static string ReadShader(string shaderFileName)
        {
            using (Stream shaderStream = typeof(Program).Assembly.GetManifestResourceStream("grafika_Ur_projekt.Shaders." + shaderFileName))
            using (StreamReader shaderReader = new StreamReader(shaderStream))
                return shaderReader.ReadToEnd();
        }

        private static void Window_Closing()
        {
            // always unbound the vertex buffer first, so no halfway results are displayed by accident
            for (int i = 0; i < 8; i++)
            {
                planets[i].ReleaseGlObject();
            }
            sun.ReleaseGlObject();
            spaceship.ReleaseGlObject();
        }
    }
}
