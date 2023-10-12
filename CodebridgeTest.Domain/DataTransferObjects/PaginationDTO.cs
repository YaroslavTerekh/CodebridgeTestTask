using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.Domain.DataTransferObjects;

public class PaginationDTO<T>
{
    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public List<T> Items { get; set; } = new();

    public PaginationDTO(int currentPage, int pageSize, int total, List<T>? items)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)total / pageSize);
        Items = items;
    }
}
