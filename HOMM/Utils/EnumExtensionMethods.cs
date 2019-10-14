using System;
using System.ComponentModel;
using System.Reflection;

namespace HOMM.Utils
{
    public static class EnumExtensionMethods
    {
        public static string GetDescription(this Enum enumElement)
        {
            Type type = enumElement.GetType();
 
            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo.Length == 0) return enumElement.ToString();
            
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attrs.Length > 0 ? ((DescriptionAttribute) attrs[0]).Description : enumElement.ToString();
        }
    }
}