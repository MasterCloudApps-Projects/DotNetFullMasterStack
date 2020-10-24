using System.Collections.Generic;

namespace Ordering.API.ViewModel
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : class
    {
        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }

        public long Count { get; private set; }
        public IEnumerable<TEntity> Data { get; private set; }
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }
    }
}