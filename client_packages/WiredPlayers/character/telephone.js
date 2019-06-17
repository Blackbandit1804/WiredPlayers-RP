let action = 0;
let contact = 0;
let contactsArray = [];

mp.events.add('showPhoneContacts', (contactsJson, selectedAction) => {
	// Store the values
	action = selectedAction;
    contactsArray = JSON.parse(contactsJson);
    
    // Show the list
    mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateContactsMenu', contactsJson, selectedAction]);
});

mp.events.add('addContactWindow', (selectedAction) => {
	// Store the action
	action = selectedAction;

	// Show the menu to add a contact
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/addPhoneContact.html']);
});

mp.events.add('preloadContactData', () => {
    if (contact > 0) {
        // Load contact's data
        let number = contactsArray[contact].contactNumber;
        let name = contactsArray[contact].contactName;

        // Show the data on the browser
	    mp.events.call('executeFunction', ['populateContactData', number, name]);
    }
});

mp.events.add('setContactData', (number, name) => {
    // Destroy the web browser
    mp.events.call('destroyBrowser');

    if (action === 4) {
        // Create new contact
        mp.events.callRemote('addNewContact', number, name);
    } else {
        // Modify the contact data
        mp.events.callRemote('modifyContact', contact, number, name);
    }
});

mp.events.add('executePhoneAction', (contactIndex) => {
    // Get the selected contact
    contact = contactsArray[contactIndex].id;

    // Destroy the current browser
    mp.events.call('destroyBrowser');

    switch(action) {
        case 2:
            // Load contact's data
            let number = contactsArray[contactIndex].contactNumber;
            let name = contactsArray[contactIndex].contactName;

            // Modify a contact
            mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/addPhoneContact.html', 'populateContactData', number, name]);

            break;
        case 3:
            // Delete a contact
            mp.events.callRemote('deleteContact', contact);
            
            break;
        case 5:
            // Send SMS to a contact
            mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sendContactMessage.html']);
            
            break;
    }
});

mp.events.add('sendPhoneMessage', (message) => {
    // Destroy the web browser
    mp.events.call('destroyBrowser');

    // Send the SMS to the target
    mp.events.callRemote('sendPhoneMessage', contact, message);
});

mp.events.add('cancelPhoneMessage', () => {
    // Destroy the web browser
    mp.events.call('destroyBrowser');

    // Show the list
    mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateContactsMenu', JSON.stringify(contactsArray), action]);
});