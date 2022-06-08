using EcommerceShop.Core.Specafiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.EF
{
    public class SpecificationEvalutor<T> where T : BaseModel
    {
        public static IQueryable<T> GetQuery(IQueryable<T> input, ISpecification<T> spec)
        {
            var query = input;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;

        } 
    }
}
