const tattooZoneArray = ['tattoo.torso', 'tattoo.head', 'tattoo.left-arm', 'tattoo.right-arm', 'tattoo.left-leg', 'tattoo.right-leg'];

let playerTattooArray = [];
let tattooList = [];
let zoneTattoos = [];
let playerSex = 0;

mp.events.add('showTattooMenu', (sex, playerTattoos, tattoosJson, business, price) => {
	// Store the variables
	playerTattooArray = JSON.parse(playerTattoos);
	tattooList = JSON.parse(tattoosJson);
	playerSex = sex;
	
	// Show tattoos menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateTattooMenu', JSON.stringify(tattooZoneArray), business, price]);
});

mp.events.add('getZoneTattoos', (zone) => {
	zoneTattoos = [];
	
	for(let i = 0; i < tattooList.length; i++) {
		if(tattooList[i].slot === zone) {
			// Add the tattoo to the list
			zoneTattoos.push(tattooList[i]);
		}
	}
	
	// Show the tattoos for the selected zone
	mp.events.call('executeFunction', ['populateZoneTattoos', JSON.stringify(zoneTattoos).replace(/'/g, "\\'")]);
});

mp.events.add('addPlayerTattoo', (index) => {
	// Get the player
	let player = mp.players.local;

	// Load the player's tattoos
	loadPlayerTattoos();
	
	// Add the tattoo to the player
	player.setDecoration(mp.game.joaat(zoneTattoos[index].library), playerSex === 0 ? mp.game.joaat(zoneTattoos[index].maleHash) : mp.game.joaat(zoneTattoos[index].femaleHash));
});

mp.events.add('clearTattoos', () => {
	// Restore player's tattoos
	loadPlayerTattoos();
});

mp.events.add('purchaseTattoo', (slot, index) => {	
	// Add the new tattoo to the list
	let tattoo = {};
	tattoo.slot = slot;
	tattoo.library = zoneTattoos[index].library;
	tattoo.hash = playerSex === 0 ? zoneTattoos[index].maleHash : zoneTattoos[index].femaleHash;
	playerTattooArray.push(tattoo);
	
	// Purchase the tattoo
	mp.events.callRemote('purchaseTattoo', slot, index);
});

mp.events.add('exitTattooShop', () => {
	// Close the purchase menu
	mp.events.call('destroyBrowser');
	
	// Dress the character
	mp.events.callRemote('loadCharacterClothes');
});

function loadPlayerTattoos() {
	// Get player's data
	let player = mp.players.local;

	// Clear all the tattoos
	player.clearDecorations();

	for (let i = 0; i < playerTattooArray.length; i++) {
		let tattoo = playerTattooArray[i];
		player.setDecoration(mp.game.joaat(tattoo.library), mp.game.joaat(tattoo.hash));
	}
}