let clothesTypeArray = [
	{type: 0, slot: 1, desc: 'clothes.masks'}, {type: 0, slot: 3, desc: 'clothes.torso'}, {type: 0, slot: 4, desc: 'clothes.legs'}, 
	{type: 0, slot: 5, desc: 'clothes.bags'}, {type: 0, slot: 6, desc: 'clothes.feet'}, {type: 0, slot: 7, desc: 'clothes.complements'}, 
	{type: 0, slot: 8, desc: 'clothes.undershirt'}, {type: 0, slot: 11, desc: 'clothes.tops'}, {type: 1, slot: 0, desc: 'clothes.hats'}, 
	{type: 1, slot: 1, desc: 'clothes.glasses'}, {type: 1, slot: 2, desc: 'clothes.earrings'}, {type: 1, slot: 6, desc: 'clothes.watches'}, 
	{type: 1, slot: 7, desc: 'clothes.bracelets'}
];

let selectedIndex = -1;
let clothesTypes = [];

mp.events.add('showClothesBusinessPurchaseMenu', (business, price) => {	
	// Show clothes menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateClothesShopMenu', JSON.stringify(clothesTypeArray), business, price]);
});

mp.events.add('getClothesByType', (index) => {
	// Save selected index
	selectedIndex = index;
	
	// Get clothes list
	mp.events.callRemote('getClothesByType', clothesTypeArray[index].type, clothesTypeArray[index].slot);
});

mp.events.add('showTypeClothes', (clothesJson) => {
	let player = mp.players.local;
	let type = clothesTypeArray[selectedIndex].type;
	let slot = clothesTypeArray[selectedIndex].slot;
	
	// Get clothes list for the type
	clothesTypes = JSON.parse(clothesJson);
	
	for(let i = 0; i < clothesTypes.length; i++) {
		// Add clothes' texture number
		clothesTypes[i].textures = type == 0 ? player.getNumberOfTextureVariations(slot, clothesTypes[i].clothesId) : player.getNumberOfPropTextureVariations(slot, clothesTypes[i].clothesId);
	}
	
	// Show all the clothes from the selected type
	mp.events.call('executeFunction', ['populateTypeClothes', JSON.stringify(clothesTypes).replace(/'/g, "\\'")]);
});

mp.events.add('replacePlayerClothes', (index, texture) => {
	let player = mp.players.local;
	
	if(clothesTypes[index].type === 0) {
		// Change player's clothes
		player.setComponentVariation(clothesTypes[index].bodyPart, clothesTypes[index].clothesId, texture, 0);
	} else {
		// Change player's accessory
		player.setPropIndex(clothesTypes[index].bodyPart, clothesTypes[index].clothesId, texture, true);
	}
});

mp.events.add('purchaseClothes', (index, texture) => {
	// Create the clothes model
	let clothesModel = {};
	clothesModel.type = clothesTypes[index].type;
	clothesModel.slot = clothesTypes[index].bodyPart;
	clothesModel.drawable = clothesTypes[index].clothesId;
	clothesModel.texture = parseInt(texture);

	// Purchase the clothes
	mp.events.callRemote('clothesItemSelected', JSON.stringify(clothesModel));
});

mp.events.add('clearClothes', (index) => {
	// Get the type and slot
	let type = clothesTypes[index].type;
	let slot = clothesTypes[index].bodyPart;

	// Clear the not purchased clothes
	mp.events.callRemote('dressEquipedClothes', type, slot);
});

