using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.Extensions;
using Core.Utilities.Results;

namespace Core.Utilities.Helpers
{
    public sealed class ReflectionHelper
    {

        public static T CreateInstance<T>(Type type) => (T)Activator.CreateInstance(type);

        public static T CreateInstance<T>() => (T)Activator.CreateInstance(typeof(T));

        public static MethodInfo GetMethodInfo<T>(string name) => typeof(T).GetMethod(name);
        public static MethodInfo GetMethodInfo(Type type, string name) => type.GetMethod(name);

        public static FieldInfo GetField(Type type, string name) => type.GetField(name);



    }
}
