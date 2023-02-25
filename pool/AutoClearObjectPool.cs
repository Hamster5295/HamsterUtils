using System.Linq;
using Godot;

namespace HamsterUtils;

public partial class AutoClearObjectPool : ObjectPool
{
    [Export] private float _clearInterval = 20;

    private Timer _timer;

    public override void _Ready()
    {
        _timer = GetNodeOrNull<Timer>("Timer");
        if (!IsInstanceValid(_timer))
        {
            _timer = new Timer();
            _timer.Name = "Timer";
            AddChild(_timer);
        }

        if (_clearInterval < 0) return;
        _timer.WaitTime = _clearInterval;
        _timer.Timeout += ClearObjects;
        _timer.Start();
    }

    public static void ClearObjects()
    {
        foreach (var pair in Pool.Where(pair => pair.Value.Count >= Capacity / 4)) pair.Value.Dequeue().PoolDestroy();
    }
}