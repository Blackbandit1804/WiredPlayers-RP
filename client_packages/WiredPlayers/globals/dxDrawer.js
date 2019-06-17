let animActions = ["/sentarse", "/fumar", "/tumbarse", "/animar", "/bailar", "/aplaudir", "/calentarmanos", "/andar", "/rendirse", "/bailesexy",
    "/apuntar", "/saludar", "/comer", "/beber", "/striptease", "/llamarpuerta", "/teclear", "/esconderse", "/ducharse", "/limpiar", "/llorar",
    "/cavar", "/hablar", "/paz", "/burla", "/flipar", "/loco", "/facepalm", "/mear", "/recogiendo"];

let animPoses = ["/brazos", "/pose", "/telefono", "/apoyarse", "/pensativo", "/muerto", "/malherido", "/mecanico", "/arrodillarse"];

let animMisc = ["/borracha", "/indiferencia", "/nervios", "/rock", "/contoneo", "/tefo", "/golpeado", "/nudillos", "/beso", "/rcp", "/sexocoche",
    "/cortemangas", "/graffiti", "/deporte", "/venaqui", "/dj"];

//array comandos lspd
let lspdcommands = ["/esposar", "/cachear", "/inculpar", "/multar", "/equipo", "/control", "/poner", "/quitar", "/comprobar", "/refuerzos", "/sr", "/(m)egafono", "/borraraviso"];

//descripciones para el panel de ayuda
let hookerdesc = "Los Santos es una ciudad movida por el dinero, la fama, el éxito... y la lujuria.\n" +
    "Esto da lugar a uun elevado índice de prostitución así como problemas de salud, si estás tan\n" +
    "desesperado que no s no sabes que hacer para poder llevarte algo a la boca esta es tu profesión.\n" +
    "Quien sabe que tipos de cclientes podrás conocer...";

let thiefdesc = "En esta ciudad todos intentan sobrevivir como pueden, algunos se matan a trabajar para llegar\n" +
    "a fin de mes y otros matan y roban para sobrevivir y por que no... hacerse un nombre en el\n" +
    "mundo del hampa y llegar lejos. Quienes se ganan la vida delinquiendo tienen una baja\n" +
    "esperanza de vida dada la efectividad del cuerpo de policía de Los Santos.";

let delivererdesc = "¿Eres un estudiante con una deuda universitaria enorme? ¿O quizás un cuarentón solitario\n" +
    "sin aspirapiraciones? ¡Da igual! En Burger Shot nos vale cualquiera que sepa encender la parrilla,\n" +
    "dar la vu elta a las hamburguesas y se conforme con un sueldo mínimo. Además a las chicas les\n" +
    "gustan las moottos y los uniformes. ¿Qué más podrías pedir? ¡Burger Shot es tu sitio!";

let mechdesc = "Los Santos es la ciudad del motor de 2017. Deportivos, superdeportivos, bólidos e incluso\n" +
    "antiguayaas convertidas en auténticas máquinas. No existe vehículo que se resista a los encantos\n" +
    "de un mecáncánico de Los Santos... Alerones, motores trucados y todo tipo de modificaciones que se\n" +
    "te ocurran . Ahora bien... que si solo quieres que te arreglen ese golpe de la puerta, también vale.";

let mechnote = "El esquema numérico de las ruedas para un coche se corresponde tal que:\n" +
    "Delantera izquierda(0), de lantera derecha(1), trasera izquierda(4), trasera derecha(5)\n" +
    "En motos es: Rueda delantera(2) y rueda trasera(3)";

let trashdesc = "Los Santos es una preciosidad, es una joya de la costa oeste y tiene que lucir como tal, ahí es\n" +
    "do nde entra a tomar protagonismo el infravalorado equipo de recogida de residuos cuya labor\n" +
    "es ni má s ni menos que dejar la ciudad como una patena, sobre todo la zona norte donde la imagen\n" +
    "lo es todo. Un oficio bien pagado. ¡Y hasta tendrás un compañero de trabajo!";

let lspddesc = "Esta es una ciudad segura gracias a valerosos hombres y mujeres que arriesgan su vida a\n" +
    "diario por un salario cuestionable. El honorable cuerpo de policía de Los Santos cuenta con una\n" +
    "gran cantidad  de recursos para llevar a cabo su deber y mantener esta ciudad a salvo del auténtico\n" +
    "problema que  la asola, la delincuencia. Siempre dispuestos a proteger y servir.";

let medicos = "El departamento de emergencias de Los Santos coordina tanto el prestigioso cuerpo de bomberos\n" +
    "como el departamento médico. Si has sufrido un accidente o has sido una víctima de agresión,\n" +
    "no debes    preocuparte pues recibirás la mejor atención médica. Pero... si no tienes seguro médico\n" +
    "quizás      es mejor no sobrevivir al accidente que hayas tenido.";

