# Demo 说明
开发用了一下午，结果打包用了一晚上......使用的BundleMaster库在android下加载不了AB包，这个问题待排查。

附带可运行apk包，如果要运行工程，可能需要自己安装一下HybirdCLR，很简单，点击工具栏中的HybirdCLR-install就行。（也可能不需要，因为上述问题把热更功能剔除了）

简单接入了UnityAds广告模块，截图如下。
![1](./Demo1.jpeg)
![2](./Demo2.jpeg)


# 前言

文档：[MixFramework文档](https://www.yuque.com/u27981712/zr68g3) (使用语雀文档，打不开的话需要关闭科学上网)

看了诸多框架，也使用了几个，觉得现有的框架用起来的不太顺手，所以决定自己缝合一个。

现在已经有很多客户端框架了，对各种基础设施都有实现，但就我个人使用体验而言，各框架都有大大小小的让我不舒服的地方。

比如JEngine 对ILRuntime 热更的封装使用起来挺方便的，自定义的生命周期也有意思，但它的事件系统和常见的观察者模式事件系统不同，是模仿Java EventBus 的发布-订阅模式，性能有什么区别先不说，用起来是有点别扭。
一开始看到JEngine 的时候觉得这就是我想用的框架了，但是深入了解后觉得功能不太符合我的需求。

ET 所谓双端，但是我反而觉得放一起很乱，程序集的划分过于别扭，自实现的组件System写法也需要适应，对标签的大量使用阅读起来十分不方便，拓展起来不知道第三方库怎么处理，可能还是我没使用熟练，反正ET在我这里优先级不高。

GameFramework是我最满意的一个框架，但还是有一些不顺手，比如强制全部异步，没有代码热更，之前使用这个框架做了个Demo，以后还是有可能会继续使用GF。

还有其他各种各样的框架。

最后决定自己缝合一个，这样可以自己决定各个功能模块和实现方案。

# 功能
在不涉及具体游戏逻辑的情况下，提供完善的基础游戏开发工具。

以我目前都游戏开发的理解，一个基础框架必须得有4个功能：
1. 资源热更
2. 代码热更
3. 配表
4. 网络

这四个是我觉得大多数游戏都需要的功能，所以作为框架的基础。

此外，还需要UI管理方案、事件系统、对象池、定时器、单例、等等功能，这些适合游戏逻辑相关性较高的功能，基本也是必备。

再其次，还有其他没想到的功能，以后会慢慢拓展，具体参考文档和源码。

# 实现参考

毫不避讳的说，本框架的主要实现方式是缝合......

在诸多开源框架中提取需要的功能，如果需要拓展会在原来基础上进行修改。

因为在这么多巨人的基础上，没有必要为了标榜原创去做一些重复性的工作，当然也是水平有限，自己写可能会有各种问题。

还是以事件系统举例，经过我的比较观察，觉得QFramework、UGF、JEngine中的事件系统都挺好，其中UGF 中的事件系统已经很成熟了，JEngine 虽然我觉得用起来不顺手，但也是一种方案，QF实现的就简单一些，我经过一些考虑还是用了QF的实现方式。

功能从哪“借鉴”来的都会标明，也推荐大家去看看原本的框架。

不过也不完全是缝合，当然还是有一些自己的东西，具体自己看吧。

以下是参考的框架列表，可能会有遗漏。
1. JEngine (https://github.com/JasonXuDeveloper/JEngine)
2. ET (https://github.com/egametang/ET)
3. GameFramework (https://github.com/EllanJiang/GameFramework)
4. QFramework (https://github.com/liangxiegame/QFramework)
5. xlua-framework (https://github.com/smilehao/xlua-framework)
6. JyGame (https://gitee.com/chenbojun/JyGame)
7. MetaExcelDataTool (https://github.com/Meta404Dev/MetaExcelDataTool)
8. hybridclr (https://github.com/focus-creative-games/hybridclr)
9. BundleMaster (https://github.com/mister91jiao/BundleMaster)
10. NPBehave (https://github.com/meniku/NPBehave)
11. UniTask (https://github.com/Cysharp/UniTask)
