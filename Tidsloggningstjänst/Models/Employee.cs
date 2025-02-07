using MongoDB.Bson;
using System.Globalization;

namespace Tidsloggningstjänst.Models
{
    public class Employee
    {
        public ObjectId Id { get; set; }
        public String Name { get; set; }
        public int BirthYear { get; set; }
    }
}
