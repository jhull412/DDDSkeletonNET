using DDDSkeletonNET.Infrastructure.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSkeletonNET.Portal.Domain.ValueObjects
{
    public static class ValueObjectBusinessRule
    {
        public static readonly BusinessRule CityInAddressRequired = new BusinessRule("An address must have a city.");
    }
}
