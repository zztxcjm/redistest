using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisTest
{
    class Program
    {

        static void Main(string[] args)
        {

            var key = "key" + new Random().Next(1, 100);

            ResdisExecutor.Exec((db) =>
            {
                db.StringSet(key, key);
            });

            var val = ResdisExecutor.Exec<string>((db) =>
            {
                return db.StringGet(key);
            });

            Console.WriteLine(val);

        }
    }
}
