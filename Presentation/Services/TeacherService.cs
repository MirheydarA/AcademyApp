using Core.Entities;
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


    public class TeacherService
    {
        private readonly TeacherRepository _teacherRepository;
        private readonly GroupRepository _groupRepository;
        public TeacherService()
        {
            _teacherRepository = new TeacherRepository();
            _groupRepository = new GroupRepository();
        }
        public void Create()
        {
            ConsoleHelper.WriteWithColor("Enter teacher name");
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter teacher surname");
            string surname = Console.ReadLine();

        BirthDateDesc: ConsoleHelper.WriteWithColor("Enter birth date");
            DateTime birthDate;
            bool isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth Date is not correct format");
                goto BirthDateDesc;
            }

            ConsoleHelper.WriteWithColor("Enter teacher speciality");
            string speciality = Console.ReadLine();

            var teacher = new Teacher
            {
                Name = name,
                Surname = surname,
                BirthDate = birthDate,
                Speciality = speciality,
                CreatedAt = DateTime.Now
            };

            _teacherRepository.Add(teacher);
            string teacherBirthDate = teacher.BirthDate.ToString("dddd, dd MMMM yyyy");
            ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} succesfully created {teacher.BirthDate}");
        }

        public void GetAll()
        {
            var teachers = _teacherRepository.GetAll();
            if (teachers.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher");
            }
            foreach (var teacher in teachers)
            {
                ConsoleHelper.WriteWithColor($"{teacher.Id} {teacher.Name} {teacher.Surname}");
                if (teacher.Groups.Count == 0) //
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this teacher");
                }
                foreach (var group in teacher.Groups)//
                {
                    ConsoleHelper.WriteWithColor($"ID {group.Id} Name {group.Name}");
                }
                Console.WriteLine();
            }

        }

        public void Delete()
        {
        List: GetAll();
            if (_teacherRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any teacher");
            }
            else
            {
                ConsoleHelper.WriteWithColor("Enter teacher id");
                int id;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Id is not correct format");
                    goto List;
                }

                var teacher = _teacherRepository.Get(id);
                if (teacher is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any teacher in this id");
                }
                _teacherRepository.Delete(teacher);
                ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is succesfully deleted");
            }



        }
        public void Update()
        {
        Enter: GetAll();

            ConsoleHelper.WriteWithColor("Enter id ");
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Inputed id in not correct format");
                goto Enter;
            }

            var teacher = _teacherRepository.Get(id);
            if (teacher is null)
            {
                ConsoleHelper.WriteWithColor("There is no teacher inthis id");
                goto Enter;
            }
            ConsoleHelper.WriteWithColor("Enter new name");
            string name = Console.ReadLine();

            ConsoleHelper.WriteWithColor("Enter new surname");
            string surname = Console.ReadLine();

        BirthDateDesc: ConsoleHelper.WriteWithColor("Enter birth date");
            DateTime birthDate;
            isSucceeded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Birth Date is not correct format");
                goto BirthDateDesc;
            }

            ConsoleHelper.WriteWithColor("Enter new speciality");
            string speciality = Console.ReadLine();

            teacher.Name = name;
            teacher.Surname = surname;
            teacher.BirthDate = birthDate;
            teacher.Speciality = speciality;

            _teacherRepository.Update(teacher);

            ConsoleHelper.WriteWithColor($"{teacher.Name} {teacher.Surname} is succesfully updated");

        }
    }
}
