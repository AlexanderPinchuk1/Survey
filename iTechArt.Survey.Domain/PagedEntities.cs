using System.Collections.Generic;

namespace iTechArt.Survey.Domain
{
    public class PagedEntities<T>
    {
        public int PageIndex { get; }
     
        public int ItemCountPerPage { get; }
        
        public int TotalCount { get; }

        public IEnumerable<T> Entities { get; }


        public PagedEntities(int pageIndex, int itemCountPerPage, int totalCount, IEnumerable<T> entities)
        {
            PageIndex = pageIndex;
            ItemCountPerPage = itemCountPerPage;
            TotalCount = totalCount;
            Entities = entities;
        }
    }
}
