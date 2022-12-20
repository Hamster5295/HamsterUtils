# HamsterUtils.Component
> namespace HamsterUtils

一个类似于Unity中MonoBehavior的脚本

## 成员
| 名称 | 访问修饰 | 说明                                    |
| ---- | -------- | --------------------------------------- |
| Host | public   | 该Component的"主人"，可以直接在子类调用 |
| T    | \        | 决定他的主人(Host)的类型                |

## 使用示例

把一个Node放在Camera2D的子节点，然后给Node添加以下脚本:

```C#
public class CameraMovement : Component<Camera2D>
{
    float speed = 10;

    public override void _Process(float delta){
        Host.Translate(Vector2.Up*delta*speed);
    }
}
```

则摄像机匀速向上运动
