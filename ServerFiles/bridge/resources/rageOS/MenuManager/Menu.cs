using System.Collections.Generic;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace MenuManagement
{
    class Menu
    {
        #region Public enums
        public enum MenuAnchor
        {
            TopLeft,
            TopMiddle,
            TopRight,
            MiddleLeft,
            Middle,
            TopLeftFull,
            MiddleRight,
            BottomLeft,
            BottomMiddle,
            BottomRight
        }
        #endregion

        #region Public delegates
        public delegate void MenuCallback(Client client, Menu menu, MenuItem menuItem, int itemIndex, dynamic data);
        public delegate void MenuFinalizer(Client client, Menu menu);
        #endregion

        #region Public properties
        public string Id { get; set; }
        public Banner BannerSprite { get; set; }
        public Color? BannerColor { get; set; }
        public string BannerTexture { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public MenuAnchor Anchor { get; set; }
        public bool NoExit { get; set; }
        public bool EnableBanner { get; set; }
        public List<MenuItem> Items { get; set; }
        public bool BackCloseMenu { get; set; }
        public string OnCheckboxChange { get; set; }
        public string OnIndexChange { get; set; }
        public string OnItemSelect { get; set; }
        public string OnListChange { get; set; }
        public string OnMenuClose { get; set; }
        public string OnMenuOpen { get; set; }
        #endregion

        #region Public Json ignored properties
        [JsonIgnore]
        public MenuCallback Callback { get; set; }
        [JsonIgnore]
        public MenuFinalizer Finalizer { get; set; }
        #endregion

        #region Constructor
        public Menu(string id, string title, string subTitle, int posX, int posY, MenuAnchor anchor, bool noExit = false, bool enableBanner = true, bool backCloseMenu = false)
        {
            if (id == null && id.Trim().Length == 0)
                Id = null;
            else
                Id = id;

            BannerSprite = null;
            BannerColor = null;
            BannerTexture = null;

            if (title != null && title.Trim().Length == 0)
                Title = null;
            else
                Title = title;

            if (subTitle != null && subTitle.Trim().Length > 0)
                SubTitle = subTitle;

            PosX = posX;
            PosY = posY;
            Anchor = anchor;
            NoExit = noExit;
            EnableBanner = enableBanner;
            BackCloseMenu = backCloseMenu;
            Items = new List<MenuItem>();
            OnCheckboxChange = null;
            OnIndexChange = null;
            OnItemSelect = null;
            OnListChange = null;
            OnMenuClose = null;
            OnMenuOpen = null;
            Callback = null;
            Finalizer = null;
        }
        #endregion

        #region public methods
        public void Add(MenuItem menuItem)
        {
            Items.Add(menuItem);
        }

        public void SaveData(dynamic data)
        {
            foreach (MenuItem menuItem in Items)
            {
                if (menuItem.Id == "")
                    continue;

                if (menuItem.Type == MenuItemType.CheckboxItem)
                    ((CheckboxItem)menuItem).Checked = data[menuItem.Id];
                else if (menuItem.Type == MenuItemType.ListItem)
                    ((ListItem)menuItem).SelectedItem = data[menuItem.Id]["Index"];
                else if (menuItem.InputMaxLength > 0)
                    menuItem.RightLabel = data[menuItem.Id];
            }
        }
        #endregion
    }
}
