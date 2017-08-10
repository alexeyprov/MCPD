using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tasks.WebApi.Models;

namespace Tasks.WebApi.Server.Mappers
{
    internal sealed class CategoryMapper : ITypeMapper<Data.Models.Category, Category>
    {
        #region ITypeMapper<Category,Category> Members

        Category ITypeMapper<Data.Models.Category, Category>.Create(Data.Models.Category from)
        {
            return new Category
            {
                CategoryId = from.CategoryId,
                Description = from.Description,
                Name = from.Name,
                Links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Title = "self",
                        Href = "/api/categories/" + from.CategoryId
                    },
                    new Link
                    {
                        Rel = "all",
                        Title = "All Categories",
                        Href = "/api/categories"
                    }
                }
            };
        }

        #endregion
    }
}