let weazeldesc = "¿Hay algo que no ocurra en esta ciudad? ¡No! ¿Y algo que se le escape a los ávidos reporteros\n" +
    "     de Weazel News? ¡Tampoco! Siempre en busca de poner la verdad y los hechos objetivos en\n" +
    "conocimien to del ciudadano de a pie. Además ¿qué mejor manera hay de ganar fama en Los Santos\n" +
    "que siendo l   a voz pública? ¡La prensa es la artillería de la verdad!";

let docdesc = "Lo primero que necesitarás es poner tus papeles en regla, para eso debes dirigirte al\n" +
    "ayuntamiento  y una vez allí podrás cumplimentar varios trámites, debería ser tu primera visita\n" +
    "en la ciudad.      Aunque siempre puedes intentar vivir al margen de la ley siendo un indocumentado\n" +
    "pero con sus consecuencias.";

let jobdesc = "Nos guste o no vivimos en una sociedad capitalista, si quieres disfrutar de\n" +
    "las comodidades que se te ofrecen tendrás que ganártelas, y eso implica trabajar. Para\n" +
    "ello tienes diferentes opciones q  ue puedes elegir. A no ser que seas de esos que prefiere\n" +
    "hacer dinero fácil... en esta ciudad hay  de  todo.";

let vehdesc = "Lo primero que necesitarás es sacarte el carnet de conducir, a no ser que quieras\n" +
    "tener problemas  con la ley claro. Para ello puedes acudir a la autoescuela de Los Santos\n" +
    "donde tendrás que aprobar  un examen teórico y otro práctico.";

let carshopdesc = "Para conseguir un vehículo puedes optar por el concesionario de motos\n" +
    "o por el de coches. Tendrás    una amplia gama para elegir, y por supuesto podrás elegir\n" +
    "el color mientras consultas el catálo  go."

var cellphone = "En una ciudad tan grande necesitarás estar en contacto con la gente así\n" +
    "como tener fácil y rápi    do acceso a todos los servicios que se te ofrecen y para ello\n" +
    "¿qué mejor que un smartphone? Acude   a cualquier tienda de electrónica para conseguir uno."

let discodesc = "Hay multitud de sitios en los que podrás relajarte y socializar con el resto\n" +
    "de gente en Los Santo s. Las opciones más famosas son el club de striptease Vanilla Unicorn\n" +
    "en Strawberry y el club nocturno Bahama Mamas en Del Perro."

let bankdesc = "Hay mas de 100 cajeros repartidos por toda la ciudad y las afueras, gracias a ellos podras\n" +
    "acceder comodamente a tu dinero, asi como realizar diversos movimientos como transferencias,\n" +
    "ingresos, retirar dinero y consultar el balance. Basta con que te acerques a un cajero y pulses la\n" +
    "la tecla F."

let buydesc = "Hay multitud de tiendas por toda la ciudad. Puedes encontrar facilmente las mas comunes\n" +
    "pero eso no quiere decir que no haya más repartidas a lo largo de la ciudad. Tiendas de ropa,\n" +
    "ferreterias, supermercados y tiendas especializadas en electronica. Puedes acceder a cualquiera\n" +
    "de ellas y ver sus productos usando el comando /comprar una vez estes dentro.";

let inventorydesc = "Cuando compres o cojas algo, primero aparecera en tu mano utiliza entonces /guardar para\n" +
    "almacenarlo en tu inventario desde el cual podras acceder de manera rapida y comoda a todas tus\n" +
    "pertenencias. Usa /inventario y haz clic en el objeto que quieras para ver las opciones disponibles."

let cellsPerRow = 10;
let optionCells = 3;
let resolution = null;
let target = null;
let inventory = [];
let inventoryOptions = [];
let helpWelcome = 0;
let action = 0;
let money = 0;
let moneySize = 0;

// Variables del vehículo
let distance = 0.0;
let consumed = 0.0;
let vehicleKms = 0.0;
let vehicleGas = 0.0;
let perMeter = 0.00065;
let lastPosition = undefined;

// Variables de pesca
let fishingState = 0;
let fishingSuccess = 0;
let fishingBarPosition = 0;
let fishingBarMin = 0;
let fishingBarMax = 0;
let movementRight = true;
let fishingAchieveStart = 0;

// Estado de esposas
let handcuffed = false;

// Comprobación de actualización
let checkFlagEvery = 500;
let lastTimeFlagChecked = new Date().getTime();

