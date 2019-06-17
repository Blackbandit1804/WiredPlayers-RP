const townHallOptions = [
	{'desc': 'townhall.identification', 'price': 500}, {'desc': 'townhall.insurance', 'price': 2000}, 
	{'desc': 'townhall.taxi', 'price': 5000}, {'desc': 'townhall.fines', 'price': 0}
];

mp.events.add('showTownHallMenu', () => {
	// Show the Town Hall's menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateTownHallMenu', JSON.stringify(townHallOptions)]);
});

mp.events.add('executeTownHallOperation', (selectedOption) => {
	// Execute the selected operation
	mp.events.callRemote('documentOptionSelected', selectedOption);
});

mp.events.add('showPlayerFineList', (playerFines) => {
	// Show fines menu
	mp.events.call('executeFunction', ['populateFinesMenu', playerFines]);
});

mp.events.add('payPlayerFines', (finesArrayJson) => {
	// Pay the selected fines
	mp.events.callRemote('payPlayerFines', finesArrayJson);
});

mp.events.add('backTownHallIndex', () => {
	// Show the Town Hall's menu
	mp.events.call('executeFunction', ['populateTownHallMenu', JSON.stringify(townHallOptions)]);
});