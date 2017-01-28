using System;

namespace MasterDetail
{
    public static class WeakReferenceExtensions
    {
        public static T Resolve<T>(this WeakReference<T> reference)
            where T : class
        {
            T val;
            if (reference == null) return null;
            return reference.TryGetTarget(out val) ? val : null;
        }

        public static void Use<T>(this WeakReference<T> reference, Action<T> action)
            where T : class
        {
            var val = reference?.Resolve();
            if (val == null) { return; }
            action(val);
        }
    }
}