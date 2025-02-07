using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Tidsloggningstjänst.Models;

namespace Tidsloggningstjänst.Controllers
{
    public class EmployeeController : Controller 
    {
        public IActionResult Index()
        {


            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Employee>("employees");

            List<Employee> employees = collection.Find(e => true).ToList();

            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Employee>("employees");

            collection.InsertOne(employee);

           return Redirect("/Employee");
        }

        public IActionResult Show(string Id)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Employee>("employees");

            Employee employee = collection.Find(e => e.Id == employeeId).FirstOrDefault();

            return View(employee);
        }

        public IActionResult Edit(string Id)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Employee>("employees");

            Employee employee = collection.Find(e => e.Id == employeeId).FirstOrDefault();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(string Id, Employee employee)
        {

            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Employee>("employees");

            employee.Id = employeeId;

            collection.ReplaceOne(e => e.Id == employeeId, employee);

            return Redirect("/Employee");
        }
        [HttpPost]
        public IActionResult Delete(string Id)
        {
            ObjectId employeeId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Employee>("employees");

            collection.DeleteOne(e => e.Id == employeeId);

            return Redirect("/Employee");
        }
    }

   

}

