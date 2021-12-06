using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DAL.DbModels;
using Task.DTOs;
using Task.DTOs.DTOs;

namespace BL.Configuration
{
    public class AutoMapperProfile
    {
        public static IMapper mapp { get; set; }
         static AutoMapperProfile()
        {

            var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<Category, CategoryDTO>().ReverseMap();

            });
            mapp = config.CreateMapper();

        }
    }
}

