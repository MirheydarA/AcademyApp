using Core.Constants;
using Core.Helpers;
using Data.Repositories.Concrete;
using System.Globalization;
using Core.Entities;
using Presentation.Services;
using Core.Extensions;

namespace Presentation
{
    public static class Program
    {
        private readonly static GroupService _groupService;
        private readonly static StudentService _studentService; 
        private readonly static GroupRepository _groupRepository;
        static Program()
        {
            _groupService = new GroupService();
            _studentService = new StudentService();
            _groupRepository = new GroupRepository();
        }
        static void Main(string[] args)
        {
            ConsoleHelper.WriteWithColor("--- WELCOME ---", ConsoleColor.DarkCyan);

        MainMenu: ConsoleHelper.WriteWithColor("1. Groups", ConsoleColor.DarkYellow);
            ConsoleHelper.WriteWithColor("2. Students", ConsoleColor.DarkYellow);
            ConsoleHelper.WriteWithColor("0. Exit", ConsoleColor.DarkYellow);

            ConsoleHelper.WriteWithColor("--- Select Option ---", ConsoleColor.DarkCyan);

            int number;
            bool IsSucceded = int.TryParse(Console.ReadLine(), out number);
            if (!IsSucceded)
            {
                ConsoleHelper.WriteWithColor("Inputed number is not correct", ConsoleColor.Red);
            }

            switch (number)
            {
                case (int)MainMenuOptions.Groups:
                    while (true)
                    {
                    GroupMenu: ConsoleHelper.WriteWithColor("1. Create Group", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("2. Update Group", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("3. Delete Group", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("4. Get All Groups", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("5. Get Group By Id", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("6. Get Group By Name", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("0. Back to Main Menu", ConsoleColor.DarkYellow);

                        ConsoleHelper.WriteWithColor("--- Select Option ---", ConsoleColor.DarkCyan);


                        IsSucceded = int.TryParse(Console.ReadLine(), out number);
                        if (!IsSucceded)
                        {
                            ConsoleHelper.WriteWithColor("Inputed number is not correct", ConsoleColor.Red);
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

                                case (int)GroupOptions.BackToMainMenu:
                                    goto MainMenu;
                                default:
                                    ConsoleHelper.WriteWithColor("Inputed number is not exist", ConsoleColor.Red);
                                    goto GroupMenu;
                            }
                        }


                    }
                case (int)MainMenuOptions.Students:
                    while (true)
                    {
                        ConsoleHelper.WriteWithColor("1. Create Student", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("2. Update Student", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("3. Delete Student", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("4. Get All Student", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("5. Get Student By Group", ConsoleColor.DarkYellow);
                        ConsoleHelper.WriteWithColor("0. Go to Main Menu", ConsoleColor.DarkYellow);
                        bool IsSucceeded = int.TryParse(Console.ReadLine(), out number);
                        if (!IsSucceeded)
                        {
                            ConsoleHelper.WriteWithColor("Inputed number is not correct", ConsoleColor.Red);
                        }
                        else
                        {
                            switch (number)
                            {   
                                case (int)StudentOptions.Create:
                                    _studentService.Create();
                                    break;

                                case (int)StudentOptions.Update:

                                    break;

                                case (int)StudentOptions.Delete:
                                    _studentService.Delete();   
                                    break;

                                case (int)StudentOptions.GetAll:
                                    _studentService.GetAll();   
                                    break;

                                case (int)StudentOptions.GetAllByGroup:
                                    _studentService.GetAllByGroup();
                                    break;

                                case (int)StudentOptions.BackToMainMenu:
                                    ConsoleHelper.WriteWithColor("Your choise is not correct");
                                    goto MainMenu;
                            }
                        }
                    }


                case (int)MainMenuOptions.Exit:
                    return;
            }
        }
    }
}