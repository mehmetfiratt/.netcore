using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Business.Abstract;
using Business.BusinessAspect;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.RuleForBusiness;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching.MemoryCache;
using Core.CrossCuttingConcerns.Logging.Log4net;
using Core.CrossCuttingConcerns.Logging.Log4net.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Helpers;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IBusinessRules _businessRules;

        public ProductManager(IProductDal productDal, IBusinessRules businessRules)
        {
            _productDal = productDal;
            _businessRules = businessRules;
        }


        [ValidationAspect(typeof(ProductValidator), Priority = 2)]
        [SecuredOperation("Admin", Priority = 1)]
        public IResult Add(Product product)
        {
            IResult result = BusinessRulesRunner.Run(_businessRules.CheckIfProductNameExists(product.ProductName));
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(ProductValidator))]
        //[CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        // [SecuredOperation("Product.List,Admin")]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        //[SecuredOperation("Admin,Product.List")]
        public IDataResult<List<Product>> GetList()
        {
            //Thread.Sleep(5000);

            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Product>> GetListByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

    }
}
