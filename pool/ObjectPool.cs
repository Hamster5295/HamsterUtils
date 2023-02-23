using System;
using System.Collections.Generic;
using Godot;

namespace HamsterUtils;

public partial class ObjectPool : Node
{
    private const int Capacity = 32;

    private static ObjectPool _instance;
    private static readonly Dictionary<PackedScene, Queue<IPoolable>> Pool = new();

    public override void _Ready()
    {
        _instance = this;
    }

    public static void Release<T>(T item) where T : Node, IPoolable
    {
        item.PoolRelease();
        var scene = item.GetPoolScene();
        if (!Pool.ContainsKey(scene)) Pool.Add(scene, new Queue<IPoolable>());
        if (Pool[scene].Count < 32)
        {
            Pool[scene].Enqueue(item);
            item.GetParent().RemoveChild(item);
            _instance.AddChild(item);
        }
        else item.PoolDestroy();
    }

    public static T Get<T>(PackedScene key) where T : IPoolable
    {
        IPoolable item;
        if (Pool.ContainsKey(key) && Pool[key].Count > 0)
            item = Pool[key].Dequeue();
        else item = key.InstantiateOrNull<IPoolable>();

        if (item is T t)
        {
            t.PoolInit();
            return t;
        }

        Log.E(_instance, $"获取到的实例类型为{item.GetType()}，但需要的是{typeof(T)}");
        throw new ArgumentException("发生错误! 请查阅Godot控制台");
    }
}