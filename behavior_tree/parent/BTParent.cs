namespace HamsterUtils.BehaviorTree
{
    public abstract class BTParent : BTNode
    {
        protected BTNode _LastRunningNode = null;
        public BTParent(params BTNode[] children) : base((node, delta) => BTState.YES, children) { }
    }
}