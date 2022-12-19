namespace HamsterUtils.BehaviorTree
{
    public class BTInveter : BTDecorator
    {
        public BTInveter(BTNode child) : base(child)
        {
            mRunnable = (node, delta) =>
            {
                var result = mChildren[0].Run(delta);
                switch (result)
                {
                    case BTState.YES: result = BTState.NO; break;
                    case BTState.NO: result = BTState.YES; break;
                }
                return result;
            };
        }
    }
}