using Core.Constants;
using Core.Helpers;
using Data.Repositories.Concrete;
using System.Globalization;
using Core.Entities;
using Presentation.Services;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupService;
        static Program()
        {
            _groupService = new GroupService();
        }
        static void Main(string[] args)
        {

        GroupMenu: ConsoleHelper.WriteWithColor("--- WELCOME ---", ConsoleColor.DarkCyan);

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
                                _groupService.Create();
                                break;

                            case (int)GroupOptions.UpdateGroup:
                                _groupService.Update();
                                break;

                            case (int)GroupOptions.DeleteGroup:
                                _groupService.Delete();
                                break;

                            case (int)GroupOptions.GetAllGroups:
                                _groupService.GetAll();
                                break;

                            case (int)GroupOptions.GetGroupById:
                                _groupService.GetGroupById();
                                break;

                            case (int)GroupOptions.GetGroupByName:
                                _groupService.GetGroupByName();
                                break;

                            case (int)GroupOptions.Exit:
                                if (_groupService.Exit() == true)
                                {
                                    return;
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}