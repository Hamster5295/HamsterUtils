using Godot;
using System.Collections.Generic;

namespace HamsterUtils.Minimap
{
    public class Minimap : ViewportContainer
    {
        public static Minimap Current { get; private set; }

        private List<MinimapItem> _Items = new List<MinimapItem>();

        private Viewport _Viewport;
        private Camera2D _Cam;
        private Node2D _CamTracePoint;

        private bool _IsInited = false;

        public override void _EnterTree()
        {
            Current = this;

            _Viewport = GetNode<Viewport>("Viewport");
            _Cam = GetNode<Camera2D>("Viewport/Cam");

            _IsInited = true;
        }

        public override void _Process(float delta)
        {
            if (!_IsInited) return;

            if (IsInstanceValid(_CamTracePoint))
            {
                _Cam.Position = _CamTracePoint.GlobalPosition;
                _Cam.Rotation = _CamTracePoint.Rotation;
            }
        }

        public static void SetCameraTracePoint(Node2D tracePoint)
        {
            Current._CamTracePoint = tracePoint;
        }

        public static void AddItem(MinimapItem item)
        {
            Current.AddItemInterval(item);
        }

        private void AddItemInterval(MinimapItem item)
        {
            _Items.Add(item);
            _Viewport.AddChild(item);
        }
    }
}