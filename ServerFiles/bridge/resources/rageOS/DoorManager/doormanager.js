var selectingDoor = false;
var lastDoor = null;
var lastDoorV = 0;

var currentTransitions = [];

function deleteMarker(marker) {
    API.deleteEntity(marker);
}

API.onServerEventTrigger.connect(function (eventName, args) {
    if (eventName === "doormanager_debug") {
        selectingDoor = true;
        API.ShowCursor(true);
    }
    else if (eventName === "doormanager_finddoor") {
        var doorHash = args[0];
        var pos = args[1];
        var handle = API.returnNative("GET_CLOSEST_OBJECT_OF_TYPE", 9, pos.X, pos.Y, pos.Z, 10.5, doorHash, false, true, true);

        if (!handle.IsNull) {
            API.SendChatMessage("Found model at " + API.GetEntityPosition(handle).ToString() + " Model: " + API.GetEntityModel(handle));
            var mark = API.createMarker(28, API.GetEntityPosition(handle),
                new Vector3(), new Vector3(), new Vector3(0.1, 0.1, 0.1),
                255, 255, 255, 100);
            API.after(3000, "deleteMarker", mark);  
        }
    }
    else if (eventName === "doormanager_finddoor_ex") {
        doorHash = args[0];
        pos = API.GetEntityPosition(API.GetLocalPlayer());

        handle = API.returnNative("GET_CLOSEST_OBJECT_OF_TYPE", 9, pos.X, pos.Y, pos.Z, 10.5, doorHash, false, true, true);
        if (!handle.IsNull) {
            API.SendChatMessage("Model: " + API.GetEntityModel(handle) + " -> Save? [/y]");

            API.TriggerServerEvent("SaveModel", API.GetEntityModel(handle), API.GetEntityPosition(handle));

            mark = API.createMarker(28, API.GetEntityPosition(handle),
                new Vector3(), new Vector3(), new Vector3(0.1, 0.1, 0.1),
                255, 255, 255, 100);
            API.after(3000, "deleteMarker", mark);       
        }
    }
    else if (eventName === "doormanager_finddoor_return") {
        doorHash = args[0];
        pos = args[1];
        handle = API.returnNative("GET_CLOSEST_OBJECT_OF_TYPE", 9, pos.X, pos.Y, pos.Z, 10.5, doorHash, false, true, true);

        if (!handle.IsNull) {
            API.TriggerServerEvent("doormanager_debug_createdoor",
                API.GetEntityModel(handle), API.GetEntityPosition(handle));
        }
    }
    else if (eventName === "doormanager_transitiondoor") {
        var model = args[0];
        pos = args[1];
        var start = args[2];
        var end = args[3];
        var duration = args[4];

        currentTransitions.push({
            'model': model,
            'pos': pos,
            'start': start,
            'end': end,
            'duration': duration,
            'startTime': API.GetGlobalTime()
        });
    }
});

API.onUpdate.connect(function () {
    if (currentTransitions.length > 0) {
        for (var i = currentTransitions.length - 1; i >= 0; i--) {
            var delta = API.GetGlobalTime() - currentTransitions[i].StartTime;
            if (delta > currentTransitions[i].duration) {
                API.callNative("SET_STATE_OF_CLOSEST_DOOR_OF_TYPE",
                    currentTransitions[i].model,
                    currentTransitions[i].pos.X,
                    currentTransitions[i].pos.Y,
                    currentTransitions[i].pos.Z,
                    true,
                    API.f(currentTransitions[i].end),
                    false);

                currentTransitions.Splice(i, 1);
            } else {
                API.callNative("SET_STATE_OF_CLOSEST_DOOR_OF_TYPE",
                    currentTransitions[i].model,
                    currentTransitions[i].pos.X,
                    currentTransitions[i].pos.Y,
                    currentTransitions[i].pos.Z,
                    true,
                    API.f(API.lerpFloat(currentTransitions[i].Start,
                        currentTransitions[i].end, delta,
                        currentTransitions[i].duration)),
                    false);
            }
        }
    }


    if (selectingDoor) {
        var cursOp = API.GetCursorPositionMaintainRatio();
        var s2w = API.ScreenToWorldMaintainRatio(cursOp);
        var rayCast = API.createRaycast(API.GetGameplayCamPos(), s2w, -1, null);
        var localH = null;
        var localV = 0;
        if (rayCast.didHitEntity) {
            localH = rayCast.hitEntity;
            localV = localH.Value;
        }

        API.displaySubtitle("Object Handle: " + localV);

        if (localV !== lastDoorV) {
            if (localH !== null) API.SetEntityTransparency(localH, 50);
            if (lastDoor !== null) API.SetEntityTransparency(lastDoor, 255);
            lastDoor = localH;
            lastDoorV = localV;
        }

        if (API.isDisabledControlJustPressed(24)) {
            API.ShowCursor(false);
            selectingDoor = false;

            if (localH !== null) {
                API.SendChatMessage("Object model is " + API.GetEntityModel(localH));
                API.TriggerServerEvent("doormanager_debug_createdoor",
                    API.GetEntityModel(localH), API.GetEntityPosition(localH));
            }
        }
    }
    else if (lastDoor !== null) {
        API.SetEntityTransparency(lastDoor, 255);
        lastDoor = null;
    }
});