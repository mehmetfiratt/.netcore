using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface IProductService
   {
       IResult Add(Product product);
       IResult Delete(Product product);
       IResult Update(Product product);
       IDataResult<Product> GetById(int productId);
       IDataResult<List<Product>> GetList();
       IDataResult<List<Product>> GetListByCategoryId(int categoryId);
    } 
}
