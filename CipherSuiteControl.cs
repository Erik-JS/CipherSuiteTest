using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CipherSuiteTest
{
    public class CipherSuiteControl
    {
        [DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
        private static extern int BCryptAddContextFunction(ConfigurationTable dwTable, string pszContext, CryptographicInterface dwInterface, string pszFunction, FunctionPosition dwPosition);

        [DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
        private static extern int BCryptRemoveContextFunction(ConfigurationTable dwTable, string pszContext, CryptographicInterface dwInterface, string pszFunction);

        [DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
        private static extern int BCryptEnumContextFunctions(ConfigurationTable dwTable, string pszContext, CryptographicInterface dwInterface, ref uint pcbBuffer, out IntPtr ppBuffer);

        [DllImport("bcrypt.dll")]
        private static extern void BCryptFreeBuffer(IntPtr pvBuffer);

        private enum FunctionPosition : uint
        {
            CRYPT_PRIORITY_TOP = 0x00000000,
            CRYPT_PRIORITY_BOTTOM = 0xFFFFFFFF
        }

        private enum CryptographicInterface : uint
        {
            BCRYPT_ASYMMETRIC_ENCRYPTION_INTERFACE = 0x00000003,
            BCRYPT_CIPHER_INTERFACE = 0x00000001,
            BCRYPT_HASH_INTERFACE = 0x00000002,
            BCRYPT_RNG_INTERFACE = 0x00000006,
            BCRYPT_SECRET_AGREEMENT_INTERFACE = 0x00000004,
            BCRYPT_SIGNATURE_INTERFACE = 0x00000005,
            NCRYPT_KEY_STORAGE_INTERFACE = 0x00010001,
            NCRYPT_SCHANNEL_INTERFACE = 0x00010002,
            NCRYPT_SCHANNEL_SIGNATURE_INTERFACE = 0x00010003
        }

        private enum ConfigurationTable : uint
        { 
            CRYPT_LOCAL = 0x00000001,
            CRYPT_DOMAIN = 0x00000002
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CRYPT_CONTEXT_FUNCTIONS
        {
            public int cFunctions;
            public IntPtr rgpszFunctions;
        }

        public static List<string> GetCipherSuiteList()
        {
            var res = new List<string>();
            uint size = 0;
            BCryptEnumContextFunctions(ConfigurationTable.CRYPT_LOCAL, "SSL", CryptographicInterface.NCRYPT_SCHANNEL_INTERFACE, ref size, out IntPtr ptrBuffer);
            CRYPT_CONTEXT_FUNCTIONS ccf = (CRYPT_CONTEXT_FUNCTIONS)Marshal.PtrToStructure(ptrBuffer, typeof(CRYPT_CONTEXT_FUNCTIONS));
            for (int i = 0; i < ccf.cFunctions; i++)
            {
                IntPtr p = Marshal.ReadIntPtr(ccf.rgpszFunctions + (IntPtr.Size * i));
                string s = Marshal.PtrToStringUni(p);
                res.Add(s);
            }
            BCryptFreeBuffer(ptrBuffer);
            return res;
        }

        public static int AddCipherSuite(string strCipherSuite, bool top = false)
        {
            return BCryptAddContextFunction(ConfigurationTable.CRYPT_LOCAL, "SSL", CryptographicInterface.NCRYPT_SCHANNEL_INTERFACE,
                strCipherSuite, top ? FunctionPosition.CRYPT_PRIORITY_TOP : FunctionPosition.CRYPT_PRIORITY_BOTTOM);
        }

        public static int RemoveCipherSuite(string strCipherSuite)
        {
            return BCryptRemoveContextFunction(ConfigurationTable.CRYPT_LOCAL, "SSL", CryptographicInterface.NCRYPT_SCHANNEL_INTERFACE, strCipherSuite);
        }

    }
}
