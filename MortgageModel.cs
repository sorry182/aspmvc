using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;


namespace MortgageCalculator.Models
{
    public class MortgageModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Principal { get; set; }

        public decimal Rate { get; set; }

        public int Years { get; set; }

        public decimal Monthly { get; set; }


        string con_string()
        {
            return ConfigurationManager.ConnectionStrings["conn"].ToString();
        }

        public int EUpdate(string query)
        {
            SqlConnection con = new SqlConnection(con_string());
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public Boolean checkpassword(string user,string pass)
        {
            

            SqlConnection con = new SqlConnection(con_string());
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            string query = "select * from account where username ='" + user + "' and password = '" + pass + "'";
            cmd.CommandText = query;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                
                return true;
            }
            else
            {
               
                return false;
            }

        }
        public List<MortgageModel> GetHistory()
        {
            List<MortgageModel> list = new List<MortgageModel>();

            SqlConnection con = new SqlConnection(con_string());
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from history";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MortgageModel temp = new MortgageModel();
                    temp.Id = reader.GetInt32(0);
                    temp.Date = reader.GetDateTime(1);
                    temp.Principal = reader.GetInt32(2);
                    temp.Rate = reader.GetDecimal(3);
                    temp.Years = reader.GetInt32(4);
                    temp.Monthly = reader.GetDecimal(5);
                    list.Add(temp);
                }
            }
            con.Close();
            return list;
        }


        public int Insert(MortgageModel model)
        {
            string query = "insert into history (date, principal, rate, years, monthly) values('" + DateTime.Now + "','" + model.Principal + "','" + model.Rate + "'," + model.Years + ",'" + model.Monthly + "')";
            
            return EUpdate(query);

        }

        public int Update(MortgageModel model)
        {
            string query = "update history update principal ='"+model.Principal+"' rate='"+model.Rate+"' years='"+model.Years+"' monthly='"+model.Monthly+"' where id='"+model.Id+"'";

            return EUpdate(query);
        }

        public int Delete(int id)
        {
            string query = "delete from history where id = '"+id+"'";

            return EUpdate(query);
        }




    }

   

}