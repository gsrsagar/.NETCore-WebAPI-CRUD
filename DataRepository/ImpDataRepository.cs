using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SampleAPI.DbContext;
using SampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace SampleAPI.DataRepository
{

    public class ImpDataRepository : IReposiroty
    {
        private readonly CustomDbContext _context;

        public ImpDataRepository(CustomDbContext context)
        {
            this._context = context;

        }
        public User Login(Login data)
        {
            throw new NotImplementedException();
        }

        public User Register(User user)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> getDetails()
        {
            var result = this._context.User.ToList();
            return (result);

        }

        public IEnumerable<MemberShip> GetMembersInChain(string sponserid)
        {
            var result = this._context.Members.Where<Models.MemberShip>(c => c.SponserId == sponserid);
            return result;
        }

        public IQueryable<MemberShip> RegisterMember(MemberShip member)
        {
            string mobileno = member.MemberMobileNumber;
            this._context.Members.Add(member);
            this._context.SaveChanges();
            var resvalue = this._context.Members.Where(c => c.MemberMobileNumber == mobileno);
            return resvalue;
        }

        public IEnumerable<MemberShip> GetAllMembers()
        {
            var result = this._context.Members.ToList();
            return result;
        }


        public MemberShip GetById(string id)
        {
            var lmc = this._context.Members.Where(C => C.MemberUniqueId == id).FirstOrDefault();
            //var ids = new SqlParameter("MemberUniqueID", id);
            //var lmc = this._context.Members.FromSqlRaw("Select * from Members where MemberUniqueId= @MemberUniqueID", ids).FirstOrDefault();
            return lmc;
        }

        /*
         *public IQueryable<MemberShip> GetById(string id)
        {
            // var res = (this._context.Members.Where<Models.MemberShip>(c => c.MemberUniqueId == id));
            using (DbContext.CustomDbContext db = new DbContext.CustomDbContext())
            {
              
               var lmc = (from c in db.Members where c.MemberUniqueId == id select c);
                return lmc;
            }
            
        }*/
         

        /*List<Models.Customer> lmc=nc.Customers.Where<Models.Customers>(C=>c.Country =="USA")
				.ToList<Models.Customers>();*/
        /*public IQueryable<MemberShip> GetById(string id)
        {
            // var res = (this._context.Members.Where<Models.MemberShip>(c => c.MemberUniqueId == id));
            using (DbContext.CustomDbContext db = new DbContext.CustomDbContext())
            {
              
               var lmc = (from c in db.Members where c.MemberUniqueId == id select c);
                return lmc;
            }
            
        }*/

        /*using(Models.MyContext nc=new Models.MyContext())
         {	
             var lmc = (from c in nc.Customers;
     where c.Country=="USA" && c.Phone.StarsWith("976")

                 select c;

     var lmc2 = from c in nc.Customers
                join d in d.Orders on c.CustomerId equals d.CustomerId
                where c.Country == "USA" && c.Phone.StartsWith("987")
                select c;
     lmc2 = lmc2.where(a=> a.ContactName.Startswith("John"));
             int a = lmc.count();

 }
     }*/
    }
}
