using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqTest
{
    public class BusinessLayerCustomer
    {
        protected ICustomer _Customer = null;
        public BusinessLayerCustomer(ICustomer customer)
        {
            _Customer = customer;
        }

        public void DeActivateCustomer()
        {
            if (_Customer.IsActive)
            {
                _Customer.DeActivate();
            }
        }
    } 
}
