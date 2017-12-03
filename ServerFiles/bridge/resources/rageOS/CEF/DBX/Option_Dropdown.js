// General
var dbx_isenabled = false;
var dbx_ishovered = false;
var dbx_isadvancedhovered = false;

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

var scrollarrow_r = 255;
var scrollarrow_g = 0;
var scrollarrow_b = 0;
var scrollarrow_a = 255;

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
var dbx_fieldHolder = 0;
var dbx_title = "";
var dbx_text = "";
var dbx_buttontext = [];
var dbx_fieldtext = [];
var dbx_selectedfield = 0;
var dbx_display_fields = 7;

// Counters
var dbx_button_count = 0;
var dbx_field_count = 0;

// Various sizes
var wsize = 550;
var hsize = 0; // will depend on how many buttons
var bsize = 0; // will depend on how many textfields

API.onServerEventTrigger.connect(function (eventName, args) {
	if (eventName == "dbx_option_dropdown_colors") {
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
				scrollarrow_r = args[1];
				scrollarrow_g = args[2];
				scrollarrow_b = args[3];
				scrollarrow_a = args[4];
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
	else if (eventName == "dbx_option_dropdown_pre") {
		if (!(0 in args)) {
			API.TriggerServerEvent("dbx_log", "[DBX Error] You didn't input any arguments when using 'dbx_option_dropdown'.");
			return;
		}

		dbx_finishfunction = args[0];
		if (dbx_finishfunction == "") {
			API.TriggerServerEvent("dbx_log", "[DBX Error] No return function has been given in 'dbx_option_dropdown'.");
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

		dbx_fieldtext = [];
		dbx_field_count = 0;
		for (var i = lastarg; i < args[4].Count; i++) {
			dbx_fieldtext.push(args[4][i]);
			dbx_field_count++;
		}

		hsize = 555;
		bsize = 237;

		API.ShowCursor(true);
		dbx_isenabled = true;
		dbx_fieldHolder = 0;
		dbx_selectedfield = 0;
	}
	else if (eventName == "dbx_option_dropdown") {
		if (!(0 in args)) {
			API.TriggerServerEvent("dbx_log", "[DBX Error] You didn't input any arguments when using 'dbx_option_dropdown'.");
			return;
		}

		dbx_finishfunction = args[0];
		if (dbx_finishfunction == "") {
			API.TriggerServerEvent("dbx_log", "[DBX Error] No return function has been given in 'dbx_option_dropdown'.");
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

		dbx_fieldtext = [];
		dbx_field_count = 0;
		for (var i = lastarg; i < args.Length; i++) {
			dbx_fieldtext.push(args[i]);
			dbx_field_count++;
		}

		hsize = 555;
		bsize = 237;

		API.ShowCursor(true);
		dbx_isenabled = true;
		dbx_fieldHolder = 0;
		dbx_selectedfield = 0;
	}
});

API.onUpdate.connect(function () {
	if (dbx_isenabled) {
		var res = API.GetScreenResolutionMaintainRatio();

		API.disableAllControlsThisFrame();

		// Checking hovering - Has to be done before rendering the drawings
		var wmin, hmin, wmax, hmax, mid;
		var pf = API.GetCursorPositionMaintainRatio();

		wmin = (res.Width / 2) - (wsize / 2) + 30;
		hmin = (res.Height / 2) - (hsize / 2) + 180;
		wmax = wmin + wsize - 60;
		hmax = hmin + 300 + 45;

		if (dbx_ishovered) {
			if (pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) {
				dbx_isadvancedhovered = true;
			}
			else {
				dbx_isadvancedhovered = false;
			}
		}
		if (!(pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) && dbx_isadvancedhovered) {
			dbx_isadvancedhovered = false;
		}

		wmin = (res.Width / 2) - (wsize / 2) + 30;
		hmin = (res.Height / 2) - (hsize / 2) + 180;
		wmax = wmin + wsize - 60;
		hmax = hmin + 45;

		if (pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) {
			dbx_ishovered = true;
		}
		else {
			dbx_ishovered = false;
		}


		// Background
		API.drawRectangle((res.Width / 2) - (wsize / 2), (res.Height / 2) - (hsize / 2), wsize, hsize, background_r, background_g, background_b, background_a);

		// Title
		API.drawText(dbx_title, (res.Width / 2), (res.Height / 2) - (hsize / 2) + 15, 1, title_r, title_g, title_b, title_a, 1, 1, true, true, 0);

		// Text
		API.drawText(dbx_text, (res.Width / 2), (res.Height / 2) - (hsize / 2) + 100, 0.35, text_r, text_g, text_b, text_a, 0, 1, true, true, wsize - 50);

		// Dropdown-box
		API.drawRectangle((res.Width / 2) - (wsize / 2) + 30, (res.Height / 2) - (hsize / 2) + 180, wsize - 60, 45, 200, 200, 200, 200);

		// Dropdown-arrow
		API.drawText("4", (res.Width / 2) - (wsize / 2) + wsize - 50, (res.Height / 2) - (hsize / 2) + 179, 0.3, 255, 255, 255, 255, 3, 1, true, true, 0);

		// Dropdown-def-text
		API.drawText(dbx_fieldtext[dbx_selectedfield], (res.Width / 2) - (wsize / 2) + 55, (res.Height / 2) - (hsize / 2) + 189, 0.33, 255, 255, 255, 255, 0, 0, true, true, 0);

		// Bottom-Buttons (I have reversed it so that it counts from 2, 1, 0 instead of up because we have a "static" hsize-variable
		if (!dbx_ishovered && !dbx_isadvancedhovered) {
			for (var i = 0; i < dbx_button_count; i++) {
				API.drawRectangle((res.Width / 2) - (wsize / 2) + 30, (res.Height / 2) - (hsize / 2) + 385 + (((3 - dbx_button_count) + i) * 50), wsize - 60, 40, buttonbackground_r, buttonbackground_g, buttonbackground_b, buttonbackground_a);
				API.drawText(dbx_buttontext[i], (res.Width / 2), (res.Height / 2) - (hsize / 2) + 386 + (((3 - dbx_button_count) + i) * 50), 0.5, buttontext_r, buttontext_g, buttontext_b, buttontext_a, 6, 1, true, true, 0);
			}
		}

		if (dbx_ishovered || dbx_isadvancedhovered) {
			// Dropdown-scroll-background
			API.drawRectangle((res.Width / 2) + (wsize / 2) - 70, (res.Height / 2) - (hsize / 2) + 225, 40, 300, 200, 200, 200, 200); // scroll (border-right)
			API.drawRectangle((res.Width / 2) - (wsize / 2) + 30, (res.Height / 2) - (hsize / 2) + 225, wsize - 60, 300, 255, 255, 255, 200); // the box itself

			// Dropdown-scroll-arrows
			if (dbx_fieldHolder > 0) {
				API.drawText("3", (res.Width / 2) + (wsize / 2) - 49, (res.Height / 2) - (hsize / 2) + 225, 0.4, scrollarrow_r, scrollarrow_g, scrollarrow_b, scrollarrow_a, 3, 1, true, true, 0); // up
			}
			else {
				API.drawText("3", (res.Width / 2) + (wsize / 2) - 49, (res.Height / 2) - (hsize / 2) + 225, 0.4, 200, 200, 200, 255, 3, 1, true, true, 0); // up
			}

			if (dbx_fieldHolder < dbx_field_count - dbx_display_fields) {
				API.drawText("4", (res.Width / 2) + (wsize / 2) - 49, (res.Height / 2) - (hsize / 2) + 487, 0.4, scrollarrow_r, scrollarrow_g, scrollarrow_b, scrollarrow_a, 3, 1, true, true, 0); // down
			}
			else {
				API.drawText("4", (res.Width / 2) + (wsize / 2) - 49, (res.Height / 2) - (hsize / 2) + 487, 0.4, 200, 200, 200, 255, 3, 1, true, true, 0); // down
			}

			// Dropdown-scroll-blip
			API.drawRectangle((res.Width / 2) + (wsize / 2) - 67.5, (res.Height / 2) - (hsize / 2) + 260 + ((bsize / (dbx_field_count - (dbx_display_fields - 1))) * dbx_fieldHolder), 35, bsize / (dbx_field_count - (dbx_display_fields - 1)), 150, 150, 150, 200);

			// Dropdown-textfields
			var fieldcounter = 0;
			for (var i = 0; i < dbx_field_count; i++) {
				if (i >= dbx_fieldHolder && i < dbx_fieldHolder + dbx_display_fields) {
					if (i == dbx_selectedfield) {
						API.drawRectangle((res.Width / 2) - (wsize / 2) + 30, (res.Height / 2) - (hsize / 2) + 224 + (fieldcounter * 42.5), wsize - 100, 40, buttonbackground_r, buttonbackground_g, buttonbackground_b, buttonbackground_a);
					}
					API.drawText(dbx_fieldtext[i], (res.Width / 2) - (wsize / 2) + 45, (res.Height / 2) - (hsize / 2) + 229 + (fieldcounter * 42.5), 0.33, txtfield_r, txtfield_g, txtfield_b, txtfield_a, 0, 0, true, true, wsize - 60);
					fieldcounter++;
				}
			}

			// Scrolls
			if (API.isControlPressed(16) && dbx_fieldHolder < dbx_field_count - dbx_display_fields) { // scroll down
				dbx_fieldHolder++;
			}
			if (API.isControlPressed(17) && dbx_fieldHolder > 0) { // scroll up
				dbx_fieldHolder--;
			}

			// Checking cursor for clicking
			wmin = (res.Width / 2) + (wsize / 2) - 67.5;
			hmin = (res.Height / 2) - (hsize / 2) + 260 + ((bsize / (dbx_field_count - (dbx_display_fields - 1))) * dbx_fieldHolder);
			wmax = wmin + 35;
			hmax = hmin + (bsize / (dbx_field_count - dbx_display_fields));
			hmid = ((hmax - hmin) / 2) + hmin;

			if (API.isControlPressed(24)) {
				// Checking scroller drag/click
				if (pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) {
					dbx_holdingLMB = true;
				}
				else {
					// Up button (non-keyboard)
					wmin = (res.Width / 2) + (wsize / 2) - 49 - 15;
					hmin = (res.Height / 2) - (hsize / 2) + 225 - 5;
					wmax = wmin + 25;
					hmax = hmin + 40;

					if (pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) {
						if (dbx_fieldHolder > 0) {
							dbx_fieldHolder--;
						}
					}

					// Down button (non-keyboard)
					wmin = (res.Width / 2) + (wsize / 2) - 49 - 15;
					hmin = (res.Height / 2) - (hsize / 2) + 487 - 5;
					wmax = wmin + 25;
					hmax = hmin + 40;

					if (pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) { // down
						if (dbx_fieldHolder < dbx_field_count - dbx_display_fields) {
							dbx_fieldHolder++;
						}
					}
				}

				// Checking if selecting a field
				for (var i = 0; i < (dbx_display_fields + 1); i++) {
					wmin = (res.Width / 2) - (wsize / 2) + 30;
					hmin = (res.Height / 2) - (hsize / 2) + 224 + (i * 42.5);
					wmax = wmin + wsize - 100;
					hmax = hmin + 40;

					if (pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) {
						dbx_selectedfield = dbx_fieldHolder + i;
						break;
					}
				}
			}
			else {
				dbx_holdingLMB = false;
			}

			if (dbx_holdingLMB) {
				if (pf.Y >= hmid + ((bsize / (dbx_field_count - (dbx_display_fields - 1))) / 2) && dbx_fieldHolder < dbx_field_count - dbx_display_fields) {
					dbx_fieldHolder++;
				}
				else if (pf.Y <= hmid - ((bsize / (dbx_field_count - (dbx_display_fields - 1))) / 2) && dbx_fieldHolder > 0) {
					dbx_fieldHolder--;
				}
			}
		}
		else {
			// Checking if buttons were clicked
			if (API.isControlJustPressed(24)) {
				for (var i = 0; i < dbx_button_count; i++) {
					wmin = (res.Width / 2) - (wsize / 2) + 30;
					hmin = (res.Height / 2) - (hsize / 2) + 385 + (((3 - dbx_button_count) + i) * 50);
					wmax = wmin + (wsize - 60);
					hmax = hmin + 40;

					if (pf.X >= wmin && pf.X <= wmax && pf.Y >= hmin && pf.Y <= hmax) {
						dbx_isenabled = false;
						API.ShowCursor(false);
						API.TriggerServerEvent(dbx_finishfunction, i, dbx_selectedfield, dbx_fieldtext[dbx_selectedfield]);
						break;
					}
				}
			}
		}
	}
});

API.onKeyDown.connect(function (sender, e) {
	if (dbx_isenabled) {
		if (dbx_ishovered || dbx_isadvancedhovered) {
			// Down arrow (keyboard)
			if (e.KeyCode == Keys.Down && dbx_fieldHolder < dbx_field_count - dbx_display_fields) {
				dbx_fieldHolder++;
			}

			// Up arrow (keyboard)
			if (e.KeyCode == Keys.Up && dbx_fieldHolder > 0) {
				dbx_fieldHolder--;
			}
		}
	}
});
