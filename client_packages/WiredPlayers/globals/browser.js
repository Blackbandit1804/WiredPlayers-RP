let customBrowser = undefined;
let parameters = [];

mp.events.add('createBrowser', (arguments) => {
	// Check if there's a browser already opened
	if(customBrowser === undefined) {
		// Save the parameters
		parameters = arguments.slice(1, arguments.length);
		
		// Create the browser
		customBrowser = mp.browsers.new(arguments[0]);
	}
});

mp.events.add('browserDomReady', (browser) => {
	if(customBrowser === browser) {
		// Enable the cursor
		mp.gui.cursor.visible = true;
		
		if(parameters.length > 0) {
			// Call the function passed as parameter
			mp.events.call('executeFunction', parameters);
		}
	}
});

mp.events.add('executeFunction', (arguments) => {
	// Check for the parameters
	let input = '';
	
	for(let i = 1; i < arguments.length; i++) {
		if(input.length > 0) {
			input += ', \'' + arguments[i] + '\'';
		} else {
			input = '\'' + arguments[i] + '\'';
		}
	}
	
	// Call the function with the parameters
	customBrowser.execute(`${arguments[0]}(${input});`);
});

mp.events.add('destroyBrowser', () => {
	// Disable the cursor
	mp.gui.cursor.visible = false;
	
	// Destroy the browser
	customBrowser.destroy();
	customBrowser = undefined;
});