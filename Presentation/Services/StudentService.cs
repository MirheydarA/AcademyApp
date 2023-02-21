using Core.Entities;
using Core.Extensions;
using Core.Helpers;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class StudentService
    {
        private readonly  GroupService _groupService;
        private readonly  GroupRepository _groupRepository;
        private readonly  StudentRepository _studentRepository;
        public StudentService()
        {
            _groupService = new GroupService();
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();

        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor("Enter Student Name");
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Enter Student Surame");
            string surname = Console.ReadLine();
        Email: ConsoleHelper.WriteWithColor("Enter Student Email");
            string email = Console.ReadLine();
            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Email is not correct format");
                goto Email;
            }
        BirthDate: ConsoleHelper.WriteWithColor("--- Enter birth date ---", ConsoleColor.DarkCyan);
            DateTime birthDate;
            bool IsSucceded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!IsSucceded)
            {
                ConsoleHelper.WriteWithColor("Birth date is not correct format", ConsoleColor.Red);
                goto BirthDate;
            }
        AddGroup: _groupService.GetAll();
            ConsoleHelper.WriteWithColor("Enter Group ID");
            int groupID;
            IsSucceded = int.TryParse(Console.ReadLine(), out groupID);
            if (!IsSucceded)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format");
                goto AddGroup;
            }
            var group = _groupRepository.Get(groupID);
            if (group is not null)
            {
                ConsoleHelper.WriteWithColor("Group is not exsist");
                goto AddGroup;
            }
            var student = new Student
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Group = group,
            };
            group.Students.Add(student); 
            _studentRepository.Add(student);
            ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is succesfully added");
        }
    }
}
