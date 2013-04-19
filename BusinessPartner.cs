using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficelinerMobileWebService
{
    public abstract class BusinessPartner
    {
        public String Name { get; set; }
    }

    public class BusinessPartnerResponse
    {
        public List<BusinessPartner> Result { get; set; }
    }
}