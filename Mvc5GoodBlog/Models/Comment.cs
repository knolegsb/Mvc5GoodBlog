//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mvc5GoodBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Comment
    {        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public System.DateTime Created { get; set; }
        public int PostId { get; set; }
    
        public virtual Post Post { get; set; }
    }
}
