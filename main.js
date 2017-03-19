
cc.game.onStart = function(){
    cc.view.adjustViewPort(false);
    cc.view.setDesignResolutionSize(1024, 768, cc.ResolutionPolicy.SHOW_ALL);
    cc.view.resizeWithBrowserSize(true);
    //load resources
    cc.LoaderScene.preload(g_resources, function () {
        cc.spriteFrameCache.addSpriteFrames("res/graphics/texture.plist");
        cc.director.runScene(new MenuScene());
    }, this);
};
cc.game.run();

var trace = function() {
    cc.log(Array.prototype.join.call(arguments, ", "));
};