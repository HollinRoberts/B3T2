using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using B3T2.Models;
using System.ComponentModel.DataAnnotations;

namespace B3T2.Models
{
    public class IdeasValidate
    {   
       
        [Required]
        [MinLength(10)]
        public string idea {get;set;}

        
    }
}