using Microsoft.AspNetCore.Mvc;
using PlanYourTrip_ClassLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_ClassLibrary.Repository
{
    public interface IUserController<T> where T : class
    {
        public Task<ActionResult<T>> GetUserData(int id);
        public Task<ActionResult<Users>> GetUserData(string email);
        //public Task<ActionResult<List<T>>> GetListUserData(List<int> ids);
    }
}
