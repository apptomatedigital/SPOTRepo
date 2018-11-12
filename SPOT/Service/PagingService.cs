using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOT.Models;

namespace SPOT.Service
{
    public class PagingService : IPagingService
    {
        public PaginationModel Pagination(int collectionCount, int pageNumber, int pageSize)
        {
            try
            {
                int count = collectionCount;
                int CurrentPage = pageNumber;
                int PageSize = pageSize;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                var PreviousPage = CurrentPage > 1 ? "Yes" : "No";
                var NextPage = CurrentPage < TotalPages ? "Yes" : "No";
                var model = new PaginationModel()
                {
                    totalCount = TotalCount,
                    pageSize = PageSize,
                    currentPage = CurrentPage,
                    totalPages = TotalPages,
                    previousPage= PreviousPage,
                    nextPage=NextPage
                };
                return model;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}