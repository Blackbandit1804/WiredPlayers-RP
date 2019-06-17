const PlayerModel = require('./WiredPlayers/model/playerModel.js');

let camera = undefined;
let characters = undefined;
let playerData = undefined;

mp.events.add('showPlayerCharacters', (charactersJson) => {
	// Store account characters
	characters = charactersJson;

	// Show character list
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateCharacterList', charactersJson]);
});

mp.events.add('loadCharacter', (characterName) => {
	// Destroy the menu
	mp.events.call('destroyBrowser');

	// Load the character
	mp.events.callRemote('loadCharacter', characterName);
});

mp.events.add('showCharacterCreationMenu', () => {
	// Destroy the menu
	mp.events.call('destroyBrowser');

	// Initialize the character creation
	playerData = new PlayerModel();
	applyPlayerModelChanges();

	// Set the character into the creator menu
	mp.events.callRemote('setCharacterIntoCreator');

	// Make the camera focus the player
	camera = mp.cameras.new('default', new mp.Vector3(152.6008, -1003.25, -98), new mp.Vector3(-20.0, 0.0, 0.0), 90);
    camera.setActive(true);
	mp.game.cam.renderScriptCams(true, false, 0, true, false);

	// Disable the interface
	mp.game.ui.displayRadar(false);
	mp.game.ui.displayHud(false);
	mp.gui.chat.activate(false);
	mp.gui.chat.show(false);

	// Load the character creation menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/characterCreator.html']);
});

mp.events.add('changePlayerSex', (sex) => {
	// Store the value into the object
	playerData.sex = sex;

	// Change the player's look
	mp.players.local.model = sex === 0 ? mp.game.joaat('mp_m_freemode_01') : mp.game.joaat('mp_f_freemode_01');
	applyPlayerModelChanges();
});

mp.events.add('storePlayerData', (dataJSONString) => {
	// Get the object from the JSON string
	dataObject = JSON.parse(dataJSONString);

    for(let key in dataObject) {
		if (dataObject.hasOwnProperty(key)) {
			// Save the new data
			playerData[key] = dataObject[key];
		}
	}
	
	// Update the model changes
	applyPlayerModelChanges();
});

mp.events.add('cameraPointTo', (bodyPart) => {
	if(bodyPart == 0) {
		// Make the camera point to the body
		camera.setCoord(152.6008, -1003.25, -98);
	} else {
		// Make the camera point to the face
		camera.setCoord(152.3708, -1001.75, -98.45);
	}
});

mp.events.add('rotateCharacter', (rotation) => {
	// Rotate the character
	mp.players.local.setHeading(rotation);
});

mp.events.add('characterNameDuplicated', () => {
	// Duplicated name
	mp.events.call('executeFunction', ['showPlayerDuplicatedWarn']);
});

mp.events.add('acceptCharacterCreation', (name, age) => {
	// Get the player's sex
	let sex = parseInt(playerData.sex);

	// Delete the keys from the object
	delete playerData.name;
	delete playerData.age;
	delete playerData.sex;

	// Create the new character
	let skinJson = JSON.stringify(playerData);
	mp.events.callRemote('createCharacter', name, parseInt(age), sex, skinJson);
});

mp.events.add('cancelCharacterCreation', () => {
	// Get the default camera
	mp.game.cam.renderScriptCams(false, false, 0, true, false);
	camera.destroy();
	camera = undefined;

	// Enable the interface
	mp.game.ui.displayRadar(true);
	mp.game.ui.displayHud(true);
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);

	// Destroy character creation menu
	mp.events.call('destroyBrowser');

	// Add clothes and tattoos
	mp.events.callRemote('loadCharacter', mp.players.local.name);

	// Show the character list
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateCharacterList', characters]);
});

mp.events.add('characterCreatedSuccessfully', () => {
	// Get the default camera
	mp.game.cam.renderScriptCams(false, false, 0, true, false);
	camera.destroy();
	camera = undefined;

	// Enable the interface
	mp.game.ui.displayRadar(true);
	mp.game.ui.displayHud(true);
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);

	// Destroy character creation menu
	mp.events.call('destroyBrowser');
});

function applyPlayerModelChanges() {
    // Get the current player
	let player = mp.players.local;

    // Apply the changes to the player
    player.setHeadBlendData(playerData.firstHeadShape, playerData.secondHeadShape, 0, playerData.firstSkinTone, playerData.secondSkinTone, 0, playerData.headMix, playerData.skinMix, 0, false);
	player.setComponentVariation(2, playerData.hairModel, 0, 0);
	player.setHairColor(playerData.firstHairColor, playerData.secondHairColor);
	player.setEyeColor(playerData.eyesColor);
	player.setHeadOverlay(1, playerData.beardModel, 1.0, playerData.beardColor, 0);
	player.setHeadOverlay(10, playerData.chestModel, 1.0, playerData.chestColor, 0);
	player.setHeadOverlay(2, playerData.eyebrowsModel, 1.0, playerData.eyebrowsColor, 0);
	player.setHeadOverlay(5, playerData.blushModel, 1.0, playerData.blushColor, 0);
	player.setHeadOverlay(8, playerData.lipstickModel, 1.0, playerData.lipstickColor, 0);
	player.setHeadOverlay(0, playerData.blemishesModel, 1.0, 0, 0);
	player.setHeadOverlay(3, playerData.ageingModel, 1.0, 0, 0);
	player.setHeadOverlay(6, playerData.complexionModel, 1.0, 0, 0);
	player.setHeadOverlay(7, playerData.sundamageModel, 1.0, 0, 0);
	player.setHeadOverlay(9, playerData.frecklesModel, 1.0, 0, 0);
	player.setHeadOverlay(4, playerData.makeupModel, 1.0, 0, 0);
	player.setFaceFeature(0, playerData.noseWidth);
	player.setFaceFeature(1, playerData.noseHeight);
	player.setFaceFeature(2, playerData.noseLength);
	player.setFaceFeature(3, playerData.noseBridge);
	player.setFaceFeature(4, playerData.noseTip);
	player.setFaceFeature(5, playerData.noseShift);
	player.setFaceFeature(6, playerData.browHeight);
	player.setFaceFeature(7, playerData.browWidth);
	player.setFaceFeature(8, playerData.cheekboneHeight);
	player.setFaceFeature(9, playerData.cheekboneWidth);
	player.setFaceFeature(10, playerData.cheeksWidth);
	player.setFaceFeature(11, playerData.eyes);
	player.setFaceFeature(12, playerData.lips);
	player.setFaceFeature(13, playerData.jawWidth);
	player.setFaceFeature(14, playerData.jawHeight);
	player.setFaceFeature(15, playerData.chinLength);
	player.setFaceFeature(16, playerData.chinPosition);
	player.setFaceFeature(17, playerData.chinWidth);
	player.setFaceFeature(18, playerData.chinShape);
	player.setFaceFeature(19, playerData.neckWidth);
}
