using Godot;

namespace HamsterUtils.Minimap
{
    // 不要被这玩意的名字迷惑了，他其实是一个Node，应当作为目标摄像机的子节点
    public class MinimapCamera2D : Component<Camera2D>
    {
        public override void _Ready()
        {
            Minimap.SetCameraTracePoint(Host);
        }
    }
}