using Godot;

namespace HamsterUtils.Minimap
{
    public class MinimapItem : Node2D
    {
        [Export] public NodePath host;
        [Export] public bool parentAsHost = true;
        [Export] public bool associateWithHost;
        [Export] public bool updateWithHost;

        public Node2D Host { get; private set; }

        public override void _Ready()
        {
            if (parentAsHost)
                Host = GetParentOrNull<Node2D>();
            else
                Host = GetNodeOrNull<Node2D>(host);

            if (Host == null)
            {
                GD.PrintErr("MinimapItem需要一个Node2D或其子类型的节点作为其控制者! ");
                return;
            }

            GetParent().RemoveChild(this);
            Minimap.AddItem(this);
        }
    }
}