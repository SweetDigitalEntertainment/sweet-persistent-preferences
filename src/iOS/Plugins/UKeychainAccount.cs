#if !UNITY_EDITOR && UNITY_IOS
namespace Sweet.Game.Preferences.iOS
{
    public struct UKeychainAccount
    {
        public string Account;
        public string Service;
        public string CreatedAt;
        public string LastModified;
    }
}
#endif