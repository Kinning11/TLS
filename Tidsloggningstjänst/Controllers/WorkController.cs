using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Tidsloggningstjänst.Models;

namespace Tidsloggningstjänst.Controllers
{
    public class WorkController : Controller
    {
        public IActionResult Index()
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Work>("works");

            List<Work> works = collection.Find(w => true).ToList();     

            return View(works);
        }

        public IActionResult Create()
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Employee>("employees");

            List<Employee> employees = collection.Find(e => true).ToList();

            return View(employees);
        }
        [HttpPost]
        public IActionResult Create(Work work)
        {
            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Work>("works");

            collection.InsertOne(work);

            return Redirect("/Work");

        }

        public IActionResult Show(string Id)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Work>("works");

            Work work = collection.Find(w => w.Id == workId).FirstOrDefault();

            return View(work);

        }

        [HttpPost]
        
        public IActionResult Delete (string Id)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Work>("works");

            collection.DeleteOne(w => w.Id == workId);

            return Redirect("/Work");

        }

        public IActionResult Edit(string Id)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Work>("works");

            Work work = collection.Find(w => w.Id == workId).FirstOrDefault();

            return View(work);
        }

        [HttpPost]
        public IActionResult Edit(string Id, Work work)
        {
            ObjectId workId = new ObjectId(Id);

            MongoClient dbClient = new MongoClient();
            var database = dbClient.GetDatabase("time_logging_service");
            var collection = database.GetCollection<Work>("works");

            work.Id = workId;
            collection.ReplaceOne(w => w.Id == workId, work);

            return Redirect("/Work");   
        }
        public IActionResult ShowWork(string Id)
        {
            List<WorkViewModel> viewModel = new List<WorkViewModel>();

            MongoClient dbClient = new MongoClient();

            var database = dbClient.GetDatabase("time_logging_service");
            var employeesCollection = database.GetCollection<Employee>("employees");
            var worksCollection = database.GetCollection<Work>("works");

            

            List<Work> works = worksCollection.Find(w => w.EmployeeId == Id).ToList();
            
            int totalWorkTime = works.Sum(w => w.Time);
            
            foreach (Work work in works)
            {
                ObjectId employeeId = new ObjectId(work.EmployeeId);
                Employee emoployee = employeesCollection.Find(e => e.Id == employeeId).FirstOrDefault();

                WorkViewModel model = new WorkViewModel();
                
                model.WorkDescription = work.Description;
                model.WorkTime = work.Time;
                model.EmployeeName = emoployee.Name;
                viewModel.Add(model);
            }

            ViewData["TotalWorkTime"] = totalWorkTime;



            return View(viewModel);

            
        }

    }
}
