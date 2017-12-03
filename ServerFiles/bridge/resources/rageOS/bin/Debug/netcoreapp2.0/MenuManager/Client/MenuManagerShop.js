function CalculatePrice(menu) {
    const price = menu.MenuItems[0].RightLabel * 5 + menu.MenuItems[1].RightLabel * 10;
    menu.MenuItems[menu.MenuItems.Count - 2].SetRightLabel("~g~$~s~" + price.toString());
}
