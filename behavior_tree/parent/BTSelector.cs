namespace HamsterUtils
{
    // 从第一个子节点开始尝试运行，直到找到一个YES就停止，返回YES
    public partial class BTSelector : BTParent
    {
        public BTSelector(params BTNode[] children) : base(children)
        {
            _Runnable = (node, delta) =>
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

                        if (i.Run(delta) == BTState.YES) return BTState.YES;
                    }
                    return BTState.NO;
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