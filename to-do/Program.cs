using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static string filePath = "tasks.json"; // File to store tasks

    static void Main(string[] args)
    {
        

        List<ToDoTask> tasks = LoadTasks();

        while (true)

        {
            Console.WriteLine("\n--- To-Do List ---");
            Console.WriteLine("1. View Tasks");
            Console.WriteLine("2. Add Task");
            Console.WriteLine("3. Mark Task Complete");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");

            Console.Write("\nChoose an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewTasks(tasks);
                    break;
                case "2":
                    AddTask(tasks);
                    break;
                case "3":
                    MarkTaskComplete(tasks);
                    break;
                case "4":
                    DeleteTask(tasks);
                    break;
                case "5":
                    SaveTasks(tasks);
                    return;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }

    }

    static void ViewTasks(List<ToDoTask> tasks)
    {
        Console.WriteLine("\nTasks:");
        for (int i = 0; i < tasks.Count; i++)
        {
            var status = tasks[i].IsCompleted ? "Completed" : "Pending";
            Console.WriteLine($"{i + 1}. {tasks[i].Description} - {status}");
        }
    }

    static void AddTask(List<ToDoTask> tasks)
    {
        Console.Write("\nEnter the task description: ");
        string description = Console.ReadLine();
        tasks.Add(new ToDoTask { Description = description, IsCompleted = false });
    }

    static void MarkTaskComplete(List<ToDoTask> tasks)
    {
        ViewTasks(tasks);
        Console.Write("\nEnter the task number to mark complete: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].IsCompleted = true;
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void DeleteTask(List<ToDoTask> tasks)
    {
        ViewTasks(tasks);
        Console.Write("\nEnter the task number to delete: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < tasks.Count)
        {
            tasks.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Invalid task number.");
        }
    }

    static void SaveTasks(List<ToDoTask> tasks)
    {
        string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine("Tasks saved.");
    }

    static List<ToDoTask> LoadTasks()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ToDoTask>>(json);
        }
        return new List<ToDoTask>();
    }
}
