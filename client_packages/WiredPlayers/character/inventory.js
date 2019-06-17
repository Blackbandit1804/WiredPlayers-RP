let targetType = undefined;

mp.events.add('showPlayerInventory', (inventoryJson, target) => {
	// Store all the inventory data
	targetType = target;
	
	// Show player's inventory
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/inventory.html', 'populateInventory', inventoryJson, 'general.inventory']);
});

mp.events.add('getInventoryOptions', (itemType, itemHash) => {
	let optionsArray = [];
	let dropable = false;
	
	switch(targetType) {
		case 0:
			// Player's inventory
			if(itemType === 0) {
				// Consumable item
				optionsArray.push('general.consume');
			} else if(itemType === 2) {
				// Container item
				optionsArray.push('general.open');
			}
			
			if(isNaN(itemHash) === false) {
				// Equipable
				optionsArray.push('general.equip');
			}
			
			dropable = true;
			break;
		case 1:
			// Player frisk
			optionsArray.push('general.confiscate');
			break;
		case 2:
			// Vehicle trunk
			optionsArray.push('general.withdraw');
			break;
		case 3:
			// Inventory store into the trunk
			optionsArray.push('general.store');
			break;
	}
	
	// Show the options into the inventory
	mp.events.call('executeFunction', ['showInventoryOptions', JSON.stringify(optionsArray), dropable]);
});

mp.events.add('executeAction', (item, option) => {
	// Execute the selected action
	mp.events.callRemote('processMenuAction', item, option);
});