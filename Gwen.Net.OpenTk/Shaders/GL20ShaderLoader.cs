﻿using System.Diagnostics;
using OpenTK.Graphics.OpenGL;

namespace Gwen.Net.OpenTk.Shaders
{
    public class GL20ShaderLoader : IShaderLoader
    {
        public IShader Load(string shaderName)
        {
            return Load(shaderName, shaderName);
        }

        public IShader Load(string vertexShaderName, string fragmentShaderName)
        {
            string vSource = EmbeddedShaderLoader.GetShader<GL20ShaderLoader>(vertexShaderName, "vert");
            string fSource = EmbeddedShaderLoader.GetShader<GL20ShaderLoader>(fragmentShaderName, "frag");

            int vShader = GL.CreateShader(ShaderType.VertexShader);
            int fShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vShader, vSource);
            GL.ShaderSource(fShader, fSource);
            // Compile shaders
            GL.CompileShader(vShader);
            GL.CompileShader(fShader);
            Debug.WriteLine(GL.GetShaderInfoLog(vShader));
            Debug.WriteLine(GL.GetShaderInfoLog(fShader));

            int program = GL.CreateProgram();
            // Link and attach shaders to program
            GL.AttachShader(program, vShader);
            GL.AttachShader(program, fShader);

            GL.BindAttribLocation(program, 0, "in_screen_coords");
            GL.BindAttribLocation(program, 1, "in_uv");
            GL.BindAttribLocation(program, 2, "in_color");

            GL.LinkProgram(program);
            Debug.WriteLine(GL.GetProgramInfoLog(program));

            return new GLShader(program, vShader, fShader);
        }
    }
}
