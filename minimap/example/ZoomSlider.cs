using Godot;
using System;

namespace HamsterUtils.Minimap
{
    // 一个可以控制小地图缩放的横向Slider
    // 用于演示的工具，基本上不可复用，不过改一改拿出来也不是不行
    public class ZoomSlider : HSlider
    {
        private bool _IsDragging = false;

        public override void _Ready()
        {
            Connect("drag_started", this, nameof(OnDragStarted));
            Connect("drag_ended", this, nameof(OnDragEnded));
        }

        public override void _Process(float delta)
        {
            if (_IsDragging) Minimap.SetZoom(Vector2.One * (float)Value);
        }

        private void OnDragStarted()
        {
            _IsDragging = true;
        }

        private void OnDragEnded(bool isChanged)
        {
            _IsDragging = false;
        }
    }
}