mp.events.add('guiReady', () => {
	// Obtenemos la resolución
	resolution = mp.game.graphics.getScreenActiveResolution(1, 1);

    // Creamos las opciones de inventario
    for (let i = 0; i < optionCells; i++) {
        let option = {};
        option.startingX = parseInt(resolution.x / 2.0 - 505.0 + i * 175.0 + 10.0);
        option.startingY = parseInt(resolution.y / 2.0 + 305.0);
        option.action = '';
        inventoryOptions.push(option);
    }

	// Calculamos el máximo y mínimo de la barra
	fishingBarMin = resolution.x - 425.0;
	fishingBarMax = resolution.x - 27.0;
});

mp.events.add('helptext', () => {
	// Mostramos el texto de ayuda
	mp.gui.cursor.show(true, true);
	mp.gui.chat.activate(false);
	mp.gui.chat.show(false);
	helpWelcome = 1;
});

mp.events.add('welcomeHelp', () => {
	// Mostramos el texto de bienvenida
	mp.gui.cursor.show(true, true);
	mp.gui.chat.activate(false);
    mp.gui.chat.show(false);
	helpWelcome = 2;
});

mp.events.add('startPlayerFishing', () => {
	// Iniciamos la pesca
	fishingState = 1;
});

mp.events.add('fishingBaitTaken', () => {
	// Iniciamos el minijuego
	fishingAchieveStart = Math.random() * 390.0 + fishingBarMin;
	fishingSuccess = 0;
	fishingState = 3;
});

mp.events.add('initializeSpeedometer', (kms, gas, engine) => {
    // Get the player's vehicle
    let vehicle = mp.players.local.vehicle;

    // Don't let the player turn the engine on if by default
    vehicle.setEngineOn(engine, true, true);

	// Initialize the kilometers and gas
	vehicleKms = parseFloat(kms);
	vehicleGas = parseFloat(gas);

	// Initialize the counters
	lastPosition = vehicle.position;
	distance = 0.0;
	consumed = 0.0;
});

mp.events.add('resetSpeedometer', (vehicle) => {
    if(lastPosition !== undefined) {
        // Reset the vehicle's position
        lastPosition = undefined;

        // Save the kilometers and gas
        mp.events.callRemote('saveVehicleConsumes', vehicle, vehicleKms, vehicleGas);
    }
});

mp.events.add('toggleHandcuffed', (toggle) => {
	// Cambiamos el estado de esposado
	handcuffed = toggle;
});

