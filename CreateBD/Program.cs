using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreateBD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Create DB";
            MySqlConnection conn1 = new MySqlConnection();
            try
            {
                if (true) //File.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}\License.lic") 22
                {
                    //string key = "";
                    //using (StreamReader sr = new StreamReader($@"{AppDomain.CurrentDomain.BaseDirectory}\License.lic"))
                    //{
                    //    key = sr.ReadToEnd();
                    //}
                    //key = key.Replace("\r\n", "");

                    //MySqlConnection conn = new MySqlConnection();
                    //try
                    //{
                    //    conn = new MySqlConnection(Properties.Resources.String1);
                    //    conn.Open();

                    //    var com = new MySqlCommand("USE `MySQL-5846`; " +
                    //     "select * from `subs` where keyLic = @keyLic AND subEnd > NOW() AND activeLic = 1 limit 1", conn);
                    //    com.Parameters.AddWithValue("@keyLic", key);

                    //    using (DbDataReader reader = com.ExecuteReader())
                    //    {
                    //        if (reader.HasRows) //тут уходит на else если нет данных
                    //        {

                    //        }
                    //        else
                    //        {
                    //            conn.Close();
                    //            Console.WriteLine("[SYSTEM] License is not active");
                    //            Thread.Sleep(5000);
                    //            Environment.Exit(0);
                    //        }
                    //    }
                    //    conn.Close();
                    //}
                    //catch
                    //{
                    //    conn.Close();
                    //    Console.WriteLine("[SYSTEM][404] Something went wrong!");
                    //    Thread.Sleep(5000);
                    //    Environment.Exit(0);
                    //}
                    //finally
                    //{
                    //    conn.Close();
                    //}

                    if (true) //PcInfo.GetCurrentPCInfo() == key
                    {
                        conn1 = DBUtils.GetDBConnection();
                        conn1.Open();

                        //-------------------------------------------------------------------------------------------------------------------
                        //СОЗДАНИЕ БД И ТАБЛИЦЫ

                        string createDBCommand = "CREATE DATABASE IF NOT EXISTS csgo; USE csgo;";
                        var cmd = new MySqlCommand(createDBCommand, conn1);
                        cmd.ExecuteNonQuery();

                        string createDBCommand1 = "CREATE TABLE IF NOT EXISTS `accounts` (" +
                            "`id` int NOT NULL AUTO_INCREMENT," +
                            "`login` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL," +
                            "`password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL," +
                            "`secretKey` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL," +
                            "`isOnline` tinyint DEFAULT '0'," +
                            "`lastDateOnline` datetime DEFAULT CURRENT_TIMESTAMP," +
                            "`canPlayDate` datetime DEFAULT CURRENT_TIMESTAMP," +
                            "`folderCreated` tinyint DEFAULT '0'," +
                            "PRIMARY KEY (`id`)," +
                            "UNIQUE KEY `id_UNIQUE` (`id`)," +
                            "UNIQUE KEY `login_UNIQUE` (`login`)" +
                            ") ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 " +
                            "COLLATE=utf8mb4_0900_ai_ci";

                        var cmd1 = new MySqlCommand(createDBCommand1, conn1);
                        cmd1.ExecuteNonQuery();

                        Console.WriteLine("Data base created");
                        Console.WriteLine("Done");
                    }
                    else
                    {
                        Console.WriteLine("[1][SYSTEM] License not found");
                        Thread.Sleep(5000);
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("[2][SYSTEM] License not found");
                    Thread.Sleep(5000);
                    conn1.Close();
                    Environment.Exit(0);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("[SYSTEM] License not found");
                Thread.Sleep(5000);
                conn1.Close();
                Environment.Exit(0);
            }
            finally
            {
                conn1.Close();
            }

            Console.ReadKey();
        }
    }
}