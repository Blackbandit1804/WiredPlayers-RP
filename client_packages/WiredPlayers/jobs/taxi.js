mp.events.add('playerCreateWaypoint', (position) => {
	// Check if the player is in a taxi
	let vehicle = mp.players.local.vehicle;

	if(vehicle !== undefined && vehicle.getModel() === mp.game.joaat('taxi') && mp.players.local.seat >= 0) {
		// Send the destination to the driver
		mp.events.callRemote('requestTaxiDestination', position);
	}
});

mp.events.add('playerReachWaypoint', () => {
	// Check if the player is in a taxi
	let vehicle = mp.players.local.vehicle;

	if(vehicle !== undefined && vehicle.getModel() === mp.game.joaat('taxi') && mp.players.local.seat == -1) {
		// End the taxi service
		mp.events.callRemote('taxiDestinationReached');
	}
});

mp.events.add('createTaxiPath', (position) => {
	// Set the waypoint
	mp.game.ui.setNewWaypoint(position.x, position.y);
});