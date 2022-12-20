using Godot;

namespace HamsterUtils
{
    /* 
    一个类似于Unity中MonoBehavior的脚本
    T 决定他的Host的类型 (Host可以简单理解为他的“主人”)
    
    使用示例:
    把一个Node放在Camera2D的子节点，然后给Node添加以下脚本:

    public class CameraMovement : Component<Camera2D>{
        float speed = 10;

        public override void _Process(float delta){
            Host.Translate(Vector2.Up*delta*speed);
        }
    }

    则摄像机匀速向上运动
    */
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