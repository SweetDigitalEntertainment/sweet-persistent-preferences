using System;
using System.Runtime.InteropServices;

namespace Sweet.Preferences
{
    public static class UKeychain
    {
        [DllImport("__Internal")]
        private static extern ulong _UKeychain_AllAccounts(out IntPtr arr);

        [DllImport("__Internal")]
        private static extern ulong _UKeychain_AccountsForService(string serviceName, out IntPtr arr);

        [DllImport("__Internal")]
        private static extern string _UKeychain_PasswordForService(string serviceName, string account);

        [DllImport("__Internal")]
        private static extern int _UKeychain_DeletePasswordForService(string servieName, string account);

        [DllImport("__Internal")]
        private static extern int _UKeychain_SetPassword(string password, string serviceName, string account);

        [DllImport("__Internal")]
        private static extern void _UKeychain_FreeAccountArray(IntPtr arr, int count);




        public static UKeychainAccount[] AllAccounts()
        {
            IntPtr arr;
            int count = (int)_UKeychain_AllAccounts(out arr);
            return MarshalAndFreeAccounts(arr, count);
        }


        public static UKeychainAccount[] AccountsForService(string serviceName)
        {
            IntPtr arr;
            int count = (int)_UKeychain_AccountsForService(serviceName, out arr);
            return MarshalAndFreeAccounts(arr, count);
        }


        private static UKeychainAccount[] MarshalAndFreeAccounts(IntPtr arr, int count)
        {
            var ret = new UKeychainAccount[count];
            IntPtr element = arr;

            for (int i = 0; i < count; i++)
            {
                var nativeElement = (UKeychainAccountNative)Marshal.PtrToStructure(element, typeof(UKeychainAccountNative));

                ret[i].Account = Marshal.PtrToStringAuto(nativeElement.Account);
                ret[i].Service = Marshal.PtrToStringAuto(nativeElement.Service);
                ret[i].CreatedAt = Marshal.PtrToStringAuto(nativeElement.CreatedAt);
                ret[i].LastModified = Marshal.PtrToStringAuto(nativeElement.LastModified);

                element = new IntPtr(element.ToInt64() + Marshal.SizeOf(typeof(UKeychainAccountNative)));
            }

            _UKeychain_FreeAccountArray(arr, count);
            return ret;
        }


        public static string PasswordForService(string serviceName, string account)
        {
            return _UKeychain_PasswordForService(serviceName, account);
        }


        public static bool DeletePasswordForService(string serviceName, string account)
        {
            return _UKeychain_DeletePasswordForService(serviceName, account) == 1;
        }


        public static bool SetPassword(string password, string serviceName, string account)
        {
            return _UKeychain_SetPassword(password, serviceName, account) == 1;
        }




        private struct UKeychainAccountNative
        {
            public IntPtr Account;
            public IntPtr Service;
            public IntPtr CreatedAt;
            public IntPtr LastModified;
        }
    }
}