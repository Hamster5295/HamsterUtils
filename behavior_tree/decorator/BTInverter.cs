namespace HamsterUtils
{
    public partial class BTInveter : BTDecorator
    {
        public BTInveter(BTNode child) : base(child)
        {
            _Runnable = (node, delta) =>
            {
                var result = _Children[0].Run(delta);
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