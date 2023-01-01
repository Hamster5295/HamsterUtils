using System;
using System.Collections.Generic;

namespace HamsterUtils
{
    // 所有节点的父类，拥有基础逻辑
    public abstract class BTNode
    {
        // 函数委托，当执行这个节点时被执行
        protected Func<BTNode, float, BTState> _Runnable;

        // 子节点列表
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