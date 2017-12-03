"use strict";
var MaskPuttingOn = false;
var safety = true;
var Menu = true;

API.OnKeyDown.Connect(function (player, args) {
    if (API.GetCanOpenChat() === false) { return; }
    if (API.isChatOpen()) return;

    switch (args.KeyCode) {
        case Keys.X: //Vehicle Engine Toogle
            if (API.GetPlayerVehicleSeat(API.GetLocalPlayer()) === -1) {
                API.TriggerServerEvent("onHotkeyPress", 0);
            }
            break;
        case Keys.U: // Unlock / Lock Vehicle
            API.TriggerServerEvent("onHotkeyPress", 1);
            break;
		case Keys.E: //Interaction
			API.TriggerServerEvent("onHotkeyPress", 2);
			break;
		case Keys.O: //Maske auf - absetzen	
			if (MaskPuttingOn === true) {
				API.TriggerServerEvent("SetPlayerMask", false);
				MaskPuttingOn = false;
			}
			else {
				API.TriggerServerEvent("SetPlayerMask", true);
				MaskPuttingOn = true;
			}
			break;
		case Keys.F4:			
				API.TriggerServerEvent("ClothingMenu", true);	
			break;
    }
});
