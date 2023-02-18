using Godot;

namespace HamsterUtils
{

    // 这个类用于限制Camera2D的位置。

    // Camera2D虽然有自己的Limit，但是他的Position不受到这个Limit的限制，即他拍摄的内容会限制在Limit里，但是
    // Position会飞出去，这使得操作的手感会很差。这个类用于将Position也一起限制住。

    public partial class CameraRestricter : Component<Camera2D>
    {
        private Vector2 _CamSize;

        public override void _Ready()
        {
            _CamSize = GetViewport().GetVisibleRect().Size * Host.Zoom;
        }

        public override void _Process(double delta)
        {
            Host.Position = Vector2.Right * Mathf.Clamp(Host.Position.X, Host.LimitLeft + _CamSize.Y / 2, Host.LimitRight - _CamSize.X / 2)
                            + Vector2.Down * Mathf.Clamp(Host.Position.X, Host.LimitTop + _CamSize.Y / 2, Host.LimitBottom + _CamSize.Y / 2);
        }
    }
}