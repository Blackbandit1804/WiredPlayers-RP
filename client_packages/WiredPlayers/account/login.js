mp.events.add('accountLoginForm', () => {
	// Create login window
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/login.html']);
});

mp.events.add('requestPlayerLogin', (password) => {
	setTimeout(function() {
		// Check for the credentials
		mp.events.callRemote('loginAccount', password);
	}, 100);
});

mp.events.add('showLoginError', () => {
	// Show the message on the panel
	mp.events.call('executeFunction', ['showLoginError']);
});

mp.events.add('clearLoginWindow', () => {
	// Unfreeze the player
	mp.players.local.freezePosition(false);

	// Destroy the login window
	mp.events.call('destroyBrowser');
});
