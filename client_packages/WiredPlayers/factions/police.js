let crimesJson = undefined;
let crimesArray = undefined;
let selectedControl = undefined;
let reinforces = [];

mp.events.add('showCrimesMenu', (crimes) => {
	// Save crimes list
	crimesJson = crimes;
	
	// Show crimes menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateCrimesMenu', crimes, '']);
});

mp.events.add('applyCrimes', (crimes) => {
	// Store crimes to be applied
	crimesArray = crimes;
	
	// Destroy crimes menu
	mp.events.call('destroyBrowser');
	
	// Show the confirmation window
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/crimesConfirm.html', 'populateCrimesConfirmMenu', crimesArray]);
});

mp.events.add('executePlayerCrimes', () => {
	// Destroy the confirmation menu
	mp.events.call('destroyBrowser');
	
	// Apply crimes to the player
	mp.events.callRemote('applyCrimesToPlayer', crimesArray);
});

mp.events.add('backCrimesMenu', () => {
	// Destroy the confirmation menu
	mp.events.call('destroyBrowser');
	
	// Show crimes menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateCrimesMenu', crimesJson, crimesArray]);
});

mp.events.add('loadPoliceControlList', (policeControls) => {
	// Show the menu with the police control list
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populatePoliceControlMenu', policeControls]);
});

mp.events.add('proccessPoliceControlAction', (control) => {
	// Check the selected option
	let controlOption = mp.players.local.getVariable('PLAYER_POLICE_CONTROL');
	
	switch(controlOption) {
		case 1:
			if(control === undefined) {
				// Save the police control with a new name
				mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/policeControlName.html']);				
			} else {
				// Override the existing police control
				mp.events.callRemote('policeControlSelected', control);
			}
			break;
		case 2:
			// Show the window to change control's name
			mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/policeControlName.html']);	
			selectedControl = control;
			break;
		default:
			// Execute the option over the police control
			mp.events.callRemote('policeControlSelected', control);
			break;
	}
});

mp.events.add('policeControlSelectedName', (name) => {
	// Save the police control with a new name
	mp.events.callRemote('updatePoliceControlName', selectedControl, name);
});

mp.events.add('updatePoliceReinforces', (reinforcesJson) => {
	let updatedReinforces = JSON.parse(reinforcesJson);

	// Search for policemen asking for reinforces
	for(let i = 0; i < updatedReinforces.length; i++) {
		// Get the identifier
		let police = updatedReinforces[i].playerId;
		let position = new mp.Vector3(updatedReinforces[i].position.X, updatedReinforces[i].position.Y, updatedReinforces[i].position.Z);

		if(reinforces[police] === undefined) {
			// Create a blip on the map
			let reinforcesBlip = mp.blips.new(487, position, {color: 38, alpha: 255, shortRange: false});

			// Add the new member to the array
			reinforces[police] = reinforcesBlip;
		} else {
			// Update the blip's position
			reinforces[police].position = position;
		}
	}
});

mp.events.add('reinforcesRemove', (officer) => {
	// Delete officer's reinforces
	reinforces[officer].destroy();
	reinforces[officer] = undefined;
});