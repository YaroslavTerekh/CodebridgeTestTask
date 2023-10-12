using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.BL.Behaviors.Dogs.CreateDog;

public class CreateDogCommand : IRequest
{
    public string Name { get; set; }

    public string Color { get; set; }

    public int TailLength { get; set; }

    public int Weight { get; set; }
}
