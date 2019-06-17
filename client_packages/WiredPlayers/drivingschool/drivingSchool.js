let licenseBlip = undefined;
let questionsArray = [];
let answersArray = [];

mp.events.add('startLicenseExam', (questionsJson, answersJson) => {
	// Get the exam questions and answers
	questionsArray = JSON.parse(questionsJson);
	answersArray = JSON.parse(answersJson);

	// Disable the chat
	mp.gui.chat.activate(false);
	mp.gui.chat.show(false);

	// Show the question
	mp.events.call('createBrowser', ['package://WiredPlayers/statics/html/licenseExam.html', 'getFirstTestQuestion']);
});

mp.events.add('getNextTestQuestion', () => {
	// Get the current question number
	let index = mp.players.local.getVariable('PLAYER_LICENSE_QUESTION');

	// Load the question and initialize the answers
	let questionText = questionsArray[index].text;
	let answers = [];

	for(let i = 0; i < answersArray.length; i++) {
		// Check if the answer and question match
		if(answersArray[i].question == questionsArray[index].id) {
			answers.push(answersArray[i]);
		}
	}

	// Show the question into the browser
	let answersJson = JSON.stringify(answers);
	mp.events.call('executeFunction', ['populateQuestionAnswers', questionText, answersJson]);
});

mp.events.add('submitAnswer', (answerId) => {
	// Check if the answer is correct
	mp.events.callRemote('checkAnswer', answerId);
});

mp.events.add('finishLicenseExam', () => {
	// Enable the chat
	mp.gui.chat.activate(true);
	mp.gui.chat.show(true);

	// Destroy the exam's window
	mp.events.call('destroyBrowser');
});

mp.events.add('showLicenseCheckpoint', (position) => {
	if(licenseBlip === undefined) {
		// Create a blip on the map
		licenseBlip = mp.blips.new(1, position, {color: 1});
	} else {
		// Update blip's position
		licenseBlip.setCoords(position);
	}
});

mp.events.add('deleteLicenseCheckpoint', () => {
	// Destroy the blip on the map
	licenseBlip.destroy();
	licenseBlip = undefined;
});
