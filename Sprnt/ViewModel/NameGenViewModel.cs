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
        [Required(ErrorMessage="First word type is required.")]
        public string FirstWord { get; set; }

        [Required(ErrorMessage = "Second word type is required.")]
        public string SecondWord { get; set; }

        [Required(ErrorMessage = "Third word type is required.")]
        public string ThirdWord { get; set; }

        public string Name { get; set; }
    }
}