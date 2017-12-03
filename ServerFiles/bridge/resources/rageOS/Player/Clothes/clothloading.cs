using GTANetworkAPI;
namespace RageOS
{
    public class Clothes
    {
        public static void Load(Client player)
        {
            if (player.GetData("SkinC") == PedHash.FreemodeMale01)
            {             
                player.SetData("Gender", 1);                
                               
            }
            if (player.GetData("SkinC") == PedHash.FreemodeFemale01)
            {               
                player.SetData("Gender", 2);
            }
        }              
        
        public static void LoadClothing(Client player)
        {
            if (player.GetData("Skin") == PedHash.FreemodeMale01 || player.GetData("Skin") == PedHash.FreemodeFemale01)
            {    
                //if (player.HasData("Draw0") && player.HasData("Tx0") && player.GetData("Draw0") != -1) player.SetClothes(0, player.GetData("Draw0"), player.GetData("Tx0")); Gesicht...
                //if (player.HasData("Draw1") && player.HasData("Tx1") && player.GetData("Draw1") != -1) player.SetClothes(1, player.GetData("Draw1"), player.GetData("Tx1")); Masken sollen nicht automatisch gesetzt werden
                //if (player.HasData("Draw2") && player.HasData("Tx2") && player.GetData("Draw2") != -1) player.SetClothes(2, player.GetData("Draw2"), player.GetData("Tx2")); Haare...
                if (player.HasData("Draw3") && player.HasData("Tx3") && player.GetData("Draw3") != -1) player.SetClothes(3, player.GetData("Draw3"), player.GetData("Tx3")); // Körper & Arme
                if (player.HasData("Draw4") && player.HasData("Tx4") && player.GetData("Draw4") != -1) player.SetClothes(4, player.GetData("Draw4"), player.GetData("Tx4")); // Hosen
                if (player.HasData("Draw5") && player.HasData("Tx5") && player.GetData("Draw5") != -1) player.SetClothes(5, player.GetData("Draw5"), player.GetData("Tx5")); // Rucksack
                if (player.HasData("Draw6") && player.HasData("Tx6") && player.GetData("Draw6") != -1) player.SetClothes(6, player.GetData("Draw6"), player.GetData("Tx6")); // Schuhe
                if (player.HasData("Draw7") && player.HasData("Tx7") && player.GetData("Draw7") != -1) player.SetClothes(7, player.GetData("Draw7"), player.GetData("Tx7")); // Hals
                if (player.HasData("Draw8") && player.HasData("Tx8") && player.GetData("Draw8") != -1) player.SetClothes(8, player.GetData("Draw8"), player.GetData("Tx8")); // Unterhemd
                if (player.HasData("Draw9") && player.HasData("Tx9") && player.GetData("Draw9") != -1) player.SetClothes(9, player.GetData("Draw9"), player.GetData("Tx9")); // Armor
                if (player.HasData("Draw10") && player.HasData("Tx10") && player.GetData("Draw10") != -1) player.SetClothes(10, player.GetData("Draw10"), player.GetData("Tx10")); //Decals
                if (player.HasData("Draw11") && player.HasData("Tx11") && player.GetData("Draw11") != -1) player.SetClothes(11, player.GetData("Draw11"), player.GetData("Tx11")); // Jacken

                if (player.HasData("Propdraw0") && player.HasData("Proptx0") && player.GetData("Propdraw0") != -1) player.SetAccessories(0, player.GetData("Propdraw0"), player.GetData("Proptx0")); //Hüte
                if (player.HasData("Propdraw1") && player.HasData("Proptx1") && player.GetData("Propdraw1") != -1) player.SetAccessories(1, player.GetData("Propdraw1"), player.GetData("Proptx1")); //Brillen
                if (player.HasData("Propdraw2") && player.HasData("Proptx2") && player.GetData("Propdraw2") != -1) player.SetAccessories(2, player.GetData("Propdraw2"), player.GetData("Proptx2")); // Ohrringe
                if (player.HasData("Propdraw3") && player.HasData("Proptx3") && player.GetData("Propdraw3") != -1) player.SetAccessories(3, player.GetData("Propdraw3"), player.GetData("Proptx3")); 
                if (player.HasData("Propdraw4") && player.HasData("Proptx4") && player.GetData("Propdraw4") != -1) player.SetAccessories(4, player.GetData("Propdraw4"), player.GetData("Proptx4"));
                if (player.HasData("Propdraw5") && player.HasData("Proptx5") && player.GetData("Propdraw5") != -1) player.SetAccessories(5, player.GetData("Propdraw5"), player.GetData("Proptx5"));
                if (player.HasData("Propdraw6") && player.HasData("Proptx6") && player.GetData("Propdraw6") != -1) player.SetAccessories(6, player.GetData("Propdraw6"), player.GetData("Proptx6")); //Uhren
                if (player.HasData("Propdraw7") && player.HasData("Proptx7") && player.GetData("Propdraw7") != -1) player.SetAccessories(7, player.GetData("Propdraw7"), player.GetData("Proptx7")); //Armband
                if (player.HasData("Propdraw8") && player.HasData("Proptx8") && player.GetData("Propdraw8") != -1) player.SetAccessories(8, player.GetData("Propdraw8"), player.GetData("Proptx8"));
                if (player.HasData("Propdraw9") && player.HasData("Proptx9") && player.GetData("Propdraw9") != -1) player.SetAccessories(9, player.GetData("Propdraw9"), player.GetData("Proptx9"));
            }
        }
    }
}