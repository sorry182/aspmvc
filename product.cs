using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace crud.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }

        public decimal price { get; set; }



        string con_string()
        {
            return ConfigurationManager.ConnectionStrings["conn"].ToString();
        }
        public int Insert(Product model)
        {   
            string query = "insert into product (name,price) values('" + model.name + "','" + model.price + "')";

            return EUpdate(query);

        }

        public int Update(Product model)
        {
            string query = "update product SET name ='" + model.name + "', price='" + model.price + "'where id = '" + id + "'";
            System.Diagnostics.Debug.WriteLine(query);
            return EUpdate(query);
        }

        public int Delete(int id)
        {
            string query = "delete from product where id = '" + id + "'";

            return EUpdate(query);
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

        public List<Product> GetAllProduct()
        {
            List<Product> list = new List<Product>();

            SqlConnection con = new SqlConnection(con_string());
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from product";
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {   Product p = new Product();
                    p.id = reader.GetInt32(0);
                    p.name = reader.GetString(1);
                    p.price = reader.GetDecimal(2);
                    list.Add(p);
                }
            }
            
            con.Close();
            return list;
        }

        public Product GetProductById(int id)
        {
            Product p = new Product();

            SqlConnection con = new SqlConnection(con_string());
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from product where id='"+id+"'";
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    p.id = reader.GetInt32(0);
                    p.name = reader.GetString(1);
                    p.price = reader.GetDecimal(2);
                    
                }
                return p;
                   
            }else
            {
                return null;
            }
            

        }

        


    }


}