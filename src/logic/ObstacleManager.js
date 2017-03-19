


var ObstacleManager = cc.Class.extend({

    _container: null,
    _gameScene: null,
    _obstaclesToAnimate: null,

    _obstacleGapCount: 0,

    ctor: function (gameScene) {
        this._container = gameScene.itemBatchLayer;
        this._gameScene = gameScene;
        this._obstaclesToAnimate = new Array();
    },

    init: function () {
        this.removeAll();
        Game.user.hitObstacle = 0;
    },

    removeAll: function () {
        if (this._obstaclesToAnimate.length > 0) {
            for (var i = this._obstaclesToAnimate.length - 1; i >= 0; i--) {
                var item = this._obstaclesToAnimate[i];
                this._obstaclesToAnimate.splice(i, 1);
                cc.pool.putInPool(item);
                this._container.removeChild(item);
            }
        }
    },

    update: function (hero, elapsed) {
        // 飞行一定距离后再加载障碍物
        if (this._obstacleGapCount < GameConstant.OBSTACLE_GAP) {
            this._obstacleGapCount += Game.user.heroSpeed * elapsed;
        }
        else if (this._obstacleGapCount != 0) {
            this._obstacleGapCount = 0;

            // 任意创建一种障碍物
            this._createObstacle(Math.ceil(Math.random() * 4), Math.random() * 1000 + 1000);
        }
        this._animateObstacles(hero, elapsed);
    },


    _createObstacle: function (type, distance) {
        var winSize = cc.director.getWinSize();
        var x = winSize.width;
        var y = 0;
        var position = null;
        if (type <= GameConstant.OBSTACLE_TYPE_3) {
            if (Math.random() > 0.5) {
                y = winSize.height - GameConstant.GAME_AREA_TOP_BOTTOM;
                position = "top";
            }
            else {
                y = GameConstant.GAME_AREA_TOP_BOTTOM;
                position = "bottom";
            }
        }
        else {
            y = Math.floor(Math.random() * (winSize.height - 2 * GameConstant.GAME_AREA_TOP_BOTTOM)) + GameConstant.GAME_AREA_TOP_BOTTOM;
            position = "middle";
        }

        var obstacle = Obstacle.create(type, true, position, GameConstant.OBSTACLE_SPEED, distance);
        obstacle.x = x + obstacle.width/2;
        obstacle.y = y;
        this._obstaclesToAnimate.push(obstacle);
        this._container.addChild(obstacle);
    },

    _animateObstacles: function (hero, elapsed) {
        var obstacle;
        for (var i = this._obstaclesToAnimate.length - 1; i >= 0; i--) {
            obstacle = this._obstaclesToAnimate[i];

            if (obstacle.distance > 0) {
                obstacle.distance -= Game.user.heroSpeed * elapsed;
            }
            else {
                obstacle.hideLookout();

                obstacle.x -= (Game.user.heroSpeed + obstacle.speed) * elapsed;
            }

            if (obstacle.x < -obstacle.width || Game.gameState == GameConstant.GAME_STATE_OVER) {
                this._obstaclesToAnimate.splice(i, 1);
                cc.pool.putInPool(obstacle);
                this._container.removeChild(obstacle);
                continue;
            }

            var heroObstacle_xDist = obstacle.x - hero.x;
            var heroObstacle_yDist = obstacle.y - hero.y;
            var heroObstacle_sqDist = heroObstacle_xDist * heroObstacle_xDist + heroObstacle_yDist * heroObstacle_yDist;

            if (heroObstacle_sqDist < 5000 && !obstacle.alreadyHit) {
                obstacle.alreadyHit = true;
                obstacle.crash();
                Sound.playHit();

                if (Game.user.coffee > 0) {
                    if (obstacle.position == "bottom") obstacle.setRotation(100);
                    else obstacle.setRotation(-100);

                    Game.user.heroSpeed *= 0.8;
                }
                else {
                    if (obstacle.position == "bottom") obstacle.setRotation(70);
                    else obstacle.setRotation(-70);

                    Game.user.hitObstacle = 30;

                    Game.user.heroSpeed *= 0.5;

                    Sound.playHurt();

                    Game.user.lives--;

                    if (Game.user.lives <= 0) {
                        Game.user.lives = 0;
                        this._gameScene.endGame();
                    }
                }
            }
        }
    }

});