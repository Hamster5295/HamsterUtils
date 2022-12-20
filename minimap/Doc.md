# HamsterUtils.Minimap
一个可以快速建立小地图的工具

## 快速入门
1. 在场景中加入一个 [Minimap](Minimap.tscn) 节点 (他是个UI节点)
2. 在你想要添加小地图图标的节点下加入一个 Node2D 子节点
3. 将 [MinimapItem](MinimapItem.cs) 加载到这个 Node2D 节点上  
4. 在这个 Node2D 下添加你需要显示在小地图上的内容(Sprite, Line2D, ...)作为子节点
5. 在这个 Node2D 的属性中编辑你想要同步的属性
6. 当然，别忘了按照上面的方法添加一个摄像机，这可以使用 [MinimapCamera2D](MinimapCamera2D.cs)
7. 成功了!

## 示例
[示例场景](example/minimap_demo.tscn)

## 文档
### MinimapItem
用于将小地图的要素添加到小地图所属的Viewport内，并控制那些要素与主世界中的对应角色同步。  
你可以将你想要添加的要素加入到一个 MinimapItem 的子节点中  

MinimapItem 本质上做了两件事：  
1. 将自己连同自己的子节点放置到小地图所属的 Viewport 里
2. 依据给出的 **“控制者”/Host** 和相关属性来与 Host 的位置，旋转，缩放同步。换句话说，Host就是在正常世界里的角色，而MinimapItem就是这个角色在小地图里的图标

| 属性              | 访问       | 说明                                                                                                       |
| ----------------- | ---------- | ---------------------------------------------------------------------------------------------------------- |
| _HostPath         | private    | 指定 控制者/Host 的 NodePath。 如果 ParentAsHost 被选中，那么这一项可以空着                                |
| ParentAsHost      | public get | 直接使用该节点的父节点作为控制者                                                                           |
| AssociateWithHost | public get | 与控制者关联，当控制者从场景树里被删除时，自动销毁Minimap中的对应节点，绝大多数情况下应该都要设置成true    |
| PositionSync      | public get | 与控制者的位置同步，大多数情况下应当选中，除非你需要一些位置与控制者不同步的元素，例如用于描绘弹道的Line2D |
| RotationSync      | public get | 与控制者的旋转同步                                                                                         |
| ScaleSync         | public get | 与控制者的缩放同步                                                                                         |

| 方法      | 修饰            | 说明                                                                                 |
| --------- | --------------- | ------------------------------------------------------------------------------------ |
| _Inited() | protect virtual | 当这个MinimapItem完成初始化（被转移到Minimap的Viewport内）后被执行，可以在子类中重写 |

| 信号                          | 说明                                                                                                  |
| ----------------------------- | ----------------------------------------------------------------------------------------------------- |
| Inited(MinimapItem which)     | 当这个MinimapItem完成初始化（被转移到Minimap的Viewport内）后触发。<br><br>which → 当前的MinimapItem   |
| HostExited(MinimapItem which) | 当这个MinimapItem的控制者被销毁后触发。通常用于销毁这个MinimapItem。<br><br>which → 当前的MinimapItem |

***
### MinimapCamera2D
> 继承自 MinimapItem  

用于向小地图中添加相应的摄像机，通常和游戏的主摄像机同步位置、旋转。

这是个用于快速实现功能的工具。你也可以直接给Camera2D写一套自己的控制系统，然后把这玩意抛弃掉。

**需要一个Camera2D子节点**

| 属性       | 访问    | 说明                                                 |
| ---------- | ------- | ---------------------------------------------------- |
| _ZoomSpeed | private | 缩放动画的快慢，本质上是在进行插值运算时输入的weight |

|方法|修饰|说明|
|-|-|-|
|SetZoom(Vector2 zoom, bool animate = true)|public void|进行缩放。<br><br>zoom → 改变后的缩放<br>animate → 是否进行插值动画|
