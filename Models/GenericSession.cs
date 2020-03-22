using System.ComponentModel.DataAnnotations;

namespace analytics.Models
{
    //created on a homepage view
    public class GenericSession
    {
        [Key]
        public int session_id{get;set;}
        
        //time on homepage
        public int time_on_homepage{get;set;}
    }
}