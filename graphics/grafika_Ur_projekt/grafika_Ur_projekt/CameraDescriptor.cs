using Silk.NET.GLFW;
using Silk.NET.Input;
using Silk.NET.Maths;
using System.Numerics;

namespace grafika_Ur_projekt
{
    internal class CameraDescriptor
    {
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public float Speed { get; set; }
        public float Fov { get; set; } = 45f;
        public float Sensitivity { get; set; }

        public Vector3D<float> Position { get; set; }
        public Vector3D<float> Front { get; set; }
        public Vector3D<float> Up { get; set; }
        public Vector3D<float> Right { get; set; }
        public Vector3D<float> WorldUp { get; set; }


        public CameraDescriptor (Vector3D<float> position, Vector3D<float> up, float yaw, float pitch)
        {
            Position = position;
            WorldUp = up;
            Yaw = yaw;
            Pitch = pitch;
            Front = new Vector3D<float>(0.0f, 0.0f, 1.0f);
            Speed = 2.5f;
            Sensitivity = 0.4f;
            //Zoom = 45.0f;

            UpdateCameraVectors();
        }

        public void ProcessKeyboard(Keys key, float deltaTime)
        {
            float velocity = Speed * deltaTime;
            if (key == Keys.W)
                Position += Front * velocity;
            if (key == Keys.S)
                Position -= Front * velocity;
            if (key == Keys.A)
                Position -= Right * velocity;
            if (key == Keys.D)
                Position += Right * velocity;
        }

        public void ProcessMouseMovement(float xOffset, float yOffset, bool constrainPitch = true)
        {
            xOffset *= Sensitivity;
            yOffset *= Sensitivity;

            Yaw += xOffset;
            Pitch -= yOffset;

            Pitch = Math.Clamp(Pitch, -80.0f, 80.0f);

            UpdateCameraVectors();
        }

        private float DegreesToRadians(float degree)
        {
            return degree * ((float)Math.PI / 180f);
        }

        public void UpdateCameraVectors()
        {
            Vector3D<float> front = new Vector3D<float>();
            front.X = (float)(Math.Cos(DegreesToRadians(Yaw)) * Math.Cos(DegreesToRadians(Pitch)));
            front.Y = (float)Math.Sin(DegreesToRadians(Pitch));
            front.Z = (float)(Math.Sin(DegreesToRadians(Yaw)) * Math.Cos(DegreesToRadians(Pitch)));
            Front = Vector3D.Normalize(front);

            Right = Vector3D.Normalize(Vector3D.Cross(Front, Vector3D<float>.UnitY));
            Up = Vector3D.Normalize(Vector3D.Cross(Right, Front));
        }
     
        public Matrix4X4<float> GetViewMatrix()
        {
            Vector3D<float> forward = new Vector3D<float>(
                (float)Math.Cos(DegreesToRadians(Yaw)) * (float)Math.Cos(DegreesToRadians(Pitch)),
                (float)Math.Sin(DegreesToRadians(Pitch)),
                (float)Math.Sin(DegreesToRadians(Yaw) * (float)Math.Cos(DegreesToRadians(Pitch)))
                );

            forward = Vector3D.Normalize(forward);

            Vector3D<float> right = Vector3D.Normalize(Vector3D.Cross(forward, WorldUp));
            Vector3D<float> up = Vector3D.Normalize(Vector3D.Cross(right, forward));

            return Matrix4X4.CreateLookAt(Position, Position + forward, up);
        }
        public Matrix4X4<float> GetProjectionMatrix(float width, float height)
        {
            return Matrix4X4.CreatePerspectiveFieldOfView(DegreesToRadians(Fov), width / height, 0.1f, 1000f);
        }
    }
}
