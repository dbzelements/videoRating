using System;

namespace ProductApi.Models{
 public class Movie{

        public int  Id { get; set; }
        public string  Title { get; set; }
        public int  Category { get; set; }
        public int  Rating { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }

    }


    public class MovieDisplay{

        public int  Id { get; set; }
        public string  Title { get; set; }
        public string  CategoryName { get; set; }
        public int  Rating { get; set; }

    }


     public class Category{
        public int  Id { get; set; }
        public string  CategoryName { get; set; }
        
    }

}