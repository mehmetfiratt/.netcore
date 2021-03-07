using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public class BusinessRulesRunner
    {
        public static IResult Run(params IResult[] logic)
        {
            foreach (var result in logic)
            {
                if (!result.Success)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
