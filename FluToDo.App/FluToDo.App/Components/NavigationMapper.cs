using System;
using System.Collections.Concurrent;

namespace FluToDo.App.Components
{
    public class NavigationMapper
    {
        private readonly ConcurrentDictionary<Type, object> _typeToAssociateDictionary = new ConcurrentDictionary<Type, object>();

        private readonly ConcurrentDictionary<object, Type> _associateToType = new ConcurrentDictionary<object, Type>();

        public void AddMapping(Type type, object associatedSource)
        {
            _typeToAssociateDictionary.TryAdd(type, associatedSource);
            _associateToType.TryAdd(associatedSource, type);
        }

        public Type GetTypeSource(object associatedSource)
        {
            Type typeSource;
            _associateToType.TryGetValue(associatedSource, out typeSource);

            return typeSource;
        }
    }
}
