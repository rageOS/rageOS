namespace MenuManagement
{
    class CheckboxItem : MenuItem
    {
        #region Public properties
        public bool Checked { get; set; }
        #endregion

        #region Constructor
        public CheckboxItem(string text, string description, string id, bool isChecked) : base(text, description, id)
        {
            Type = MenuItemType.CheckboxItem;
            Checked = isChecked;
        }
        #endregion

        #region Public overrided methods
        public override bool IsInput()
        {
            return false;
        }
        #endregion
    }
}
