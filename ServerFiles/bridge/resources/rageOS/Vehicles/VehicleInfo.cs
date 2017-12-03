using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace RageOS.Vehicles
{
    class VehicleInfo : Script
    {

        private static Dictionary<int,String> ClassList = new Dictionary<int,String>
        {
            {  0, "Kompakt"},
            {  1, "Limusinen" },
            {  2, "SUVs" },
            {  3, "Coupes" },
            {  4, "Muscle" },
            {  5, "Sport Klassiker" },
            {  6, "Sport" },
            {  7, "Super Sport" },
            {  8, "Motorräder" },
            {  9, "Gelände" },
            { 10, "Industrie" },
            { 11, "Nutzfahrzeuge" },
            { 12, "Vans" },
            { 13, "Fahrräder" },
            { 14, "Boote" },
            { 15, "Helikopter" },
            { 16, "Flugzeuge" },
            { 17, "Service" },
            { 18, "Einsatzfahrzeuge" },
            { 19, "Militär" },
            { 20, "LKWs" },
            { 21, "Züge" }
        };

        /*  
         *  Converts a int to a VehicleName 
         *  INPUT: Vehicle Hash
         *  RETURN: VehicleName if exists, else NULL 
         */
        public static String GetName(int VehHash)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return null;
                return VehInfo.Name;
            }
        }

        /*  
         *  Converts a int to a VehicleManufactor 
         *  INPUT: Vehicle Hash
         *  RETURN: VehicleManufactor if exists, else NULL 
         */
        public static String GetManufactor(int VehHash)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return null;
                return VehInfo.Manufactor;
            }
        }

        /*  
         *  Converts a int to the MinPrice 
         *  INPUT: Vehicle Hash
         *  RETURN: MinPrice if exists, else -1 
         */
        public static Double GetMinPrice(int VehHash)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return -1;
                return VehInfo.MinPrice;
            }
        }

        /*  
         *  Converts a int to the Price 
         *  INPUT: Vehicle Hash
         *  RETURN: Price if exists, else -1 
         */
        public static Double GetPrice(int VehHash)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return -1;
                return VehInfo.Price;
            }
        }

        /*  
         *  Sets the Price of a Vehicle 
         *  INPUT: Vehicle Hash, Price
         *  RETURN: true if success, else false 
         */
        public static Boolean SetPrice(int VehHash, double Price)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return false;
            }

            if (Price < 0 || Price < VehicleInfo.GetMinPrice(VehHash) || Price > VehicleInfo.GetMaxPrice(VehHash))
                return false;

            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                VehInfo.Price = Price;
                db.SaveChanges();
            }

            return true;
        }

        /*  
         *  Converts a int to the MaxPrice 
         *  INPUT: Vehicle Hash
         *  RETURN: MaxPrice if exists, else -1 
         */
        public static Double GetMaxPrice(int VehHash)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return -1;
                return VehInfo.MaxPrice;
            }
        }

        /*  
         *  Converts a int to the VehicleClass 
         *  INPUT: Vehicle Hash
         *  RETURN: Class if exists, else -1 
         */
        public static int GetClass(int VehHash)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return -1;
                return VehInfo.Class;
            }
        }

        /*  
         *  Converts a int to the EngineType 
         *  INPUT: Vehicle Hash
         *  RETURN: EngineType if exists, else -1 
         */
        public static int GetEngineType(int VehHash)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return -1;
                return VehInfo.Enginetype;
            }
        }

        /*  
         *  Returns the Blacklist Status
         *  INPUT: Vehicle Hash
         *  RETURN: Blacklist Status if exists, else true 
         */
        public static Boolean GetBlacklist(int VehHash)
        {
            using (var db = new DBContext())
            {
                var VehInfo = db.Vehicleinfo.First(x => x.Hash == VehHash);
                if (VehInfo == null)
                    return true;
                return VehInfo.Blacklist;
            }
        }

        /*  
         *  Returns the Name of a Vehicle Class
         *  INPUT: Vehicle Class
         *  RETURN: Class Name if exists, else false 
         */
        public static String ClassToName(int VehClass)
        {
            if (ClassList.TryGetValue(VehClass, out String ClassName))
                return ClassName;
            else
                return null;
            
        }
    }
}
