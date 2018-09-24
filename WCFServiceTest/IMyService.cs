using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net.Security;
using System.Text;

namespace WCFServiceTest
{
    //[ServiceContract(SessionMode = SessionMode.Required)]
    [ServiceContract]
    public interface IMyService
    {
        /// <summary>
        /// The operations that are to be called on HTTP GET protocol, we need to decorate them with the WebGet attribute. 
        /// The operations that will be called by protocols, like POST, PUT, DELETE will be decorated with WebInvoke attribute.
        /// </summary>

        [OperationContract]
        [WebGet(UriTemplate = "/GetJsonData/{value}", ResponseFormat = WebMessageFormat.Json)]
        string GetJsonData(string value);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GetXmlData/{value}", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, Method="GET")]
        string GetXmlData(string value);

        [OperationContract]
        [WebGet(UriTemplate = "/GetDefaultObject/*", ResponseFormat = WebMessageFormat.Xml)]
        CompositeType GetDefaultObject();


        [OperationContract(ProtectionLevel = ProtectionLevel.None, IsOneWay = false)]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    [DataContract(Name="SomeExampleDataContract")]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember(Order=10)]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
