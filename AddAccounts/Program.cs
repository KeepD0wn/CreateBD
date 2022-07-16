using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AddAccounts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Add Accounts";
            MySqlConnection conn1 = new MySqlConnection();
            //ДОСТАЁМ И РАССКИДЫВАЕМ ДАННЫЕ АККАУНТОВ ИЗ ТЕКСТОВИКА
            try
            {
                if (true)//File.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}\License.lic")
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
                        int countGood = 0;

                        string[] subs = default;
                        if (File.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}\accounts.txt"))
                        {
                            string path = $@"{AppDomain.CurrentDomain.BaseDirectory}\accounts.txt";
                            string str = "";
                            try
                            {
                                using (StreamReader sr = new StreamReader(path))
                                {
                                    str = sr.ReadToEnd();
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            subs = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        }
                        else
                        {
                            Console.WriteLine("[SYSTEM] Accounts.txt not found");
                        }

                        for (int i = 0; i < subs.Length; i++)
                        {
                            string[] subs2 = subs[i].Split(':');


                            string login = subs2[0];
                            string password = subs2[1];
                            string secretKey = subs2[2];

                            try
                            {
                                var com = new MySqlCommand("USE csgo; " +
                                "insert into accounts (login, password, secretKey)" +
                                " values (@login, @password, @secretKey)", conn1);
                                com.Parameters.AddWithValue("@login", login);
                                com.Parameters.AddWithValue("@password", password);
                                com.Parameters.AddWithValue("@secretKey", secretKey);

                                com.ExecuteNonQuery();
                                countGood += 1;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }

                        try
                        {
                            var com1 = new MySqlCommand("USE csgo; SET SQL_SAFE_UPDATES = 0; set @`i`:= 0;update csgo.accounts set id = (@`i`:= @`i` + 1 ); ", conn1);
                            com1.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        Console.WriteLine($"Accounts added {countGood}");
                        Console.WriteLine("Done");
                    }
                    else
                    {
                        Console.WriteLine("[1][SYSTEM] License not found");
                        Thread.Sleep(5000);
                        conn1.Close();
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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn1.Close();
            }

            Console.ReadKey();
        }
    }
}
