using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;

namespace Connection_with_system
{
    class Repostortemployee
    {
        DbProviderFactory factory;
        string provider;
        string connectionString;
        public Repostortemployee()
        {
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }
        public List<Employee>getAll()
        {
            //جمع معلومات على شكل ليست
            var emplyees = new List<Employee>();
            //اتصال من الفاكتوري
            using (var conniction = factory.CreateConnection()) 
            {
                //معرفه سيرفر و اسم داتا بيس
                conniction.ConnectionString = connectionString;
                //خلق اتصال
                conniction.Open();
                var command = factory.CreateCommand();
                command.Connection = conniction;
                //الان ارسال الامر الى داتا بيس 
                //ارسال امر بلغه sql
                command.CommandText = "Select * From Employess";
                //بيانات تم قرائتها و ارجاعها
                using (DbDataReader reader = command.ExecuteReader())
                {
                    //لما يكون ريدر لها قابليه على قرائه
                    while(reader.Read())
                    {
                        //تعريف انانيموس و تحويل تايب نفس داتا بيس استدعاء ريدر نفس اسم داتا بيس موجوده
                        emplyees.Add(new Employee() { ID = (int)reader["id"], FirstName = (string)reader["FirstName"], LastName = (string)reader["LastName"] });
                    }
                }
                

            }
            return emplyees;
        }
        public void ADD(Employee emplyee)
        {
            using(var conniction =factory.CreateConnection())
            {
                conniction.ConnectionString = connectionString;
                conniction.Open();
                var command = factory.CreateCommand();
                command.Connection = conniction;
                command.CommandText = $"Insert Into Employess(FirstName , LastName)Values('{emplyee.FirstName}','{emplyee.LastName}');";
                command.ExecuteNonQuery();
            }

        }
        public void UPdate(Employee employee)
        {
            using(var conniction =factory.CreateConnection())
            {
                conniction.ConnectionString = connectionString;
                conniction.Open();
                var command = factory.CreateCommand();
                command.Connection = conniction;
                command.CommandText = $"Update Employess Set FirstName = '{employee.FirstName}', LastName ='{employee.LastName}' WHERE Id ='{employee.ID}';";
                command.ExecuteNonQuery();
            }
                    
        }
        public void Delete(int id)
        {
            using(var conniction=factory.CreateConnection())
            {
                conniction.ConnectionString = connectionString;
                conniction.Open();
                var command = factory.CreateCommand();
                command.Connection = conniction;
                command.CommandText = $"Delete From Employess Where Id = {id};";
                command.ExecuteNonQuery();
            }
        }
    }
}
