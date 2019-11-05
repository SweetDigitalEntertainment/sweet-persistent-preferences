using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sweet.PersistentPreferences
{
    public interface IPersistentPreferences
    {
        bool HasKey(string key);

        void SetValue(string key, string value);

        string GetValue(string key);

        bool TryGetValue(string key, out string value);
    }
}