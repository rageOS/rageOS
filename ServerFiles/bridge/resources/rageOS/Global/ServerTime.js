var oldDateTime = 0;
var player = null;
var PLAYER_NAME = "";
var DAYNIGHT_DAY_STRING = "";
var DAYNIGHT_TEXT = "";

API.onResourceStart.connect(function() {
	player = API.GetLocalPlayer();
});

API.onUpdate.connect(function () {
    if (!API.GetHudVisible()) return;
	var dateTime = API.GetGameTime();
	if (dateTime - oldDateTime >= 2000)
	{
		player = API.GetLocalPlayer();
		oldDateTime = dateTime;
		PLAYER_NAME = API.GetEntitySyncedData(player, "PLAYER_NAME");
		DAYNIGHT_DAY_STRING = API.GetWorldSyncedData("DAYNIGHT_DAY_STRING");
		DAYNIGHT_TEXT = API.GetWorldSyncedData("DAYNIGHT_TEXT");
	}

    var res = API.GetScreenResolutionMaintainRatio();

	if(API.hasEntitySyncedData(player, "HUD") && API.GetEntitySyncedData(player, "HUD") == true)
	{
		API.drawText(PLAYER_NAME, (res.Width / 2) - 600, res.Height - 145, 0.65, 255, 255, 255, 255, 4, 0, true, true, 0);
		API.drawText(DAYNIGHT_DAY_STRING, (res.Width / 2) - 600, res.Height - 105, 0.40, 241, 196, 15, 255, 4, 0, true, true, 0);
		API.drawText(DAYNIGHT_TEXT, (res.Width / 2) - 600, res.Height - 80, 0.65, 255, 255, 255, 255, 4, 0, true, true, 0);
	}
});
