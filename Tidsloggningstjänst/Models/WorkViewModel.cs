using MongoDB.Bson;

namespace Tidsloggningstjänst.Models
{
    public class WorkViewModel
    {
       
        public string EmployeeName { get; set; }
        public string WorkDescription { get; set; }

        public int WorkTime { get; set; }
    }
}
