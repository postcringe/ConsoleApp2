using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {

        static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=1;Integrated Security=True";
        static void Main(string[] args)
        {
            /*     Console.Write("Введите имя пользователя:");
                 string name = Console.ReadLine();

                 Console.Write("Введите возраст пользователя:");
                 int age = Int32.Parse(Console.ReadLine());

                 AddUser(name, age);*/

            // AddUser(10, "Иван", "1", "2", "3");
            Console.Write("Введите id пользователя:");
            int id = Int32.Parse(Console.ReadLine());
            Console.Write("Введите ФИО пользователя:");
            string fio = Console.ReadLine();
            Console.Write("Введите кол-во комнат пользователя:");
            string kolvo = Console.ReadLine();
            Console.Write("Введите этаж пользователя:");
            string et = Console.ReadLine();
            Console.Write("Введите метраж пользователя:");
            string metr = Console.ReadLine();
            AddUser(id, fio, kolvo, et, metr);
            Console.WriteLine();
            GetUsers();

            Console.Read();
        }
        // добавление пользователя
        private static void AddUser(int id, string fio,string kolvo,string et,string metr)
        {
            // название процедуры
            string sqlExpression = "sp_InsertUser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id
                };
                // добавляем параметр
                command.Parameters.Add(idParam);
                // параметр для ввода возраста
                SqlParameter fioParam = new SqlParameter
                {
                    ParameterName = "@fio",
                    Value = fio
                };
                command.Parameters.Add(fioParam);

                SqlParameter kolvoParam = new SqlParameter
                {
                    ParameterName = "@kolvo",
                    Value = kolvo
                };
                command.Parameters.Add(kolvoParam);

                SqlParameter etParam = new SqlParameter
                {
                    ParameterName = "@et",
                    Value = et
                };
                command.Parameters.Add(etParam);

                SqlParameter metrParam = new SqlParameter
                {
                    ParameterName = "@metr",
                    Value = metr
                };
                command.Parameters.Add(metrParam);
                
                var result = command.ExecuteScalar();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

                Console.WriteLine("Id добавленного объекта: {0}", result);
            }
        }

        // вывод всех пользователей
        private static void GetUsers()
        {
            // название процедуры
            string sqlExpression = "sp_GetUsers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetName(0), reader.GetName(1), reader.GetName(2),reader.GetName(3), reader.GetName(4));

                    while (reader.Read())
                    {
                        int id;
                        string fio;
                        string kolvo;
                        string et;
                        string metr;

                         id = reader.GetInt32(0);
                         fio = reader.GetString(1);
                        kolvo = reader.GetString(2);
                        et = reader.GetString(2);
                        metr = reader.GetString(2);
                        Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}\t{4}", id, fio, kolvo, et, metr);
                    }
                }
                reader.Close();
            }
        }
    }
}


