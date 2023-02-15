using Core.Constants;
using Core.Helpers;
using Data.Repositories.Concrete;
using System.Globalization;
using Core.Entities;

namespace Presentation
{
    public static class Program
    {
        static void Main(string[] args)
        {
            GroupRepository _groundrepository = new GroupRepository();
            ConsoleHelper.WriteWithColor("--- WELCOME ---", ConsoleColor.DarkCyan);

            while (true)
            {
                ConsoleHelper.WriteWithColor("1. Create Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("2. Update Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("3. Delete Group", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("4. Get All Groups", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("5. Get Group By Id", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("6. Get Group By Name", ConsoleColor.DarkYellow);
                ConsoleHelper.WriteWithColor("0. Exit", ConsoleColor.DarkYellow);

                ConsoleHelper.WriteWithColor("--- Select Option ---", ConsoleColor.DarkCyan);

                int number;
                bool IsSucceded = int.TryParse(Console.ReadLine(), out number);
                if (!IsSucceded)
                {
                    ConsoleHelper.WriteWithColor("Inputed number is not correct", ConsoleColor.Red);
                }
                else
                {
                    if (!(number >= 0 && number <= 6))
                    {
                        ConsoleHelper.WriteWithColor("Inputed number is not exist", ConsoleColor.Red);
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)GroupOptions.CreateGroup:
                                ConsoleHelper.WriteWithColor("--- Enter Name ---", ConsoleColor.DarkCyan);
                                string name = Console.ReadLine();
                                MaxSize: ConsoleHelper.WriteWithColor("--- Enter max size ---", ConsoleColor.DarkCyan);
                                int maxsize;
                                IsSucceded = int.TryParse(Console.ReadLine(), out maxsize);
                                if (!IsSucceded)
                                {
                                    ConsoleHelper.WriteWithColor("Max size is not correct format", ConsoleColor.Red);
                                    goto MaxSize;
                                }
                                if (maxsize > 18)
                                {
                                    ConsoleHelper.WriteWithColor("Max size can not be bigger than 18", ConsoleColor.Red);
                                    goto MaxSize;
                                }

                                StartDate: ConsoleHelper.WriteWithColor("--- Enter start date ---", ConsoleColor.DarkCyan);
                                DateTime startDate;
                                IsSucceded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

                                DateTime boundaryDate = new DateTime(2015,01,01);
                                if (startDate < boundaryDate) 
                                {
                                    ConsoleHelper.WriteWithColor("Start date is not right", ConsoleColor.Red);
                                    goto StartDate;
                                }

                                if (!IsSucceded)
                                {
                                    ConsoleHelper.WriteWithColor("Start date is not correct format", ConsoleColor.Red);
                                    goto StartDate;
                                }

                                EndDate: ConsoleHelper.WriteWithColor("--- Enter end date ---", ConsoleColor.DarkCyan);
                                DateTime endDate;
                                IsSucceded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                                if (!IsSucceded)
                                {
                                    ConsoleHelper.WriteWithColor("End date is not correct format", ConsoleColor.Red);
                                    goto EndDate;
                                }

                                if (startDate > endDate)
                                {
                                    ConsoleHelper.WriteWithColor("End date must be bigger than start date", ConsoleColor.Red);
                                    goto EndDate;
                                }

                                Group group = new Group()
                                {
                                    Name = name,
                                    MaxSize = maxsize,
                                    StartDate = startDate,
                                    EndDate = endDate
                                };

                                _groundrepository.Add(group);


                                break;
                            case (int)GroupOptions.UpdateGroup:
                                break;
                            case (int)GroupOptions.GetAllGroups:
                                break;
                            case (int)GroupOptions.GetGroupById:
                                break;
                            case (int)GroupOptions.GetGroupByName:
                                break;
                            case (int)GroupOptions.Exit:
                                return;
                                

                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}