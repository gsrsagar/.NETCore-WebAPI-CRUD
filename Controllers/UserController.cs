using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SampleAPI.DataRepository;
using SampleAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleAPI.Controllers
          
{
    [EnableCors]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    //[ApiController]
    public class UserController : ControllerBase
    {

        private readonly IReposiroty _repository;

        public UserController( IReposiroty reposiroty)
        {
            this._repository=reposiroty;
        }

        
        public string randomGenerator(string input)
        {   Random r = new Random();
            int genRand = r.Next(10000, 99999);
            return input+genRand.ToString();
        }

        //[Route("Post")]
        [HttpPost]
        [AllowAnonymous]
        public IQueryable<MemberShip> Post([FromBody] MemberShip member)
        {
            IQueryable<MemberShip> result;
            if (member != null)
            {
                int day = (int)System.DateTime.Now.Day;
                int month = (int)System.DateTime.Now.Month;
                int Year = (int)System.DateTime.Now.Year;
                member.MemberJoinedDate = DateTime.Now;
                member.MemberPassword = CalculateHash(member.MemberPassword);
                member.MemberUniqueId = randomGenerator("RSM" + day.ToString() + month.ToString() + Year.ToString());
                var checkDuplicatekey = Get(member.MemberUniqueId);
                for (int i=0;i==0 ; )
                {   
                    if (checkDuplicatekey == null) { break; }
                    else {
                        member.MemberUniqueId = randomGenerator("RSM" + day.ToString() + month.ToString() + Year.ToString());
                        checkDuplicatekey = Get(member.MemberUniqueId);
                        continue;
                    }                                          
                }                                         
                    result = this._repository.RegisterMember(member);
                    return result;           
            }      
            else
            {
                result = null;
                return result;
            }
        }

        [Route("[action]/{sponserid}")]
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<MemberShip> GetMembersInChain(string sponserid)
        {
            var members = _repository.GetMembersInChain(sponserid);
            return members;
        }
        
        
        
        public string CalculateHash(string input)
        {
            using(var algorithm=SHA512.Create()) //or MD5 SHA5256 etc.
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }



        // GET: api/<UserController>
        //[Route("Get")]
        [HttpGet]
        [AllowAnonymous] //Can be added in webconfig
        public IEnumerable<MemberShip> Get()
        {
            var result = this._repository.GetAllMembers();
            return result;
        }

        // GET api/<UserController>/5
        [Route("{memberuniqueid}")]
        [HttpGet]
        [AllowAnonymous]
        public MemberShip Get(string memberuniqueid)
        {
            var result = this._repository.GetById(memberuniqueid);
            return result;
        }

        [Route("[action]/{id}")]
        [HttpGet]
       // [AllowAnonymous]
        public string GetbyId(int id)
        {
            return "Get Value by Id";

        }
        [Route("UserRegister")]
        [HttpPost]
        //[AllowAnonymous]
        public User UserRegister([FromBody]User user)
        {
             var user1 = user;
            return user1;
        }

        [Route("MemberRegister")]
        [HttpPost]
       // [AllowAnonymous]
        public MemberShip MemberRegister([FromBody]MemberShip member)
        {
            var user1 = member;
            return user1;
        }

        // POST api/<UserController>
        /*[HttpPost]
        public string Post([FromBody] string value)
        {
            return "Post";
        }*/

        // PUT api/<UserController>/5
        [Route("Put/{id}")]
        [HttpPut]
        public string Put(int id, [FromBody] string value)
        {
            return "Put";
        }

        // DELETE api/<UserController>/5
        [Route("Delete/{id}")]
        [HttpDelete]
        public string Delete(int id)
        {
            return "Delete";
        }
    }
}
