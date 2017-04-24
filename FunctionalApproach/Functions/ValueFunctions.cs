namespace FunctionalApproach.Functions
{
    using System;
    using System.Collections.Generic;
    using Model;

    public static class ValueFunctions
    {
        public static Func<IEnumerable<Value>> ReadAll(Func<Func<IEnumerable<Value>>> getAll)
        {
            return getAll();
        }

        public static Func<int, Value> ReadAllById(Func<Func<int, Value>> getById)
        {
            return getById();
        }

        public static Func<Value, Value> Create(Func<Func<Value, Value>> create)
        {
            return value =>
            {
                var func = create();
                return func.Invoke(value);
            };
        }

        public static Func<int, Value, Value> Update(Func<Func<int, Value, Value>> update)
        {
            return (id, value) =>
            {
                var func = update();
                return func.Invoke(id, value);
            };
        }

        public static Action<int> Delete(Func<Action<int>> delete)
        {
            return delete();
        }
    }
}

