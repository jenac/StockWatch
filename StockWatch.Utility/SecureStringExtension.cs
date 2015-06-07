using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace StockWatch.Utility
{
    public static class SecureStringExtension 
    {
        public static SecureString ToSecureString(this string value)
        {
            var secured = new SecureString();
            if (!string.IsNullOrEmpty(value))
            {
                foreach (var c in value.ToCharArray())
                {
                    secured.AppendChar(c);
                }
            }
            return secured;
        }

        public static string ToPlainString(this SecureString value)
        {
            IntPtr psz = IntPtr.Zero;
            try
            {
                psz = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(psz);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(psz);
            }
        }
    }
}
