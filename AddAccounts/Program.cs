using System;
using System.Collections.Generic;
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
            MySqlConnection conn = new MySqlConnection();
            //ДОСТАЁМ И РАССКИДЫВАЕМ ДАННЫЕ АККАУНТОВ ИЗ ТЕКСТОВИКА
            try
            {
                if (File.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}\License.lic"))
                {
                    string key = "";
                    using (StreamReader sr = new StreamReader($@"{AppDomain.CurrentDomain.BaseDirectory}\License.lic"))
                    {
                        key = sr.ReadToEnd();
                    }
                    key = key.Replace("\r\n", "");

                    if (PcInfo.GetCurrentPCInfo() == key)
                    {
                        conn = DBUtils.GetDBConnection();
                        conn.Open();
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
                                " values (@login, @password, @secretKey)", conn);
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

                        Console.WriteLine($"Accounts added {countGood}");
                        Console.WriteLine("Done");
                    }
                    else
                    {
                        Console.WriteLine("[SYSTEM] License not found");
                        Thread.Sleep(5000);
                        conn.Close();
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("[SYSTEM] License not found");
                    Thread.Sleep(5000);
                    conn.Close();
                    Environment.Exit(0);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            Console.ReadKey();
        }
    }
}
