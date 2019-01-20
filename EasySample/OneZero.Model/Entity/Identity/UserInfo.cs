using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Model.Identity
{
   
    public class UserInfo
    {
        public Guid UserId{get;set;}

        public Guid RoleId{get;set;}

        public string Avatar{get;set;}

        public Guid DepartmentId{get;set;}
        
        public string Remark{get;set;}
    }
}