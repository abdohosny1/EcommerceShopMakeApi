using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Core.Specafiation
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;

        }


        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDes { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnable { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpersion)
        {
            Includes.Add(includeExpersion);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderbyExpersion)
        {
            OrderBy = orderbyExpersion;
        }

        protected void AddOrderByDEs(Expression<Func<T, object>> orderbyExpersionDes)
        {
            OrderByDes = orderbyExpersionDes;
        }


        protected void ApplyPaging(int take, int skip)
        {
            Skip = skip;
            Take = take;
            IsPagingEnable = true;
        }



    }
}
