using System;

namespace HamsterUtils.BehaviorTree
{
    public class BTDecorator : BTNode
    {
        public BTDecorator(BTNode child) : base((node, delta) => BTState.YES, child) { }
    }
}