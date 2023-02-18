using Godot;

namespace HamsterUtils
{
    // 不要被这玩意的名字迷惑了，他其实是一个Node，应当作为目标摄像机的子节点
    public partial class MinimapCamera2D : MinimapItem
    {
        [Export] private float _ZoomSpeed = 0.1f;

        private Camera2D _Cam;
        private Vector2 _TargetZoom;

        public override void _Inited()
        {
            foreach (var item in GetChildren())
            {
                if (item is Camera2D cam)
                {
                    _Cam = cam;
                    break;
                }
            }
            if (_Cam == null)
            {
                GD.PrintErr("MinimapCamera2D需要有一个Camera2D子节点, 作为小地图的摄像机!");
                return;
            }
            _TargetZoom = _Cam.Zoom;
            _Cam.MakeCurrent();
        }

        public override void _Process(double delta)
        {
            if ((_TargetZoom - _Cam.Zoom).LengthSquared() > 1e-4)
            {
                _Cam.Zoom = _Cam.Zoom.Lerp(_TargetZoom, _ZoomSpeed);
            }
            else _Cam.Zoom = _TargetZoom;
        }

        public void SetZoom(Vector2 zoom, bool animate = true)
        {
            _TargetZoom = zoom;
            if (!animate) _Cam.Zoom = zoom;
        }
    }
}