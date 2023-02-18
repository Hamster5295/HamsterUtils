namespace HamsterUtils
{
    public partial class BTUtils
    {
        public static BTState GetState(bool state)
        {
            return state ? BTState.YES : BTState.NO;
        }
    }
}