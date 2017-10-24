using System.ComponentModel.DataAnnotations;

namespace B3T2.Models
{
    public class regvalidate
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
        ErrorMessage = "Characters are not allowed in first name.")]
        public string name {get;set;}
        
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
        ErrorMessage = "Characters are not allowed in last name.")]
        public string alias {get;set;}
    
        [Required]
        [EmailAddress]
        public string email {get;set;}
        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$#!%^()*?&])[A-Za-z\d$@$!%*?&]{8,32}$", 
        ErrorMessage = "Password must be 8 characters long containing a number, a letter and a special character.")]
        [DataType(DataType.Password)]
        public string password {get;set;}
        [Required]
        [MinLength(8)]
        [Compare("password")]
        [DataType(DataType.Password)]
        public string confirm {get;set;}

    }
}