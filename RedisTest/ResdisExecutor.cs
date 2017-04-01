using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StackExchange.Redis;

namespace RedisTest
{

    public delegate T RedisAction<T>(IDatabase db);
    public delegate void RedisAction(IDatabase db);

    public class ResdisExecutor
    {

        private static string GetConfig(string config)
        {
            if (!String.IsNullOrEmpty(config))
            {
                config = System.Configuration.ConfigurationManager.AppSettings[config];
            }

            if (String.IsNullOrEmpty(config))
                config = "127.0.0.1";

            return config;

        }

        public static void Exec(RedisAction act, string config = null)
        {
            if (act != null)
            {
                using (var client = ConnectionMultiplexer.Connect(GetConfig(config)))
                {
                    var db = client.GetDatabase();
                    if (db != null)
                    {
                        act(db);
                    }
                }
            }
        }

        public static T Exec<T>(RedisAction<T> act, string config = null)
        {
            if (act != null)
            {
                using (var client = ConnectionMultiplexer.Connect(GetConfig(config)))
                {
                    var db = client.GetDatabase();
                    if (db != null)
                    {
                        return act(db);
                    }
                }
            }

            return default(T);
        }

    }

}
