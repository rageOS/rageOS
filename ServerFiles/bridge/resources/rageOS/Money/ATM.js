"use strict";

var safety = true;
var isInAtmRange = 0;


API.onKeyDown.Connect(function (player, args) {
    if (API.GetCanOpenChat() === false) { return; }
    if (API.isChatOpen()) return;

    if (args.KeyCode === Keys.E && isInAtmRange === true) {
        if (safety === true) {
            API.TriggerServerEvent("OpenATMOverlay");
            safety = false;
        }
    }
});

API.onKeyUp.Connect(function (player, args) {
    if (args.KeyCode === Keys.E && !API.isChatOpen()) {
        safety = true;
    }
});

API.onServerEventTrigger.Connect(function (event, args) {
    switch (event) {
        case 'TriggerATMOverlay':
            isInAtmRange = args[0];
            break;
    }
});

function DisplayBankMoney(money) {
    var geld = money + " $";
    $('#guthaben').text(geld);
}
function depositMoney() {
    var eingabe = $("#einzahlenTextBox").val();
    resourceCall("depositMoney", eingabe);
}
function withdrawMoney(money) {
    var eingabe = $("#auszahlenTextBox").val();
    resourceCall("withdrawMoney", eingabe);

}
function exit() {
    resourceCall('closeCEF');
}
