using System;
using MathLibrary;  // Reference to the MathLibrary
using System.Reflection;
using ConsoleApp1.Models;
using ClassLibrary1.Model;
using System.Collections.Generic;
using MySqlX.XDevAPI.Common;
using System.Xml.Linq;

namespace MathApp
{
    class Program
    {

        public static void Main(string[] args)
        {
            string dllFile = @"C:\Users\New Joinee\Projects\ClassLibrary1\ClassLibrary1\bin\Debug\net8.0\ClassLibrary1.dll";


            var assembly = Assembly.LoadFile(dllFile);
            Type? type = assembly.GetType("ClassLibrary1.Class1");
            if (type != null)
            {
                var obj = Activator.CreateInstance(type);

                string? more = "";
                do
                {
                    Console.WriteLine("Enter option");
                    string? input = Console.ReadLine();  // Read the input as a string
                    int option = Convert.ToInt32(input);
                    if (option == 1)
                    {
                        Console.WriteLine("Adduser");
                        var method = type.GetMethod("Adduser");
                        ConsoleApp1.Models.NewUser user = new ConsoleApp1.Models.NewUser();
                        Console.WriteLine("Name");
                        string? name = Console.ReadLine();
                        Console.WriteLine("Email");

                        string? email = Console.ReadLine();

                        Console.WriteLine("Password");
                        string? password = Console.ReadLine();

                        if (name != null && email != null && password != null && method != null)
                        {
                            var result = method.Invoke(obj, new Object[] { name, email, password });

                            if (result != null)
                            {
                                bool isUserAdded = (bool)result;

                                // Output the result
                                if (isUserAdded)
                                {
                                    Console.WriteLine("User added successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("User addition failed.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("error in result");
                        }



                    }

                    else if (option == 2)
                    {
                        //try
                        //{
                        Console.WriteLine("Updateuser");
                        var method = type.GetMethod("Updateuser");
                        ConsoleApp1.Models.NewUser user = new ConsoleApp1.Models.NewUser();
                        Console.WriteLine("Id");
                        string? guid = Console.ReadLine();
                        if (guid != null)
                        {
                            Guid id = Guid.Parse(guid);
                            Console.WriteLine("Name");
                            string? name = Console.ReadLine();
                            Console.WriteLine("Email");

                            string? email = Console.ReadLine();

                            Console.WriteLine("Password");
                            string? password = Console.ReadLine();
                            if (name != null && email != null && password != null && method != null)
                            {
                                var result = method.Invoke(obj, new Object[] { id, name, email, password });
                                if (result != null)
                                {
                                    bool isUserUpdated = (bool)result;
                                    if (isUserUpdated)
                                    {
                                        Console.WriteLine("User Updated successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("User updation failed.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("error in result");
                                }
                            }
                        }
                        //}
                        //catch (Exception)
                        //{
                        //    Console.WriteLine("error ");
                        //}
                    }
                    else if (option == 3)
                    {
                        Console.WriteLine("Deleteuser");
                        var method = type.GetMethod("Deleteuser");
                        ConsoleApp1.Models.NewUser user = new ConsoleApp1.Models.NewUser();
                        Console.WriteLine("Id");
                        string? guid = Console.ReadLine();
                        Guid id;
                        if (guid != null && method != null)
                        {
                            id = Guid.Parse(guid);




                            var result = method.Invoke(obj, new Object[] { id });
                            if (result != null)
                            {
                                bool isUserDeleted = (bool)result;

                                // Output the result
                                if (isUserDeleted)
                                {
                                    Console.WriteLine("User Deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("User Deletion failed.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("error in result");
                            }
                        }
                    }
                    //if (option == 4)
                    //{
                    //    Console.WriteLine("Getuser");
                    //    var method = type.GetMethod("Getuser");
                    //    var result = method.Invoke(obj, new object[] { });
                    //    Console.WriteLine("Result",result);

                    //    List<ConsoleApp1.Models.GetUser> users = result as List<ConsoleApp1.Models.GetUser>;
                    //    Console.WriteLine("User",users);
                    //    if (users.Count > 0)
                    //        {
                    //            foreach (var user in users)
                    //            {
                    //                Console.WriteLine($"{user.Id}  {user.Name}  {user.Email}");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine("User not found.");
                    //        }

                    //}
                    if (option == 4)
                    {
                        Console.WriteLine("Getuser");

                        // Get the Getuser method dynamically
                        var method = type.GetMethod("Getuser");

                        if (method == null)
                        {
                            Console.WriteLine("Method 'Getuser' not found.");
                            return;
                        }

                        // Invoke the method to get the result
                        //var result = method.Invoke(obj, new object< ClassLibrary1.Model.GetUser > [] { });
                        List<ConsoleApp1.Models.GetUser> result = method.Invoke(obj, null);


                     
                        // Ensure that result is not null
                        if (result != null)
                        {
                            foreach (ConsoleApp1.Models.GetUser user in result)
                            {
                                Console.WriteLine(user.Name);
                            }
                        }

                        Console.WriteLine($"Returned result type: {result.GetType()}");
                        Console.WriteLine("Result",result);

                            List<ClassLibrary1.Model.GetUser>? getUsers = (List < ClassLibrary1.Model.GetUser > )result;
                        List<ConsoleApp1.Models.GetUser> consoleAppModels = new List<ConsoleApp1.Models.GetUser>();
                        // Check if the casting succeeded
                        if (getUsers != null)
                            {
                                // Map List<ClassLibrary1.Model.GetUser> to List<ConsoleApp1.Models.GetUser>
                                List<ConsoleApp1.Models.GetUser> users = getUsers.Select(g => new ConsoleApp1.Models.GetUser
                                {
                                    Id = g.Id,
                                    Name = g.Name,
                                    Email = g.Email
                                }).ToList();

                                // Check if the list contains any users
                                if (users.Count > 0)
                                {
                                    foreach (var user in users)
                                    {
                                        Console.WriteLine($"{user.Id}  {user.Name}  {user.Email}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("User not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Unable to cast the result to List<ClassLibrary1.Model.GetUser>.");
                            }
                       
                    }

                    else
                    {
                        Console.WriteLine("Invalid option selected.");
                    }

                    Console.WriteLine("Have to do more CRUD, press any letter");
                    more = Console.ReadLine();
                }
                while (more != null);


            }
            //foreach (Type type in assembly.GetTypes())
            //{
            //    Console.WriteLine($"  Methods in type: {type.FullName}");
            //    foreach (MethodInfo method in type.GetMethods())
            //    {
            //        Console.WriteLine($"  Method: {method.Name}");
            //    }
            //}

            //Console.WriteLine(result);
            //Console.WriteLine(mulresult);


            Console.Read();
        }
    }
}
