let fastFoodBlip = undefined;

mp.events.add('showFastfoodOrders', (orders, distances) => {
	// Create the fastfood menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateFastfoodOrders', orders, distances]);
});

mp.events.add('deliverFastfoodOrder', (order) => {
	// Close the menu and attend the order
	mp.events.callRemote('takeFastFoodOrder', order);
	mp.events.call('destroyBrowser');
});

mp.events.add('fastFoodDestinationCheckPoint', (position) => {
	// Create a blip on the map
	fastFoodBlip = mp.blips.new(1, position, {color: 1});
});

mp.events.add('fastFoodDeliverBack', (position) => {
	// Set the blip at the starting position
	fastFoodBlip.setCoords(position);
});

mp.events.add('fastFoodDeliverFinished', () => {
	// Destroy the blip on the map
	fastFoodBlip.destroy();
	fastFoodBlip = undefined;
});