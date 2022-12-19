using Godot;

namespace HamsterUtils.BehaviorTree
{// 随机从子节点中选择一个执行
    public class BTRandom : BTParent
    {
        public BTRandom(params BTNode[] children) : base(children)
        {
            _Runnable = (node, delta) =>
            {
                if (_LastRunningNode == null)
                {
                    var picked = _Children[(int)GD.Randi() % _Children.Count];
                    var result = picked.Run(delta);
                    if (result == BTState.RUNNING) _LastRunningNode = picked;
                    return result;
                }
                else
                {
                    var result = _LastRunningNode.Run(delta);
                    if (result != BTState.RUNNING) _LastRunningNode = null;
                    return result;
                }
            };
        }
    }
}