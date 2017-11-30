using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTMarsvineService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private string GetConnectionString = "Server=tcp:bamm.database.windows.net,1433;Initial Catalog=Bamm;Persist Security Info=False;User ID=Bamm;Password=Mik112mik112;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public IList<Measurements> GetAllMeasurements()
        {
                const string sqlstring = "SELECT * from dbo.Measurement order by Time DESC";

                using (var sqlConnection = new SqlConnection(GetConnectionString))
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(sqlstring, sqlConnection))
                    {
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            var liste = new List<Measurements>();
                            while (reader.Read())
                            {
                                var _measurements = ReadMeasurements(reader);
                                liste.Add(_measurements);
                            }
                            return liste;
                        }
                    }
                }
        }





        public void UpdateMail(string id, Userinfo user)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString);
            conn.Open();



            string sql = $"UPDATE Apartment SET Id = @id, Mail = @mail, PhoneNo = @phone WHERE id = '{id}'";

            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@mail", user.Mail);
            command.Parameters.AddWithValue("@phone", user.PhoneNo);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var _Userinfo = ReadMeasurements(reader);
            }
        }

        public IList<Userinfo> GetAllUsers()
        {
            const string sqlstring = "SELECT * from dbo.UserTable order by id";

            using (var sqlConnection = new SqlConnection(GetConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(sqlstring, sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        var liste = new List<Userinfo>();
                        while (reader.Read())
                        {
                            var _users = ReadUserinfo(reader);
                            liste.Add(_users);
                        }
                        return liste;
                    }
                }
            }
        }

        private static Measurements ReadMeasurements(IDataRecord reader)
        {
            var Id = reader.GetInt32(0);
            var Time = reader.GetDateTime(1);
            var dB = reader.GetInt32(2);
            var ImageLink = reader.GetString(3);


            var i = new Measurements { Id = Id, DateTime = Time, dB = dB, ImageLink = ImageLink };

            return i;
        }

        private static Userinfo ReadUserinfo(IDataRecord reader)
        {
            var Id = reader.GetInt32(0);
            var Mail = reader.GetString(1);
            var Phone = reader.GetInt32(2);


            var i = new Userinfo { Id = Id, Mail = Mail, PhoneNo = Phone };

            return i;
        }
    }

}