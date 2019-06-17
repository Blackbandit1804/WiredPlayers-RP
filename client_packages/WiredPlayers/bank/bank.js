mp.events.add('showATM', () => {
	// Disable the chat
	mp.gui.chat.activate(false);
	mp.gui.chat.show(false);
	
	// Bank menu creation
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/bankMenu.html']);
});

mp.events.add('updateBankAccountMoney', () => {
	// Get player's bank balance
	let money = mp.players.local.getVariable('PLAYER_BANK');
	
	// Update the balance on the screen
	mp.events.call('executeFunction', ['updateAccountMoney', money]);	
});

mp.events.add('executeBankOperation', (operation, amount, target) => {
	// Execute a bank operation
	mp.events.callRemote('executeBankOperation', operation, amount, target);
});

mp.events.add('bankOperationResponse', (response) => {
	// Check the action taken
	if (response == '') {
		mp.events.call('executeFunction', ['bankBack']);
	} else {
		mp.events.call('executeFunction', ['showOperationError', response]);
	}
});

mp.events.add('loadPlayerBankBalance', () => {
	// Load player's bank balance
	mp.events.callRemote('loadPlayerBankBalance');
});

mp.events.add('showPlayerBankBalance', (operationJson, playerName) => {
	// Show the player's bank operations
	mp.events.call('executeFunction', ['showBankOperations', operationJson, playerName]);
});

mp.events.add('closeATM', () => {
	// Destroy the browser
	mp.events.call('destroyBrowser');
	
	// Enable the chat
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);
});