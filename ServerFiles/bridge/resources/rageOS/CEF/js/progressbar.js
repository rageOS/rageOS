var mainBrowser = null;
var lastInVehicle = false;
var path = "CEF/";

API.onServerEventTrigger.connect(function (event, args) {
    switch (event) {
        case 'ShowProgressBar':
            showCEF('progressbar.html');
            break;
        case 'killProgressBar':
            API.destroyCefBrowser(mainBrowser);
            mainBrowser = null;
            API.SetCanOpenChat(true);
            break;
    }
});

API.onUpdate.connect(function () {
    if (mainBrowser !== null) {
        var player = API.GetLocalPlayer();
        var progress = parseInt(API.GetEntitySyncedData(player, "ProgressbarProgress"));
        var name = API.GetEntitySyncedData(player, "ProgressbarName");
        mainBrowser.call("updateProgress", progress, name);
    }
});

API.onResourceStop.connect(function () {
    if (mainBrowser !== null) {
        API.destroyCefBrowser(mainBrowser);
    }
});

function showCEF(string) {
    var res = API.GetScreenResolution();
    mainBrowser = API.createCefBrowser(res.Width, res.Height);
    API.waitUntilCefBrowserInit(mainBrowser);
    API.loadPageCefBrowser(mainBrowser, path + string);
}