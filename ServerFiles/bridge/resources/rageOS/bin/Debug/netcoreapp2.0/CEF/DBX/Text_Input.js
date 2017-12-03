// General
var dbx_isenabled = false;
var dbx_iscensored = false;
var dbx_isenabled_keylock

// Colors
var background_r = 0;
var background_g = 0;
var background_b = 0;
var background_a = 200;

var title_r = 255;
var title_g = 0;
var title_b = 0;
var title_a = 255;

var text_r = 255;
var text_g = 255;
var text_b = 255;
var text_a = 255;

var inputbox_r = 200;
var inputbox_g = 200;
var inputbox_b = 200;
var inputbox_a = 200;

var txtfield_r = 255;
var txtfield_g = 255;
var txtfield_b = 255;
var txtfield_a = 255;

var buttontext_r = 255;
var buttontext_g = 255;
var buttontext_b = 255;
var buttontext_a = 255;

var buttonbackground_r = 21;
var buttonbackground_g = 213;
var buttonbackground_b = 117;
var buttonbackground_a = 240;

// Holders
var dbx_finishfunction = "";
var dbx_title = "";
var dbx_text = "";
var dbx_buttontext = [];
var dbx_realText = "";
var dbx_censorText = "";
var dbx_error = "";

// Counters
var dbx_button_count = 0;

// Various sizes
var wsize = 700;
var hsize = 0; // will depend on how many buttons

API.onServerEventTrigger.connect(function (eventName, args) {
	if (eventName == "dbx_text_input_colors") {
		switch (args[0]) {
			case 0:
				background_r = args[1];
				background_g = args[2];
				background_b = args[3];
				background_a = args[4];
				break;
			case 1:
				title_r = args[1];
				title_g = args[2];
				title_b = args[3];
				title_a = args[4];
				break;
			case 2:
				text_r = args[1];
				text_g = args[2];
				text_b = args[3];
				text_a = args[4];
				break;
			case 3:
				inputbox_r = args[1];
				inputbox_g = args[2];
				inputbox_b = args[3];
				inputbox_a = args[4];
				break;
			case 4:
				txtfield_r = args[1];
				txtfield_g = args[2];
				txtfield_b = args[3];
				txtfield_a = args[4];
				break;
			case 5:
				buttontext_r = args[1];
				buttontext_g = args[2];
				buttontext_b = args[3];
				buttontext_a = args[4];
				break;
			case 6:
				buttonbackground_r = args[1];
				buttonbackground_g = args[2];
				buttonbackground_b = args[3];
				buttonbackground_a = args[4];
				break;
		}
	}
	else if (eventName == "dbx_text_input_pre") {
		if (!(0 in args)) {
			API.TriggerServerEvent("dbx_log", "[DBX Error] You didn't input any arguments when using 'dbx_text_input'.");
			return;
		}

		dbx_finishfunction = args[0];
		if (dbx_finishfunction == "") {
			API.TriggerServerEvent("dbx_log", "[DBX Error] No return function has been given in 'dbx_text_input'.");
			return;
		}

		dbx_title = args[1];
		dbx_text = args[2];
		dbx_button_count = args[3];


		var lastarg = 0;
		dbx_buttontext = [];
		for (var i = 0; i < dbx_button_count; i++) {
			dbx_buttontext.push(args[4][i]);
			lastarg++;
		}

		if (args[4].Count <= lastarg) {
			dbx_iscensored = false;
		}
		else {
			dbx_iscensored = args[4][lastarg];
			lastarg++;
		}

		if (args[4].Count <= lastarg) {
			dbx_error = "";
		}
		else {
			dbx_error = args[4][lastarg];
		}

		switch (dbx_button_count) {
			case 1:
				hsize = 370;
				break;
			case 2:
				hsize = 420;
				break;
			case 3:
				hsize = 470;
				break;
		}

		API.showCursor(true);
		API.setCanOpenChat(false);
		dbx_isenabled = true;
	}
	else if (eventName == "dbx_text_input") {

		if (!(0 in args)) {
			API.TriggerServerEvent("dbx_log", "[DBX Error] You didn't input any arguments when using 'dbx_text_input'.");
			return;
		}

		dbx_finishfunction = args[0];
		if (dbx_finishfunction == "") {
			API.TriggerServerEvent("dbx_log", "[DBX Error] No return function has been given in 'dbx_text_input'.");
			return;
		}

		dbx_title = args[1];
		dbx_text = args[2];
		dbx_button_count = args[3];

		if (dbx_button_count <= 0) {
			API.TriggerServerEvent("dbx_log", "[DBX Error] 'dbx_button_count' parameter may not be zero or below.");
			return;
		}
		else if (dbx_button_count > 3) {
			API.TriggerServerEvent("dbx_log", "[DBX Error] 'dbx_button_count' parameter may not be above three.");
			return;
		}

		var lastarg = 4;
		dbx_buttontext = [];
		for (var i = 0; i < dbx_button_count; i++) {
			dbx_buttontext.push(args[4 + i]);
			lastarg++;
		}

		if (args.Count <= lastarg) {
			dbx_iscensored = false;
		}
		else {
			dbx_iscensored = args[lastarg];
			lastarg++;
		}

		if (args.Count <= lastarg) {
			dbx_error = "";
		}
		else {
			dbx_error = args[lastarg];
		}
		
		switch (dbx_button_count) {
			case 1:
				hsize = 370;
				break;
			case 2:
				hsize = 420;
				break;
			case 3:
				hsize = 470;
				break;
		}

		API.showCursor(true);
		API.setCanOpenChat(false);
		dbx_isenabled = true;

		dbx_censorText = "";
		dbx_realText = "";
	}
});

