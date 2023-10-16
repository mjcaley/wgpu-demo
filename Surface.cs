using Silk.NET.GLFW;
using Silk.NET.WebGPU;
using Silk.NET.Windowing;

namespace WgpuDemo;

public class Surface : IDisposable
{
    static private readonly WebGPU _api = WebGPU.GetApi();
    private unsafe Silk.NET.WebGPU.Surface* _surface;
    private bool disposedValue;

    public Surface(IWindow window, Instance instance)
    {
        Initialize(window, instance);
    }

    private unsafe void Initialize(IWindow window, Instance instance)
    {
        _surface = window.CreateWebGPUSurface(WebGPU.GetApi(), instance.Raw);
    }

    internal unsafe Silk.NET.WebGPU.Surface* Raw => _surface;


    //public unsafe SurfaceCapabilities* GetCapabilities() => _api.SurfaceGetCapabilities(_surface, adapter)


    public unsafe void Release()
    {
        if (_surface != null)
        {
            WebGPU.GetApi().SurfaceRelease(_surface);
            _surface = null;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }
            Release();
            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Surface()
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
