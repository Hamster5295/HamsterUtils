using System.Collections.Generic;

namespace HamsterUtils
{
    // 开始执行后，就会一直RUNNING，直到到所有子节点执行完毕或遇到错误（子节点返回NO）
    public class BTProcess : BTParent
    {
        private Queue<BTNode> _Queue = new Queue<BTNode>();

        public BTProcess(params BTNode[] children) : base(children)
        {
            _Runnable = (nod, delta) =>
            {
                // 如果上一次已经跑完，则将队列重新填满
                if (_Queue.Count == 0) _Queue = new Queue<BTNode>(_Children);

                // 执行队列中的下一个任务
                var result = _Queue.Peek().Run(delta);

                // YES - 跑完了，就从队列里删除
                if (result == BTState.YES) _Queue.Dequeue();
                // NO - 失败了，就重新来过，删除整个队列
                else if (result == BTState.NO)
                {
                    _Queue.Clear();
                    return BTState.NO;
                }
                // RUNNING - 还在跑，就不做处理

                // 如果队列没跑完，则Process还没跑完，返回RUNNING；否则就是跑完了，返回YES
                return _Queue.Count == 0 ? BTState.YES : BTState.RUNNING;
            };
        }
    }
}