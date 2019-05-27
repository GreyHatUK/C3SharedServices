using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace C3SS_Web_Main.Models
{
    [DataContract]
    public class SearchResultModel
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public object Data { get; set; }
    }
}
