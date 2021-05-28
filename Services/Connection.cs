using System;
using Microsoft.Extensions.Configuration;

namespace ConFriend.Services
{
    public abstract class Connection
    {
        protected String connectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public Connection(string connectionString)
        {
            Configuration = null;
            this.connectionString = connectionString;
        }
    }
}