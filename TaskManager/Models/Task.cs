using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TaskManager.Helpers;

namespace TaskManager.Models
{
    //attribute needed to serialize object of type Task
    [JsonConverter(typeof(TaskSerializerConverter))]
    public class Task
    {
        #region privateMembers
        //task name
        private string name;
        //person who's assigned the task, top level tasks won't have any name displayed
        private string assignedTo; 
        
        //list of subtasks
        //using a tree data structure wasn't the best idea, a faster method would be a dictionary and saving the parent key in a variable instead
        private List<Task> children;

        //uid used to identify and lazy load given task when clicking the dropdown arrow
        private string uid;
        #endregion

        #region publicMemebers
        public string Name
        {
            get => !string.IsNullOrWhiteSpace(name) ? name : string.Empty;
            set => name = !string.IsNullOrWhiteSpace(value) ? value : string.Empty;
        }
        public List<Task> Children
        {
            get => children ?? new List<Task>();
            set => children = value ?? new List<Task>();
        }
        public string AssignedTo
        {
            get => !string.IsNullOrWhiteSpace(assignedTo) ? assignedTo : string.Empty;
            set => assignedTo = !string.IsNullOrWhiteSpace(value) ? value : string.Empty;
        }

        public string UID
        {
            get => !string.IsNullOrWhiteSpace(uid) ? uid : string.Empty;
            init => uid = !string.IsNullOrWhiteSpace(value) ? value : string.Empty;
        }
        #endregion

        public Task()
        {
            name=string.Empty;
            assignedTo=string.Empty;
            children = new List<Task>();
        }

        public Task(string name, string assignedTo, List<Task> tasks)
        {
            this.name = name;
            this.assignedTo = assignedTo;
            this.children = tasks;
        }
        
    }
    static class TaskExtensions
    {
        public static IEnumerable<Task> Descendants(this Task node)
        {
            //it's not efficient and will cause memory overflow if the tree it's big
            return node.Children.Concat(node.Children.SelectMany(n => n.Descendants()));
        }
    }
}