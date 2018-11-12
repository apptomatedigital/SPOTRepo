using SPOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOT.Service
{
    public interface IPagingService
    {
         PaginationModel Pagination(int collectionCount,int pageNumber,int pageSize);
    }
}