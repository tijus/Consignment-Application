using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLogicServiceMvc.Models
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuGroupId { get; set; }
        public string MenuGroupName { get; set; }
        public string MenuGroupUrl { get; set; }
        public string MenuUrl { get; set; }
    }
}