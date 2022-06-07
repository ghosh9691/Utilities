using Microsoft.AspNetCore.Mvc;

namespace PrabalGhosh.Utilities.Attributes
{
    public class MultiPolicyAuthorizeAttribute : TypeFilterAttribute
    {
        public MultiPolicyAuthorizeAttribute(string policies, bool isAnd = false)
            : base(typeof(MultiPolicyAuthorizeFilter))
        {
            Arguments = new object[] { policies, isAnd };
        }
    }
}