using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using B3T2.Models;

namespace B3T2.Models
{
    public class User
    {
        public int userid {get;set;}
        public string name {get;set;}  
      
        public string alias {get;set;}
    
        public string email {get;set;}
      
        public string password {get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime createdAt {get;set;}

        public int ideasid {get;set;}
        public List<Ideas> ideas {get;set;}
        public int likedid {get;set;}

        public List<Liked> liked {get;set;}

       
        public User()
        {
            liked = new List<Liked>();
            ideas= new List<Ideas>();
           
        }
    }
}