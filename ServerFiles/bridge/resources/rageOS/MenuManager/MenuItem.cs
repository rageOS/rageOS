namespace MenuManagement
{
    class MenuItem
    {
        #region Public properties
        public MenuItemType Type { get; protected set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string LeftBadge { get; set; }
        public string RightBadge { get; set; }
        public string RightLabel { get; set; }
        public bool ExecuteCallback { get; set; }
        public byte? InputMaxLength { get; set; }
        public InputType? InputType { get; set; }
        #endregion

        #region Constructor
        public MenuItem(string text, string description = null, string id = null)
        {
            Type = MenuItemType.MenuItem;

            if (text != null && text.Trim().Length == 0)
                Text = null;
            else
                Text = text;

            if (description != null && description.Trim().Length == 0)
                Description = null;
            else
                Description = description;

            if (id != null && id.Trim().Length == 0)
                Id = null;
            else
                Id = id;

            LeftBadge = null;
            RightBadge = null;
            RightLabel = null;
            ExecuteCallback = false;
            InputMaxLength = null;
            InputType = null;
        }
        #endregion

        #region Public virtual methods
        public virtual bool IsInput()
        {
            return InputMaxLength > 0;
        }
        #endregion

        #region Public methods
        public void SetInput(string defaultText, byte maxLength, InputType inputType)
        {
            if (defaultText != null && defaultText.Length > 0)
                RightLabel = defaultText;

            InputMaxLength = maxLength;
            InputType = inputType;
        }
        #endregion
    }
}
