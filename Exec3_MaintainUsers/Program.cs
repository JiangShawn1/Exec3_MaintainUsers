using ISPan.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Exec3_MaintainUsers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbHelper = new SqlDbHelper("default");

            string sq1 = @"insert into Users(Name ,Account ,Password ,DateOfBirthd ,Height )
                                      values(@Name,@Account,@Password,@DateOfBirthd,@Height)";
            try
            {
                var parameters = new SqlParameterBuilder()
                   .AddNvachar("Name", 50, "LeBron-James")
                   .AddNvachar("Account", 50, "洛杉磯")
                   .AddNvachar("Password", 50, "passLeBron")
                   .AddDateTime("DateOfBirthd", new DateTime(1984, 12, 30))
                   .AddInt("Height", 206)

                //var parameters = new SqlParameterBuilder()
                //    .AddNvachar("Name", 50, "Russell_Westbrook")
                //    .AddNvachar("Account", 50, "洛杉磯")
                //    .AddNvachar("Password", 50, "passWestbrook")
                //    .AddDateTime("DateOfBirthd", new DateTime(1988, 11, 12))
                //    .AddInt("Height", 191)

                    .Build();

                dbHelper.ExecuteNonQuery(sq1, parameters);

                Console.WriteLine("資料已新增");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"操作失敗，原因:{ex.Message}");
            }

            string sq2 = @"delete from Users
                         where id=@id";
            try
            {
                var parameters = new SqlParameterBuilder()
                    .AddInt("id", 1)
                    .Build();

                dbHelper.ExecuteNonQuery(sq2, parameters);

                Console.WriteLine("資料已刪除");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"操作失敗，原因:{ex.Message}");
            }

            string sq3 = @"select * from Users
                           where id >@id
                           order by id desc";
            try
            {
                var parameter = new SqlParameterBuilder()
                .AddInt("id", 0)
                .Build();

                DataTable news = dbHelper.Select(sq3, parameter);

                foreach (DataRow row in news.Rows)
                {
                    int id = row.Field<int>("id");
                    string name = row.Field<string>("Name");
                    string account = row.Field<string>("Account");
                    string password = row.Field<string>("Password");
                    DateTime dateOfBirthd = row.Field<DateTime>("DateOfBirthd");
                    int height = row.Field<int>("Height");

                    Console.WriteLine($"Id={id},Name={name},Account={account}," +
                                      $"Password={password},DateOfBirthd={dateOfBirthd},Height={height}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"操作失敗，原因:{ex.Message}");
            }

            string sq4 = @"update Users set
                         Password=@Password
                         where id=@id";
            try
            {
                var parameters = new SqlParameterBuilder()
                    .AddNvachar("Password", 50, "passLeBron")
                    .AddInt("id", 2)
                    .Build();

                dbHelper.ExecuteNonQuery(sq4, parameters);

                Console.WriteLine("資料已修改");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"操作失敗，原因:{ex.Message}");
            }


        }
    }
}
