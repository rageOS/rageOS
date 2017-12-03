var masksMenu = null;

API.onServerEventTrigger.connect(function (eventname, args) {
	switch (eventname) {
		case "masksMenu":

			maData = JSON.parse(args[0]);
			playerGender = args[1];			
			masksMenu = API.createMenu(" ", "Such dir was passendes aus", 0, 0, 6);
			API.SetMenuBannerSprite(masksMenu, "shopui_title_movie_masks", "shopui_title_movie_masks");
			API.ShowCursor(false);
			var masksData = null;
			if (playerGender === "1") {
				masksData = maData.male.masks;
			} else {
				masksData = maData.female.masks;
			}

			var masksModelList = new List(String);
			for (var i = 0; i < masksData.length; i++) {
				masksModelList.Add(masksData[i].m.toString());
			}

			var masksTextureList = new List(String);
			for (i = 0; i < masksData[0].t.length; i++) {
				var a = masksData[0].t;
				masksTextureList.Add(a[i].toString());
			}

			var masksModelMenuItem = API.createListItem("Maske", "", masksModelList, 0);
			var masksTextureMenuItem = API.createListItem("Farbe", "", masksTextureList, 0);

			masksMenu.AddItem(masksModelMenuItem);
			masksMenu.AddItem(masksTextureMenuItem);

			masksMenu.OnListChange.connect(function (sender, item, index) {
				if (index !== 0) masksModelMenuItem.Description = "900$ - Kaufe mit ~r~[ENTER]";
				if (index !== 0) masksTextureMenuItem.Description = "900$ - Kaufe mit ~r~[ENTER]";
				else masksModelMenuItem.Description = "Verlasse mit ~r~[ENTER]";
				if (item === masksModelMenuItem) {
					masksModelSelection = parseInt(item.IndexToItem(index));
					masksTextureList = new List(String);
					for (var i = 0; i < masksData[index].t.length; i++) {
						var a = masksData[index].t;
						masksTextureList.Add(a[i].toString());
					}
					masksTextureSelection = parseInt(masksTextureList[0]);
					masksMenu.RemoveItemAt(1);
					masksTextureMenuItem = API.createListItem("Farbe", "", masksTextureList, 0);
					masksMenu.AddItem(masksTextureMenuItem);
					API.SetPlayerClothes(API.getLocalPlayer(), 1, masksModelSelection, masksTextureSelection);
				}
				if (item === masksTextureMenuItem) {
					masksTextureSelection = parseInt(item.IndexToItem(index));
					API.SetPlayerClothes(API.getLocalPlayer(), 1, masksModelSelection, masksTextureSelection);
				}


			});
			masksMenu.OnItemSelect.connect(function (sender, item, index) {
				masksMenu.Visible = false;
				if (index !== -1) {
					API.SetPlayerClothes(API.getLocalPlayer(), 1, masksModelSelection, masksTextureSelection);
					API.TriggerServerEvent("BuyingMask", masksModelSelection, masksTextureSelection);
				}
			});
			masksMenu.Visible = true;
			break;
		case "leaveMaskmenu":
			if (masksMenu !== null) {
				masksMenu.Visible = false;
			}
			break;
	}
});
