using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Events;

namespace Serilog.Sinks.AspNetCore.SignalR.Core.Sinks
{
    public static class SignalRPropertyFormatter
    {
        static readonly HashSet<Type> SignalRSpecialScalars = new HashSet<Type>
        {
            typeof(bool),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(byte[])
        };

        public static object Simplify(LogEventPropertyValue value)
        {
            if (value is ScalarValue scalar)
                return SimplifyScalar(scalar.Value);

            if (value is DictionaryValue dict)
                return dict
                    .Elements
                    .ToDictionary(kv => SimplifyScalar(kv.Key), kv => Simplify(kv.Value));

            if (value is SequenceValue seq)
                return seq.Elements.Select(Simplify).ToArray();

            if (value is StructureValue str)
            {
                var props = str.Properties.ToDictionary(p => p.Name, p => Simplify(p.Value));
                if (str.TypeTag != null)
                    props["$typeTag"] = str.TypeTag;
                return props;
            }

            return null;
        }

        static object SimplifyScalar(object value)
        {
            if (value == null)
                return null;

            var valueType = value.GetType();
            if (SignalRSpecialScalars.Contains(valueType))
                return value;

            return value.ToString();
        }
    }
}
