let garbageBlip = undefined; 

mp.events.add('showGarbageCheckPoint', (position) => {
	// Create the blip on the map
	garbageBlip = mp.blips.new(1, position, {color: 1});
});

mp.events.add('deleteGarbageCheckPoint', () => {
	// Destroy the blip on the map
	garbageBlip.destroy();
	garbageBlip = undefined;
});