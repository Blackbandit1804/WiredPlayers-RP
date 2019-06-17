let carShopVehicles = undefined;
let carShopTestBlip = undefined;
let previewVehicle = undefined;
let previewCamera = undefined;
let dealership = undefined;

mp.events.add('showVehicleCatalog', (vehicles, dealer) => {
	// Get the vehicles and car dealer
	carShopVehicles = vehicles;
	dealership = dealer;

	// Disable the chat
	mp.gui.chat.activate(false);
	mp.gui.chat.show(false);

	// Show the catalog
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/vehicleCatalog.html', 'populateVehicleList', dealership, carShopVehicles]);
});

mp.events.add('previewCarShopVehicle', (model) => {
	if (previewVehicle !== undefined) {
		previewVehicle.destroy();
	}

	// Destroy the catalog
	mp.events.call('destroyBrowser');

	switch(dealership) {
		case 2:
		previewVehicle = mp.vehicles.new(mp.game.joaat(model), new mp.Vector3(-878.5726, -1353.408, 0.1741), {heading: 90.0});
		previewCamera = mp.cameras.new('default', new mp.Vector3(-882.3361, -1342.628, 5.0783), new mp.Vector3(-20.0, 0.0, 200.0), 90);
		break;
		default:
		previewVehicle = mp.vehicles.new(mp.game.joaat(model), new mp.Vector3(-31.98111, -1090.434, 26.42225), {heading: 180.0});
		previewCamera = mp.cameras.new('default', new mp.Vector3(-37.83527, -1088.096, 27.92234), new mp.Vector3(-20.0, 0.0, 250.0), 90);
		break;
	}

	// Make the camera point the vehicle
	previewCamera.setActive(true);
	mp.game.cam.renderScriptCams(true, false, 0, true, false);

	// Disable the HUD
	mp.game.ui.displayHud(false);
	mp.game.ui.displayRadar(false);

	// Vehicle preview menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/vehiclePreview.html', 'checkVehiclePayable']);
});

mp.events.add('rotatePreviewVehicle', (rotation) => {
	// Set the vehicle's heading
	previewVehicle.setHeading(rotation);
});

mp.events.add('previewVehicleChangeColor', (color, colorMain) => {
	if (colorMain) {
		previewVehicle.setCustomPrimaryColour(hexToRgb(color).r, hexToRgb(color).g, hexToRgb(color).b);
	} else {
		previewVehicle.setCustomSecondaryColour(hexToRgb(color).r, hexToRgb(color).g, hexToRgb(color).b);
	}
});

mp.events.add('showCatalog', () => {
	// Destroy preview menu
	mp.events.call('destroyBrowser');

	// Destroy the vehicle
	previewVehicle.destroy();
	previewVehicle = undefined;

	// Enable the HUD
	mp.game.ui.displayHud(true);
	mp.game.ui.displayRadar(true);

	// Position the camera behind the character
	mp.game.cam.renderScriptCams(false, false, 0, true, false);
	previewCamera.destroy();
	previewCamera = undefined;

	// Show the catalog
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/vehicleCatalog.html', 'populateVehicleList', dealership, carShopVehicles]);
});

mp.events.add('closeCatalog', () => {
	// Destroy the catalog
	mp.events.call('destroyBrowser');

	// Enable the chat
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);
});

mp.events.add('checkVehiclePayable', () => {
	// Get the vehicles' list
	let vehicleArray = JSON.parse(carShopVehicles);

	for (var i = 0; i < vehicleArray.length; i++) {
		if (mp.game.joaat(vehicleArray[i].model) == previewVehicle.model) {
			// Check if the player has enough money in the bank
			if(mp.players.local.getVariable('PLAYER_BANK') >= vehicleArray[i].price) {
				// Enable purchase button
				mp.events.call('executeFunction', ['showVehiclePurchaseButton']);
			}
			break;
		}
	}
});

mp.events.add('purchaseVehicle', () => {
	// Get the vehicle's data
	let model = String(previewVehicle.model);
	let firstColorObject = previewVehicle.getCustomPrimaryColour(0, 0, 0);
	let secondColorObject = previewVehicle.getCustomSecondaryColour(0, 0, 0);

	// Get color strings
	let firstColor = firstColorObject.r + ',' + firstColorObject.g + ',' + firstColorObject.b;
	let secondColor = secondColorObject.r + ',' + secondColorObject.g + ',' + secondColorObject.b;

	// Destroy preview menu
	mp.events.call('destroyBrowser');

	// Destroy preview vehicle
	previewVehicle.destroy();
	previewVehicle = undefined;

	// Enable the HUD
	mp.game.ui.displayHud(true);
	mp.game.ui.displayRadar(true);

	// Delete the custom camera
	mp.game.cam.renderScriptCams(false, false, 0, true, false);
	previewCamera.destroy();
	previewCamera = undefined;

	// Enable the chat
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);

	// Purchase the vehicle
	mp.events.callRemote('purchaseVehicle', model, firstColor, secondColor);
});

mp.events.add('testVehicle', () => {
	// Get the vehicle's model
	let model = String(previewVehicle.model);

	// Destroy preview menu
	mp.events.call('destroyBrowser');

	// Destroy preview vehicle
	previewVehicle.destroy();
	previewVehicle = undefined;

	// Enable the HUD
	mp.game.ui.displayHud(true);
	mp.game.ui.displayRadar(true);

	// Delete the custom camera
	mp.game.cam.renderScriptCams(false, false, 0, true, false);
	previewCamera.destroy();
	previewCamera = undefined;

	// Enable the chat
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);

	// Test the vehicle
	mp.events.callRemote('testVehicle', model);
});

mp.events.add('showCarshopCheckpoint', (position) => {
	// Add a blip with the delivery place
	carShopTestBlip = mp.blips.new(1, position, {color: 1});
});

mp.events.add('deleteCarshopCheckpoint', () => {
	// Delete the blip
	carShopTestBlip.destroy();
	carShopTestBlip = undefined;
});

function hexToRgb(hex) {
	var shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i;
	hex = hex.replace(shorthandRegex, function(m, r, g, b) {
		return r + r + g + g + b + b;
	});

	var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
	return result ? {
		r: parseInt(result[1], 16),
		g: parseInt(result[2], 16),
		b: parseInt(result[3], 16)
	} : null;
}
