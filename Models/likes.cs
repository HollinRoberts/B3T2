using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using B3T2.Models;

namespace B3T2.Models
{
    public class Liked
    {
        public int likedid {get;set;}
        public int userid {get;set;}

        public User User{get;set;}
      
        public int ideasid {get;set;}

        public Ideas ideas{get;set;}
     
    }
}