using System;
using GTANetworkAPI;

namespace RageOS
{
    public class PlayerCommands : Script
    {
        #region Variables

        public const float Pi = (float)Math.PI;
        private const string errorAdmin = "~r~[FEHLER]~w~ Du besitzt nicht die benötigten Rechte oder du bist nicht im Admindienst.";

        #endregion

        public static bool CheckAdminPermission(Client player, int adminlevel)
        {
            int myLevel = (int)player.GetData("account.AdminLevel");

            if (myLevel >= adminlevel)
            {
                return true;
            }
            player.SendChatMessage(errorAdmin);
            return false;
        }

        public static Client FindPlayer(Client sender, string name)
        {
            if (name.Contains("_")) name = name.Replace("_", " ");
            Client returnClient = null;
            var players = API.Shared.GetAllPlayers();
            int playersCount = 0;
            foreach (var player in players)
            {
                // Skip if list element is null
                if (player == null) continue;

                // If player name contains provided name
                if (player.Name.ToLower().Contains(name.ToLower()))
                {
                    // If player name == provided name
                    if ((player.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        return player;
                    }
                    else
                    {
                        playersCount++;
                        returnClient = player;
                    }
                }
            }

            if (playersCount != 1)
            {
                if (playersCount > 0)
                {
                    API.Shared.SendChatMessageToPlayer(sender, "~r~ERROR: ~w~Es wurden mehrere Spieler mit dem angegebenen Namen gefunden.");
                }
                else
                {
                    API.Shared.SendChatMessageToPlayer(sender, "~r~ERROR: ~w~Spieler wurde nicht gefunden.");
                }
                return null;
            }

            return returnClient;
        }

        [Command("stuck")]
        public void Stuck(Client player)
        {
            if (API.GetEntitySyncedData(player, "IS_DEATH") == true) { return; }
            player.Position = new Vector3(-1040.907f, -2743.189f, 13.94503f);
            player.Dimension = 0;
        }
    }
}
