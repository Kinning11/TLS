using MongoDB.Bson;

namespace Tidsloggningstjänst.Models
{
    public class Work
    {
        public ObjectId Id { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public string EmployeeId { get; set; }
        


    }
}
