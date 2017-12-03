"use strict";

var playerHUD = null;
var oldDateTime = 0;
var path = "CEF/";
var currentMoney = 0;

API.onServerEventTrigger.connect(function (event, args) {
    switch (event) {
        case 'update_money_display':
            currentMoney = args[0];
            break;

        case 'createPlayerHUD':
            showCEF('playerHUD.html');
            break;
    }
});


API.onUpdate.connect(function () {
    var dateTime = API.getGameTime();
    if (dateTime - oldDateTime >= 3000) {
        if (playerHUD !== null) {
            var player = API.getLocalPlayer();

            var eat = API.getEntitySyncedData(player, "FoodLevel");
            var durst = API.getEntitySyncedData(player, "DrinkLevel");
            var voice = API.getEntitySyncedData(player, "VoiceLevel");

            playerHUD.call("updateEat", eat);
            playerHUD.call("updateDurst", durst);
            playerHUD.call("updateMoney", currentMoney);
            playerHUD.call("updateVoice", voice);
        }
    }
});

API.onResourceStop.connect(function () {
    if (playerHUD !== null) {
        API.destroyCefBrowser(playerHUD);
    }
});

function showCEF(string) {
    var res = API.getScreenResolution();
    playerHUD = API.createCefBrowser(res.Width, res.Height);
    API.waitUntilCefBrowserInit(playerHUD);
    API.loadPageCefBrowser(playerHUD, path + string);
}

function updateEat(value) {
    document.getElementById("food_fill").style.height = value + "%";

    var heightStyleValue = document.getElementById("food_fill").style.height;
    if (parseInt(heightStyleValue) < parseInt("10%")) {
        document.getElementById("food_fill").style.background = "#ec3434";
    }
    else if (parseInt(heightStyleValue) < parseInt("50%")) {
        document.getElementById("food_fill").style.background = "#ae8d2e";
    } else if (parseInt(heightStyleValue) <= parseInt("100%")) {
        document.getElementById("food_fill").style.background = "#4a9248";
    }
}

function updateDurst(value) {
    document.getElementById("drink_fill").style.height = value + "%";

    var heightStyleValuedrink = document.getElementById("drink_fill").style.height;
    if (parseInt(heightStyleValuedrink) < parseInt("10%")) {
        document.getElementById("drink_fill").style.background = "#ec3434";
    }
    else if (parseInt(heightStyleValuedrink) < parseInt("50%")) {
        document.getElementById("drink_fill").style.background = "#ae8d2e";
    } else if (parseInt(heightStyleValuedrink) <= parseInt("100%")) {
        document.getElementById("drink_fill").style.background = "#4a9248";
    }
}

function updateMoney(value) {
    document.getElementById("moneyContainer").innerHTML = value + "$";
}

function updateVoice(value) {
    if (value === 1) {
        document.getElementById("voice_img").style.backgroundImage = "url('./img/voice1.png')";
    }
    else if (value === 2) {
        document.getElementById("voice_img").style.backgroundImage = "url('./img/voice2.png')";
    }
    else if (value === 3) {
        document.getElementById("voice_img").style.backgroundImage = "url('./img/voice3.png')";
    }
}