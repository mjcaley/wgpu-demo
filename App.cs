using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Silk.NET.Maths;
using Silk.NET.WebGPU;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Glfw;
using Silk.NET.Windowing.Sdl;

namespace WgpuDemo;

public class App : IDisposable
{
    private IWindow? _window;
    private Instance? _instance;
    private Surface? _surface;
    private Adapter? _adapter;
    private Device? _device;
    
    private unsafe Silk.NET.WebGPU.RenderPipeline* _pipeline;
    private unsafe Silk.NET.WebGPU.RenderPassDescriptor _renderPassDescriptor;

    public void Run()
    {
        GlfwWindowing.RegisterPlatform();
        SdlWindowing.RegisterPlatform();

        _window = Window.Create(WindowOptions.Default with
        {
            Size = new Vector2D<int>(800, 600),
            Title = "My WebGPU demo"
        });
        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Run();
    }
    
    public unsafe void OnLoad()
    {
        _instance = new Instance();
        _surface = new Surface(_window, _instance);
        _adapter = new Adapter(_instance, _surface);
        _device = new Device(_adapter);

        var redTriangle = new RedTriangle(_device);
        var pipelineLayout = WebGPU.GetApi().DeviceCreatePipelineLayout(
            _device.Raw,
            new PipelineLayoutDescriptor
            {
                Label = (byte*)Marshal.StringToBSTR("Pipeline layout"),
            }
        );

        var surfaceCapabilities = new SurfaceCapabilities();
        WebGPU.GetApi().SurfaceGetCapabilities(
            _surface.Raw,
            _adapter.Raw,
            ref surfaceCapabilities
        );

        var fragmentState = new FragmentState()
        {
            Module = redTriangle.Raw,
            EntryPoint = (byte*)Marshal.StringToBSTR("fs"),
            // Targets = 
        };
        
        _pipeline = WebGPU.GetApi().DeviceCreateRenderPipeline(
            _device.Raw,
            new RenderPipelineDescriptor
            {
                // Label = (byte*)Marshal.StringToBSTR("Hardcoded red triangle pipeline"),
                Layout = pipelineLayout,
                Vertex = new VertexState {
                    Module = redTriangle.Raw,
                    EntryPoint = (byte*)Marshal.StringToBSTR("vs")
                },
                Fragment = &fragmentState
            }
        );

        var colorAttachments = new RenderPassColorAttachment
        {
            ClearValue = new Color { R=0.3, B=0.3, G=0.3, A=1.0 },
            LoadOp = LoadOp.Clear,
            StoreOp = StoreOp.Store
        };
        _renderPassDescriptor = new RenderPassDescriptor
        {
            Label = (byte*)Marshal.StringToBSTR("Our basic canvas render pass"),
            ColorAttachments = &colorAttachments
        };
        

        // var queue = api.DeviceGetQueue(device);

        // var swapChainFormat = api.SurfaceGetPreferredFormat(surface, adapter);
        // var swapChainDescriptor = new SwapChainDescriptor {
        //     Usage = TextureUsage.RenderAttachment,
        //     Format = swapChainFormat,
        //     Width = (uint)window.FramebufferSize.X,
        //     Height = (uint)window.FramebufferSize.Y,
        //     PresentMode = PresentMode.Fifo,
        // };
        // var swapChain = api.DeviceCreateSwapChain(device, surface, swapChainDescriptor);
    }

    public unsafe void OnUpdate(double delta)
    {
        var capabilities = new SurfaceCapabilities {};
        WebGPU.GetApi().SurfaceGetCapabilities(_surface.Raw, _adapter.Raw, ref capabilities);
        WebGPU.GetApi().SurfaceConfigure(
            _surface.Raw,
            new SurfaceConfiguration
            {
                Usage = TextureUsage.RenderAttachment,
                // Format = 
            }
        );
    }

    private bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                unsafe { WebGPU.GetApi().RenderPipelineRelease(_pipeline); }
                _device?.Dispose();
                _adapter?.Dispose();
                _surface?.Dispose();
                _instance?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~App()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
