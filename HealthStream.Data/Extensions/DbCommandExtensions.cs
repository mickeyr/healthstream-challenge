using System;
using System.Data;
using System.Data.Common;

namespace HealthStream.Data.Extensions
{
    public static class DbCommandExtensions
    {
        public static void AddParamaters<T>(this IDbCommand command, T obj) where T : class
        {
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = String.Format("@{0}", property.Name);
                parameter.Value = property.GetValue(obj);
                command.Parameters.Add(parameter);
            }
        }
    }
}