mp.events.add('render', () => {
	// Comprobamos que se haya inicializado todo
	if(resolution != null) {
		// Obtenemos el jugador local
		let player = mp.players.local;

		// Obtenemos el tiempo local
		let currentTime = new Date().getTime();
		/*
		if(helpWelcome == 1) {
			// Mostramos el menú de ayuda
			drawHelpMenu();
		} else if(helpWelcome == 2) {
			// Mostramos el menú de bienvenida
			drawWelcomeMenu();
		}*/

		if(fishingState > 0) {
			// Cargamos la barra de pesca
			drawFishingMinigame();
		}

		if(player.vehicle) {
			// Actualizamos el velocímetro
			updateSpeedometer(player);
		}

		// Comprobamos si hay que actualizar el dinero
		if (currentTime - lastTimeFlagChecked > checkFlagEvery) {
			money = player.getVariable('PLAYER_MONEY') || 0;
			moneySize = money.toString().length + 1;
			lastTimeFlagChecked = currentTime;
		}

		// Dibujamos el dinero del jugador
		mp.game.graphics.drawText(`${money}$`, [0.99 - moneySize * 0.005, 0.05], {font: 7, color: [30, 150, 0, 255], scale: [0.5, 0.5], outline: true});

		// Miramos si está esposado
		if(handcuffed) {
			mp.game.controls.disableControlAction(2, 12, true);
			mp.game.controls.disableControlAction(2, 13, true);
			mp.game.controls.disableControlAction(2, 14, true);
			mp.game.controls.disableControlAction(2, 15, true);
			mp.game.controls.disableControlAction(2, 16, true);
			mp.game.controls.disableControlAction(2, 17, true);
			mp.game.controls.disableControlAction(2, 22, true);
			mp.game.controls.disableControlAction(2, 24, true);
			mp.game.controls.disableControlAction(2, 25, true);
		}
	}
});
/*

function drawHelpMenu() {
	// Obtenemos la posición del cursor
	let cursorPosition = NAPI.GetCursorPositionMaintainRatio();

	// Miramos si se ha hecho click
	let mouseClick = NAPI.IsControlJustPressed(24);

    if (isPositionInArea(cursorPosition, resolution.x / 2.0 + 478.0, resolution.y / 2.0 - 377.0, 24.0, 24.0) && mouseClick) {
        NAPI.SetCanOpenChat(true);
        NAPI.ShowCursor(false);
        NAPI.SetChatVisible(true);
		helpWelcome = 0;
        action = 0;
		return;
    }

    if (isPositionInArea(cursorPosition, resolution.x / 2.0 + 450.0, resolution.y / 2.0 - 85.0, 64.0, 64.0) && mouseClick && action != 1) {
        action++;
    } else if (isPositionInArea(cursorPosition, resolution.x / 2.0 - 505.0, resolution.y / 2.0 - 85.0, 64.0, 64.0) && mouseClick && action !=0) {
        action--;
    }

    // Cargamos la cabecera
    mp.game.graphics.drawRect(resolution.x / 2.0 - 505.0, resolution.y / 2.0 - 380.0, 1010.0, 30.0, 43, 32, 32, 245);

    // Cargamos el pie de pagina
    mp.game.graphics.drawRect(resolution.x / 2.0 - 505.0, resolution.y / 2.0 + 300.0, 1010.0, 50.0, 43, 32, 32, 245);

    // Cargamos la cruz para cerrar el menú
    NAPI.DxDrawTexture("statics/img/close.png", new Point(parseInt(resolution.x / 2.0 + 478.0), parseInt(resolution.y / 2.0 - 377.0)), new Size(24, 24), 0.0);

    // Cargamos el fondo
    mp.game.graphics.drawRect(resolution.x / 2.0 - 505.0, resolution.y / 2.0 - 350.0, 1010.0, 700.0, 43, 32, 32, 245);

    // Título centrado
    NAPI.DrawText("Lista de Animaciones", resolution.x / 2.0 - 0.0, resolution.y / 2.0 - 350.0, 0.7, 255, 255, 0, 255, 1, 1, false, false, 0);

    switch (action)
    {
        case 0:
			// Flecha de siguiente
            NAPI.DxDrawTexture("statics/img/nextwhite.png", new Point(parseInt(resolution.x / 2.0 + 450.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

            NAPI.DrawText("Acciones del personaje", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 280.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);

            var animRow = "";
            var currentRow = 0;

            //recorremos el array de animaciones
            for (var i = 0; i < animActions.length; i++) {
                animRow += animActions[i] + " ";
                //cellsPerRow es cada cuantos items hay un salto de linea donde +30 es la distancia entre lineas
                if ((i + 1) % cellsPerRow == 0) {
                    NAPI.DrawText(animRow, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 240.0 + (30.0 * currentRow), 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
                    currentRow++;
                    animRow = "";
                }
            }
            //si cada 10 items hay salto de linea, este if contempla los casos restantes
            if (animActions.length % cellsPerRow != 0) {
                NAPI.DrawText(animRow, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 240.0 + (30.0 * currentRow), 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
                currentRow++;
                animRow = "";
            }

            NAPI.DrawText("Poses del personaje", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 150.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);

            var animIdles = "";
            var currentRowIdles = 0;

            //recorremos el array de animaciones
            for (var i = 0; i < animPoses.length; i++) {
                animIdles += animPoses[i] + " ";
                //cellsPerRow es cada cuantos items hay un salto de linea donde +30 es la distancia entre lineas
                if ((i + 1) % cellsPerRow == 0) {
                    NAPI.DrawText(animIdles, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 200.0 + (30.0 * currentRow), 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
                    currentRow++;
                    animIdles = "";
                }
            }
            //si cada 10 items hay salto de linea, este if contempla los casos restantes
            if (animPoses.length % cellsPerRow != 0) {
                NAPI.DrawText(animIdles, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 200.0 + (30.0 * currentRow), 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
                currentRow++;
                animIdles = "";
            }

            NAPI.DrawText("Animaciones miscelánea", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 70.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);

            var animMisce = "";
            var currentRowIdles = 0;

            //recorremos el array de animaciones
            for (var i = 0; i < animMisc.length; i++) {
                animMisce += animMisc[i] + " ";
                //cellsPerRow es cada cuantos items hay un salto de linea donde +30 es la distancia entre lineas
                if ((i + 1) % cellsPerRow == 0) {
                    NAPI.DrawText(animMisce, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 150.0 + (30.0 * currentRow), 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
                    currentRow++;
                    animMisce = "";
                }
            }
            //si cada 10 items hay salto de linea, este if contempla los casos restantes
            if (animMisc.length % cellsPerRow != 0) {
                NAPI.DrawText(animMisce, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 150.0 + (30.0 * currentRow), 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
                currentRow++;
                animMisce = "";
            }

            NAPI.DrawText("Pulsando la E si en algún momento quieres detener la animación.", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -30.0, 0.4, 0, 255, 255, 255, 6, 0, false, true, 0);

            NAPI.DrawText("Comandos Básicos de Vehículo", resolution.x / 2.0 - 0.0, resolution.y / 2.0 - -80.0, 0.7, 255, 255, 0, 255, 1, 1, false, false, 0);

            NAPI.DrawText("/bloqueo /cinturon /capo /maletero /localizar /repostar /desaparcar /desguace /vender /lavar", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -130.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);

            break;
        case 1:
			// Flecha de atrás
            NAPI.DxDrawTexture("statics/img/backwhite.png", new Point(parseInt(resolution.x / 2.0 - 505.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

            NAPI.DrawText("Teléfono", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 300.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText("/llamar /contestar /sms /agenda /colgar", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 270.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);

            NAPI.DrawText("Chat", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 225.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText("/decir /(gr)itar /do /me /su /(sus)urrar /ooc /ame /mp /duda /anunciar", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 195.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);

            NAPI.DrawText("Personaje", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 150.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText("/inventario /guardar /consumir /comprar /mostrar /pagar /aceptar /cancelar /recoger /jugador /puerta /alquilar /alquilable /armario", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 120.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DrawText("Poner /alquilable sin ningun valor eliminara el alquiler de la casa", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 100.0, 0.4, 0, 255, 255, 255, 6, 0, false, true, 0);

            NAPI.DrawText("Comandos para las Facciones Estatales", resolution.x / 2.0 - 0.0, resolution.y / 2.0 - 55.0, 0.7, 255, 255, 0, 255, 1, 1, false, false, 0);

            NAPI.DrawText("Comandos comunes de facciones(radios y canal de facción)", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -5.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText("/f /r /dp /de /fr /reclutar /expulsar /rango", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -35.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);

            NAPI.DrawText("Departamento de policía de Los Santos (LSPD)", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -75.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            var policeRow = "";
            var currentPoliceRow = 0;

            for (var i = 0; i < lspdcommands.length; i++) {
                policeRow += lspdcommands[i] + " ";
                //cellsPerRow es cada cuantos items hay un salto de linea donde +30 es la distancia entre lineas
                if ((i + 1) % cellsPerRow == 0) {
                    NAPI.DrawText(policeRow, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -105.0 + (30.0 * currentPoliceRow), 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
                    currentPoliceRow++;
                    policeRow = "";
                }
            }
            //si cada 10 items hay salto de linea, este if contempla los casos restantes
            if (lspdcommands.length % cellsPerRow != 0) {
                NAPI.DrawText(policeRow, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -105.0 + (30.0 * currentPoliceRow), 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
                currentPoliceRow++;
                policeRow = "";
            }

            NAPI.DrawText("Departamento de Emergencias de Los Santos (EMS)", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -165.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText("/curar /extraer /reanimar", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -195.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);

            NAPI.DrawText("Weazel News", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -235.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText("/n /entrevistar /premiar", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -265.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);

            break;
    }
}

function drawWelcomeMenu() {
	// Obtenemos la posición del cursor
	let cursorPosition = NAPI.GetCursorPositionMaintainRatio();

	// Miramos si se ha hecho click
	let mouseClick = NAPI.IsControlJustPressed(24);

    if (isPositionInArea(cursorPosition, resolution.x / 2.0 + 478.0, resolution.y / 2.0 - 377.0, 24.0, 24.0) && mouseClick) {
        NAPI.SetCanOpenChat(true);
        NAPI.ShowCursor(false);
        NAPI.SetChatVisible(true);
		helpWelcome = 0;
        action = 0;
		return;
    }

    if (isPositionInArea(cursorPosition, resolution.x / 2.0 + 450.0, resolution.y / 2.0 - 85.0, 64.0, 64.0) && mouseClick && action != 5) {
        action++;
    } else if (isPositionInArea(cursorPosition, resolution.x / 2.0 - 505.0, resolution.y / 2.0 - 85.0, 64.0, 64.0) && mouseClick && action != 0) {
        action--;
    }

    // Cargamos la cabecera
    mp.game.graphics.drawRect(resolution.x / 2.0 - 505.0, resolution.y / 2.0 - 380.0, 1010.0, 30.0, 43, 32, 32, 245);

    // Cargamos el pie de pagina
    mp.game.graphics.drawRect(resolution.x / 2.0 - 505.0, resolution.y / 2.0 + 300.0, 1010.0, 50.0, 43, 32, 32, 245);

    // Cargamos la cruz para cerrar el menú
    NAPI.DxDrawTexture("statics/img/close.png", new Point(parseInt(resolution.x / 2.0 + 478.0), parseInt(resolution.y / 2.0 - 377.0)), new Size(24, 24), 0.0);

    // Cargamos el fondo
    mp.game.graphics.drawRect(resolution.x / 2.0 - 505.0, resolution.y / 2.0 - 350.0, 1010.0, 700.0, 43, 32, 32, 245);

    // Título centrado
    NAPI.DrawText("Tus primeros pasos en Los Santos", resolution.x / 2.0 - 0.0, resolution.y / 2.0 - 360.0, 0.7, 255, 255, 0, 255, 1, 1, false, false, 0);

    switch (action) {
        case 0:
			// Flecha de siguiente
			NAPI.DxDrawTexture("statics/img/nextwhite.png", new Point(parseInt(resolution.x / 2.0 + 450.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

            // Descripción rolera del job
            NAPI.DrawText("Papeles en regla", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 310.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(docdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 275.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/townhalldesc.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 300.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Empieza a ganarte la vida", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 110.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(jobdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 75.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/jobdesc.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 100.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Si quieres conducir...", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -90.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(vehdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -125.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/drivingsch.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - -100.0)), new Size(249, 152), 0.0);
            break;
        case 1:
			// Flecha de atrás
            NAPI.DxDrawTexture("statics/img/backwhite.png", new Point(parseInt(resolution.x / 2.0 - 505.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

			// Flecha de siguiente
            NAPI.DxDrawTexture("statics/img/nextwhite.png", new Point(parseInt(resolution.x / 2.0 + 450.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

            // Descripción rolera del job
            NAPI.DrawText("Consigue un vehículo", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 310.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(carshopdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 275.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/carshop.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 300.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("No pierdas el contacto", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 110.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(cellphone, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 75.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/cellphone.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 100.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Si quieres relajarte", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -90.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(discodesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -125.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/bahama.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - -100.0)), new Size(249, 152), 0.0);
            break;
        case 2:
			// Flecha de atrás
            NAPI.DxDrawTexture("statics/img/backwhite.png", new Point(parseInt(resolution.x / 2.0 - 505.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

			// Flecha de siguiente
            NAPI.DxDrawTexture("statics/img/nextwhite.png", new Point(parseInt(resolution.x / 2.0 + 450.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

            // Descripción rolera del job
            NAPI.DrawText("Accede a tu dinero comodamente", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 310.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(bankdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 275.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/bank.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 300.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Vete de compras", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 110.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(buydesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 75.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/electronics.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 100.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Accede a tus pertenencias", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -90.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(inventorydesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -125.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/inventory.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - -100.0)), new Size(249, 152), 0.0);
            break;
        case 3:
			// Flecha de atrás
            NAPI.DxDrawTexture("statics/img/backwhite.png", new Point(parseInt(resolution.x / 2.0 - 505.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

			// Flecha de siguiente
            NAPI.DxDrawTexture("statics/img/nextwhite.png", new Point(parseInt(resolution.x / 2.0 + 450.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

            // Descripción rolera del job
            NAPI.DrawText("Prostitución", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 310.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(hookerdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 275.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/resizedhooker.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 300.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Ladrón", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 110.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(thiefdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 75.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/resizedthief.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 100.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Repartidor", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -90.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(delivererdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -125.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/resizedbshot.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - -100.0)), new Size(249, 152), 0.0);
            break;
        case 4:
			// Flecha de atrás
            NAPI.DxDrawTexture("statics/img/backwhite.png", new Point(parseInt(resolution.x / 2.0 - 505.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

			// Flecha de siguiente
            NAPI.DxDrawTexture("statics/img/nextwhite.png", new Point(parseInt(resolution.x / 2.0 + 450.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

            // Descripción rolera del job
            NAPI.DrawText("Mecánico de Los Santos Custom", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 310.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(mechdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 275.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/resizedmech.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 300.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Basurero", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 110.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(trashdesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 75.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/resizedtrash.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 100.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Comandos de Trabajo", resolution.x / 2.0 - 0.0, resolution.y / 2.0 - -65.0, 0.7, 255, 255, 0, 255, 1, 1, false, false, 0);
            NAPI.DrawText("Basurero", resolution.x / 2.0 - 350.0, resolution.y / 2.0 - -115.0, 0.5, 0, 204, 0, 255, 1, 1, false, false, 0);
            NAPI.DrawText("/basurero", resolution.x / 2.0 - 350.0, resolution.y / 2.0 - -140.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);

            NAPI.DrawText("Repartidor", resolution.x / 2.0 - 200.0, resolution.y / 2.0 - -115.0, 0.5, 0, 204, 0, 255, 1, 1, false, false, 0);
            NAPI.DrawText("/pedidos", resolution.x / 2.0 - 200.0, resolution.y / 2.0 - -140.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);

            NAPI.DrawText("Mecánico", resolution.x / 2.0 - 50.0, resolution.y / 2.0 - -115.0, 0.5, 0, 204, 0, 255, 1, 1, false, false, 0);
            NAPI.DrawText("/reparar", resolution.x / 2.0 - 50.0, resolution.y / 2.0 - -140.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);
            NAPI.DrawText("/cambiorueda", resolution.x / 2.0 - 50.0, resolution.y / 2.0 - -165.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);
            NAPI.DrawText("/tunning", resolution.x / 2.0 - 50.0, resolution.y / 2.0 - -190.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);

            NAPI.DrawText("Ladrón", resolution.x / 2.0 - -100.0, resolution.y / 2.0 - -115.0, 0.5, 0, 204, 0, 255, 1, 1, false, false, 0);
            NAPI.DrawText("/puente", resolution.x / 2.0 - -100.0, resolution.y / 2.0 - -140.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);
            NAPI.DrawText("/forzar", resolution.x / 2.0 - -100.0, resolution.y / 2.0 - -165.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);
            NAPI.DrawText("/robar", resolution.x / 2.0 - -100.0, resolution.y / 2.0 - -190.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);
            NAPI.DrawText("/empeñar", resolution.x / 2.0 - -100.0, resolution.y / 2.0 - -215.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);

            NAPI.DrawText("Prostitución", resolution.x / 2.0 - -250.0, resolution.y / 2.0 - -115.0, 0.5, 0, 204, 0, 255, 1, 1, false, false, 0);
            NAPI.DrawText("/servicio", resolution.x / 2.0 - -250.0, resolution.y / 2.0 - -140.0, 0.4, 255, 255, 255, 255, 4, 1, false, false, 0);

            NAPI.DrawText(mechnote, resolution.x / 2.0 - 0.0, resolution.y / 2.0 - -250.0, 0.35, 0, 255, 255, 255, 6, 1, false, false, 0);
            break;
        case 5:
			// Flecha de atrás
            NAPI.DxDrawTexture("statics/img/backwhite.png", new Point(parseInt(resolution.x / 2.0 - 505.0), parseInt(resolution.y / 2.0 - 85.0)), new Size(64, 64), 0.0);

            // Descripción rolera del job
            NAPI.DrawText("Departamento de policía de Los Santos (LSPD)", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 310.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(lspddesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 275.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/resizedlspd.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 300.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Departamento de Emergencias de Los Santos (LSEMS)", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 110.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(medicos, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - 75.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/resizedlsmd.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - 100.0)), new Size(249, 152), 0.0);

            NAPI.DrawText("Weazel News", resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -90.0, 0.5, 0, 204, 0, 255, 1, 0, false, false, 0);
            NAPI.DrawText(weazeldesc, resolution.x / 2.0 - 450.0, resolution.y / 2.0 - -125.0, 0.4, 255, 255, 255, 255, 4, 0, false, false, 0);
            NAPI.DxDrawTexture("statics/img/resizedweazel.png", new Point(parseInt(resolution.x / 2.0 + 210.0), parseInt(resolution.y / 2.0 - -100.0)), new Size(249, 152), 0.0);
            break;
    }
}*/

