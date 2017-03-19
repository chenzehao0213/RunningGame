


var Food=cc.Sprite.extend({
   type:0,
    ctor:function (type) {
        this._super("#item"+type+".png");
        this.type=type;
        return true;
    },
    reuse:function (type) {
        this.setSpriteFrame("item" + type + ".png");
        this.type=type;
    },
    unuse:function () {

    }
});
Food.create=function (type) {
    if(cc.pool.hasObject(Food))
        return cc.pool.getFromPool(Food,type);
    else {
        return new Food(type);
    }

};