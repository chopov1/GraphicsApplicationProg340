using OpenGL;
using Tao.FreeGlut;
using System.Numerics;

namespace OpenGLpractice
{
    internal class Program
    {
        public static ShaderProgram shaderProg;
        public static string vertexShader = @"
vec3 vertexPosition = new vec3( 1, 1, 0);
uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;
void main(void)
{
    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
}
";
        public static string fragmentShader = @"
void main(void){
    gl_FragColor = vec4(1,1,1,1);
}
";
        private static VBO<Vector3> triangle, square;
        private static VBO<int> triangleElement, squareElement;
        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(1200, 720);
            Glut.glutCreateWindow("Glut Open GL Demo");
            Glut.glutIdleFunc(OnRenderFrame);
            SetupMyShader();
            DrawTriangle();
            Glut.glutLeaveMainLoop();
            Console.ReadKey();

        }

        private static void DrawTriangle()
        {
            Vector3 p1 = new Vector3(0, 1, 0);
            Vector3 p2 = new Vector3(-1, -1, 0);
            Vector3 p3 = new Vector3(1, -1, 0);

            triangle = new VBO<Vector3>(new Vector3[] {p1, p2, p3 });
            triangleElement = new VBO<int>(new int[] { 0, 1, 2 }, BufferTarget.ElementArrayBuffer);
        }

        private static void SetupMyShader()
        {
            shaderProg = new ShaderProgram(vertexShader, fragmentShader);
            shaderProg.Use();

            shaderProg["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)1200 / 720, .1f, 1000f));
            shaderProg["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.UnitX));

        }


        private static void OnRenderFrame()
        {
            Gl.Viewport(0, 0, 1200, 720);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shaderProg.Use();
            shaderProg["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(-1.5f, 0, 0)));
            uint vertexPosIndex = (uint)Gl.GetAttribLocation(shaderProg.ProgramID, "vertexPosition");
            Gl.EnableVertexAttribArray(vertexPosIndex);
            Gl.BindBuffer(triangle);
            Gl.VertexAttribPointer(vertexPosIndex, triangle.Size, triangle.PointerType, true, 12, IntPtr.Zero);
            Gl.BindBuffer(triangleElement);
            Gl.DrawElements(BeginMode.Triangles, triangleElement.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Glut.glutSwapBuffers();
        }
    }

   
}