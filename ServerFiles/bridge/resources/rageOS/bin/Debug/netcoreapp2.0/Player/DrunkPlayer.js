"use strict";

API.OnServerEventTrigger.Connect(function (eventName, args) {
    if (eventName == 'Drunk') {
        API.SetPlayerMovementClipset(args[0], args[1], 0.1);
    }
    else if (eventName == 'ResetDrunk') {
        API.resetPlayerMovementClipset(args[0]);
    }
});