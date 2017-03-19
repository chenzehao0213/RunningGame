


var FoodManage=cc.Class.extend({
    _container:null,
    _gameScene:null,
    _itemToAnimate:null,

    _pattern:0,                 //排列类型  0-水平 1-垂直 2-之字形 3-随机
    _patternPosY:0,             //食物的y坐标
    _patternStep:0,             //每个食物的垂直距离
    _patternDirection:0,        //食物排列的方向
    _patternGap:0,              //每个食物的水平距离
    _patternGapCount:0,         //当前食物的累计水平距离
    _patternChangeDistance:0,   //食物组的水平跨度
    _patternLength:0,           //垂直排列下食物的长度
    _patternOnce:false,         //用在垂直排列下，部分逻辑只执行一次
    _patternPosYStart:0,        //垂直排列食物生成起点的y坐标

    ctor:function (gameScene) {
        this._container=gameScene.itemBatchLayer;
        this._gameScene=gameScene;
        this._itemToAnimate=new Array();

    },
    init:function () {
        this.removeAll();
        this._pattern=1;
        this._patternPosY=cc.director.getWinSize().height-GameConstant.GAME_AREA_TOP_BOTTOM;
        this._patternStep=15;
        this._patternDirection=1;
        this._patternGap=20;
        this._patternGapCount=0;
        this._patternChangeDistance=100;
        this._patternLength=50;
        this._patternOnce=true;
        Game.user.coffee=Game.user.mushroom=0;
    },
    removeAll:function (){
        if(this._itemToAnimate.length>0){
            for(var i=this._itemToAnimate.length-1;i>=0;i--){
                var item=this._itemToAnimate[i];
                this._itemToAnimate.splice(i,1);
                cc.pool.putInPool(item);
                this._container.removeChild(item);
            }
        }
    },
    update:function (hero,elapsed) {
        this._setFoodPattern(elapsed);
        this._createFoodPattern(elapsed);
        this._animateFoodItem(hero,elapsed);
    },
    _setFoodPattern:function (elapsed) {
        if(this._patternChangeDistance>0){
            this._patternChangeDistance-=Game.user.heroSpeed*elapsed;
        }
        else {
            if (Math.random() < 0.2)
                this._pattern = Math.ceil(Math.random() * 4);
            else
                this._pattern = Math.ceil(Math.random() * 9 + 2);


            if (this._pattern == 1) {
                this._patternStep = 15;
                this._patternChangeDistance = Math.random() * 500 + 500;
            }
            else if (this._pattern == 2) {
                this._patternOnce = true;
                this._patternStep = 40;
                this._patternChangeDistance = this._patternGap * Math.random() * 3 + 5;
            }
            else if (this._pattern == 3) {
                this._patternStep = Math.round(Math.random() * 2 + 2) * 10;
                if (Math.random() > 0.5) {
                    this._patternDirection *= -1;
                }
                this._patternChangeDistance = Math.random() * 800 + 800;
            }
            else if (this._pattern == 4) {
                this._patternStep = Math.round(Math.random() * 3 + 2) * 50;
                this._patternChangeDistance = Math.random() * 400 + 400;
            }
            else {
                this._patternChangeDistance = 0;
            }
        }

    },

    _createFoodPattern:function (elapsed) {
        if(this._patternGapCount<this._patternGap){
            this._patternGapCount+=Game.user.heroSpeed*elapsed;
        }
        else if(this._pattern != 0) {
            this._patternGapCount = 0;
            var winSize = cc.director.getWinSize();
            var item = null;

            switch (this._pattern) {
                case 1:
                    //创建水平的食物
                    if (Math.random() > 0.9) {
                        this._patternPosY = Math.floor(Math.random() * (winSize.height - 2 * GameConstant.GAME_AREA_TOP_BOTTOM)) + GameConstant.GAME_AREA_TOP_BOTTOM;
                    }

                    // 从缓冲池去除并设置item的类别
                    item = Food.create(Math.ceil(Math.random() * 5));

                    // 重新设置 item的位置
                    item.x = winSize.width + item.width;
                    item.y = this._patternPosY;

                    // 加入Animate和container的队列
                    this._itemToAnimate.push(item);
                    this._container.addChild(item,1);
                    break;

                case 2:
                    // 创建垂直方向的食物
                    if (this._patternOnce == true) {
                        this._patternOnce = false;
                        this._patternPosY = Math.floor(Math.random() * (winSize.height - 2 * GameConstant.GAME_AREA_TOP_BOTTOM)) + GameConstant.GAME_AREA_TOP_BOTTOM;
                        this._patternLength = (Math.random() * 0.4 + 0.4) * winSize.height;
                    }

                    // 设置第一个食物开始的纵坐标
                    this._patternPosYstart = this._patternPosY;

                    // 根据样式长度创建但是不可以溢出屏幕
                    while (this._patternPosYstart + this._patternStep < this._patternPosY + this._patternLength
                    && this._patternPosYstart + this._patternStep < winSize.height * 0.8) {
                        item = Food.create(Math.ceil(Math.random() * 5));
                        item.x = winSize.width + item.width;
                        item.y = this._patternPosYstart;
                        this._itemToAnimate.push(item);
                        this._container.addChild(item, 1);

                        // 每个食物的垂直距离依次增加
                        this._patternPosYstart += this._patternStep;
                    }
                    break;
                case 3:
                    //创建之字形，——patterDirection是-1或1决定往下创建还是往上创建
                    if (this._patternDirection == 1 && this._patternPosY < GameConstant.GAME_AREA_TOP_BOTTOM) {
                        this._patternDirection = -1;
                    }
                    else if (this._patternDirection == -1 && this._patternPosY > winSize.height - GameConstant.GAME_AREA_TOP_BOTTOM) {
                        this._patternDirection = 1;
                    }

                    if (this._patternPosY <= winSize.height - GameConstant.GAME_AREA_TOP_BOTTOM && this._patternPosY >= GameConstant.GAME_AREA_TOP_BOTTOM) {
                        item = Food.create(Math.ceil(Math.random() * 5));
                        item.x = winSize.width + item.width;
                        item.y = this._patternPosY;
                        this._itemToAnimate.push(item);
                        this._container.addChild(item, 1);
                        this._patternPosY += this._patternStep * this._patternDirection;
                    }
                    else {
                        this._patternPosY = winSize.height - GameConstant.GAME_AREA_TOP_BOTTOM;
                    }

                    break;
                case 4:
                    // 创建随机的食物形状
                    if (Math.random() > 0.5) {
                        this._patternPosY = Math.floor(Math.random() * (winSize.height - 2 * GameConstant.GAME_AREA_TOP_BOTTOM)) + GameConstant.GAME_AREA_TOP_BOTTOM;
                        item = Food.create(Math.ceil(Math.random() * 5));
                        item.x = winSize.width + item.width;
                        item.y = this._patternPosY;
                        this._itemToAnimate.push(item);
                        this._container.addChild(item, 1);
                    }
                    break;
                case 10:
                    // 创建coffee出现的情况
                    this._patternPosY = Math.floor(Math.random() * (winSize.height - 2 * GameConstant.GAME_AREA_TOP_BOTTOM)) + GameConstant.GAME_AREA_TOP_BOTTOM;
                    item = Food.create(GameConstant.ITEM_TYPE_COFFEE);
                    item.x = winSize.width + item.width;
                    item.y = this._patternPosY;
                    this._itemToAnimate.push(item);
                    this._container.addChild(item, 2);
                    break;

                case 11:
                    //mushroom出现的情况
                    this._patternPosY = Math.floor(Math.random() * (winSize.height - 2 * GameConstant.GAME_AREA_TOP_BOTTOM)) + GameConstant.GAME_AREA_TOP_BOTTOM;
                    item = Food.create(GameConstant.ITEM_TYPE_MUSHROOM);
                    item.x = winSize.width + item.width;
                    item.y = this._patternPosY;
                    this._itemToAnimate.push(item);
                    this._container.addChild(item, 2);
                    break;

            }
        }
    },

    _animateFoodItem:function (hero,elapsed) {
        var item;
        for(var i=this._itemToAnimate.length-1;i>=0;i--){
            item=this._itemToAnimate[i];
            if(item) {
                //吃了蘑菇吸收食物,不吸收mushroom and coffee
                if (Game.user.mushroom > 0 && item.type <= GameConstant.ITEM_TYPE_5) {
                    item.x -= (item.x - hero.x) * 0.2;
                    item.y -= (item.y - hero.y) * 0.2;
                }
                else {
                    item.x -= Game.user.heroSpeed * elapsed;
                }

                // 如果食物没有吃到或者die,记得将食物收进缓冲池，从食物放映组
                // （itemToAnimate）删除,从内容组（itemBatchLayer剔除）
                if (item.x < -80 || Game.user.state == GameConstant.GAME_STATE_OVER) {
                    this._itemToAnimate.splice(i, 1);
                    cc.pool.putInPool(item);
                    this._container.removeChild(item);
                    continue;
                }
                else {
                    var heroItem_xDist = item.x - hero.x;
                    var heroItem_yDist = item.y - hero.y;
                    var heroItem_sqDist = heroItem_xDist * heroItem_xDist + heroItem_yDist * heroItem_yDist;
                    if (heroItem_sqDist < 5000) {
                        if (item.type <= GameConstant.ITEM_TYPE_5) {
                            Game.user.score += item.type;
                            Sound.playEat();
                        }
                        else if (item.type == GameConstant.ITEM_TYPE_COFFEE) {
                            Game.user.score += 1;
                            Game.user.coffee = 5;
                            this._gameScene.showCoffeeEffect();
                            Sound.playCoffee();
                        }
                        else if (item.type == GameConstant.ITEM_TYPE_MUSHROOM) {
                            Game.user.score += 1;
                            Game.user.mushroom = 4;
                            this._gameScene.showMushroomEffect();

                            Sound.playMushroom();
                        }

                        this._gameScene.showEatEffect(item.x, item.y);

                        this._itemToAnimate.splice(i, 1);
                        cc.pool.putInPool(item);
                        this._container.removeChild(item);
                    }
                }
            }
        }

    }
});