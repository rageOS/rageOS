using System;
using GTANetworkAPI;

namespace DBX.Handler
{
    public class DBXHandler : Script
    {
        public DBXHandler()
        {
            API.OnClientEventTrigger += OnClientTriggerEvent;
        }

        private void OnClientTriggerEvent(Client sender, string eventName, object[] args)
        {
            if (eventName == "dbx_log")
            {
                API.ConsoleOutput(args[0].ToString());
            }
        }

        public static void ShowDialog(Client player, string dialog_type, string function, string title, string text, int buttons, params object[] args)
        {
            API.Shared.TriggerClientEvent(player, dialog_type + "_pre", function, title, text, buttons, args);
        }

        #region Functions: dbx_option_dropdown

        public static void SetColorOptionDropdown_Background(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_dropdown_colors", 0, r, g, b, a);
        }

        public static void SetColorOptionDropdown_Title(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_dropdown_colors", 1, r, g, b, a);
        }

        public static void SetColorOptionDropdown_Text(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_dropdown_colors", 2, r, g, b, a);
        }

        public static void SetColorOptionDropdown_ScrollArrow(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_dropdown_colors", 3, r, g, b, a);
        }

        public static void SetColorOptionDropdown_Textfields(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_dropdown_colors", 4, r, g, b, a);
        }

        public static void SetColorOptionDropdown_ButtonText(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_dropdown_colors", 5, r, g, b, a);
        }

        public static void SetColorOptionDropdown_ButtonBackground(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_dropdown_colors", 6, r, g, b, a);
        }

        #endregion

        #region Functions: dbx_option_dropdown

        public static void SetColorOptionScroll_Background(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_scroll_colors", 0, r, g, b, a);
        }

        public static void SetColorOptionScroll_Title(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_scroll_colors", 1, r, g, b, a);
        }

        public static void SetColorOptionScroll_Text(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_scroll_colors", 2, r, g, b, a);
        }

        public static void SetColorOptionScroll_ScrollArrow(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_scroll_colors", 3, r, g, b, a);
        }

        public static void SetColorOptionScroll_Textfields(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_scroll_colors", 4, r, g, b, a);
        }

        public static void SetColorOptionScroll_ButtonText(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_scroll_colors", 5, r, g, b, a);
        }

        public static void SetColorOptionScroll_ButtonBackground(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_option_scroll_colors", 6, r, g, b, a);
        }

        #endregion

        #region Functions: dbx_text_scroll

        public static void SetColorTextScroll_Background(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 0, r, g, b, a);
        }

        public static void SetColorTextScroll_Title(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 1, r, g, b, a);
        }

        public static void SetColorTextScroll_Text(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 2, r, g, b, a);
        }

        public static void SetColorTextScroll_ScrollArrow(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 3, r, g, b, a);
        }

        public static void SetColorTextScroll_Textfields(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 4, r, g, b, a);
        }

        public static void SetColorTextScroll_ButtonText(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 5, r, g, b, a);
        }

        public static void SetColorTextScroll_ButtonBackground(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 6, r, g, b, a);
        }

        #endregion

        #region Functions: dbx_text_input

        public static void SetColorTextInput_Background(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 0, r, g, b, a);
        }

        public static void SetColorTextInput_Title(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 1, r, g, b, a);
        }

        public static void SetColorTextInput_Text(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 2, r, g, b, a);
        }

        public static void SetColorTextInput_Box(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 3, r, g, b, a);
        }

        public static void SetColorTextInput_BoxText(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 4, r, g, b, a);
        }

        public static void SetColorTextInput_ButtonText(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 5, r, g, b, a);
        }

        public static void SetColorTextInput_ButtonBackground(Client player, int r, int g, int b, int a)
        {
            API.Shared.TriggerClientEvent(player, "dbx_text_scroll_colors", 6, r, g, b, a);
        }

        #endregion
    }
}
