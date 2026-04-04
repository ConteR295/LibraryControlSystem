using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryControlSystem_Persistence;
using LibraryControlSystem_Application.Services;
using LibraryControlSystem_Application;
using LibraryControlSystem_Domain.Interfaces;
using LibraryControlSystem_Infrastructure;
using static System.Console;
using System.ComponentModel;

namespace LibraryControlSystem_API;

public static class UserInput
{
    public async static void Run(ServiceProvider provider)
    {
        Console.WriteLine("--LibraryControlSystem version-1.0--");
        var breakCycle = false;

        while(!breakCycle)
        {
            Console.WriteLine();
            Console.WriteLine("Выберите сущность,с которой хотите сделать операцию:");
            Console.WriteLine("0-Книга");
            Console.WriteLine("1-Автор");
            Console.WriteLine("2-Пользователь");
            Console.WriteLine("3-прекратить работу программы");

            var userInput = Console.ReadLine();

            var mainCommandIsNumber = int.TryParse(userInput,out var mainCommandNumber);

            if(!mainCommandIsNumber)
            {
                Console.WriteLine("Введёння команда должна быть цифрой от 0 до 2 !");
                continue;
            }

            switch(mainCommandNumber)
            {
                case 0:
                {
                    var bookOperations = provider.GetService<BookOperationsService>();

                    if(bookOperations is null)
                    {
                        throw new Exception("BookOperationsService не найден!");
                    }

                    Console.WriteLine("Все операции с книгами:");
                    Console.WriteLine("0-удалить книгу по id");
                    Console.WriteLine("1-добавить книгу");
                    Console.WriteLine("2-Вывести список всех книг");
                    Console.WriteLine("3-Вывести список всех книг по id автора");
                    Console.WriteLine("4-Вывести список всех книг определённого жанра");
                    
                    var secondUserInput = Console.ReadLine();
                    var commandIsNumber = int.TryParse(secondUserInput,out var commandNumber);

                    if(!commandIsNumber)
                    {
                        Console.WriteLine("Введёння команда должна быть цифрой от 0 до 4 !");
                        continue;
                    }

                    switch(commandNumber)
                    {
                        case 0:
                        {
                            Console.WriteLine("Введите id книги,которую хотите удалить");

                            var deleteId = Console.ReadLine();
                            var isNumber = int.TryParse(deleteId,out var id);

                            if(!isNumber)
                            {
                                Console.WriteLine("Id должен быть числом!");
                                continue;
                            }

                            await bookOperations.DeleteBook(id);
                            Console.WriteLine($"Книга с id {id} удалена!");
                        }
                        break;

                        case 1:
                        {
                            Console.WriteLine("Введите параметры книги.\n"+
                            "Пример:{Title} {AuthorId} {Year} {Genre}");

                            var bookInput = Console.ReadLine();
                            if(bookInput is null)
                            {
                                Console.WriteLine("Вы ввели пустую строку!");
                                continue;
                            }

                            var param = bookInput.Split(' ');
                            if(param.Length < 4 || string.IsNullOrWhiteSpace(param[0])
                                || string.IsNullOrWhiteSpace(param[1]) ||string.IsNullOrWhiteSpace(param[2])
                                ||string.IsNullOrWhiteSpace(param[3]))
                            {
                                Console.WriteLine("Вы ввели не все аргументы!");
                                continue;
                            }

                            if(!int.TryParse(param[1],out var id) || !int.TryParse(param[2],out var year))
                            {
                                Console.WriteLine("Вы ввели id автора или год не как число!");
                                continue;
                            }

                            await bookOperations.AddBook(param[0],id,year,param[3]);
                            Console.WriteLine("Книга успешно добавлена!");
                        }
                        break;

                        case 2:
                        {
                            var list = bookOperations.GetAllBooks().Result;

                            foreach(var book in list)
                            {
                                Console.WriteLine(book.Id + "   " + book.Title + " " + $"Id_автора:{book.AuthorId}" + " " + book.Year
                                                + " " + book.Genre);
                            }
                        }
                        break;

                        case 3:
                        {
                            Console.WriteLine("Введите id автора");
                            var authorIdInput = Console.ReadLine();

                            if(!int.TryParse(authorIdInput,out var authorId))
                            {
                                Console.WriteLine("Id автора должно быть числом!");
                                continue;           
                            }

                            var list = bookOperations.GetBooksByAuthor(authorId).Result;

                            foreach(var book in list)
                            {
                                Console.WriteLine(book.Id + "   " + book.Title + " " + $"Id_автора:{book.AuthorId}" + " " + book.Year
                                                + " " + book.Genre);
                            }      
                        }
                        break;

                        case 4:
                        {
                            Console.WriteLine("Введите жанр");
                            var genre = Console.ReadLine();
                            if(string.IsNullOrWhiteSpace(genre))
                            {
                                Console.WriteLine("Вы ввели пустую строку!");
                                continue;
                            }

                            var list = bookOperations.GetBooksByGenre(genre).Result;

                            foreach(var book in list)
                            {
                                Console.WriteLine(book.Id + "   " + book.Title + " " + $"Id_автора:{book.AuthorId}" + " " + book.Year
                                                + " " + book.Genre);
                            }      
                        }
                        break;

                        default:
                        {
                            Console.WriteLine("Команда не найдена!");
                            continue;       
                        }
                    }
                }
                break;

                case 1:
                {
                    var authorOperations = provider.GetService<AuthorOperationsService>();

                    if(authorOperations is null)
                    {
                        throw new Exception("AuthorOperationsService не найден!");
                    }

                    Console.WriteLine("Все операции с авторами:");
                    Console.WriteLine("0-добавить автора");
                    Console.WriteLine("1-Вывести всех авторов");

                    var secondUserInput = Console.ReadLine();
                    var commandIsNumber = int.TryParse(secondUserInput,out var commandNumber);

                    if(!commandIsNumber)
                    {
                        Console.WriteLine("Введёння команда должна быть цифрой от 0 до 1 !");
                        continue;
                    }

                    switch(commandNumber)
                    {
                        case 0:
                        {
                            Console.WriteLine("Введите параметры автора.\n"+
                            "Пример:{FirsName} {LastName}");

                            var authorInput = Console.ReadLine();
                            if(authorInput is null)
                            {
                                Console.WriteLine("Вы ввели пустую строку!");
                                continue;
                            }

                            var param = authorInput.Split(' ');
                            if(param.Length < 2 || string.IsNullOrWhiteSpace(param[0])
                                || string.IsNullOrWhiteSpace(param[1]))
                            {
                                Console.WriteLine("Вы ввели не все аргументы!");
                                continue;
                            }

                            await authorOperations.AddAuthor(param[0],param[1]);
                            Console.WriteLine("Автор успешно добавлен!");
                        }
                        break;

                        case 1:
                        {
                            var list = authorOperations.GetAllAuthors().Result;

                            foreach(var author in list)
                            {
                                Console.WriteLine(author.Id + "  " + author.FirstName
                                                    + " " + author.LastName);
                            }
                        }
                        break;

                        default:
                        {
                            Console.WriteLine("Команда не найдена!");
                            continue;       
                        }
                    }

                }
                break;

                case 2:
                {
                    var userOperations = provider.GetService<UserOperationsService>();

                    if(userOperations is null)
                    {
                        throw new Exception("UserOperationsService не найден!");
                    }

                    Console.WriteLine("Все операции с пользователями:");
                    Console.WriteLine("0-добавить пользователя");
                    Console.WriteLine("1-обновить пользователя");
                    Console.WriteLine("2-связать пользователя и книгу");
                    Console.WriteLine("3-вывести всех пользователей");

                    var secondUserInput = Console.ReadLine();
                    var commandIsNumber = int.TryParse(secondUserInput,out var commandNumber);

                    switch(commandNumber)
                    {
                        case 0:
                        {
                            Console.WriteLine("Введите параметры пользователя.\n"+
                            "Пример:{FirsName} {LastName} {Email} {PhoneNumber}");

                            var usInput = Console.ReadLine();
                            if(usInput is null)
                            {
                                Console.WriteLine("Вы ввели пустую строку!");
                                continue;
                            }

                            var param = usInput.Split(' ');
                            if(param.Length < 2 || string.IsNullOrWhiteSpace(param[0])
                                || string.IsNullOrWhiteSpace(param[1]) || string.IsNullOrWhiteSpace(param[2])
                                || string.IsNullOrWhiteSpace(param[3]))
                            {
                                Console.WriteLine("Вы ввели не все аргументы!");
                                continue;
                            }

                            await userOperations.AddUser(param[0],param[1],param[2],param[3]);
                            Console.WriteLine("Пользователь успешно добавлен!");
                        }
                        break;

                        case 1:
                        {
                            Console.WriteLine("Введите id и изменённые параметры пользователя.\n"+
                            "Пример:{Id} {FirsName} {LastName} {Email} {PhoneNumber}");

                            var usInput = Console.ReadLine();
                            if(usInput is null)
                            {
                                Console.WriteLine("Вы ввели пустую строку!");
                                continue;
                            }

                            var param = usInput.Split(' ');
                            if(param.Length < 2 || string.IsNullOrWhiteSpace(param[0])
                                || string.IsNullOrWhiteSpace(param[1]) || string.IsNullOrWhiteSpace(param[2])
                                || string.IsNullOrWhiteSpace(param[3]) || string.IsNullOrWhiteSpace(param[4]))
                            {
                                Console.WriteLine("Вы ввели не все аргументы!");
                                continue;
                            }

                            if(int.TryParse(param[0],out var id))
                            {
                                Console.WriteLine("Id должно быть числом!");
                                continue;                   
                            }

                            await userOperations.UpdateUser(id,param[1],param[2],param[3],param[4]);
                            Console.WriteLine("Пользователь успешно обновлён!");
                        }
                        break;

                        case 2:
                        {
                            Console.WriteLine("Введите сначала id пользователя,потом id книги через пробел");

                            var input = Console.ReadLine();

                            if(input is null)
                            {
                                Console.WriteLine("Вы ввели пустую строку!");
                                continue;       
                            }

                            string[] usInput = input.Split(' ');

                            if(!int.TryParse(usInput[0],out var usId) ||
                                !int.TryParse(usInput[1],out var bookId))
                            {
                                Console.WriteLine("Id пользователя и книги должны быть числом!");
                                continue;     
                            }

                            await userOperations.Link(usId,bookId);
                            Console.WriteLine("Пользователь и книга успешно связаны!");
                        }  
                        break;

                        case 3:
                        {
                            var list = userOperations.GetAllUsers().Result;

                            foreach(var user in list)
                            {
                                Console.WriteLine(user.Id + "  " + user.FirstName
                                                    + " " + user.LastName + " "
                                                    + user.Email + " " + user.PhoneNumber);
                            }                    
                        }
                        break;        
                    }
                }
                break;

                case 3:
                {
                    breakCycle = true;
                }
                break;

                default:
                {
                    Console.WriteLine("Команда не найдена!");
                    continue;   
                }
            }
        }
    }
}