 这个项目是2015年的时候写的，那个时候还不懂得如何使用git，所以便没有太多的提交操作<br>
 RunningGame是基于cocos2d-js 引擎， 脚本是js，总得来说难度并不大，比较适合练习js的童鞋<br>
 涉及知识 html + css + javascript + cocos2d(缓冲池 + 粒子系统 + 雪碧图 + 位图字体)<br>
 项目结构
```
   |—— frameworks                          cocos2d框架
   |—— publish                             项目打包的入口
   |—— res                                 项目需要的资源
   |—— rungame.html                        游戏入口，启动页面
   |—— main.js                             游戏主逻辑
   |—— |—— fonts                           位图字体
   |—— |—— graphics                        帧动画和静态图片资源
   |—— |—— particles                       粒子系统
   |—— |—— sounds                          背景音乐和特效音乐
   |—— src                                 生产目录
   |—— |—— GameStartScene                  游戏的三个页面
   |—— |—— |—— AboutScene.js               游戏详情页面
   |—— |—— |—— GameScene.js                游戏界面
   |—— |—— |—— MenuScene.js                游戏菜单（这个是游戏一开始看到的页面）
   |—— |—— logic                           游戏逻辑
   |—— |—— |—— FoodManage.js               游戏中食物的逻辑
   |—— |—— |—— ObstacleManager.js          游戏中障碍物的逻辑
   |—— |—— ui                              游戏中组件的初始化或者逻辑
   |—— |—— Game.js                         玩家的状态
   |—— |—— GameConstant.js                 游戏中的常量
   |—— |—— jsList.js                       游戏js文件列表
   |—— |—— resource.js                     游戏中资源的索引文件
   
   
