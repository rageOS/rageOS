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
    var dateTime = API.GetGameTime();
    if (dateTime - oldDateTime >= 3000) {
        if (playerHUD !== null) {
            var player = API.GetLocalPlayer();

            var eat = API.GetEntitySyncedData(player, "FoodLevel");
            var durst = API.GetEntitySyncedData(player, "DrinkLevel");
            var voice = API.GetEntitySyncedData(player, "VoiceLevel");

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
    var res = API.GetScreenResolution();
    playerHUD = API.createCefBrowser(res.Width, res.Height);
    API.waitUntilCefBrowserInit(playerHUD);
    API.loadPageCefBrowser(playerHUD, path + string);
}

function updateEat(value) {
    document.GetElementById("food_fill").Style.height = value + "%";

    var heightStyleValue = document.GetElementById("food_fill").Style.height;
    if (parseInt(heightStyleValue) < parseInt("10%")) {
        document.GetElementById("food_fill").Style.background = "#ec3434";
    }
    else if (parseInt(heightStyleValue) < parseInt("50%")) {
        document.GetElementById("food_fill").Style.background = "#ae8d2e";
    } else if (parseInt(heightStyleValue) <= parseInt("100%")) {
        document.GetElementById("food_fill").Style.background = "#4a9248";
    }
}

function updateDurst(value) {
    document.GetElementById("drink_fill").Style.height = value + "%";

    var heightStyleValuedrink = document.GetElementById("drink_fill").Style.height;
    if (parseInt(heightStyleValuedrink) < parseInt("10%")) {
        document.GetElementById("drink_fill").Style.background = "#ec3434";
    }
    else if (parseInt(heightStyleValuedrink) < parseInt("50%")) {
        document.GetElementById("drink_fill").Style.background = "#ae8d2e";
    } else if (parseInt(heightStyleValuedrink) <= parseInt("100%")) {
        document.GetElementById("drink_fill").Style.background = "#4a9248";
    }
}

function updateMoney(value) {
    document.GetElementById("moneyContainer").innerHTML = value + "$";
}

function updateVoice(value) {
    if (value === 1) {
        document.GetElementById("voice_img").Style.backgroundImage = "url('./img/voice1.png')";
    }
    else if (value === 2) {
        document.GetElementById("voice_img").Style.backgroundImage = "url('./img/voice2.png')";
    }
    else if (value === 3) {
        document.GetElementById("voice_img").Style.backgroundImage = "url('./img/voice3.png')";
    }
}