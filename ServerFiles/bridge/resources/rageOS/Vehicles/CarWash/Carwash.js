"use strict";

var safety = true;

API.onKeyDown.Connect(function (Player, args) {
    if (API.GetCanOpenChat() === false) { return; }
    if (API.isChatOpen()) return;

    if (args.KeyCode === Keys.G) {
        if (safety === true) {
            if (API.HasEntitySyncedData(API.GetLocalPlayer(), "IsInCarwash") && API.GetEntitySyncedData(API.GetLocalPlayer(), "IsInCarwash") === true) {
                API.TriggerServerEvent("waschen");
                safety = false;
            }
        }
    }
});

API.onKeyUp.Connect(function (player, args) {
    if (args.KeyCode === Keys.G && !API.isChatOpen()) {
        safety = true;
    }
});
