using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {

        public static string ProductAdded = "Urun Eklendi";
        public static string ProductNameInvalid = "Urun Ismi Gecersiz";
        public static string MaintenanceTime="Sistem bakimda";
        public static string ProductsListed="Urunler Listelendi";

        public static string ProductCountOfCategoryError = "Bir kateqoride 10dan fazla urun olamaz";

        public static string ProductNameAlreadyExists = "Bu isimde zaten basqa urun var";

        public static string CategoryLimitExceded = "Kategori limiti asildiqi ucun yeni urun eklenemeiyor";
    }
}