function drawFishingMinigame() {
	// Miramos si ha hecho click
	if(mp.game.controls.isControlPressed(0, 24) == true) {
		switch(fishingState) {
			case 1:
				// Lanza la caña para pescar
				fishingState = 2;
				fishingBarPosition = resolution.x - 224.0;
				mp.events.callRemote('startFishingTimer');
				break;
			case 2:
				// Ha sacado la caña antes de que piquen
				fishingState = -1;
				mp.events.callRemote('fishingCanceled');
				break;
			case 3:
				if(fishingBarPosition > fishingAchieveStart && fishingBarPosition < fishingAchieveStart + 15.0) {
					// Sumamos una captura válida
					fishingSuccess++;

					if(fishingSuccess == 3) {
						// Ha pescado lo que había
						fishingState = -1;
						mp.events.callRemote('fishingSuccess');
					} else {
						// Generamos de nuevo las barras
						movementRight = true;
						fishingBarPosition = resolution.x - 224.0;
						fishingAchieveStart = Math.random() * 390.0 + fishingBarMin;
					}
				} else {
					// Ha fallado al recoger el pescado
					fishingState = -1;
					mp.events.callRemote('fishingFailed');
				}
				break;
		}

		// No dibujamos nada
		return;
	}

	if(fishingState == 3) {
		// Dibujamos la barra del minijuego
		mp.game.graphics.drawRect(resolution.x - 425.0, resolution.y - 40.0, 400.0, 25.0, 0, 0, 0, 200);

		// Dibujamos la zona de acierto
		mp.game.graphics.drawRect(fishingAchieveStart, resolution.y - 40.0, 10.0, 25.0, 0, 255, 0, 255);

		// Dibujamos la barra movible
		mp.game.graphics.drawRect(fishingBarPosition, resolution.y - 41.0, 2.0, 26.0, 255, 255, 255, 255);

		// Actualizamos la posición de la barra
		if(movementRight) {
			// Sumamos unidades
			fishingBarPosition += 1.0;

			if(fishingBarPosition > fishingBarMax) {
				fishingBarPosition = fishingBarMax;
				movementRight = false;
			}
		} else {
			// Restamos unidades
			fishingBarPosition -= 1.0;

			if(fishingBarPosition < fishingBarMin) {
				fishingBarPosition = fishingBarMin;
				movementRight = true;
			}
		}
	}
}

