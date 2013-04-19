using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficelinerMobileWebService
{
    public class BusinessPartnerMethods
    {
        public BusinessPartnerResponse GetBusinessPartner()
        {
            var result = new List<BusinessPartner>();
            result.Add(new Person { Name = "Peter", LastName = "Pan" });
            result.Add(new Company { Name = "Wkg", Addition = "GmbH" });

            return new BusinessPartnerResponse { Result = result };
        }
    }
}