using System;
using System.Collections.Generic;
using System.Text;

namespace OneZero.Entity.Identity
{
   
    public class User:IdentityUser<Guid>
    {

        /// <summary>
        /// ��ɫ���ϣ��������ԣ�
        /// </summary>
        public ICollection<Role> Roles { get; set; }
    }
}