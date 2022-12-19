using Godot;
using System.Collections.Generic;

namespace HamsterUtils.Minimap
{
    public class Minimap : ViewportContainer
    {
        public static Minimap Current { get; private set; }
        public static Camera2D Camera { get => Current._Cam; }

        [Signal] public delegate void CameraTracePointExited();

        [Export] public float ZoomRate { get; set; } = 0.1f;

        private List<MinimapItem> _Items = new List<MinimapItem>();

        private Viewport _Viewport;
        private Camera2D _Cam;
        private Node2D _CamTracePoint;

        private bool _IsReady = false;
        private Vector2 _TargetZoom;

        public override void _EnterTree()
        {
            Current = this;

            _Viewport = GetNode<Viewport>("Viewport");
            _Cam = GetNode<Camera2D>("Viewport/Cam");

            _TargetZoom = _Cam.Zoom;

            _IsReady = true;
        }

        public override void _Process(float delta)
        {
            if (!_IsReady) return;

            if (IsInstanceValid(_CamTracePoint))
            {
                _Cam.Position = _CamTracePoint.GlobalPosition;
                _Cam.Rotation = _CamTracePoint.Rotation;
            }

            if ((_TargetZoom - _Cam.Zoom).LengthSquared() > 0.01f)
            {
                _Cam.Zoom = _Cam.Zoom.LinearInterpolate(_TargetZoom, ZoomRate);
            }
        }

        public static void SetCameraTracePoint(Node2D tracePoint)
        {
            Current._CamTracePoint = tracePoint;
            tracePoint.Connect("tree_exited", Current, nameof(CamTracePointExited), flags: (uint)ConnectFlags.Oneshot);
        }

        public static void AddItem(MinimapItem item) => Current.AddItemInterval(item);

        public static void SetZoom(Vector2 zoom, bool animate = true) => Current.SetZoomInterval(zoom, animate);

        private void SetZoomInterval(Vector2 zoom, bool animate = true)
        {
            _TargetZoom = zoom;
            if (!animate) _Cam.Zoom = zoom;
        }

        private void AddItemInterval(MinimapItem item)
        {
            _Items.Add(item);
            _Viewport.AddChild(item);

            item.Position = Vector2.Zero;

            item.Connect(nameof(MinimapItem.HostExited), this, nameof(OnItemHostExited));
        }

        private void CamTracePointExited() => EmitSignal(nameof(CameraTracePointExited));

        private void OnItemHostExited(MinimapItem which)
        {
            if (!which.AssociateWithHost) return;
            _Items.Remove(which);
        }

    }
}