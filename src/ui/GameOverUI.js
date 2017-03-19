


var GameOverUI=cc.Layer.extend({
    _gameScene:null,
    _scoreText:null,
    _distanceText:null,
    ctor:function (gameScene) {
        this._super();
        this._gameScene=gameScene;

        var size=cc.director.getWinSize();
        var bg=new cc.LayerColor(cc.color(0,0,0,200),size.width,size.height);
        this.addChild(bg);

        //die--提示标语
        var fnt="res/fonts/font.fnt";
        var heroDie=new cc.LabelBMFont("HERO WAS KILLED!",fnt);
        heroDie.x=size.width/2;
        heroDie.y=size.height-120;
        heroDie.setColor(cc.color(243,231,95));
        this.addChild(heroDie);

        //die--游戏成绩
        this._distanceText=new cc.LabelBMFont("DISTANCE TRAVELLED:00000",fnt);
        this._distanceText.x=size.width/2;
        this._distanceText.y=size.height-220;
        this.addChild(this._distanceText);
        this._scoreText=new cc.LabelBMFont("SCORE:00000",fnt);
        this._scoreText.x=size.width/2;
        this._scoreText.y=size.height-270;
        this.addChild(this._scoreText);

        //die--游戏操作按钮
        var PlayAgain_Button=new cc.MenuItemImage("#gameOver_playAgainButton.png","#gameOver_playAgainButton.png",this._playAgain.bind(this));
        var Main_Button=new cc.MenuItemImage("#gameOver_mainButton.png","#gameOver_mainButton.png",this._main);
        var About_Button=new cc.MenuItemImage("#gameOver_aboutButton.png","#gameOver_aboutButton.png",this._about);
        var menu=new cc.Menu(PlayAgain_Button,Main_Button,About_Button);
        menu.alignItemsVertically();
        this.addChild(menu);

        menu.y=size.height/2-100;
        return true;
    },
    init:function () {
        this._distanceText.setString("DISTANCE TRAVELLED:"+parseInt(Game.user.distance));
        this._scoreText.setString("SCORE:"+Game.user.score);
    },
    _playAgain:function () {
        this._gameScene.init();
    },
    _main:function () {
        cc.director.runScene(new MenuScene());
    },
    _about:function () {
        cc.director.runScene(new AboutScene())
    }

});