API.onUpdate.connect(function () {
	if (dbx_isenabled) {
		var res = API.getScreenResolutionMaintainRatio();
		API.disableAllControlsThisFrame();

		// Background
		API.drawRectangle((res.Width / 2) - (wsize / 2), (res.Height / 2) - (hsize / 2), wsize, hsize, background_r, background_g, background_b, background_a);

		// Title
		API.drawText(dbx_title, (res.Width / 2), (res.Height / 2) - (hsize / 2) + 15, 1, title_r, title_g, title_b, title_a, 1, 1, true, true, 0);

		// Text
		API.drawText(dbx_text, (res.Width / 2), (res.Height / 2) - (hsize / 2) + 100, 0.35, text_r, text_g, text_b, text_a, 0, 1, true, true, wsize - 50);

		// Input-box
		API.drawText(">", (res.Width / 2) - (wsize / 2) + 40, (res.Height / 2) - (hsize / 2) + 204, 0.38, txtfield_r, txtfield_g, txtfield_b, txtfield_a, 6, 0, true, true, 0);

		if (dbx_iscensored) {
			API.drawText(dbx_censorText, (res.Width / 2) - (wsize / 2) + 55, (res.Height / 2) - (hsize / 2) + 210, 0.38, txtfield_r, txtfield_g, txtfield_b, txtfield_a, 6, 0, true, true, 0);
		}
		else {
			API.drawText(dbx_realText, (res.Width / 2) - (wsize / 2) + 55, (res.Height / 2) - (hsize / 2) + 204, 0.38, txtfield_r, txtfield_g, txtfield_b, txtfield_a, 6, 0, true, true, 0);
		}

		API.drawRectangle((res.Width / 2) - (wsize / 2) + 30, (res.Height / 2) - (hsize / 2) + 200, wsize - 60, 40, inputbox_r, inputbox_g, inputbox_b, inputbox_a);

		if (dbx_error.length != 0) {
			API.drawText(dbx_error, (res.Width / 2), (res.Height / 2) - (hsize / 2) + 245, 0.3, 255, 0, 0, 255, 0, 1, true, true, 0);
		}

		// Buttons
		for (var i = 0; i < dbx_button_count; i++) {
			API.drawRectangle((res.Width / 2) - (wsize / 2) + 30, (res.Height / 2) - (hsize / 2) + 300 + (i * 50), wsize - 60, 40, buttonbackground_r, buttonbackground_g, buttonbackground_b, buttonbackground_a);
			API.drawText(dbx_buttontext[i], (res.Width / 2), (res.Height / 2) - (hsize / 2) + 300 + (i * 50), 0.5, buttontext_r, buttontext_g, buttontext_b, buttontext_a, 6, 1, true, true, 0);
		}

		// Button clicking
		if (API.isControlJustPressed(24)) {
			for (var i = 0; i < dbx_button_count; i++) {
				var pf = API.getCursorPositionMaintainRatio();

				var wmin = (res.Width / 2) - (wsize / 2) + 30;
				var hmin = (res.Height / 2) - (hsize / 2) + 300 + (i * 50);
				var wmax = wmin + (wsize - 60);
				var hmax = hmin + 40;

				if (pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) {
					dbx_isenabled = false;
					API.showCursor(false);
					API.setCanOpenChat(true);
					API.TriggerServerEvent(dbx_finishfunction, i, dbx_realText);
					break;
				}
			}
		}
	}
});

API.onKeyDown.connect(function (sender, e) {
	if (!dbx_isenabled_keylock) {
		switch (e.KeyCode) {
			case Keys.Back:
				if (dbx_realText.length != 0) {
					dbx_realText = dbx_realText.substring(0, dbx_realText.length - 1);
					dbx_censorText = dbx_censorText.substring(0, dbx_censorText.length - 1);
				}
				break;
			case Keys.Enter:
			case Keys.Alt:
			case Keys.Control:
			case Keys.Shift:
				// Had to add otherwise it would go into default
				break;
			default:
				if (dbx_iscensored) {
					dbx_censorText += "*";
					dbx_realText += API.getCharFromKey(e.KeyValue, e.Shift, e.Control, e.Alt);
				}
				else {
					dbx_realText += API.getCharFromKey(e.KeyValue, e.Shift, e.Control, e.Alt);
				}
				break;
		}
		dbx_isenabled_keylock = true;
		API.after(50, "dbx_EnableKeys");
	}
});

API.onKeyUp.connect(function (sender, e) {
	dbx_isenabled_keylock = false;
});

function dbx_EnableKeys() {
	dbx_isenabled_keylock = false;
}
