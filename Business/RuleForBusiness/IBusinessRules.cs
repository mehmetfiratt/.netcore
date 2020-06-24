using Core.Utilities.Results;

namespace Business.RuleForBusiness
{
    public interface IBusinessRules
    {
        IResult CheckIfProductNameExists(string name);
    }
}
