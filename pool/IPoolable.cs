using Godot;

namespace HamsterUtils;

public interface IPoolable
{
    PackedScene GetPoolScene();
    void PoolInit();
    void PoolRelease();
    void PoolDestroy();
}