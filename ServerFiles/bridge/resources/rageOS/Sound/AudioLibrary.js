"use strict";

API.onServerEventTrigger.connect(function (eventName, args) {
    
	if (eventName === 'UnlockVehicle'){
		API.StartMusic("Soundfiles/UnlockVehicle.mp3", false);
	}
	else if (eventName === 'LockVehicle'){
        API.StartMusic("Soundfiles/LockVehicle.mp3", false);
	}
	else if (eventName === 'soundstop'){
		API.StopMusic();
	}
});