using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOAprototype.Models
{
    public class ITFunction
    {
        [Display(Name = "Name")]
        public string Id { get; set; }

        [Display(Name = "Wikipedia Page")]
        public string WikiPage { get; set; }
    }
}
