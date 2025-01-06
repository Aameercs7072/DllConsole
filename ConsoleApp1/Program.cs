using System;
using MathLibrary;  // Reference to the MathLibrary
using System.Reflection;
using ConsoleApp1.Models;
//using ClassLibrary1.Model
using System.Collections.Generic;
using MySqlX.XDevAPI.Common;
using System.Xml.Linq;
using System.Collections;
using System.Reflection.PortableExecutable;

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
                int option = 5;

                do
                {
                    
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
                    else if (option == 4)
                    {
                        Console.WriteLine("GetuserById");

                        var method = type.GetMethod("GetuserById");

                        if (method == null)
                        {
                            Console.WriteLine("Method 'Getuser' not found.");
                            return;
                        }
                        Console.WriteLine("Id");
                        string? guid = Console.ReadLine();
                        Guid inputId;
                        if (guid != null)
                        {
                            inputId = Guid.Parse(guid);
                            var result = method.Invoke(obj, new Object[] { inputId });



                            if (result == null)
                            {
                                Console.WriteLine("The result is null.");
                                return;
                            }
                            else
                            {
                                GetUser? user = new GetUser();
                                var idProperty = result.GetType().GetProperty("Id");
                                if (idProperty != null)
                                {
                                    var value = idProperty.GetValue(result);
                                    if (value != null)
                                    {
                                        user.Id = (Guid)value;
                                        user.Name = Convert.ToString(result.GetType().GetProperty("Name")?.GetValue(result));
                                        user.Email = Convert.ToString(result.GetType().GetProperty("Email")?.GetValue(result));

                                        Console.WriteLine($"ID: {user.Id}");
                                        Console.WriteLine($"Name: {user.Name}");
                                        Console.WriteLine($"Email: {user.Email}");
                                    }
                                }
                            }
                            //else
                            //{
                            //    // Use reflection to get properties of the result
                            //    var id = result.GetType().GetProperty("Id")?.GetValue(result);
                            //    var name = result.GetType().GetProperty("Name")?.GetValue(result);
                            //    var email = result.GetType().GetProperty("Email")?.GetValue(result);
                            //       GetUser? userList = new GetUser();
                            //    // Print the user details
                            //    Console.WriteLine($"ID: {id}");
                            //    Console.WriteLine($"Name: {name}");
                            //    Console.WriteLine($"Email: {email}");
                            //}

                            //Console.WriteLine("The result Type: ",result.GetType);
                            //if (result is GetUser user)
                            //{
                            //    Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}");
                            //}
                            //    if (result is IEnumerable user)
                            //{
                            //    GetUser? userList = new GetUser();

                            //    var id = user.GetType().GetProperty("Id")?.GetValue(user);
                            //    var name = user.GetType().GetProperty("Name")?.GetValue(user);
                            //    var email = user.GetType().GetProperty("Email")?.GetValue(user);

                            //    if (id!= null && user != null && name != null)
                            //    {
                            //        userList = new GetUser()
                            //        {

                            //            Id = (Guid)id,
                            //            Name = Convert.ToString(name),
                            //            Email = Convert.ToString(email)
                            //        };
                            //        Console.WriteLine($"ID: {userList.Id}, Name: {userList.Name}, Email:{userList.Email}");
                            //    }
                            //    else
                            //    {
                            //        Console.WriteLine("Failed to retrieve property values.");
                            //    }


                            //}
                            //else
                            //{
                            //    Console.WriteLine("The result is not an IEnumerable.");
                            //}

                        }

                    }

                    else if (option == 5)
                    {
                        Console.WriteLine("Getuser");

                        var method = type.GetMethod("Getuser");

                        if (method == null)
                        {
                            Console.WriteLine("Method 'Getuser' not found.");
                            return;
                        }

                        var result = method.Invoke(obj, null);

                        if (result == null)
                        {
                            Console.WriteLine("The result is null.");
                            return;
                        }

                        if (result is IEnumerable users)
                        {
                            List<GetUser> userList = new List<GetUser>();
                            foreach (var user in users)
                            {
                                var id = user.GetType().GetProperty("Id")?.GetValue(user);
                                var name = user.GetType().GetProperty("Name")?.GetValue(user);
                                var email = user.GetType().GetProperty("Email")?.GetValue(user);

                                if (id != null && name != null)
                                {
                                    GetUser User = new GetUser()
                                    {

                                        Id = (Guid)id,
                                        Name = Convert.ToString(name),
                                        Email = Convert.ToString(email)
                                    };
                                    userList.Add(User);
                                }
                                else
                                {
                                    Console.WriteLine("Failed to retrieve property values.");
                                }


                            }
                            foreach (var user in userList)
                            {
                                Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email:{user.Email}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The result is not an IEnumerable.");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid option selected.");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Enter option");
                    string? input = Console.ReadLine();  // Read the input as a string
                    option = Convert.ToInt32(input);
                }
                while (true);


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
