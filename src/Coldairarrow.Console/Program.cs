using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Console1
{
    class Program
    {
        static async Task Main()
        {
            IQueryable<string> iq = new List<string>().AsQueryable();
            var list = await iq.ToListAsync();

            Console.WriteLine("完成");
            Console.WriteLine();
        }
    }
}