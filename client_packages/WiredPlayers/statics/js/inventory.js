let messagesLoaded = false;
let selected = undefined;
let timeout = undefined;

$(document).ready(function() {
	i18next.use(window.i18nextXHRBackend).init({
		backend: {
			loadPath: '../i18n/en.json'
		}
	}, function(err, t) {
        jqueryI18next.init(i18next, $);
		$(document).localize();
		messagesLoaded = true;
	});
});

function populateInventory(inventoryJson, title) {
	if(messagesLoaded) {
		// Initialize the selection
		selected = undefined;
		
		// Get the items in the inventory
		let inventory = JSON.parse(inventoryJson);
		
		// Get the item containers
		let titleContainer = document.getElementById('identifier');
		let inventoryContainer = document.getElementById('inventory');

		// Add the text to the header
		titleContainer.textContent = i18next.t(title);
		
		for(let i = 0; i < inventory.length; i++) {
			// Get each item
			let item = inventory[i];
			
			// Create the elements to show the items
			let itemContainer = document.createElement('div');
			let amountContainer = document.createElement('div');
			let itemImage = document.createElement('img');
			
			// Get the needed classes
			itemContainer.classList.add('inventory-item');
			amountContainer.classList.add('inventory-amount');
			
			// Get the content of each item
			itemImage.src = '../img/inventory/' + item.hash + '.png';
			amountContainer.textContent = item.amount;
			
			itemContainer.onclick = (function() {
				// Check if a new item has been selected
				if(selected !== i) {
					// Get the previous selection
					if(selected != undefined) {
						let previousSelected = document.getElementsByClassName('inventory-item')[selected];
						previousSelected.classList.remove('active-item');
					}
					
					// Select the clicked element
					let currentSelected = document.getElementsByClassName('inventory-item')[i];
					currentSelected.classList.add('active-item');
					selected = item.id;
					
					// Show the options
					mp.trigger('getInventoryOptions', item.type, item.hash);
				}
			});
			
			// Create the item hierarchy	
			inventoryContainer.appendChild(itemContainer);
			itemContainer.appendChild(amountContainer);
			itemContainer.appendChild(itemImage);		
		}	
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { populateInventory(inventoryJson, title); }, 100);
	}
}

function showInventoryOptions(optionsArrayJson, dropable) {
	if(messagesLoaded) {
		// Get the footer options
		let root = document.getElementById('item-options');
		
		// Clear the children
		while(root.firstChild) {
			root.removeChild(root.firstChild);
		}

		// Get the options
		let optionsArray = JSON.parse(optionsArrayJson);

		// Add the options
		for(let i = 0; i < optionsArray.length; i++) {

			// Create the container
			let optionContainer = document.createElement('div');

			// Add the text to the option
			optionContainer.textContent = i18next.t(optionsArray[i]);

			// Add the click event
			optionContainer.onclick = (function() {
				// Execute the selected option
				mp.trigger('executeAction', selected, i18next.t(optionsArray[i]));
			});

			// Add the container to the option list
			root.appendChild(optionContainer);
		}
		
		if(dropable) {
			// Add drop option
			let optionContainer = document.createElement('div');

			// Add the text to the option
			optionContainer.textContent = i18next.t('general.drop');

			// Add the click event
			optionContainer.onclick = (function() {
				// Execute the selected option
				mp.trigger('executeAction', selected, i18next.t('general.drop'));
			});

			// Add the container to the option list
			root.appendChild(optionContainer);
		}
			
		clearTimeout(timeout);
	} else {
		// Wait for the messages to be loaded
		clearTimeout(timeout);
		timeout = setTimeout(function() { showInventoryOptions(optionsArrayJson, dropable); }, 100);
	}
}