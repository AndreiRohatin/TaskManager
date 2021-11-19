using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Task = TaskManager.Models.Task;

namespace TaskManager
{
    public class Program
    {
        public static Task MasterNode=new Task();
        
        public static List<Task> Tasks = new List<Task>();

        private static List<string> taskNames = new List<string>(){"Client Meeting", "Team building", "UI/UX Research", "Backend securities","Project Management","Sales","Implementation","Quality Assurance","Pentesting","Bug fixing"};

        private static List<string> assigneeNames = new List<string>() {"Andrei","Alex","Mihai","Liviu","Adriana","Alexandra","Mihaela","Oana","Tudor"}; 
        
        public static void Main(string[] args)
        {
            Tasks.Add(new Task(){AssignedTo = String.Empty,Name = "Task 1",UID = Guid.NewGuid().ToString()});
            Tasks.Add(new Task(){AssignedTo = String.Empty,Name = "Task 2",UID = Guid.NewGuid().ToString()});
            Tasks.Add(new Task(){AssignedTo = String.Empty,Name = "Task 3",UID = Guid.NewGuid().ToString()});
            Tasks.Add(new Task(){AssignedTo = String.Empty,Name = "Task 4",UID = Guid.NewGuid().ToString()});
            Tasks.Add(new Task(){AssignedTo = String.Empty,Name = "Task 5",UID = Guid.NewGuid().ToString()});
            Random random = new Random();
            foreach (Task task in Tasks)
            {
                //generate random subtasks from 1-4 for existing tasks 
                int rand = random.Next(1, 4);
                List<Task> leaves;
                while (rand-- != 0)
                {
                    leaves = new List<Task>();
                    //generate random number of leaves for tree
                    int randAuxiliary = random.Next(1, 4);
                    for (; randAuxiliary > 0; --randAuxiliary) leaves.Add(new Task(){AssignedTo = assigneeNames[random.Next(assigneeNames.Count)], Name = taskNames[random.Next(taskNames.Count)],UID = Guid.NewGuid().ToString()});
                    task.Children = leaves;
                }
            }

            foreach (Task parent in Tasks)
            {
                foreach (Task task in parent.Children)
                {
                    //generate random subtasks from 1-4 for existing tasks 
                    int rand = random.Next(1, 4);
                    List<Task> leaves;
                    while (rand-- != 0)
                    {
                        leaves = new List<Task>();
                        //generate random number of leaves for tree
                        int randAuxiliary = random.Next(1, 4);
                        for (; randAuxiliary > 0; --randAuxiliary) leaves.Add(new Task(){AssignedTo = assigneeNames[random.Next(assigneeNames.Count)], Name = taskNames[random.Next(taskNames.Count)],UID = Guid.NewGuid().ToString()});
                        task.Children = leaves;
                    }
                }
            }

            MasterNode.Children = Tasks;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}