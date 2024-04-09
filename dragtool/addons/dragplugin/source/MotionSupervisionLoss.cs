using System;
using System.Collections.Generic;
using System.Diagnostics;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;

using static TorchSharp.torch.nn;
using static TorchSharp.torch.nn.functional;
using static TorchSharp.torch.utils.data;


internal class MotionSupervisionLoss : Module<Tensor, Tensor>
{
    public MotionSupervisionLoss(
        string name, 
        List<List<Tensor>> _internalPoints, 
        List<List<Tensor>> _targetPoints,
        Tensor _featureMaps, // F
        Tensor _featureMaps0, // F0
        Tensor _mask = null,
        torch.Device device = null
    ) : base(name)
    {
        RegisterComponents();
        if (device != null && device.type == DeviceType.CUDA)
            this.to(device);
    }

    public override Tensor forward(Tensor input)
    {
        return input;
    }

    private Tensor GetNormalizedVector(Tensor ti, Tensor pi)
    {
        double eps = 1e-7;
        Tensor di = torch.zeros_like(ti);

        Tensor norm = torch.norm(ti - pi);

        return (ti - pi) / norm;
    }
}

