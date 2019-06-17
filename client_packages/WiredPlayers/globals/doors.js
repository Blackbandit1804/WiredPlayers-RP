let policeMainDoors = undefined;
let policeBackDoors = undefined;
let policeCellDoors = undefined;
let motorsportMain = undefined;
let motorsportParking = undefined;
let supermarketDoors = undefined;
let clubhouseDoor = undefined;

mp.events.add('guiReady', () => {
	// Police station's doors
	policeMainDoors = mp.colshapes.newSphere(468.535, -1014.098, 26.386, 5.0);
	policeBackDoors = mp.colshapes.newSphere(435.131, -981.9197, 30.689, 5.0);
	policeCellDoors = mp.colshapes.newSphere(461.7501, -998.361, 24.915, 5.0);

	// Car dealer's doors
	motorsportMain = mp.colshapes.newSphere(-59.893, -1092.952, 26.8836, 5.0);
	motorsportParking = mp.colshapes.newSphere(-39.134, -1108.22, 26.72, 5.0);

	// Supermarket's doors
	supermarketDoors = mp.colshapes.newSphere(-711.545, -915.54, 19.216, 5.0);

	// Club house's doors
	clubhouseDoor = mp.colshapes.newSphere(981.7533, -102.7987, 74.8487, 5.0);
});

mp.events.add('playerEnterColshape', (shape) => {
	switch(shape.id) {
		case policeMainDoors.id:
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_ph_door002'), 434.7479, -983.2151, 30.83926, true, 0, false);
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_ph_door01'), 434.7479, -980.6184, 30.83926, true, 0, false);
			break;
		case policeBackDoors.id:
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_rc_door2'), 469.9679, -1014.452, 26.53623, true, 0, false);
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_rc_door2'), 467.3716, -1014.452, 26.53623, true, 0, false);
			break;
		case policeCellDoors.id:
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_ph_cellgate'), 461.8065, -994.4086, 25.06443, true, 0, false);
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_ph_cellgate'), 461.8065, -997.6583, 25.06443, true, 0, false);
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_ph_cellgate'), 461.8065, -1001.302, 25.06443, true, 0, false);
			break;
		case motorsportMain.id:
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_csr_door_l'), -59.89302, -1092.952, 26.88362, false, 0, false);
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_csr_door_r'), -60.54582, -1094.749, 26.88872, false, 0, false);
			break;
		case supermarketDoors.id:
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_gasdoor'), -711.5449, -915.5397, 19.21559, true, 0, false);
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_gasdoor_r'), -711.5449, -915.5397, 19.2156, true, 0, false);
			break;
		case clubhouseDoor.id:
			mp.game.object.setStateOfClosestDoorOfType(mp.game.joaat('v_ilev_lostdoor'), 981.7533, -102.7987, 74.84873, true, 0, false);
			break;
	}
});
