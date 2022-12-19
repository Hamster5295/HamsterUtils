using System;
using System.Collections.Generic;

namespace HamsterUtils.BehaviorTree
{
    public abstract class BTNode
    {
        protected Func<BTNode, float, BTState> mRunnable;
        protected List<BTNode> mChildren;

        public BTNode(Func<BTNode, float, BTState> r, params BTNode[] children)
        {
            this.mRunnable = r;
            this.mChildren = new List<BTNode>(children);
        }

        public virtual BTState Run(float delta) => mRunnable(this, delta);
    }

    public enum BTState
    {
        YES, NO, RUNNING
    }
}