let resolution = null;
let repaintBrowser = null;
let selected = 0;
let indexArray = [];
let slotsArray = [
	{slot: 0, desc: 'mechanic.spoiler', products: 250}, {slot: 1, desc: 'mechanic.front-bumper', products: 250}, {slot: 2, desc: 'mechanic.rear-bumper', products: 250}, 
	{slot: 3, desc: 'mechanic.side-skirt', products: 250}, {slot: 4, desc: 'mechanic.exhaust', products: 100}, {slot: 5, desc: 'mechanic.frame', products: 500}, 
	{slot: 6, desc: 'mechanic.grille', products: 200}, {slot: 7, desc: 'mechanic.hood', products: 300}, {slot: 8, desc: 'mechanic.fender', products: 100},
	{slot: 9, desc: 'mechanic.right-fender', products: 100}, {slot: 10, desc: 'mechanic.roof', products: 400}, {slot: 14, desc: 'mechanic.horn', products: 100}, 
	{slot: 15, desc: 'mechanic.suspension', products: 900}, {slot: 22, desc: 'mechanic.xenon', products: 150}, {slot: 23, desc: 'mechanic.front-wheels', products: 100}, 
	{slot: 24, desc: 'mechanic.back-wheels', products: 100}, {slot: 25, desc: 'mechanic.plaque', products: 100}, {slot: 27, desc: 'mechanic.trim-design', products: 800},
	{slot: 28, desc: 'mechanic.ornaments', products: 150}, {slot: 33, desc: 'mechanic.steering-wheel', products: 100}, {slot: 34, desc: 'mechanic.shift-lever', products: 100}, 
	{slot: 38, desc: 'mechanic.hydraulics', products: 1200}, {slot: 69, desc: 'mechanic.window-tint', products: 200}
];

mp.events.add('showTunningMenu', () => {
	// Obtenemos el vehículo en el que está subido
	let vehicle = mp.players.local.vehicle;
	
	// Inicializamos el array con los grupos
	let componentGroups = [];
	
	for(let i = 0; i < slotsArray.length; i++) {
		// Miramos el número de modificaciones
		let modNumber = vehicle.getNumMods(slotsArray[i].slot);
		
		// Si tiene modificaciones, añadimos la opción al menú
		if(modNumber > 0) {
			// Inicializamos el array de componentes y el grupo
			let group = {'slot': slotsArray[i].slot, 'desc': slotsArray[i].desc, 'products': slotsArray[i].products};
			let components = [];
			
			for(let m = 0; m < modNumber; m++) {
				let component = {'id': m, 'desc': slotsArray[i].desc + " tipo " + (m + 1)};
				components.push(component);
			}
			
			// Añadimos la lista de componentes a la lista
			group.components = components;
			componentGroups.push(group);
		}
	}
	
	// Show the tunning menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/sideMenu.html', 'populateTunningMenu', JSON.stringify(componentGroups)]);
});

mp.events.add('showRepaintMenu', () => {
	// Disable the HUD
	mp.game.ui.displayHud(false);
	mp.game.ui.displayRadar(false);

	// Show the paint menu
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/repaintVehicle.html']);
});

mp.events.add('addVehicleComponent', (slot, component) => {
	// Añadimos el componente al vehículo
	mp.players.local.vehicle.setMod(slot, component);
});

mp.events.add('repaintVehicle', (colorType, firstColor, secondColor, pearlescentColor, paid) => {
	// Repaint the vehicle
	mp.events.callRemote('repaintVehicle', colorType, firstColor, secondColor, pearlescentColor, paid);
});

mp.events.add('closeRepaintWindow', () => {
	// Enable the HUD
	mp.game.ui.displayHud(true);
	mp.game.ui.displayRadar(true);

	// Destroy the browser
	mp.events.call('destroyBrowser');

	// Restore the vehicle's colors
	mp.events.callRemote('cancelVehicleRepaint');
});