using SampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.DataRepository
{
   public interface IReposiroty
    {
        public User Login(Login data);

        public User Register(User user);
        public IEnumerable<User> getDetails();

        public IEnumerable<MemberShip> GetMembersInChain(string memberuniqueid);
        public IQueryable<MemberShip> RegisterMember(MemberShip member);
        public IEnumerable<MemberShip> GetAllMembers();
        public MemberShip GetById(string id);
    }
}
