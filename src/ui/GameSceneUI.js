



var GameSceneUI=cc.Layer.extend({    //生命值，距离，分数
    _lifeText:null,
    _distanceText:null,
    _scoreText:null,

    ctor:function () {
        this._super();
        var size=cc.director.getWinSize();

        //生命值
        var lifeLabel=new cc.LabelBMFont("L I V E S","res/fonts/font.fnt");
        lifeLabel.x=360;
        lifeLabel.y=size.height-25;
        this.addChild(lifeLabel);
        this._lifeText=new cc.LabelBMFont("0","res/fonts/font.fnt");
        this._lifeText.x=360;
        this._lifeText.y=size.height-60;
        this.addChild(this._lifeText);

        //距离
        var distanceLabel=new cc.LabelBMFont("D I S T A N C E","res/fonts/font.fnt");
        distanceLabel.x=680;
        distanceLabel.y=size.height-25;
        this.addChild(distanceLabel);
        this._distanceText=new cc.LabelBMFont("30","res/fonts/font.fnt");
        this._distanceText.x=680;
        this._distanceText.y=size.height-60;
        this.addChild(this._distanceText);

        //分数
        var scoreLabel=new cc.LabelBMFont("S C O R E","res/fonts/font.fnt");
        scoreLabel.x=915;
        scoreLabel.y=size.height-25;
        this.addChild(scoreLabel);
        this._scoreText=new cc.LabelBMFont("100","res/fonts/font.fnt");
        this._scoreText.x=915;
        this._scoreText.y=size.height-60;
        this.addChild(this._scoreText);

        //暂停按钮
        var pauseButton=new cc.MenuItemImage("#pauseButton.png","#pauseButton.png",this._pauseResume,this);

        //声音按钮
        var soundButton=new SoundButton();
        var menu =new cc.Menu(soundButton,pauseButton);
        menu.alignItemsHorizontallyWithPadding(30);
        menu.x=80;
        menu.y=size.height-45;
        this.addChild(menu);
        return true;
    },
    _pauseResume:function () {
        if(cc.director.isPaused())
            cc.director.resume();
        else
            cc.director.pause();
    },


    //生命，距离和分数需要实时更新，调用更新接口
    update:function() {
        this._lifeText.setString(Game.user.lives.toString());
        this._distanceText.setString(parseInt(Game.user.distance).toString());
        this._scoreText.setString(parseInt(Game.user.score).toString());
    }


});