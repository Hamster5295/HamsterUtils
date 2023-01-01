using System;
using System.Collections.Generic;

namespace HamsterUtils
{
    // 叶节点，可以自定义逻辑，没有子项

    public class BTLeaf : BTNode
    {
        private Dictionary<string, object> _Cache = new Dictionary<string, object>();

        public BTLeaf(Func<BTNode, float, BTState> r) : base(r) { }

        public T Get<T>(string key, T defaultValue)
        {
            if (_Cache.ContainsKey(key))
            {
                if (_Cache[key] is T t)
                {
                    return t;
                }
            }
            return defaultValue;
        }

        public void Set(string key, object value)
        {
            if (!_Cache.ContainsKey(key))
                _Cache.Add(key, value);
            else _Cache[key] = value;
        }
    }
}