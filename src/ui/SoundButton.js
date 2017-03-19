


var SoundButton=cc.MenuItemToggle.extend({
    ctor:function () {
        var sprite=new cc.Sprite("#soundOn0000.png");
        var animation=new cc.Animation();
        //动画里面addSpriteFrameWithFile后面跟的是文件而非碎图
        animation.addSpriteFrame(cc.spriteFrameCache.getSpriteFrame("soundOn0000.png"));
        animation.addSpriteFrame(cc.spriteFrameCache.getSpriteFrame("soundOn0001.png"));
        animation.addSpriteFrame(cc.spriteFrameCache.getSpriteFrame("soundOn0002.png"));
        animation.setDelayPerUnit(1/3);
        var action=cc.animate(animation);
        action.repeatForever();
        sprite.runAction(action);
        //没有规定this.super 一定要放在最前面
        this._super(new cc.MenuItemSprite(sprite, null, null), new cc.MenuItemImage("#soundOff.png"));
        this.setCallback(this._soundOnOff, this);

    },
    _soundOnOff:function () {
        Sound.toggleOnOff();
    },

});