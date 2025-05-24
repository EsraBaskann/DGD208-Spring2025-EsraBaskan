using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Dgd;

public class Program
{
    public static async Task Main(string[] args)
    {        
        var menu = new Menu();
        await menu.ShowMainMenu();
    }
}