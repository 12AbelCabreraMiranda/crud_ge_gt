using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace crud_genesis.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(225)]
        public string UserPassword { get; set; }
    }
}