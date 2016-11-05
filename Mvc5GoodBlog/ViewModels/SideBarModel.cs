using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5GoodBlog.ViewModels
{
    public class SideBarModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}