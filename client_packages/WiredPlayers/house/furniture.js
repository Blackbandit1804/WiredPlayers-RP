let furnitureList = null;
let heldFurniture = null;
let holdingFurniture = false;

NAPI.OnServerEventTrigger.connect(function (name, args) {
    if (name == "moveFurniture") {
        furnitureList = args[0];
        NAPI.SetCanOpenChat(false);
        NAPI.ShowCursor(true);
    }
});
/*
NAPI.OnUpdate.connect(function () {
    if(NAPI.Data.HasEntitySharedData(NAPI.GetLocalPlayer(), "PLAYER_MOVING_FURNITURE") == true) {
        if(NAPI.IsControlJustPressed(24) && !holdingFurniture) {
            heldFurniture = getClickedFurniture();
            holdingFurniture = true;
        } else if(NAPI.IsDisabledControlJustPressed(24) && holdingFurniture) {
            holdingFurniture = false;
        } else if (holdingFurniture) {
            var cursOp = NAPI.GetCursorPositionMaintainRatio();
            var s2w = NAPI.ScreenToWorldMantainRatio(cursOp);
            //NAPI.SetCameraPosition(NAPI.GetActiveCamera(), s2w);
            NAPI.TriggerServerEvent("moveFurniture", heldFurniture, s2w.X, s2w.Y, s2w.Z);
        }
    }
});*/

function getClickedFurniture() {
    var furnitureArray = JSON.parse(furnitureList);
    var cursOp = NAPI.GetCursorPositionMaintainRatio();
    var s2w = NAPI.ScreenToWorldMantainRatio(cursOp);
    for (var i = 0; i < furnitureArray.length; i++) {
        var position = new Vector3(furnitureArray[i].position.X, furnitureArray[i].position.Y, furnitureArray[i].position.Z);
        if (s2w.DistanceTo(position) <= 1.5) {
            heldFurniture = furnitureArray[i].id;
        }
    }
}