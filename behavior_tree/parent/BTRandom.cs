using Godot;

namespace HamsterUtils.BehaviorTree
{// 随机从子节点中选择一个执行
    public class BTRandom : BTParent
    {
        public BTRandom(params BTNode[] children) : base(children)
        {
            mRunnable = (node, delta) =>
            {
                if (last == null)
                {
                    var picked = mChildren[(int)GD.Randi() % mChildren.Count];
                    var result = picked.Run(delta);
                    if (result == BTState.RUNNING) last = picked;
                    return result;
                }
                else
                {
                    var result = last.Run(delta);
                    if (result != BTState.RUNNING) last = null;
                    return result;
                }
            };
        }
    }
}