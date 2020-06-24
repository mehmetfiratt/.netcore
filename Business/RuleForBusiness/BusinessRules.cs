using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.RuleForBusiness
{
    public class BusinessRules : IBusinessRules
    {
        private readonly IProductDal _productDal;

        public BusinessRules(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult CheckIfProductNameExists(string name)
        {
            if (_productDal.Get(p => p.ProductName == name) != null)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
