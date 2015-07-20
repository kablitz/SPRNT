using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sprinter.Helpers
{
    public static class EnumUtil
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}