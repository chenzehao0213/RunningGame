


var MenuScene=cc.Scene.extend({

    _hero:null,
    _playBtn:null,
    _aboutBtn:null,

    ctor:function () {
        this._super();
        var layer=new cc.Layer();
        var size=cc.director.getWinSize();
        this.addChild(layer);

         //添加背景
        var bgWelcome=new cc.Sprite("res/graphics/bgWelcome.jpg");
        bgWelcome.x=size.width/2;
        bgWelcome.y=size.height/2;
        layer.addChild(bgWelcome);

        var title=new cc.Sprite("#welcome_title.png");          //logo_"welcome.title
        title.x=800;
        title.y=555;
        layer.addChild(title);

        //添加英雄
        this._hero=new cc.Sprite("#welcome_hero.png");
        this._hero.x=-this._hero.width/2;
        this._hero.y=400;
        layer.addChild(this._hero);
        var heroAction=cc.moveTo(2,cc.p(this._hero.width/2+100,this._hero.y)).easing(cc.easeOut(2));
        this._hero.runAction(heroAction);

        //开始按钮
        this._playBtn=new cc.MenuItemImage("#welcome_playButton.png","#welcome_playButton.png",this._startGame,this);
        this._playBtn.x=700;
        this._playBtn.y=350;

        //游戏按钮
        this._aboutBtn=new cc.MenuItemImage("#welcome_aboutButton.png","#welcome_aboutButton.png",this._aboutGame,this);
        this._aboutBtn.x=500;
        this._aboutBtn.y=250;

        var soundButton=new SoundButton();
        soundButton.x=45;
        soundButton.y=size.height-45;

        var menu=new cc.Menu(this._aboutBtn,this._playBtn,soundButton);
        menu.x=0;
        menu.y=0;
        layer.addChild(menu);

        Sound.playGameBgMusic();
        this.scheduleUpdate();
        return true;
    },

    _startGame:function() {
        Sound.playCoffee();
        cc.director.runScene(new GameScene());
    },

    _aboutGame:function () {
        Sound.playMushroom();
        cc.director.runScene(new AboutScene());
    },
    update:function () {
        var current=new Date();
        this._hero.y=400+Math.sin(current.getTime()*0.002)*25;
        this._playBtn.y=350+Math.sin(current.getTime()*0.002)*10;
        this._aboutBtn.y=250+Math.sin(current.getTime()*0.002)*10;
    }
});