using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class GroupService
    {
        private readonly GroupRepository _grouprepository;
        public GroupService()
        {
            _grouprepository = new GroupRepository();
        }
        public void GetAll()
        {
            var groups = _grouprepository.GetAll();

            ConsoleHelper.WriteWithColor(" --- All Groups --- ");
            if (groups.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any group", ConsoleColor.Red);
            }

            foreach (var group in groups)
            {
                ConsoleHelper.WriteWithColor($"Id: {group.Id} Name: {group.Name} Max size:{group.MaxSize} Start date:{group.StartDate.ToShortDateString()} End date:{group.EndDate.ToShortDateString()}", ConsoleColor.DarkCyan);
            }

        }

        public void GetGroupById()
        {
            var groups = _grouprepository.GetAll();
            if (groups.Count == 0)
            {
                ConsoleHelper.WriteWithColor("There is no any group", ConsoleColor.Red);
            }
            else
            {
                GetAll();
            EnterId: ConsoleHelper.WriteWithColor(" --- Enter Id --- ", ConsoleColor.Cyan);
                int id;
                bool IsSucceeded = int.TryParse(Console.ReadLine(), out id);
                if (!IsSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Inputed Id is not correct format", ConsoleColor.Red);
                    goto EnterId;
                }
                var group = _grouprepository.Get(id);
                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this Id", ConsoleColor.Red);
                }
                else
                {
                    ConsoleHelper.WriteWithColor($"Id: {group.Id} Name: {group.Name} Max size:{group.MaxSize} Start date:{group.StartDate.ToShortDateString()} End date:{group.EndDate.ToShortDateString()}");
                }
            }

        }

        public void GetGroupByName()
        {
            GetAll();
            ConsoleHelper.WriteWithColor(" *--- Enter Group Name ---*");
            string name = Console.ReadLine();

            var group = _grouprepository.GetByName(name);
            if (group is null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this name");
            }
            else
            {
                ConsoleHelper.WriteWithColor($"Id: {group.Id} Name: {group.Name} Max size:{group.MaxSize} Start date:{group.StartDate.ToShortDateString()} End date:{group.EndDate.ToShortDateString()}");
            }
        }


        public void Create()
        {
            ConsoleHelper.WriteWithColor("--- Enter Name ---", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();
        MaxSize: ConsoleHelper.WriteWithColor("--- Enter max size ---", ConsoleColor.DarkCyan);
            int maxsize;
            bool IsSucceded = int.TryParse(Console.ReadLine(), out maxsize);
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

            DateTime boundaryDate = new DateTime(2015, 01, 01);
            if (startDate < boundaryDate)
            {
                ConsoleHelper.WriteWithColor("Start date is not correct format", ConsoleColor.Red);
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

            _grouprepository.Add(group);

            ConsoleHelper.WriteWithColor($"Group succesfully created with name :{group.Name}\n Max size:{group.MaxSize}\nStart date:{group.StartDate.ToShortDateString()}\nEnd date:{group.EndDate.ToShortDateString()}");
        }

        public void Update()
        {
            GetAll();
        IDorNAme: ConsoleHelper.WriteWithColor(" *--- Enter group name or ID ---*\n For ID press 1 | For Name press 2");
            int decision;
            bool IsSuceeded = int.TryParse(Console.ReadLine(), out decision);
            if (!(decision == 1 || decision == 2))
            {
                ConsoleHelper.WriteWithColor("Your choise is not correct");
                goto IDorNAme;
            }
            if (decision == 1)
            {
                ConsoleHelper.WriteWithColor("Enter ID Which you want update");
                int id;
                bool isSucceded = int.TryParse(Console.ReadLine(), out id);
                if (!isSucceded)
                {
                    ConsoleHelper.WriteWithColor("Inputed ID is not correct");
                    goto IDorNAme;
                }
                var group = _grouprepository.Get(id);

                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this ID", ConsoleColor.Red);
                    goto IDorNAme;
                }

                ConsoleHelper.WriteWithColor("*--- Enter new name ---*");
                string name = Console.ReadLine();

            MaxSize: ConsoleHelper.WriteWithColor("*--- Enter new max size ---*");
                int maxsize;
                isSucceded = int.TryParse(Console.ReadLine(), out maxsize);
                if (!isSucceded)
                {
                    ConsoleHelper.WriteWithColor("New max size in not correct format");
                    goto MaxSize;
                }

            StartDate: ConsoleHelper.WriteWithColor("*--- Enter new start date ---*", ConsoleColor.DarkCyan);
                DateTime startDate;
                bool IsSucceded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

                DateTime boundaryDate = new DateTime(2015, 01, 01);
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

            EndDate: ConsoleHelper.WriteWithColor("--- Enter new end date ---", ConsoleColor.DarkCyan);
                DateTime endDate;
                IsSucceded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                if (!IsSucceded)
                {
                    ConsoleHelper.WriteWithColor("End date is not correct format", ConsoleColor.Red);
                    goto EndDate;
                }
                group.Name = name;
                group.MaxSize = maxsize;
                group.StartDate = startDate;
                group.EndDate = endDate;
                _grouprepository.Update(group);
            }

            else
            {
                ConsoleHelper.WriteWithColor("Enter name Which you want update");
                string name = Console.ReadLine();
                bool IsSucceded;


                var group = _grouprepository.GetByName(name);

                if (group is null)
                {
                    ConsoleHelper.WriteWithColor("There is no any group in this Name", ConsoleColor.Red);
                    return;
                }

                ConsoleHelper.WriteWithColor("*--- Enter new name ---*");
                name = Console.ReadLine();

            MaxSize: ConsoleHelper.WriteWithColor("*--- Enter new max size ---*");
                int maxsize;
                IsSucceded = int.TryParse(Console.ReadLine(), out maxsize);
                if (!IsSucceded)
                {
                    ConsoleHelper.WriteWithColor("New max size in not correct format");
                    goto MaxSize;
                }

            StartDate: ConsoleHelper.WriteWithColor("*--- Enter new start date ---*", ConsoleColor.DarkCyan);
                DateTime startDate;
                IsSucceded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

                DateTime boundaryDate = new DateTime(2015, 01, 01);
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

            EndDate: ConsoleHelper.WriteWithColor("--- Enter new end date ---", ConsoleColor.DarkCyan);
                DateTime endDate;
                IsSucceded = DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                if (!IsSucceded)
                {
                    ConsoleHelper.WriteWithColor("End date is not correct format", ConsoleColor.Red);
                    goto EndDate;
                }
                group.Name = name;
                group.MaxSize = maxsize;
                group.StartDate = startDate;
                group.EndDate = endDate;
                _grouprepository.Update(group);
            }

        }
        public void Delete()
        {
            GetAll();


        ID: ConsoleHelper.WriteWithColor("--- Enter Id ---", ConsoleColor.DarkCyan);
            int id;
            bool IsSucceded = int.TryParse(Console.ReadLine(), out id);
            if (!IsSucceded)
            {
                ConsoleHelper.WriteWithColor("ID is not correct format", ConsoleColor.Red);
                goto ID;
            }
            var dbGroup = _grouprepository.Get(id);
            if (dbGroup == null)
            {
                ConsoleHelper.WriteWithColor("There is no any group in this ID", ConsoleColor.Red);
            }
            else
            {
                _grouprepository.Delete(dbGroup);
                ConsoleHelper.WriteWithColor("Group succesfully deleted");
            }
        }

        public bool Exit()
        {
        AreYouSure: ConsoleHelper.WriteWithColor("Are you sure?| -- *Y* or *N* -- ", ConsoleColor.DarkRed);
            char decision;
            bool IsSucceded = char.TryParse(Console.ReadLine(), out decision);
            if (!IsSucceded)
            {
                ConsoleHelper.WriteWithColor("Your choise is not correct format", ConsoleColor.Red);
                goto AreYouSure;
            }
            if (!(decision == 'Y' || decision == 'N'))
            {
                ConsoleHelper.WriteWithColor("Your choise is not correct", ConsoleColor.Red);
                goto AreYouSure;
            }
            if (decision == 'Y')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
