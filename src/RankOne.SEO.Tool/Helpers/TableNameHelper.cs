using RankOne.Interfaces;
using System;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace RankOne.Helpers
{
    public class TableNameHelper<T> : ITableNameHelper<T>
    {
        public string GetTableName()
        {
            var type = typeof(T);
            var tableNameAttribute = type.FirstAttribute<TableNameAttribute>();
            if (tableNameAttribute == null)
                throw new Exception(
                   $"The Type '{type.Name}' does not contain a TableNameAttribute, which is used to find the name of the table to drop. The operation could not be completed."
                        );
            return tableNameAttribute.Value;
        }
    }
}