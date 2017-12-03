using System.Security.Cryptography;
using System;
using System.Linq;
using GTANetworkAPI;

namespace RageOS
{
    public class CompaniesManager : Script
    {
        public static string GenerateIdentificationnumber() //Erstellt 8stellige Identifikationsnummer
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }

        public static void LoadCompaniesMember(Client player)
        {
            if (player.HasData("characters.Id"))
            {
                using (var db = new DBContext())
                {
                    //NEU SCHREIBEN

                    int CharacterId = player.GetData("characters.Id");
                    var players = db.Job.Where(X => X.CharacterId == CharacterId).ToList();

                    foreach (var p in players)
                    {
                        var Company = db.Company.First(X => X.Id == p.CompaniesId);
                        player.SetData("CompanyMember", p);
                        player.SetData("Company", Company);

                        API.Shared.ConsoleOutput(Company.ToString());
                        API.Shared.ConsoleOutput(p.ToString());
                    }
                }
            }
        }
    }
}