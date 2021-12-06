using BL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.Bases;
using Task.DTOs;
using Task.DAL.DbModels;

namespace BL.AppService
{
    public class CategoryAppService: BaseAppService
    {
        public CategoryAppService(IUnitOfWork theUnitOfWork) : base(theUnitOfWork)
        {

        }

        public List<CategoryDTO> GetAllCateogries()
        {

            return Mapper.Map<List<CategoryDTO>>(TheUnitOfWork.Category.GetAllCategory());
        }
        public CategoryDTO GetCategory(int id)
        {
            return Mapper.Map<CategoryDTO>(TheUnitOfWork.Category.GetById(id));
        }



        public bool AddNewCategory(CategoryDTO categoryViewModel)
        {
            if (categoryViewModel == null)

                throw new ArgumentNullException();

            bool result = false;
            var category = Mapper.Map<Category>(categoryViewModel);
            if (TheUnitOfWork.Category.Insert(category))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateCategory(CategoryDTO categoryViewModel)
        {
            var category = Mapper.Map<Category>(categoryViewModel);
            TheUnitOfWork.Category.Update(category);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteCategory(int id)
        {
            bool result = false;

            TheUnitOfWork.Category.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckCategoryExists(CategoryDTO categoryViewModel)
        {
            Category category = Mapper.Map<Category>(categoryViewModel);
            return TheUnitOfWork.Category.CheckCategoryExists(category);
        }
        public int CountEntity()
        {
            return TheUnitOfWork.Category.CountEntity();
        }
        public IEnumerable<CategoryDTO> GetPageRecords(int pageSize, int pageNumber)
        {
            return Mapper.Map<List<CategoryDTO>>(TheUnitOfWork.Category.GetPageRecords(pageSize, pageNumber));
        }
    }
}
