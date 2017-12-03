"use strict";

API.onServerEventTrigger.connect(function (eventName, args) {
    
	if (eventName === 'UnlockVehicle'){
		API.startMusic("Soundfiles/UnlockVehicle.mp3", false);
	}
	else if (eventName === 'LockVehicle'){
        API.startMusic("Soundfiles/LockVehicle.mp3", false);
	}
	else if (eventName === 'soundstop'){
		API.stopMusic();
	}
});