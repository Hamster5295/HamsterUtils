using System;
using Godot;

namespace HamsterUtils
{
    public class BTCondition : BTLeaf
    {
        public BTCondition(Func<bool> func) : base((a, b) => BTUtils.GetState(func())) { }
    }
}