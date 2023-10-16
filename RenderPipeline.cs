using Silk.NET.WebGPU;

namespace WgpuDemo;


// public class RenderPipeline : IDisposable
// {
//     private unsafe Silk.NET.WebGPU.RenderPipeline* _renderPipeline;

//     public RenderPipeline(Adapter adapter)
//     {
//         Initialize(adapter);
//     }

//     private unsafe void Initialize(Adapter adapter)
//     {
//         WebGPU.GetApi().AdapterRequestDevice(
//             adapter.Raw,
//             new DeviceDescriptor(),
//             new PfnRequestDeviceCallback((status, device, message, userData) => {
//                 if (status != RequestDeviceStatus.Success) {
//                     throw new Exception("Can't request device");
//                 }
//                 _renderPipeline = device;
//             }),
//             null
//         );
//     }

//     private unsafe void Release()
//     {
//         WebGPU.GetApi().RenderPipelineRelease(_renderPipeline);
//     }

//     internal unsafe Silk.NET.WebGPU.RenderPipeline* Raw => _renderPipeline;

//     private bool disposedValue;

//     protected virtual void Dispose(bool disposing)
//     {
//         if (!disposedValue)
//         {
//             if (disposing)
//             {
//                 // TODO: dispose managed state (managed objects)
//             }
//             Release();
//             // TODO: free unmanaged resources (unmanaged objects) and override finalizer
//             // TODO: set large fields to null
//             disposedValue = true;
//         }
//     }

//     // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
//     // ~Instance()
//     // {
//     //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
//     //     Dispose(disposing: false);
//     // }

//     public void Dispose()
//     {
//         // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
//         Dispose(disposing: true);
//         GC.SuppressFinalize(this);
//     }
// }
