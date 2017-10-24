using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using B3T2.Models;

namespace B3T2.Models
{
    public class Ideas
    {
        public int ideasid {get;set;}
        public string idea {get;set;}

        public int userid{get;set;}

        public User author{get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime createdAt {get;set;}

        public int likedid {get;set;}

        public List<Liked> liked {get;set;}

        public Ideas()
        {
            liked = new List<Liked>();
        }
    }
}