using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceTest
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/magazine/cc163590.aspx
    /// http://msdn.microsoft.com/en-us/magazine/dd315413.aspx
    /// http://www.codeproject.com/Articles/571813/A-Beginners-Tutorial-on-Creating-WCF-REST-Services
    /// http://www.codeproject.com/Articles/803409/REST-enabled-WCF-service
    /// http://www.codeproject.com/Articles/86007/ways-to-do-WCF-instance-management-Per-call-Per
    /// </summary>
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class MyServiceExample : IMyService
    {
        private int _incrementMe = 0;

        public string GetJsonData(string value)
        {
            _incrementMe += int.Parse(value);
            return string.Format("You entered: {0:D3}", _incrementMe);
        }

        public string GetXmlData(string value)
        {
            _incrementMe += int.Parse(value);
            return string.Format("You entered: {0:D3}", _incrementMe);
        }

        public CompositeType GetDefaultObject()
        {

            return new CompositeType() { BoolValue = false, StringValue = "yyoyoyo madafaka!"};
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
