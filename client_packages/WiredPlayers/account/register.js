mp.events.add('showRegisterWindow', () => {
	// Create login window
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/register.html']);
});

mp.events.add('createPlayerAccount', (password) => {
	// Check for the credentials
	mp.events.callRemote('registerAccount', password);
});

mp.events.add('clearRegisterWindow', () => {
	// Unfreeze the player
	mp.players.local.freezePosition(false);

	// Destroy the login window
	mp.events.call('destroyBrowser');
});
