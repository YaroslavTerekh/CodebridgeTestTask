using CodebridgeTest.Domain.Constants;
using CodebridgeTest.Domain.DbConnection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.BL.Behaviors.Dogs.CreateDog;

public class CreateDogCommandValidator : AbstractValidator<CreateDogCommand>
{
    public CreateDogCommandValidator(DataContext context)
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.DogNameIsRequired)
            .MustAsync(async (name, cancellationToken) =>
            {
                return !await context.Dogs.AnyAsync(t => t.Name == name, cancellationToken);
            })
            .WithMessage(ValidationMessages.DogWithSameNameExists);

        RuleFor(t => t.Color)
            .NotEmpty()
            .WithMessage(ValidationMessages.ColorIsRequired);

        RuleFor(t => t.TailLength)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.TailLengthWrong);

        RuleFor(t => t.TailLength)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.WrongWeight);
    }
}
