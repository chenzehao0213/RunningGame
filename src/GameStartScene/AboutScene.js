


var AboutScene=cc.Scene.extend({
   ctor:function () {
       this._super();
       var layer=new cc.Layer();
       this.addChild(layer);
       var size=cc.director.getWinSize();

       var bg=new cc.Sprite("res/graphics/bgWelcome.jpg");
       bg.x=size.width/2;
       bg.y=size.height/2;
       layer.addChild(bg);

       var aboutText = "Hungry Hero is a free and open source game built on Adobe Flash using Starling Framework.\n\nhttp://www.hungryherogame.com\n\n" +
           " And this is a cocos2d-js version modified by Kenko.\n\n" +
           " The concept is very simple. The hero is pretty much always hungry and you need to feed him with food.\n\n" +
           " You score when your Hero eats food.There are different obstacles that fly in with a \"Look out!\"\n\n" +
           " caution before they appear. Avoid them at all costs. You only have 5 lives. Try to score as much as possible and also\n\n" +
           " try to travel the longest distance.";
       var text=new cc.LabelTTF(aboutText,"Arial", 18);
       text.x = size.width/2;
       text.y = size.height/2 + 80;
       layer.addChild(text);

       var backBtn=new cc.MenuItemImage("#about_backButton.png","#about_backButton.png",this._back,this);

       //默认情况下，sprite添加后是在scene的正中央（0，0）
       backBtn.x=150;
       backBtn.y=-70;
       var menu=new cc.Menu(backBtn);
       layer.addChild(menu);
       return true;
   },
    _back:function () {
        Sound.playCoffee();
        cc.director.runScene(new MenuScene());
    }
});
