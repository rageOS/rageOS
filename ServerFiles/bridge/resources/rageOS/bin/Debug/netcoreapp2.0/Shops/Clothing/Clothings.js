"use strict";
/// <reference path="../../types-gt-mp/Definitions/index.d.ts" />
var BodyMenu = null;
var char_data = null;
var playerGender = null;
var Hatmenu = null;
var hatModelSelection = null;
var hatTextureSelection = null;
var UnderwearMenu = null;
var undershirtModelSelection = null;
var undershirtTextureSelection = null;
var JacketMenu = null;
var shirtModelSelection = null;
var shirtTextureSelection = null;
var PantsMenu = null;
var pantsModelSelection = null;
var pantsTextureSelection = null;
var ShoesMenu = null;
var shoesModelSelection = null;
var shoesTextureSelection = null;
var shopui_title_gr_gunmod = null;
var bodyModelSelection = null;
var bodyTextureSelection = null;
API.onServerEventTrigger.connect(function (event, args) {
    switch (event) {
        case "HatMenu":
            char_data = JSON.parse(args[0]);
            playerGender = args[1];
            Hatmenu = null;
            Hatmenu = API.createMenu("Hüte", 0, 0, 6);
            var hatData = null;
            if (playerGender == "1") {
                hatData = char_data.male.hat;
            }
            else {
                hatData = char_data.female.hat;
            }
            var hatModelList = new List(String);
            for (var i = 0; i < hatData.length; i++) {
                hatModelList.Add(hatData[i].m.toString());
            }
            var hatTextureList = new List(String);
            for (var i = 0; i < hatData[0].t.length; i++) {
                var a = hatData[0].t;
                hatTextureList.Add(a[i].toString());
            }
            var hatModelMenuItem = API.createListItem("Hüte", "", hatModelList, 0);
            var hatTextureMenuItem = API.createListItem("Farbe", "", hatTextureList, 0);
            Hatmenu.AddItem(hatModelMenuItem);
            Hatmenu.AddItem(hatTextureMenuItem);
            Hatmenu.OnListChange.connect(function (sender, item, index) {
                if (item == hatModelMenuItem) {
                    hatModelSelection = parseInt(item.IndexToItem(index));
                    hatTextureList = new List(String);
                    for (var i = 0; i < hatData[index].t.length; i++) {
                        var a = hatData[index].t;
                        hatTextureList.Add(a[i].toString());
                    }
                    hatTextureSelection = parseInt(hatTextureList[0]);
                    Hatmenu.RemoveItemAt(1);
                    hatTextureMenuItem = API.createListItem("Farbe", "", hatTextureList, 0);
                    Hatmenu.AddItem(hatTextureMenuItem);
                    API.TriggerServerEvent("SetAcc", 0, hatModelSelection, hatTextureSelection);
                }
                if (item == hatTextureMenuItem) {
                    hatTextureSelection = parseInt(item.IndexToItem(index));
                    API.TriggerServerEvent("SetAcc", 0, hatModelSelection, hatTextureSelection);
                }
            });
            Hatmenu.OnItemSelect.connect(function (sender, item, index) {
                API.TriggerServerEvent("ClothingMenu");
                Hatmenu.Visible = false;
            });
            Hatmenu.Visible = true;
            break;
        case "UnderwearMenu":
            char_data = JSON.parse(args[0]);
            playerGender = args[1];
            UnderwearMenu = null;
            UnderwearMenu = API.createMenu("Unterhemden", 0, 0, 6);
            var undershirtData = null;
            if (playerGender == "1") {
                undershirtData = char_data.male.undershirt;
            }
            else {
                undershirtData = char_data.female.undershirt;
            }
            var undershirtModelList = new List(String);
            for (var i = 0; i < undershirtData.length; i++) {
                undershirtModelList.Add(undershirtData[i].m.toString());
            }
            var undershirtTextureList = new List(String);
            for (var i = 0; i < undershirtData[0].t.length; i++) {
                var a = undershirtData[0].t;
                undershirtTextureList.Add(a[i].toString());
            }
            var undershirtModelMenuItem = API.createListItem("Unterhemden", "", undershirtModelList, 0);
            var undershirtTextureMenuItem = API.createListItem("Farbe", "", undershirtTextureList, 0);
            UnderwearMenu.AddItem(undershirtModelMenuItem);
            UnderwearMenu.AddItem(undershirtTextureMenuItem);
            UnderwearMenu.OnListChange.connect(function (sender, item, index) {
                if (item == undershirtModelMenuItem) {
                    undershirtModelSelection = parseInt(item.IndexToItem(index));
                    undershirtTextureList = new List(String);
                    for (var i = 0; i < undershirtData[index].t.length; i++) {
                        var a = undershirtData[index].t;
                        undershirtTextureList.Add(a[i].toString());
                    }
                    undershirtTextureSelection = parseInt(undershirtTextureList[0]);
                    UnderwearMenu.RemoveItemAt(1);
                    undershirtTextureMenuItem = API.createListItem("Farbe", "", undershirtTextureList, 0);
                    UnderwearMenu.AddItem(undershirtTextureMenuItem);
                    API.TriggerServerEvent("SetClothes", 8, undershirtModelSelection, undershirtTextureSelection);
                }
                if (item == undershirtTextureMenuItem) {
                    undershirtTextureSelection = parseInt(item.IndexToItem(index));
                    API.TriggerServerEvent("SetClothes", 8, undershirtModelSelection, undershirtTextureSelection);
                }
            });
            UnderwearMenu.OnItemSelect.connect(function (sender, item, index) {
                API.TriggerServerEvent("ClothingMenu");
                UnderwearMenu.Visible = false;
            });
            UnderwearMenu.Visible = true;
            break;
        case "JacketMenu":
            char_data = JSON.parse(args[0]);
            playerGender = args[1];
            JacketMenu = null;
            JacketMenu = API.createMenu("Oberkörper", 0, 0, 6);
            var shirtData = null;
            if (playerGender == "1") {
                shirtData = char_data.male.shirt;
            }
            else {
                shirtData = char_data.female.shirt;
            }
            var shirtModelList = new List(String);
            for (var i = 0; i < shirtData.length; i++) {
                shirtModelList.Add(shirtData[i].m.toString());
            }
            var shirtTextureList = new List(String);
            for (var i = 0; i < shirtData[0].t.length; i++) {
                var a = shirtData[0].t;
                shirtTextureList.Add(a[i].toString());
            }
            var bodyModelList = new List(String);
            for (var i = 0; i < shirtData[0].b.length; i++) {
                var a = shirtData[0].b;
                bodyModelList.Add(a[i].toString());
            }
            var undershirtData = null;
            if (playerGender == "1") {
                undershirtData = char_data.male.undershirt;
            }
            else {
                undershirtData = char_data.female.undershirt;
            }
            var shirtModelMenuItem = API.createListItem("Jacken", "", shirtModelList, 0);
            var shirtTextureMenuItem = API.createListItem("Jackenfarbe", "", shirtTextureList, 0);
            var bodyModelMenuItem = API.createListItem("Handschuhe", "", bodyModelList, 0);
            JacketMenu.AddItem(shirtModelMenuItem);
            JacketMenu.AddItem(shirtTextureMenuItem);
            JacketMenu.AddItem(bodyModelMenuItem);
            JacketMenu.OnListChange.connect(function (sender, item, index) {
                if (item == shirtModelMenuItem) {
                    bodyTextureSelection = 0;
                    shirtModelSelection = parseInt(item.IndexToItem(index));
                    shirtTextureList = new List(String);
                    for (var i = 0; i < shirtData[index].t.length; i++) {
                        var a = shirtData[index].t;
                        shirtTextureList.Add(a[i].toString());
                    }
                    bodyModelList = new List(String);
                    for (var i = 0; i < shirtData[index].b.length; i++) {
                        var a = shirtData[index].b;
                        bodyModelList.Add(a[i].toString());
                    }
                    shirtTextureSelection = parseInt(shirtTextureList[0]);
                    bodyModelSelection = parseInt(bodyModelList[0]);
                    JacketMenu.RemoveItemAt(1);
                    JacketMenu.RemoveItemAt(1);
                    shirtTextureMenuItem = API.createListItem("Farbe", "", shirtTextureList, 0);
                    JacketMenu.AddItem(shirtTextureMenuItem);
                    bodyModelMenuItem = API.createListItem("Handschuhe", "", bodyModelList, 0);
                    JacketMenu.AddItem(bodyModelMenuItem);
                    API.TriggerServerEvent("SetClothes", 3, bodyModelSelection, bodyTextureSelection);
                    API.TriggerServerEvent("SetClothes", 11, shirtModelSelection, shirtTextureSelection);
                }
                if (item == shirtTextureMenuItem) {
                    shirtTextureSelection = parseInt(item.IndexToItem(index));
                    API.TriggerServerEvent("SetClothes", 11, shirtModelSelection, shirtTextureSelection);
                }
                if (item == bodyModelMenuItem) {
                    bodyTextureSelection = 0;
                    bodyModelSelection = parseInt(item.IndexToItem(index));
                    API.TriggerServerEvent("SetClothes", 3, bodyModelSelection, bodyTextureSelection);
                }
            });
            JacketMenu.OnItemSelect.connect(function (sender, item, index) {
                API.TriggerServerEvent("ClothingMenu");
                JacketMenu.Visible = false;
            });
            JacketMenu.Visible = true;
            break;
        case "PantsMenu":
            char_data = JSON.parse(args[0]);
            playerGender = args[1];
            PantsMenu = null;
            PantsMenu = API.createMenu("Hosen", 0, 0, 6);
            var pantsData = null;
            if (playerGender == "1") {
                pantsData = char_data.male.pants;
            }
            else {
                pantsData = char_data.female.pants;
            }
            var pantsModelList = new List(String);
            for (var i = 0; i < pantsData.length; i++) {
                pantsModelList.Add(pantsData[i].m.toString());
            }
            var pantsTextureList = new List(String);
            for (var i = 0; i < pantsData[0].t.length; i++) {
                var a = pantsData[0].t;
                pantsTextureList.Add(a[i].toString());
            }
            var pantsModelMenuItem = API.createListItem("Hosen", "", pantsModelList, 0);
            var pantsTextureMenuItem = API.createListItem("Farbe", "", pantsTextureList, 0);
            PantsMenu.AddItem(pantsModelMenuItem);
            PantsMenu.AddItem(pantsTextureMenuItem);
            PantsMenu.OnListChange.connect(function (sender, item, index) {
                if (item == pantsModelMenuItem) {
                    pantsModelSelection = parseInt(item.IndexToItem(index));
                    pantsTextureList = new List(String);
                    for (var i = 0; i < pantsData[index].t.length; i++) {
                        var a = pantsData[index].t;
                        pantsTextureList.Add(a[i].toString());
                    }
                    pantsTextureSelection = parseInt(pantsTextureList[0]);
                    PantsMenu.RemoveItemAt(1);
                    pantsTextureMenuItem = API.createListItem("Farbe", "", pantsTextureList, 0);
                    PantsMenu.AddItem(pantsTextureMenuItem);
                    API.TriggerServerEvent("SetClothes", 4, pantsModelSelection, pantsTextureSelection);
                }
                if (item == pantsTextureMenuItem) {
                    pantsTextureSelection = parseInt(item.IndexToItem(index));
                    API.TriggerServerEvent("SetClothes", 4, pantsModelSelection, pantsTextureSelection);
                }
            });
            PantsMenu.OnItemSelect.connect(function (sender, item, index) {
                API.TriggerServerEvent("ClothingMenu");
                PantsMenu.Visible = false;
            });
            PantsMenu.Visible = true;
            break;
        case "ShoesMenu":
            char_data = JSON.parse(args[0]);
            playerGender = args[1];
            ShoesMenu = null;
            ShoesMenu = API.createMenu("Schuhe", 0, 0, 6);
            var shoesData = null;
            if (playerGender == "1") {
                shoesData = char_data.male.shoes;
            }
            else {
                shoesData = char_data.female.shoes;
            }
            var shoesModelList = new List(String);
            for (var i = 0; i < shoesData.length; i++) {
                shoesModelList.Add(shoesData[i].m.toString());
            }
            var shoesTextureList = new List(String);
            for (var i = 0; i < shoesData[0].t.length; i++) {
                var a = shoesData[0].t;
                shoesTextureList.Add(a[i].toString());
            }
            var shoesModelMenuItem = API.createListItem("Schuhe", "", shoesModelList, 0);
            var shoesTextureMenuItem = API.createListItem("Farbe", "", shoesTextureList, 0);
            ShoesMenu.AddItem(shoesModelMenuItem);
            ShoesMenu.AddItem(shoesTextureMenuItem);
            ShoesMenu.OnListChange.connect(function (sender, item, index) {
                if (item == shoesModelMenuItem) {
                    shoesModelSelection = parseInt(item.IndexToItem(index));
                    shoesTextureList = new List(String);
                    for (var i = 0; i < shoesData[index].t.length; i++) {
                        var a = shoesData[index].t;
                        shoesTextureList.Add(a[i].toString());
                    }
                    shoesTextureSelection = parseInt(shoesTextureList[0]);
                    ShoesMenu.RemoveItemAt(1);
                    shoesTextureMenuItem = API.createListItem("Farbe", "", shoesTextureList, 0);
                    ShoesMenu.AddItem(shoesTextureMenuItem);
                    API.TriggerServerEvent("SetClothes", 6, shoesModelSelection, shoesTextureSelection);
                }
                if (item == shoesTextureMenuItem) {
                    shoesTextureSelection = parseInt(item.IndexToItem(index));
                    API.TriggerServerEvent("SetClothes", 6, shoesModelSelection, shoesTextureSelection);
                }
            });
            ShoesMenu.OnItemSelect.connect(function (sender, item, index) {
                API.TriggerServerEvent("ClothingMenu");
                ShoesMenu.Visible = false;
            });
            ShoesMenu.Visible = true;
            break;
    }
});
