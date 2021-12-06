using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DTOs;

namespace Task.UnitTest.Controller
{
    [TestClass]
    public class CategoryUnitTest
    {
        APIConsumer anyonousApiConsumer;

        [TestInitialize]
        public void TestInitialize()
        {
            anyonousApiConsumer = new APIConsumer();
        }
        [TestMethod]
        public void GetAllCategoriesTest()
        {
            var Result = anyonousApiConsumer.Get($"Category/GetAllCategories");
            Assert.AreEqual(Result.IsError, false);
        }
        [TestMethod]
        public void CreateCategory_With_Wrong_Shipment()
        {
            CategoryDTO category = new CategoryDTO
            {
                Name = "Category 1",
            };
            var result = anyonousApiConsumer.Post<CategoryDTO>($"Category/Create", category);
            Assert.AreEqual(true, result.IsError);

        }
        [TestMethod]
        public void GetCategoryById()
        {
            var headers = new Dictionary<string, object>
            {
                {"id",1 },
            };
            var result = anyonousApiConsumer.Get($"Category/GetCategoryById/", headers);
            Assert.AreEqual(true, result.IsError);

        }
        [TestMethod]
        public void GetCategoryById_WithWrong_Id()
        {
            var headers = new Dictionary<string, object>
            {
                {"id",120},
            };
            var result = anyonousApiConsumer.Get($"Category/GetCategoryById/", headers);
            Assert.AreEqual(true, result.IsError);

        }
    }
}
