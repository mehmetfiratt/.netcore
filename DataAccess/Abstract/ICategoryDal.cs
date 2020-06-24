using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Abstract
{
   public interface ICategoryDal:IEntityRepository<Category>
    {
    }
}
