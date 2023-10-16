namespace WgpuDemo;

using Silk.NET.WebGPU;

public static class WebGPUExtensions
{
    // Doesn't work
    
    public static unsafe Silk.NET.WebGPU.Surface* CreateSurface(this Silk.NET.WebGPU.Instance instance, SurfaceDescriptor surfaceDescriptor)
    {
        var api = WebGPU.GetApi();
        return api.InstanceCreateSurface(&instance, surfaceDescriptor);
    }

    public static unsafe Silk.NET.WebGPU.Surface* CreateSurface(Silk.NET.WebGPU.Instance instance, SurfaceDescriptor* surfaceDescriptor)
    {
        var api = WebGPU.GetApi();
        return api.InstanceCreateSurface(&instance, surfaceDescriptor);
    }

    public static unsafe Silk.NET.WebGPU.RenderPipeline* CreateRenderPipeline(Silk.NET.WebGPU.Device device, RenderPipelineDescriptor pipelineDescriptor)
    {
        var api = WebGPU.GetApi();
        return api.DeviceCreateRenderPipeline(&device, pipelineDescriptor);
    }

    public static unsafe Silk.NET.WebGPU.RenderPipeline* CreateRenderPipeline(Silk.NET.WebGPU.Device device, RenderPipelineDescriptor* pipelineDescriptor)
    {
        var api = WebGPU.GetApi();
        return api.DeviceCreateRenderPipeline(&device, pipelineDescriptor);
    }
}
