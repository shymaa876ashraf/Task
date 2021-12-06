using AutoMapper;
using BL.Configuration;
using BL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class BaseAppService : IDisposable
    {
        protected IUnitOfWork TheUnitOfWork { get; set; }
        protected readonly IMapper Mapper = AutoMapperProfile.mapp;
        public BaseAppService(IUnitOfWork theUnitOfWork)
        {
            TheUnitOfWork = theUnitOfWork;
        }
        public void Dispose()
        {
            TheUnitOfWork.Dispose();
        }
    }
}

