

// var Game={
//     user:{
//         lives:GameConstant.HERO_LIVES,
//         score:0,
//         distance:0,
//         heroSpeed:0,
//         coffee:0,
//         mushroom:0,
//         hitObstacle:0
//     },
//     gameState:null
// };


// 游戏过程场景中有几个层：视差滚动背景层，超人层，食物和障碍物，UI层和最后的游戏结束对话
var GameScene=cc.Scene.extend({
    _hero:null,
    itemBatchLayer:null,
    _background:null,
    _GameSceneUI:null,
    _GameOverUI:null,
    _MouseY:null,
    _MouseX:null,
    _TouchX:null,
    _touchY:null,
    _foodManager:null,
    _obstacleManager:null,
    _coffeeEffect:null,
    _mushroomEffect:null,
    _windEffect:null,


   ctor:function () {
       this._super();
       var layer=new cc.Layer();
       this.addChild(layer);

       //background
       this._background=new GameBackground();
       layer.addChild(this._background);

       //hero
       this._hero=new Hero();
       this.addChild(this._hero);

       //itemBatchLayer
       this.itemBatchLayer = new cc.SpriteBatchNode("res/graphics/texture.png");
       this.addChild(this.itemBatchLayer);

       //GameSceneUI
       this._GameSceneUI=new GameSceneUI();
       this.addChild(this._GameSceneUI);
       this._GameSceneUI.update();

       // touches事件,兼容移动端
       if("touches" in cc.sys.capabilities)
           cc.eventManager.addListener({event: cc.EventListener.TOUCH_ALL_AT_ONCE, onTouchesMoved: this._onTouchMoved.bind(this)}, this);
       else
           cc.eventManager.addListener({event: cc.EventListener.MOUSE, onMouseMove: this._onMouseMove.bind(this)}, this);
             cc.eventManager.addListener({event: cc.EventListener.KEYBOARD, onKeyReleased: this._back}, this);

       //food
       this._foodManager=new FoodManage(this);
       this._obstacleManager=new ObstacleManager(this);

       this.init();

       return true;
   },

    //初始化操作
    init:function () {
        Sound.stop();
        Sound.playGameBgMusic();

        if(this._gameOverUI)
            this._gameOverUI.setVisible(false);

        var size=cc.director.getWinSize();
        this._GameSceneUI.setVisible(true);

        Game.user.lives=GameConstant.HERO_LIVES;
        Game.user.score=Game.user.distance=0;
        Game.gameState=GameConstant.GAME_STATE_IDLE;
        Game.heroSpeed=this._background.speed=0;

        this._touchY = size.height/2;

        this._hero.x=-size.width/2;
        this._hero.y=size.height/2;

        this._foodManager.init();
        this._obstacleManager.init();
        this.scheduleUpdate();

        this.stopCoffeeEffect();
        this.stopWindEffect();
        this.stopMushroomEffect();

    },



    _onTouchMoved:function(touches,event) {
        if(Game.gameState!=GameConstant.GAME_STATE_OVER){
            this._touchY=touches[0].getLocation().y;
        }
    },
    _onMouseMove:function (event) {
        if(Game.gameState!=GameConstant.GAME_STATE_OVER){
            this._MouseX=event.getLocationX();
            this._MouseY=event.getLocationY();
        }
    },
    _back:function(keyCode, event) {
        if (keyCode == cc.KEY.back)
            cc.director.runScene(new MenuScene());
    },
    _handleHeroPose:function() {
        var winSize = cc.director.getWinSize();
        if (Math.abs(this._hero.y - this._MouseY) * 0.2 < 30)
            this._hero.setRotation((this._hero.y - this._MouseY) * 0.2);

        //在上边界和下边界设置player不旋转
        if (this._hero.y < this._hero.height * 0.5)
        {
            this._hero.y = this._hero.height * 0.5;
            this._hero.setRotation(0);
        }
        if (this._hero.y > winSize.height - this._hero.height * 0.5)
        {
            this._hero.y = winSize.height - this._hero.height * 0.5;
            this._hero.setRotation(0);
        }
    },
    _shakeAnimation:function() {
        if (Game.user.hitObstacle > 0){
            this.x = parseInt(Math.random() * Game.user.hitObstacle - Game.user.hitObstacle * 0.5);
            this.y = parseInt(Math.random() * Game.user.hitObstacle - Game.user.hitObstacle * 0.5);
        } else if (this.x != 0) {
            this.x = 0;
            this.y = 0;
        }
    },
    showWindEffect:function() {
        if(this._windEffect)
            return;
        this._windEffect = new cc.ParticleSystem("res/particles/wind.plist");
        this._windEffect.x = cc.director.getWinSize().width;
        this._windEffect.y = cc.director.getWinSize().height/2;
        this._windEffect.setScaleX(100);
        this.addChild(this._windEffect);
    },
    stopWindEffect:function() {
        if(this._windEffect){
            this._windEffect.stopSystem();
            this.removeChild(this._windEffect);
            this._windEffect = null;
        }
    },
    showCoffeeEffect:function(){
        if(this._coffeeEffect)
            return;
        this._coffeeEffect = new cc.ParticleSystem("res/particles/coffee.plist");
        this.addChild(this._coffeeEffect);
        this._coffeeEffect.x = this._hero.x + this._hero.width/4;
        this._coffeeEffect.y = this._hero.y;
    },
    stopCoffeeEffect:function(){
        if(this._coffeeEffect){
            this._coffeeEffect.stopSystem();
            this.removeChild(this._coffeeEffect);
            this._coffeeEffect = null;
        }
    },

    showMushroomEffect:function(){
        if(this._mushroomEffect)
            return;
        this._mushroomEffect = new cc.ParticleSystem("res/particles/mushroom.plist");
        this.addChild(this._mushroomEffect);
        this._mushroomEffect.x = this._hero.x + this._hero.width/4;
        this._mushroomEffect.y = this._hero.y;
    },

    stopMushroomEffect:function(){
        if(this._mushroomEffect){
            this._mushroomEffect.stopSystem();
            this.removeChild(this._mushroomEffect);
            this._mushroomEffect = null;
        }
    },

    showEatEffect:function(itemX, itemY){
        var eat = new cc.ParticleSystem("res/particles/eat.plist");
        eat.setAutoRemoveOnFinish(true);
        eat.x = itemX;
        eat.y = itemY;
        this.addChild(eat);
    },

    endGame:function () {
        Game.gameState=GameConstant.GAME_STATE_OVER;
    },
    _gameOver:function () {
            if(!this._gameOverUI){
                this._gameOverUI = new GameOverUI(this);
                this.addChild(this._gameOverUI);
            }
            this._gameOverUI.setVisible(true);
            this._gameOverUI.init();
            Sound.playLose();
    },

    update:function (elapsed) {
        var winSize = cc.director.getWinSize();

        switch (Game.gameState) {
            //游戏空闲状态
            case GameConstant.GAME_STATE_IDLE:
                if(this._hero.x<winSize.width*0.5*0.5){
                    //缓慢让hero从游戏左屏幕出场到winSize.width*0.5*0.5+10
                    this._hero.x+=((winSize.width*0.5*0.5+10)-this._hero.x)*0.05;
                    //缓慢让hero的速度从0增加到MIN_SPEED
                    Game.user.heroSpeed+=(GameConstant.HERO_MIN_SPEED-Game.user.heroSpeed)*0.05;
                    this._background.speed=Game.user.heroSpeed*elapsed;
                }
                else
                {
                    Game.gameState=GameConstant.GAME_STATE_FLYING;
                    this._hero.state=GameConstant.HERO_STATE_FLYING;
                }
                this._handleHeroPose();
                this._GameSceneUI.update();
                break;

            case GameConstant.GAME_STATE_FLYING:
                //加速状态
                if(Game.user.coffee>0) {
                    Game.user.heroSpeed += (GameConstant.HERO_MAX_SPEED - Game.user.heroSpeed) * 0.2;
                }
                else
                    this.stopCoffeeEffect();
                //正常飞行，没有障碍物
                if(Game.user.hitObstacle <= 0) {
                    this._hero.state=GameConstant.HERO_STATE_FLYING;
                    // 鼠标跟随，设定了一定的延迟
                    this._hero.y -= (this._hero.y - this._MouseY) * 0.1;
                    this._hero.x -= (this._hero.x -  this._MouseX) * 0.1;
                    //正常飞行速度在缓慢增加，当达到MIN_SPEED + 100出现Wind特效，随后切换速度到MIN_SPEED
                    if (Game.user.heroSpeed > GameConstant.HERO_MIN_SPEED + 100) {
                        this.showWindEffect();
                        this._hero.toggleSpeed(true);
                    }
                    else {
                        this._hero.toggleSpeed(false);
                        this.stopWindEffect();
                    }
                    this._handleHeroPose();
                }
                //有障碍物，但是得判断hero的状态是否是吃了coffee的加速状态，在此状态下是不受撞击影响
                else {
                    if(Game.user.coffee<=0) {                   //正常情况下的撞击
                        if (this._hero.state != GameConstant.HERO_STATE_HIT) {
                            this._hero.state = GameConstant.HERO_STATE_HIT;
                        }
                        this._hero.y -= (this._hero.y - winSize.height / 2) * 0.1;

                        if (this._hero.y > winSize.height / 2) {
                            this._hero.rotation -= Game.user.hitObstacle * 2;
                        }
                        else {
                            this._hero.rotation += Game.user.hitObstacle * 2;
                        }
                    }
                    Game.user.hitObstacle--;
                    this._shakeAnimation();

                }


                if(Game.user.mushroom>0)
                    Game.user.mushroom-=elapsed;
                else
                    this.stopMushroomEffect();

                if(Game.user.coffee>0)
                    Game.user.coffee-=elapsed;
                Game.user.heroSpeed-=(Game.user.heroSpeed-GameConstant.HERO_MIN_SPEED)*0.01;

                this._foodManager.update(this._hero,elapsed);
                this._obstacleManager.update(this._hero, elapsed);
                this._background.speed=Game.user.heroSpeed*elapsed;

                Game.user.distance+=(Game.user.heroSpeed*elapsed)*0.1;
                this._GameSceneUI.update();
                break;


            case GameConstant.GAME_STATE_OVER:
                this._foodManager.removeAll();
                this._obstacleManager.removeAll();

                // 旋转
                this._hero.setRotation(30);

                //如果player在屏幕上方，减速移除屏幕
                if (this._hero.y > -this._hero.height/2)
                {
                    Game.user.heroSpeed -= Game.user.heroSpeed * elapsed;
                    this._hero.y -= winSize.height * elapsed;
                }
                else
                {
                    // 移除屏幕后速度减为0
                    Game.user.heroSpeed = 0;

                    //停止刷新游戏场景
                    this.unscheduleUpdate();

                    // Game over.
                    this._gameOver();
                }

                // 背景速度跟随player的速度
                this._background.speed = Game.user.heroSpeed * elapsed;
                break;
        }
        if(this._mushroomEffect) {
            this._mushroomEffect.x = this._hero.x + this._hero.width/4;
            this._mushroomEffect.y = this._hero.y;
        }
        if(this._coffeeEffect) {
            this._coffeeEffect.x = this._hero.x + this._hero.width/4;
            this._coffeeEffect.y = this._hero.y;
        }

    }
});
