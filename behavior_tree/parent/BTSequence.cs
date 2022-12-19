namespace HamsterUtils.BehaviorTree
{
    // 子节点依次执行，遇到一个NO就停止，返回NO，所有都执行完返回YES，中途RUNNING就不会执行后面的内容了
    public class BTSequence : BTParent
    {
        public BTSequence(params BTNode[] children) : base(children)
        {
            mRunnable = (nod, delta) =>
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

                        if (i.Run(delta) == BTState.NO) return BTState.NO;
                    }
                    return BTState.YES;
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