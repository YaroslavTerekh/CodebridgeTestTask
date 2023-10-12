using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.BL.Settings;

public class ApplicationInfo
{
    public string ApplicationName { get; set; }

    public string ApplicationVersion { get; set; }

    public override string ToString()
    {
        return String.Concat(ApplicationName, ".Version", ApplicationVersion);
    }
}
