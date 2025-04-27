using System;
using System.Collections.Generic;

namespace OMEGA.Backend
{
    public class CachePair
    {
        public Type type;
        public object value;
    }

    public static class GlobalModCache
    {
        private static Dictionary<string, CachePair> _cachePairs = new Dictionary<string, CachePair>(); 
        
        public static bool GetCacheMember<T>(string memberName, out T member)
        {
            member = default;

            if (!_cachePairs.TryGetValue(memberName, out CachePair pair)) return false;
            if (!typeof(T).IsAssignableFrom(pair.type)) return false;

            member = (T)pair.value;
            return true;
        }
        public static void SetCacheMember<T>(string memberName, T obj)
        {
            if (_cachePairs.ContainsKey(memberName)) _cachePairs.Remove(memberName);
            CachePair pair = new CachePair();
            pair.type = typeof(T);
            pair.value = obj;

            _cachePairs.Add(memberName, pair);
        }
    }
}
