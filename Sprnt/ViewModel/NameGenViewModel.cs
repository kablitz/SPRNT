using Sprinter.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sprinter.ViewModel
{
    public class NameGenViewModel
    {
        [Required]
        public string FirstWord { get; set; }

        [Required]
        public string SecondWord { get; set; }

        [Required]
        public string ThirdWord { get; set; }

        public string Name { get; set; }
    }
}