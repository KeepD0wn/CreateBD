﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CreateBD
{
    class DBMySQLUtils
    {

        public static MySqlConnection GetDBConnection(string host, int port, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

    }
}