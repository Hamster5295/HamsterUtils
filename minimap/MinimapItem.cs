using Godot;

namespace HamsterUtils.Minimap
{
    public class MinimapItem : Node2D
    {
        // 当控制者从场景树被删除时释放该信号
        [Signal] public delegate void HostExited(MinimapItem which);

        [Export] private NodePath HostPath { get; set; }

        // 直接使用该节点的父节点作为控制者
        [Export] public bool ParentAsHost { get; private set; } = true;

        // 与控制者关联，当控制者从场景树里被删除时，自动销毁Minimap中的对应节点，绝大多数情况下应该都要设置成true
        [Export] public bool AssociateWithHost { get; private set; } = true;

        // 与控制者的位置同步
        [Export] public bool PositionSync { get; private set; } = false;

        // 与控制者的旋转同步
        [Export] public bool RotationSync { get; private set; } = false;

        // 与控制者的缩放同步
        [Export] public bool ScaleSync { get; private set; } = false;

        public Node2D Host { get; private set; }

        private bool _IsReady = false;

        public override void _Ready()
        {
            if (ParentAsHost)
                Host = GetParentOrNull<Node2D>();
            else
                Host = GetNodeOrNull<Node2D>(HostPath);

            if (Host == null)
            {
                GD.PrintErr("MinimapItem需要一个Node2D或其子类型的节点作为其控制者! ");
                return;
            }

            Host.Connect("tree_exited", this, nameof(OnHostExited), flags: (uint)ConnectFlags.Oneshot);

            CallDeferred(nameof(Init));
        }

        private void Init()
        {
            GetParent().RemoveChild(this);
            Minimap.AddItem(this);
            _IsReady = true;
        }

        public override void _Process(float delta)
        {
            if (!_IsReady) return;

            if (PositionSync) Position = Host.GlobalPosition;
            if (RotationSync) Rotation = Host.GlobalRotation;
            if (ScaleSync) Scale = Host.Scale;
        }

        private void OnHostExited()
        {
            EmitSignal(nameof(HostExited), this);
            if (AssociateWithHost) QueueFree();
        }
    }
}