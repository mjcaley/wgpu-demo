using System.Runtime.InteropServices;
using System.Text;
using Silk.NET.WebGPU;

namespace WgpuDemo;

class RedTriangle : IDisposable
{
    private readonly static WebGPU _api = WebGPU.GetApi();

    private static readonly unsafe byte* redShader = (byte*)Marshal.StringToBSTR(@"
        @vertex fn vs(@builtin(vertex_index) vertexIndex : u32) -> @builtin(position) vec4f
        {
            let pos = array(
            vec2f( 0.0,  0.5),  // top center
            vec2f(-0.5, -0.5),  // bottom left
            vec2f( 0.5, -0.5)   // bottom right
            );
    
            return vec4f(pos[vertexIndex], 0.0, 1.0);
        }
        
        @fragment fn fs() -> @location(0) vec4f {
            return vec4f(1.0, 0.0, 0.0, 1.0);
        }");

    private unsafe ShaderModule* _shader;

    public RedTriangle(Device device)
    {
        Initalize(device);
    }

    public unsafe void Initalize(Device device)
    {
        var shaderDescriptor = new ShaderModuleWGSLDescriptor { Code = redShader };
        shaderDescriptor.Chain.SType = SType.ShaderModuleWgslDescriptor;
        
        fixed (byte* b = Encoding.ASCII.GetBytes("Hardcoded shader"))
        {
            _shader = WebGPU.GetApi().DeviceCreateShaderModule(
                device.Raw,
                new ShaderModuleDescriptor
                {
                    // Label = b,
                    NextInChain = &shaderDescriptor.Chain
                }
            );
        }
    }

    internal unsafe ShaderModule* Raw => _shader;

    private unsafe void Release()
    {
        if (_shader != null)
        {
            WebGPU.GetApi().ShaderModuleRelease(_shader);
            _shader = null;
        }
    }

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
    // ~Triangle()
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
