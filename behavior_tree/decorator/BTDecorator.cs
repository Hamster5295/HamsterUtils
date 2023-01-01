using System;

namespace HamsterUtils
{
    public class BTDecorator : BTNode
    {
        public BTDecorator(BTNode child) : base((node, delta) => BTState.YES, child) { }
    }
}