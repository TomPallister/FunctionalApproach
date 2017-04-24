namespace FunctionalApproach.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model;

    public static class Database
    {
        private static readonly List<Value> Values = new List<Value>
        {
            new Value("value1", 1),
            new Value("value2", 2)
        };

        public static Func<Value, Value> Create()
        {
            return v =>
            {
                Values.Add(v);
                return v;
            };
        }

        public static Func<IEnumerable<Value>> ReadAll()
        {
            return () => Values;
        }
        
        public static Func<int, Value> ReadById()
        {
            return i => Values.First(x => x.Id == i);
        }

        public static Func<int, Value, Value> Update()
        {
            return (i, v) =>
            {
                var valueToRemove = Values.First(x => x.Id == i);
                Values.Remove(valueToRemove);
                Values.Add(v);
                return v;
            };
        }

        public static Action<int> Delete()
        {
            return i =>
            {
                var valueToRemove = Values.First(x => x.Id == i);
                Values.Remove(valueToRemove);
            };
        }
    }
}