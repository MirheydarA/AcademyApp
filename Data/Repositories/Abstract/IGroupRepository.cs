using Core.Entities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    internal interface IGroupRepository
    {
        List<Group> GetAll(int id);
        Group Get(int id);
        void Add(Group group);
        void Update(Group group);
        void Delete(Group group);
        
    }
}
