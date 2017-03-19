
// 游戏状态的常量

var GameConstant = {

    // 游戏状态：空闲，飞行，结束
    GAME_STATE_IDLE : 0,
    GAME_STATE_FLYING : 1,
    GAME_STATE_OVER : 2,

    // player状态：空闲，飞行，撞击，坠落
    HERO_STATE_IDLE : 0,
    HERO_STATE_FLYING : 1,
    HERO_STATE_HIT : 2,
    HERO_STATE_FALL : 3,

    // 食物出现的种类

    ITEM_TYPE_1 : 1,
    ITEM_TYPE_2 : 2,
    ITEM_TYPE_3 : 3,
    ITEM_TYPE_4 : 4,
    ITEM_TYPE_5 : 5,

    //coffee出现
    ITEM_TYPE_COFFEE : 6,

    //蘑菇出现
    ITEM_TYPE_MUSHROOM : 7,

    // 障碍物的种类

    OBSTACLE_TYPE_1 : 1,
    OBSTACLE_TYPE_2 : 2,
    OBSTACLE_TYPE_3 : 3,
    OBSTACLE_TYPE_4 : 4,

    //粒子系统
    PARTICLE_TYPE_1 : 1,
    PARTICLE_TYPE_2 : 2,


    //英雄的生命值，初始
    HERO_LIVES : 5,

    //最低速度
    HERO_MIN_SPEED : 650,

    //最快速度
    HERO_MAX_SPEED : 1400,

    GRAVITY : 10,


    //飞行限定的距离后遭遇障碍物
    OBSTACLE_GAP : 1200,

    //障碍物的速度
    OBSTACLE_SPEED : 300,

    GAME_AREA_TOP_BOTTOM : 100
};