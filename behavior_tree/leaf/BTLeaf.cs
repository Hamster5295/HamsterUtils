using System;
using System.Collections.Generic;

namespace HamsterUtils.BehaviorTree
{
    // 叶节点，可以自定义逻辑，没有子项

    public class BTLeaf : BTNode
    {
        private Dictionary<string, object> mDict = new Dictionary<string, object>();

        public BTLeaf(Func<BTNode, float, BTState> r) : base(r) { }

        public T Get<T>(string key, T defaultValue)
        {
            if (mDict.ContainsKey(key))
            {
                if (mDict[key] is T t)
                {
                    return t;
                }
            }
            return defaultValue;
        }

        public void Set(string key, object value)
        {
            if (!mDict.ContainsKey(key))
                mDict.Add(key, value);
            else mDict[key] = value;
        }
    }
}