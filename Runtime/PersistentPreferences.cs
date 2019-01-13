namespace Sweet.PersistentPreferences
{
    public sealed class PersistentPreferences : IPersistentPreferences
    {
        IPersistentPreferences _persistentPreferences;


        public PersistentPreferences()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            _persistentPreferences = new Sweet.PersistentPreferences.Standalone.UnityPlayerPreferences();
#elif UNITY_IOS
            _persistentPreferences = new Sweet.PersistentPreferences.iOS.iOSPersistentPreferences();
#elif UNITY_ANDROID
            _persistentPreferences = new Sweet.PersistentPreferences.Android.AndroidPersistentPreferences();
#endif
        }


        public string GetValue(string key)
        {
            return _persistentPreferences.GetValue(key);
        }

        public bool HasKey(string key)
        {
            return _persistentPreferences.HasKey(key);
        }

        public void SetValue(string key, string value)
        {
            _persistentPreferences.SetValue(key, value);
        }

        public bool TryGetValue(string key, out string value)
        {
            return _persistentPreferences.TryGetValue(key, out value);
        }
    }
}