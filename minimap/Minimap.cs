using Godot;
using System.Collections.Generic;

namespace HamsterUtils
{
    public partial class Minimap : SubViewportContainer
    {
        public static Minimap Current { get; private set; }

        [Export] public float ZoomRate { get; set; } = 0.1f;

        private List<MinimapItem> _Items = new List<MinimapItem>();

        private SubViewport _Viewport;

        private bool _IsReady = false;

        public override void _EnterTree()
        {
            Current = this;

            _Viewport = GetNode<SubViewport>("SubViewport");

            _IsReady = true;
        }

        public static void AddItem(MinimapItem item) => Current.AddItemInterval(item);

        private void AddItemInterval(MinimapItem item)
        {
            _Items.Add(item);
            _Viewport.AddChild(item);

            item.Position = Vector2.Zero;

            item.Connect(nameof(MinimapItem.HostExited),new Callable(this,nameof(OnItemHostExited)));
        }

        private void OnItemHostExited(MinimapItem which)
        {
            if (!which.AssociateWithHost) return;
            _Items.Remove(which);
        }

    }
}