let businessItems = undefined;
let businessPriceMultiplier = 0.0;

mp.events.add('showBusinessPurchaseMenu', (itemsJsonArray, business, multiplier) => {
	// Store the products and price
	businessItems = itemsJsonArray;
	businessPriceMultiplier = multiplier;

	// Show the menu with the items available to purchase
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateBusinessItems', itemsJsonArray, business, multiplier]);
});

mp.events.add('purchaseItem', (index, amount) => {
	// Get the purchased item and its cost
	let purchasedItem = JSON.parse(businessItems)[index];
	mp.events.callRemote('businessPurchaseMade', purchasedItem.description, amount);
});
