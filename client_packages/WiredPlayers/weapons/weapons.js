let gunHashes = [
	// Pistolas
	453432689, 1593441988, -1716589765, -1076751822, -771403250, 137902532, -598887786, -1045183535, 584646201, 1198879012,
	// Metralletas
	324215364, -619010992, 736523883, 171789620, -1660422300, 2144741730, 1627465347, -1121678507,
	// Rifles de asalto
	-1074790547, -2084633992, -1357824103, -1063057011, 2132975508, 1649403952,
	// Rifles de francotirador
	100416529, 205991906, -952879014,
	// Escopetas
	487013001, 2017895192, -1654528753, -494615257, -1466123874, 984333226, -275439685, 317205821
];

let weaponCylinder = null;
let weaponBlip = null;
/*
NAPI.OnServerEventTrigger.connect(function (eventName, args) {
	switch(eventName) {
		case 'showWeaponCheckpoint':
			// Colocamos un checkpoint en el punto de entrega
            weaponCylinder = NAPI.CreateMarker(1, args[0], new Vector3(0.0, 0.0, 0.0), new Vector3(0.0, 0.0, 0.0), new Vector3(9.0, 9.0, 9.0), 255, 0, 0, 70);
			weaponBlip = NAPI.Blip.CreateBlip(args[0]);
			break;
		case 'deleteWeaponCheckpoint':
			// Eliminamos los checkpoints del mapa
			if (weaponCylinder != null && weaponBlip != null) {
				NAPI.Entity.DeleteEntity(weaponCylinder);
				NAPI.Entity.DeleteEntity(weaponBlip);
			}
			weaponCylinder = null;
			weaponBlip = null;
			break;
    }
});

NAPI.OnKeyUp.connect(function (sender, e) {
	if (e.KeyCode === Keys.R) {
		let currentWeapon = NAPI.GetPlayerCurrentWeapon();
		if(!NAPI.IsPlayerReloading(NAPI.GetLocalPlayer()) && gunHashes.indexOf(currentWeapon) > -1) {
			NAPI.TriggerServerEvent("reloadPlayerWeapon");
		}
	}
});

NAPI.OnUpdate.connect(function () {
	// Miramos si tiene alguna caja de armas entre manos
	if(NAPI.Data.HasEntitySharedData(NAPI.GetLocalPlayer(), "PLAYER_WEAPON_CRATE") == true) {
		NAPI.DisableControlThisFrame(21);
		NAPI.DisableControlThisFrame(24);
		NAPI.DisableControlThisFrame(25);
	}
});*/