namespace HamsterUtils
{
    // 子节点依次执行，遇到一个NO就停止，返回NO，所有都执行完返回YES，中途遇到RUNNING时，会将RUNNING的节点执行完后中断
    public partial class BTSequence : BTParent
    {
        public BTSequence(params BTNode[] children) : base(children)
        {
            _Runnable = (nod, delta) =>
            {
                if (_LastRunningNode == null)
                {
                    foreach (var i in _Children)
                    {
                        if (i.Run(delta) == BTState.RUNNING)
                        {
                            _LastRunningNode = i;
                            return BTState.RUNNING;
                        }

                        if (i.Run(delta) == BTState.NO) return BTState.NO;
                    }
                    return BTState.YES;
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