function updateSpeedometer(player) {
	// Obtenemos el modelo del vehículo
	let vehicle = player.vehicle;

	// Miramos que no sea una bicicleta
	if(lastPosition !== undefined) {
		// Obtenemos la posición del vehículo
		let currentPosition = vehicle.position;

		// Calculamos datos del velocímetro
		let velocity = vehicle.getVelocity();
		let health = vehicle.getHealth();
		let maxHealth = vehicle.getMaxHealth();
		let healthPercent = Math.floor((health / maxHealth) * 100);
		let speed = Math.round(Math.sqrt(velocity.x * velocity.x + velocity.y * velocity.y + velocity.z * velocity.z) * 3.6);

		// Calculamos la distancia y consumo
		distance = distanceTo(currentPosition, lastPosition);
		consumed = distance * perMeter;
		lastPosition = currentPosition;

		if(vehicleGas - consumed <= 0.0) {
			mp.events.callRemote('stopPlayerCar');
			consumed = 0.0;
		}

		// Obtenemos la gasolina y kms totales
		let totalKms = Math.round((vehicleKms + distance) / 10) / 100 + " km";
		let totalGas = Math.round((vehicleGas - consumed) * 100) / 100 + " litros";

		// Mostramos los indicadores del vehículo
		mp.game.graphics.drawText(`Combustible:`, [0.9075, 0.7], {font: 4, color: [255, 255, 255, 255], scale: [0.5, 0.5], outline: true});
		mp.game.graphics.drawText(`${totalGas}`, [0.9595, 0.7], {font: 4, color: [255, 255, 255, 255], scale: [0.5, 0.5], outline: true});
		mp.game.graphics.drawText(`Kilometraje:`, [0.9075, 0.75], {font: 4, color: [255, 255, 255, 255], scale: [0.5, 0.5], outline: true});
		mp.game.graphics.drawText(`${totalKms}`, [0.9595, 0.75], {font: 4, color: [255, 255, 255, 255], scale: [0.5, 0.5], outline: true});
		mp.game.graphics.drawText(`Kmph:`, [0.9595, 0.825], {font: 4, color: [255, 255, 255, 255], scale: [0.5, 0.5], outline: true});
		mp.game.graphics.drawText(`${speed}`, [0.9179, 0.815], {font: 4, color: [255, 255, 255, 255], scale: [0.75, 0.75], outline: true});
		mp.game.graphics.drawText(`Integridad:`, [0.9335, 0.7917], {font: 4, color: [255, 255, 255, 255], scale: [0.5, 0.5], outline: true});
		if (healthPercent < 30) {
			mp.game.graphics.drawText(`${healthPercent}%`, [0.9335, 0.7917], {font: 4, color: [219, 46, 46, 255], scale: [0.5, 0.5], outline: true});
		} else if (healthPercent < 60) {
			mp.game.graphics.drawText(`${healthPercent}%`, [0.9335, 0.7917], {font: 4, color: [219, 122, 46, 255], scale: [0.5, 0.5], outline: true});
		} else {
			mp.game.graphics.drawText(`${healthPercent}%`, [0.9335, 0.7917], {font: 4, color: [255, 255, 255, 255], scale: [0.5, 0.5], outline: true});
		}

		// Actualizamos los valores del vehículo
		vehicleKms += distance;
		vehicleGas -= consumed;

		// Reinicializamos las variables
		distance = 0.0;
		consumed = 0.0;
	}
}

function distanceTo(vectorFrom, vectorTo) {
	// Obtenemos las distancias cardinales
    let distanceX = vectorFrom.x - vectorTo.x;
    let distanceY = vectorFrom.y - vectorTo.y;
    let distanceZ = vectorFrom.z - vectorTo.z;

    return Math.sqrt(distanceX * distanceX + distanceY * distanceY + distanceZ * distanceZ);
}