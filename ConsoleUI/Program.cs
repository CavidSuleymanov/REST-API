using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI;

class Program
{
    static void Main(string[] args)
    {
        // ProductTest();
        // CategoryTest();

        UserManager usermanager = new UserManager(new EfUserDal());
        var user = usermanager.GetByMail("engin@engin.com");
        foreach (var claim in usermanager.GetClaims(user))
        {
            Console.WriteLine(claim.Name);
        }

    }

    private static void CategoryTest()
    {
        CategoryManager categorymanager = new CategoryManager(new EfCategoryDal());
        foreach (var category in categorymanager.GetAll().Data)
        {
            Console.WriteLine(category.CategoryName);
        }
    }

    private static void ProductTest()
    {
        ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
        //var result=productManager.GetAll();
        //Console.WriteLine(result.Message);
        //foreach (var p in result.Data)
        // {
        //    Console.WriteLine(p.ProductName);
        // }
        var result = productManager.GetProductDetails();
        // Console.WriteLine(result.Data[1].ProductName);
        if (result.Success == true)
        {
            foreach (var p in result.Data)
            {
                Console.WriteLine(p.ProductName + "..." + p.CategoryName);


            }


            //var result = productManager.Add(new Product { ProductName = "A" }
            //   );
            // Console.WriteLine(result.Message);

        }
        else
        {
            Console.WriteLine(result.Message);
        }
    }
}




