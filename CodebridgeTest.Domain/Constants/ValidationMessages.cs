using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.Domain.Constants;

public static class ValidationMessages
{
    public const string DogNameIsRequired = "Please, enter dog name!";
    public const string DogWithSameNameExists = "Dog with this name already exists in database!";

    public const string ColorIsRequired = "Please, enter dog color!";

    public const string TailLengthWrong = "Tail lenght is wrong!";

    public const string WrongWeight = "Dog weight is wrong!";
}
