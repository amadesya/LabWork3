namespace ErrorsApp
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        class User
        {
            public string Name;
            public int Age;

            public User(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }


        static Dictionary<string, string> userCredentials = new Dictionary<string, string>();

        static List<User> users = new List<User>();


        static void Main(string[] args)
        {
            bool exit = false;
            bool isAuthenticated = false;

            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                if (!isAuthenticated)
                {
                    Console.WriteLine("1. Авторизоваться");
                    Console.WriteLine("2. Зарегистрироваться");
                    Console.WriteLine("3. Выйти из программы");
                }
                else
                {
                    //ошибка 11 - опечатка, нужно "Добавить пользователя"
                    Console.WriteLine("1. Добавить пользователя");
                    Console.WriteLine("2. Удалить пользователя");
                    //ошибка 12 - опечатка, пункт должен быть не 2, а 3
                    Console.WriteLine("3. Найти пользователя по имени");
                    Console.WriteLine("4. Вывести всех пользователей");
                    Console.WriteLine("5. Выйти из учетной записи");
                    Console.WriteLine("6. Выйти из программы");
                }

                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();

                if (!isAuthenticated)
                {
                    switch (choice)
                    {
                        case "1":
                            isAuthenticated = Authorize();
                            break;
                        case "2":
                            Register();
                            break;
                        case "3":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case "1":
                            AddUser();
                            break;
                        case "2":
                            RemoveUser();
                            break;
                        case "3":
                            FindUser();
                            break;
                        case "4":
                            DisplayUsers();
                            break;
                        case "5":
                            isAuthenticated = false;
                            Console.WriteLine("Вы вышли из учетной записи.");
                            break;
                        case "6":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
            }
        }

        static bool Authorize()
        {
            Console.WriteLine("Введите имя пользователя:");
            string username = Console.ReadLine().Trim();

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine().Trim();

            //ошибка 2 - при вводе несуществующих данных программа останавливалась
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (userCredentials.ContainsKey(username))
                {
                    if (userCredentials[username] == password)
                    {
                        Console.WriteLine("Успешная авторизация!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Неверный пароль.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Пользователь не найден.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Имя пользователя и пароль не могут быть пустыми.");
                return false;
            }
        }



        static void Register()
        {
            Console.WriteLine("Введите имя пользователя для регистрации:");
            string username = Console.ReadLine();

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Имя пользователя не может быть пустым.");
            }

            if (userCredentials.ContainsKey(username))
            {
                Console.WriteLine("Пользователь с таким именем уже существует.");
                return; //ошибка 4 - в условии нет ни return, ни возвращения к методу из-за чего при добавлении такого же пользователя возникает ошибка
            }

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            if (password.Length < 8)
            {
                Console.WriteLine("Пароль слишком короткий. Минимум 8 символов.");
                return; //ошибка 3 - программа позволяет создать пользователя, хотя пароль короткий
            }

            userCredentials.Add(username, password);
            Console.WriteLine("Пользователь успешно зарегистрирован.");
        }

        static void AddUser()
        {
            Console.WriteLine("Введите имя пользователя:");

            /*ошибка 4 - нет проверочного ограничения для добавления пользователя,  
            возможно добавить в друзья пользователя, которого нет на сайте*/
            string name = Console.ReadLine();

            if (!userCredentials.ContainsKey(name))
            {
                Console.WriteLine("Пользователь не найден");
                return;
            }

            Console.WriteLine("Введите возраст пользователя:");
            //ошибка 5 - программа позволяет пользователю писать любые символы, из-за чего программа ломается
            int age;

            while (!int.TryParse(Console.ReadLine(), out age))
            {
                Console.WriteLine("Попробуйте еще раз");
            }

            //ошибка 6 - нет проверки условия, что пользователю не может быть 0 лет
            if (age < 0 || age == 0)
            {
                Console.WriteLine("Возраст должен быть положительным числом.");
                return;
            }

            else
            {
                Console.WriteLine($"Успешно введен возраст: {age}");
            }

            //ошибка 7 - пользователь сам себя может добавить в друзья

            //if (name != username)
            //{
            //    users.Add(new User(name, age));
            //    Console.WriteLine($"Пользователь {name} успешно добавлен.");
            //}
            //else
            //{
            //    Console.WriteLine("Вы не можете добавить самого себя в друзья.");
            //}

        }

        //ошибка 14 - метод не работает
        static void RemoveUser()
        {
            Console.WriteLine("Введите имя пользователя для удаления:");
            string name = Console.ReadLine();

            User userToRemove = users.Find(u => u.Name == name);

            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                Console.WriteLine("Пользователь удален.");
            }
            else
            {
                Console.WriteLine("Пользователь не найден.");
            }
        }

        //ошибка 13 - метод не работает
        static void FindUser()
        {
            Console.WriteLine("Введите имя пользователя для поиска:");
            string name = Console.ReadLine().Trim();

            User userFound = users.Find(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (userFound != null)
            {
                //ошибка 16 - нет интерполяции
                Console.WriteLine($"Найден пользователь: {userFound.Name}, возраст {userFound.Age}");
            }
            else
            {
                Console.WriteLine("Пользователь не найден.");
            }
        }

        static void DisplayUsers()
            {
                if (users.Count == 0)
                {
                    Console.WriteLine("Список пользователей пуст.");
                    return; //ошибка 1 -  нет точки с запятой
                }

                //ошибка 8 - Пользователей не выводит на экран
                for (int i = 0; i <= users.Count; i++)
                {
                    Console.WriteLine($"Имя: {users[i].Name}, Возраст: {users[i].Age}");
                }

            }
        }
    }


