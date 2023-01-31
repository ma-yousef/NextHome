using NextHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Persistence;

namespace NextHome.Helpers
{
    public class WRDataAccess
    {
        UmbracoDatabase _dbCntxt;
        public WRDataAccess(UmbracoDatabase db)
        {
            _dbCntxt = db;
        }

        #region Website Inquires
        public IEnumerable<T> GetAll<T>()
        {
            return Enumerable.Empty<T>();
        }

        public PagedResult<T> GetByPage<T>(int itemsPerPage, int pageNumber, string sortColumn, string sortOrder, string searchTerm)
            where T : IEntity
        {
            var items = new List<T>();

            var currentType = typeof(T);

            var query = new Sql().Select("*").From(currentType.Name);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                int c = 0;
                foreach (var property in currentType.GetProperties())
                {
                    string before = "WHERE";
                    if (c > 0)
                    {
                        before = "OR";
                    }

                    var columnAttri =
                           property.GetCustomAttributes(typeof(ColumnAttribute), false);

                    var columnName = property.Name;
                    if (columnAttri.Any())
                    {
                        columnName = ((ColumnAttribute)columnAttri.FirstOrDefault()).Name;
                    }

                    query.Append(before + " [" + columnName + "] like @0", "%" + searchTerm + "%");
                    c++;
                }
            }
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortOrder))
                query.OrderBy(sortColumn + " " + sortOrder);
            else
            {
                query.OrderBy("id desc");
            }

            var p = _dbCntxt.Page<T>(pageNumber, itemsPerPage, query);
            var result = new PagedResult<T>
            {
                TotalPages = p.TotalPages,
                TotalItems = p.TotalItems,
                ItemsPerPage = p.ItemsPerPage,
                CurrentPage = p.CurrentPage,
                Items = p.Items.ToList()
            };
            return result;
        }

        public int InsertEntity<T>(T entity) where T : IEntity
        {
            entity.CreationDate = DateTime.Now;
            var obj = _dbCntxt.Insert(entity);
            return 1;
        }
        #endregion
    }
}