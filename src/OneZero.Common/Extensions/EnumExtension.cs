using OneZero.Common.Enums;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OneZero.Common.Extensions
{
    public static class EnumExtension
    {
        public static string GetRemark(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null)
            {
                return value.ToString();
            }

            object[] attributes = fi.GetCustomAttributes(typeof(RemarkAttribute),false);
            if (attributes.Count() > 0)
            {
                return ((RemarkAttribute)attributes[0]).Remark;
            }
            else
            {
                return value.ToString();
            }
        
        }
    }
}
