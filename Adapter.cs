using Silk.NET.WebGPU;

namespace WgpuDemo;


public class Adapter : IDisposable
{
    private unsafe Silk.NET.WebGPU.Adapter* _adapter;

    public Adapter(Instance instance, Surface surface)
    {
        Initialize(instance, surface);
    }

    private unsafe void Initialize(Instance instance, Surface surface)
    {
        WebGPU.GetApi().InstanceRequestAdapter(
            instance.Raw,
            new RequestAdapterOptions() {
                CompatibleSurface = surface.Raw,
                PowerPreference = PowerPreference.HighPerformance,
                ForceFallbackAdapter = false,
            },
            new PfnRequestAdapterCallback((status, adapter, message, userData) => {
                if (status != RequestAdapterStatus.Success) {
                    throw new Exception("Can't request adapter");
                }
                _adapter = adapter;
            }),
            null
        );
    }

    private unsafe void Release()
    {
        WebGPU.GetApi().AdapterRelease(_adapter);
    }

    internal unsafe Silk.NET.WebGPU.Adapter* Raw => _adapter;

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
