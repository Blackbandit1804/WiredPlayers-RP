let clothesTypeArray = [
	{type: 0, slot: 1, desc: 'clothes.masks'}, {type: 0, slot: 3, desc: 'clothes.torso'}, {type: 0, slot: 4, desc: 'clothes.legs'}, 
	{type: 0, slot: 5, desc: 'clothes.bags'}, {type: 0, slot: 6, desc: 'clothes.feet'}, {type: 0, slot: 7, desc: 'clothes.complements'}, 
	{type: 0, slot: 8, desc: 'clothes.undershirt'}, {type: 0, slot: 11, desc: 'clothes.tops'}, {type: 1, slot: 0, desc: 'clothes.hats'}, 
	{type: 1, slot: 1, desc: 'clothes.glasses'}, {type: 1, slot: 2, desc: 'clothes.earrings'}
];

let clothes = [];

mp.events.add('showPlayerWardrobe', () => {	
	// Show wardrobe's menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateWardrobeMenu', JSON.stringify(clothesTypeArray)]);
});

mp.events.add('getPlayerPurchasedClothes', (index) => {	
	// Get the player's clothes
	mp.events.callRemote('getPlayerPurchasedClothes', clothesTypeArray[index].type, clothesTypeArray[index].slot);
});

mp.events.add('showPlayerClothes', (clothesJson, namesJson) => {
	let clothesNames = JSON.parse(namesJson);
	clothes = JSON.parse(clothesJson);
	
	for(let i = 0; i < clothes.length; i++) {
		// Add the name of the clothes
		clothes[i].name = clothesNames[i];
	}
	
	// Show clothes of the selected type
	mp.events.call('executeFunction', ['populateWardrobeClothes', JSON.stringify(clothes).replace(/'/g, "\\'")]);
});

mp.events.add('previewPlayerClothes', (index) => {
	let player = mp.players.local;
	
	if(clothes[index].type === 0) {
		// Change player's clothes
		player.setComponentVariation(clothes[index].slot, clothes[index].drawable, clothes[index].texture, 0);
	} else {
		// Change player's accessory
		player.setPropIndex(clothes[index].slot, clothes[index].drawable, clothes[index].texture, 0);
	}
});

mp.events.add('changePlayerClothes', (index) => {	
	// Equip the clothes
	mp.events.callRemote('wardrobeClothesItemSelected', clothes[index].id);
});