using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WSDailyMeals
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceDailyMeals
    {

        [OperationContract]
        [WebGet(UriTemplate = "/GetData/{value}",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        Individuo GetData(string value);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
}
