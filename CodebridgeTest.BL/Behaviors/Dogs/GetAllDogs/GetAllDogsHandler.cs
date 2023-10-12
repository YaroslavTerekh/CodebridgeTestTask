using AutoMapper;
using CodebridgeTest.Domain.DataTransferObjects;
using CodebridgeTest.Domain.DbConnection;
using CodebridgeTest.Domain.Entities;
using CodebridgeTest.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.BL.Behaviors.Dogs.GetAllDogs;

public class GetAllDogsHandler : IRequestHandler<GetAllDogsQuery, PaginationDTO<DogDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetAllDogsHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationDTO<DogDTO>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
    {
        var dogs = _context.Dogs.AsQueryable();
        var total = await dogs.CountAsync(cancellationToken);
        var desc = request.Order == "desc";

        if(request.Attribute is not null)
        {
            dogs = request.Attribute switch
            {
                DogSortingAttribute.Name => desc ? dogs.OrderByDescending(t => t.Name) : dogs.OrderBy(t => t.Name),
                DogSortingAttribute.TailLength => desc ? dogs.OrderByDescending(t => t.TailLength) : dogs.OrderBy(t => t.TailLength),
                DogSortingAttribute.Weight => desc ? dogs.OrderByDescending(t => t.Weight) : dogs.OrderBy(t => t.Weight),
                DogSortingAttribute.Color => desc ? dogs.OrderByDescending(t => t.Color) : dogs.OrderBy(t => t.Color),
                _ => dogs
            };
        }

        if(request.CurrentPage is not null && request.PageSize is not null)
        {
            dogs = dogs
                .Skip((int)request.CurrentPage * (int)request.PageSize)
                .Take((int)request.PageSize);

            return new PaginationDTO<DogDTO>(
                (int)request.CurrentPage, 
                (int)request.PageSize, 
                total,
                await dogs
                .Select(t => _mapper.Map<DogDTO>(t))
                .ToListAsync(cancellationToken));
        }

        return new PaginationDTO<DogDTO>(1, 1, total,
                await dogs
                .Select(t => _mapper.Map<DogDTO>(t))
                .ToListAsync(cancellationToken));
    }
}
