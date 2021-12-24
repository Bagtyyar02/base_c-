using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static string MSSQLServerName { get; private set; }
        public static string MSSQLServerDbName { get; private set; }

        public static void Main(string[] args)
        {
            string SERVER_NAME = ".";
            string DATABASE_NAME = "akhasap";
            SqlConnection conn;
            try
            {
                #region config okamaly
                using (StreamReader _reader = new StreamReader("config.sys"))
                {
                    string _jsonFile = _reader.ReadToEnd();
                    config_file _dk_globalConf = JsonConvert.DeserializeObject<config_file>(_jsonFile);
                    MSSQLServerName = _dk_globalConf.ServerName;
                    MSSQLServerDbName = _dk_globalConf.ServerDbName;

                }
                Console.WriteLine("Read file successfully");
                Console.ReadKey();
                #endregion
            }
            catch //(Exception ex)
            {
                
            }
            try
            {

                // servere(baza) baglanyas
                conn = new SqlConnection($"Server = {SERVER_NAME}; Database = {DATABASE_NAME}; Trusted_Connection = True;");
                
                conn.Open();


                #region baza maglumat girizyas 
                try
                {
                    string query_insert_data = "insert into tbl_mg_materials (material_code) values('birzat')";
                    SqlCommand cmd_insert = new SqlCommand();
                    cmd_insert.Connection = conn;
                    cmd_insert.CommandText = query_insert_data;
                    //yazan querymyzy isledyas
                    cmd_insert.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Maglumat girizmekde problema cykdy");
                    Console.ReadKey();
                }

                #endregion

                #region bazadan maglumat cekyas
                try
                {
                    SqlDataReader reader;

                    string query_get_data = "select * from tbl_mg_materials";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = query_get_data;
                    reader = cmd.ExecuteReader();
                    // sqldatareader komegi bilen bazadan maglumat cekyas
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetValue(1) + " " + reader.GetValue(1));

                    }
                    Console.WriteLine("Total records: ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("bazadan maglumat cekmekde Problema cydky");
                    Console.ReadKey();
                }
                
                #endregion


            }
            catch (Exception ex)
            {
                Console.WriteLine("baza bilen islesip bolmady");
                Console.WriteLine(ex);
            }
            try
            {
                #region papkan icinde fayllary okamaly
                string[] file_paths = Directory.GetFiles(@"D:\DesktopReal\films");
                for (int i = 0; i < file_paths.Length; i++)
                {
                    file_paths[i] = file_paths[i].Substring(file_paths[i].LastIndexOf('\\') + 1);
                    Console.WriteLine(file_paths[i]);
                }
                #endregion
            }
            catch (Exception ex)
            {
               
                throw;
            }
            Console.ReadKey();
        }

       
       
    }
}
