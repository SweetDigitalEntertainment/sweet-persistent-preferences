﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_IOS
namespace Sweet.PersistentPreferences.iOS
{
    public class iOSPersistentPreferences : IPersistentPreferences
    {
        public string GetValue(string key)
        {
            return UKeychain.PasswordForService("uPreferences", key);
        }

        public bool HasKey(string key)
        {
            return GetValue(key) != null;
        }

        public void SetValue(string key, string value)
        {
            UKeychain.SetPassword(value, "uPreferences", key);
        }

        public bool TryGetValue(string key, out string value)
        {
            value = GetValue(key);
            return value == null;
        }
    }
}
#endif