namespace HamsterUtils.BehaviorTree
{
    // 从第一个子节点开始尝试运行，直到找到一个YES就停止，返回YES
    public class BTSelector : BTParent
    {
        public BTSelector(params BTNode[] children) : base(children)
        {
            mRunnable = (node, delta) =>
            {
                if (last == null)
                {
                    foreach (var i in mChildren)
                    {
                        if (i.Run(delta) == BTState.RUNNING)
                        {
                            last = i;
                            return BTState.RUNNING;
                        }

                        if (i.Run(delta) == BTState.YES) return BTState.YES;
                    }
                    return BTState.NO;
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