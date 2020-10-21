using System;
using System.Collections.Generic;
using System.Linq;

namespace EntitiesLibrary.Entities.RequestFeatures
{
    public class RequestList<T> : List<T>
    {
        public static RequestList<T> ToRequestList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();

            List<T> items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new RequestList<T>(items, count, pageNumber, pageSize);
        }
        public RequestList(List<T> items, int count, int pageNumber, int pageSize)
        {
            this.MetaData = new RequestListMetaData
            {
                PageItemsCount = count,
                PageSize = pageSize,
                PageCurrent = pageNumber,
                PagesTotal = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }
        public RequestListMetaData MetaData { get; set; }
    }
    public class RequestListMetaData
    {
        public int PageCurrent { get; set; }
        public int PageSize { get; set; }
        public int PagesTotal { get; set; }
        public int PageItemsCount { get; set; }
        public bool IsPageHasPrevious => PageCurrent > 1;
        public bool IsPageHasNext => PageCurrent < PagesTotal;
    }
}
