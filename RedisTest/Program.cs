using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace RedisTest
{
    class Program
    {

        static void Main(string[] args)
        {

            var redis = new ResdisExecutor();

            //write
            redis.Exec((db) =>
            {
                //db.StringSet("key1", new Random().Next(10000, 99999));
                //db.HashSet("key2", new HashEntry[] {
                //    new HashEntry("name", "崔津铭"),
                //    new HashEntry("loginname", "cjm"),
                //    new HashEntry("pwd", "123456"),
                //    new HashEntry("email", "cjm@handday.com"),
                //    new HashEntry("age", 30),
                //});

                for(int i=100;i<200;i++)
                {
                    var key = "key" + i;
                    db.StringSet(key, new Random().Next(10000, 99999));
                }



            });

            //show
            redis.Exec((db) =>
            {
                //Console.WriteLine(db.StringGet("key1"));
                //db.HashValues("key2").All((val) =>
                //{
                //    Console.WriteLine(val);
                //    return true;
                //});

                //Console.WriteLine(db.KeyRandom());



            });


        }
    }
}
