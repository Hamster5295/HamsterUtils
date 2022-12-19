using System;

namespace HamsterUtils.BehaviorTree
{
    public class BTDecorator : BTNode
    {
        public BTDecorator(BTNode child) : base((node, delat) => BTState.YES, child) { }
    }
}