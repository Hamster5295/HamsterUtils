namespace HamsterUtils
{
    public partial class BTWait : BTLeaf
    {
        private float _Timer = 0, _Length = 0;
        private bool _IsTicking = false;

        public BTWait(float length) : base((a, b) => BTState.YES)
        {
            _Length = length;
            _Runnable = (node, delta) =>
            {
                if (_IsTicking)
                {
                    _Timer -= delta;
                    if (_Timer <= 0)
                    {
                        _IsTicking = false;
                        return BTState.YES;
                    }
                    return BTState.RUNNING;
                }
                else
                {
                    _IsTicking = true;
                    _Timer = _Length;
                    return BTState.RUNNING;
                }
            };
        }
    }
}