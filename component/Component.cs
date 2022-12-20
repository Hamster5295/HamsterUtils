using Godot;

namespace HamsterUtils
{
    public class Component<T> : Node where T : Node
    {
        private const int MAX_SEARCH_DEPTH = 5;

        public T Host { get; private set; }

        public override void _EnterTree()
        {
            string path = "..";
            for (int i = 0; i < MAX_SEARCH_DEPTH; i++)
            {
                Host = GetNodeOrNull<T>(path);
                if (Host == null) path += "/..";
                else break;
            }
        }
    }
}