const dropZoneSize = 10;
var blips = [];
var markers = [];
API.onServerEventTrigger.connect((eventName, arguments) => {
    if (eventName == "SetDestination")
    {
        clearMarkers();
        var destination = arguments[0];
        if (destination == null)
        {
            return;
        }
        
        var blip = API.createBlip(destination);
        API.SetBlipSprite(blip, 162);
        API.callNative("SET_BLIP_ROUTE", blip, true);
        API.callNative("SET_BLIP_ROUTE_COLOUR", blip, 6);
        blips.push(blip);
        markers.push(API.createMarker(1, destination, new Vector3(), new Vector3(), new Vector3(dropZoneSize, dropZoneSize, 1), 255, 255, 0, 255));
    }
});

function clearMarkers() {
    for (var marker of markers) {
        API.deleteEntity(marker);
    }
    for (var blip of blips) {
        API.callNative("SET_BLIP_ROUTE", blip, false);
        API.deleteEntity(blip);
    }
}

