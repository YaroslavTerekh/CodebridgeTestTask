using CodebridgeTest.Domain.DataTransferObjects;
using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.BL.Behaviors.Dogs.GetAllDogs;

public class GetAllDogsQuery : IRequest<PaginationDTO<DogDTO>>
{
    public DogSortingAttribute? Attribute { get; set; }

    public string? Order { get; set; }

    public int? CurrentPage { get; set; }

    public int? PageSize { get; set; }

    public GetAllDogsQuery(string? attribute, string? order, int? currentPage, int? pageSize)
    {
        Order = order;
        CurrentPage = currentPage;
        PageSize = pageSize;

        Attribute = attribute switch
        {
            "weight" => DogSortingAttribute.Weight,
            "tail_length" => DogSortingAttribute.TailLength,
            "name" => DogSortingAttribute.Name,
            "color" => DogSortingAttribute.Color,
            _ => null
        };
    }
}
