using System.Runtime.InteropServices;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.WebGPU;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Glfw;
using Silk.NET.Windowing.Sdl;

namespace WgpuDemo;

public class WgpuDemo
{
    public static void Main() {
        using var demo = new App();
        demo.Run();        
    }
}
