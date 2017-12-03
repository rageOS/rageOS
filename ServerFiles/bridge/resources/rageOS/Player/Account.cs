using System;
using System.Linq;
using GTANetworkAPI;

namespace RageOS
{
    public class Account : Script
    {
        //Prüfem, ob der Account existiert
        public static bool DoesAccountExist(Client player, string email, string Password)
        {
            using (var db = new DBContext())
            {
                if (db.Account.Count(X => X.EMail == email) > 0)
                {
                    var account = db.Account.First(X => X.EMail == email);
                    return true;
                }
            }
            return false;
        }

        //Character erstellen
        public static void CreatePlayerCharacter(Client player, int pskin)
        {
            if (player.HasData("account.Id"))
            {
                int AccountID = player.GetData("account.Id");
                var CharacterData = new Database.Models.Character()
                {
                    AccountId = AccountID,
                    Name = player.Name,
                    HealthLevel = 100,
                    FoodLevel = 100,
                    DrinkLevel = 100,
                    DrunkLevel = 0,
                    CashMoney = 100,
                    BankMoney = 0,
                    Level = 1,
                    MinutesToNextLevel = 60,
                    MinutesInThisLevel = 0,
                    Jailed = false,
                    JailTime = 0,
                    LocationX = -1040.907f,
                    LocationY = -2743.189f,
                    LocationZ = 13.94503f,
                    Skin = pskin,
                };
                using (var db = new DBContext())
                {
                    db.Character.Add(CharacterData);
                    player.SetData("character.Id", CharacterData.Id);
                    db.SaveChanges();
                }
            }
            else
            {
                API.Shared.ConsoleOutput(player.SocialClubName + " versucht CreatePlayerCharacter zu nutzen, hat aber keine Variable 'account.Id'");
            }
        }

        public static void LoadPlayerCharacter(Client player)
        {
            using (var db = new DBContext())
            {
                if (player.HasData("character.Id"))
                {
                    int CharacterId = player.GetData("character.Id");
                    if (db.Character.Count(X => X.Id == CharacterId) == 1)
                    {
                        var character = db.Character.First(X => X.Id == CharacterId);

                        foreach (var property in typeof(Database.Models.Character).GetProperties())
                        {
                            if (property.CanRead && !property.PropertyType.IsClass)
                            {
                                try
                                {
                                    player.SetData(property.Name, property.GetValue(character, null));
                                }
                                catch (Exception ex)
                                {
                                    API.Shared.ConsoleOutput("LoadPlayerCharacter: Beim laden der Variable '" + property.Name + "' ist ein Fehler aufgetreten: " + ex.Message);
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        API.Shared.ConsoleOutput(player.SocialClubName + " versucht LoadPlayerCharacter zu nutzen, der Character ist aber mehrfach oder gar nicht vorhanden");
                    }
                }
                else
                {
                    API.Shared.ConsoleOutput(player.SocialClubName + " versucht LoadPlayerCharacter zu nutzen, hat aber keine Variable 'character.Id'");
                }
            }
        }

        public static void SavePlayerCharacter(Client player)
        {
            using (var db = new DBContext())
            {
                if (player.HasData("account.Id"))
                {
                    int AccountId = player.GetData("account.Id");
                    if (player.HasData("character.Id"))
                    {
                        int CharacterId = player.GetData("character.Id");
                        if (db.Character.Count(X => X.Id == CharacterId) == 1)
                        {
                            var Character = db.Character.First(X => X.Id == CharacterId);
                            var Account = Character.accounts;

                            if (Account.CopLevel != player.GetData("CopLevel")) { player.SetData("CopLevel", Account.CopLevel); }
                            if (Account.MedicLevel != player.GetData("MedicLevel")) { player.SetData("MedicLevel", Account.MedicLevel); }
                            if (Account.JustizLevel != player.GetData("JustizLevel")) { player.SetData("JustizLevel", Account.JustizLevel); }
                            if (Account.AdminLevel != player.GetData("AdminLevel")) { player.SetData("AdminLevel", Account.AdminLevel); }

                            foreach (var property in typeof(Database.Models.Character).GetProperties())
                            {
                                if (property.CanWrite && !property.PropertyType.IsClass)
                                {
                                    if (player.HasData(property.Name))
                                    {
                                        try
                                        {
                                            property.SetValue(Character, player.GetData(property.Name), null);
                                        }
                                        catch (Exception ex)
                                        {
                                            API.Shared.ConsoleOutput("SavePlayerCharacter: Beim speichern der Variable '" + property.Name + "' ist ein Fehler aufgetreten: " + ex.Message);
                                        }
                                        
                                    }
                                    else if (player.HasSyncedData(property.Name))
                                    {
                                        try
                                        {
                                            property.SetValue(Character, player.GetSyncedData(property.Name), null);
                                        }
                                        catch (Exception ex)
                                        {
                                            API.Shared.ConsoleOutput("SavePlayerCharacter: Beim speichern der Synced-Variable '" + property.Name + "' ist ein Fehler aufgetreten: " + ex.Message);
                                        }
                                        
                                    }
                                }
                            }
                            db.SaveChanges();
                        }
                        else
                        {
                            API.Shared.ConsoleOutput(player.SocialClubName + " versucht SavePlayerCharacter zu nutzen, der Character ist aber mehrfach oder gar nicht vorhanden");
                        }
                    }
                    else
                    {
                        API.Shared.ConsoleOutput(player.SocialClubName + " versucht SavePlayerCharacter zu nutzen, hat aber keine Variable 'character.Id'");
                    }
                }
                else
                {
                    API.Shared.ConsoleOutput(player.SocialClubName + " versucht SavePlayerCharacter zu nutzen, hat aber keine Variable 'account.Id'");
                }
            }
        }

        public Database.Models.Gender GetPlayerGender(Client player)
        {
            using (var db = new DBContext())
            {
                if (player.HasData("character.Id"))
                {
                    int CharacterId = player.GetData("character.Id");
                    var Character = db.Character.First(X => X.Id == CharacterId);
                    return Character.Gender;
                }
                else
                {
                    API.ConsoleOutput(player.SocialClubName + " versucht GetPlayerGender zu nutzen, hat aber keine Variable 'character.Id'");
                    return Database.Models.Gender.Male;
                }
            }
        }

        public void GetRPName(Client player)
        {
            using (var db = new DBContext())
            {
                if (player.HasData("character.Id"))
                {
                    int CharacterId = player.GetData("character.Id");
                    var Character = db.Character.First(X => X.Id == CharacterId);
                    API.SetPlayerName(player, Character.Name);
                    API.SetPlayerNametag(player, Character.Name);
                    API.SetPlayerNametagVisible(player, false);
                    player.SetData("RPName", Character.Name);
                }
                else
                {
                    API.ConsoleOutput(player.SocialClubName + " versucht GetRPName zu nutzen, hat aber keine Variable 'character.Id'");
                }
            }
        }

        public void SavePlayerRPName(Client player, string RPName)
        {
            using (var db = new DBContext())
            {
                if (player.HasData("character.Id"))
                {
                    int CharacterId = player.GetData("character.Id");
                    Database.Models.Character Character = db.Character.First(X => X.Id == CharacterId);
                    Character.Name = RPName;
                    db.SaveChanges();
                }
                else
                {
                    API.ConsoleOutput(player.SocialClubName + " versucht SavePlayerRPName zu nutzen, hat aber keine Variable 'character.Id'");
                }
            }
        }
    }
}
