namespace HamsterUtils.BehaviorTree
{
    public abstract class BTParent : BTNode
    {
        protected BTNode last = null;
        public BTParent(params BTNode[] children) : base((node, delta) => BTState.YES, children) { }
    }
}