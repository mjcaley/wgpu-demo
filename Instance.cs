using Silk.NET.WebGPU;
using Silk.NET.Windowing;

namespace WgpuDemo;


public class Instance : IDisposable
{
    private static readonly WebGPU _api = WebGPU.GetApi();

    private unsafe Silk.NET.WebGPU.Instance* _instance;

    public Instance()
    {
        Initialize();
    }

    private unsafe void Initialize()
    {
        _instance = WebGPU.GetApi().CreateInstance(new InstanceDescriptor());
    }

    private unsafe void Release()
    {
        WebGPU.GetApi().InstanceRelease(_instance);
    }

    internal unsafe Silk.NET.WebGPU.Instance* Raw => _instance;

    public unsafe Silk.NET.WebGPU.Surface* CreateSurface(SurfaceDescriptor surfaceDescriptor) => _api.InstanceCreateSurface(_instance, surfaceDescriptor);

    public unsafe Silk.NET.WebGPU.Surface* CreateSurface(SurfaceDescriptor* surfaceDescriptor) => _api.InstanceCreateSurface(_instance, surfaceDescriptor);

    private bool disposedValue;

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
    // ~Instance()
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
