let factionWarningBlip = undefined;

mp.events.add('showFactionWarning', (position) => {
	// Create the blip on the map
	factionWarningBlip = mp.blips.new(1, position, {color: 1});
});

mp.events.add('deleteFactionWarning', () => {
	// Destroy the blip on the map
	factionWarningBlip.destroy();
	factionWarningBlip = undefined;
});