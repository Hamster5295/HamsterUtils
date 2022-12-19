using System;
using System.Collections.Generic;

namespace HamsterUtils.BehaviorTree
{
    public abstract class BTNode
    {
        protected Func<BTNode, float, BTState> _Runnable;
        protected List<BTNode> _Children;

        public BTNode(Func<BTNode, float, BTState> r, params BTNode[] children)
        {
            this._Runnable = r;
            this._Children = new List<BTNode>(children);
        }

        public virtual BTState Run(float delta) => _Runnable(this, delta);
    }

    public enum BTState
    {
        YES, NO, RUNNING
    }
}