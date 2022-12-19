namespace HamsterUtils.BehaviorTree
{
    public class BTWait : BTLeaf
    {
        private float mTimer = 0, mLength = 0;
        private bool mIsTicking = false;

        public BTWait(float length) : base((a, b) => BTState.YES)
        {
            mLength = length;
            mRunnable = (node, delta) =>
            {
                if (mIsTicking)
                {
                    mTimer -= delta;
                    if (mTimer <= 0)
                    {
                        mIsTicking = false;
                        return BTState.YES;
                    }
                    return BTState.RUNNING;
                }
                else
                {
                    mIsTicking = true;
                    mTimer = mLength;
                    return BTState.RUNNING;
                }
            };
        }
    }
}