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

        private string _config;

        public ResdisExecutor(string config)
        {
            _config = config;
        }
        public ResdisExecutor()
        {
            _config = "127.0.0.1";
        }

        public void Exec(RedisAction act)
        {
            ResdisExecutor.ExecCommand(act, _config);
        }
        public T Exec<T>(RedisAction<T> act)
        {
            return ResdisExecutor.ExecCommand<T>(act, _config);
        }

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

        public static void ExecCommand(RedisAction act, string config = null)
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

        public static T ExecCommand<T>(RedisAction<T> act, string config = null)
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
