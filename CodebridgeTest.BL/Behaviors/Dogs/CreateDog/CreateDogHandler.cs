using CodebridgeTest.Domain.DbConnection;
using CodebridgeTest.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.BL.Behaviors.Dogs.CreateDog;

public class CreateDogHandler : IRequestHandler<CreateDogCommand>
{
    private readonly DataContext _context;

    public CreateDogHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        var dog = new Dog
        {
            Name = request.Name,
            Color = request.Color,
            TailLength = request.TailLength,
            Weight = request.Weight
        };

        await _context.Dogs.AddAsync(dog, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
