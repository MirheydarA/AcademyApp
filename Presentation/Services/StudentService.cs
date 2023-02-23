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
        private readonly GroupService _groupService;
        private readonly GroupRepository _groupRepository;
        private readonly StudentRepository _studentRepository;
        public StudentService()
        {
            _groupService = new GroupService();
            _groupRepository = new GroupRepository();
            _studentRepository = new StudentRepository();

        }
        public void GetAll()
        {
            var students = _studentRepository.GetAll();
            if (students.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any student");
            }

            foreach (var student in students)
            {
                ConsoleHelper.WriteWithColor($"Studemt ID {student.Id} Fullname :  {student.Name} {student.Surname} Group {student.Group?.Name} ");
            }
        }
        public void GetAllByGroup()
        {
            _groupService.GetAll();
            ConsoleHelper.WriteWithColor("--- Enter Group id");
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Id is not correct format");
            }
            var group = _groupRepository.Get(id);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this ID");
            }
            if (group.Students.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any student in this group");
            }
            else
            {
                foreach (var student in group.Students)
                {
                    ConsoleHelper.WriteWithColor($"{student.Name}, {student.Surname}, {student.Id}");
                }
            }
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

            if (_studentRepository.IsDuplicatEmail(email))
            {
                ConsoleHelper.WriteWithColor("This email already used");
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
            if (_groupRepository.GetAll().Count != 0)
            {

                ConsoleHelper.WriteWithColor("Enter Group ID");
                int groupID;
                IsSucceded = int.TryParse(Console.ReadLine(), out groupID);
                if (!IsSucceded)
                {
                    ConsoleHelper.WriteWithColor("Id is not correct format");
                    goto AddGroup;
                }
                var group = _groupRepository.Get(groupID);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("Group is not exsist");
                    goto AddGroup;
                }
                if (group.MaxSize <= group.Students.Count)
                {
                    ConsoleHelper.WriteWithColor("This group is full");
                    goto AddGroup;
                }


                var student = new Student
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    BirthDate = birthDate,
                    Group = group,
                };
                group.Students.Add(student);
                _studentRepository.Add(student);
                ConsoleHelper.WriteWithColor($"{student.Name} {student.Surname} is succesfully added");
            }
        }
        public void Delete()
        {
            GetAll();
            if (_groupRepository.GetAll().Count != 0)
            {
                ConsoleHelper.WriteWithColor("--- Enter id ---");
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Id is not correct format");
                }
                var dbStudent = _studentRepository.Get(id);


                _groupRepository.Delete(dbStudent);
                ConsoleHelper.WriteWithColor($"{dbStudent.Name} {dbStudent.Surname} is succesfully deleted", ConsoleColor.Green);
            }
        }
    }
}
