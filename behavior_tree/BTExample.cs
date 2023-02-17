using System;
// 演示一下这个行为树的使用方法

namespace HamsterUtils
{
    public class BTExample
    {
        // 4种复合节点：BTSelector, BTSequence, BTRandom, BTProcess
        // 2种叶节点：BTLeaf, BTWait
        // 1种装饰节点：BTInverser

        // 根节点，可以是上面的任意一种，这里以Sequence为例
        // 目标：实现一个计数器，每数到10就输出一次
        BTSequence root = new BTSequence(
            // 直接在构造函数里面实例化子节点，这个语法有点像 Dart 或者安卓的 Jetpack Compose

            // Leaf叶节点，用Lambda表达式实现功能，成功则返回 BTState.YES, 失败 .NO, 进行中 .RUNNING
            new BTLeaf((node, delta) =>
            {
                // node是这个BTLeaf本身的实参，这可以转型为BTLeaf以调用其内置的数据存储/读取方法
                BTLeaf n = node as BTLeaf;
                int i = n.Get<int>("i", 0);

                if (i < 10)
                {
                    i++;
                    n.Set("i", i);

                    // 这里i还没到10，所以处于“正在执行”的RUNNING状态
                    return BTState.RUNNING;
                }
                i = 0;
                n.Set("i", i);

                // i到10了，告诉Sequence这个节点的任务结束了，让Sequence跳到下一个兄弟节点
                return BTState.YES;
            }),

            new BTLeaf((node, delta) =>
            {
                // 数到10了，输出！
                Console.Write("gg");

                // 末端的节点，只要不返回RUNNING就行
                return BTState.YES;
            })
        );
    }
}