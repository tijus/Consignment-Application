using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLogicServiceMvc.Models
{
    public class MessageModel
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public dynamic ResultObject { get; set; }
    }
}