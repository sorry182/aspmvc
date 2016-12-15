# aspmvc


Message reply:
Response.Write("message");

Conversion
Convert.ToInt32(Request["id"]);
Convert.ToDecimal();

AJAX CALL

 jQuery.ajax({
                type: "POST",
                url: "/Product/Update",
                data: {
                    id:id,
                    name: name,
                    price: price
                },
                success: function (data) {
                    document.getElementById("confirm").innerHTML = data;
                }
        });
        
DB ACCESS

WEB.CONFIG
 <connectionStrings>
    <add name="conn"
   connectionString="Data Source=(localdb)\ProjectsV13;Initial Catalog=mortgage;Integrated Security=True"
   providerName="System.Data.SqlClient"/>
  </connectionStrings>
  
string con_string()
{
    return "Data Source = (localdb)\\ProjectsV13; Initial Catalog = mortgage; Integrated Security = True";
}

Query
{
   SqlConnection con = new SqlConnection(con_string());
   SqlCommand cmd = new SqlCommand(query, con);
   cmd.Connection.Open();
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
}

Update

 public int EUpdate(string query)
{
           SqlConnection con = new SqlConnection(con_string());
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Connection.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
 }
 
 TABLE CREATE
 CREATE TABLE product (
    id   INT             IDENTITY NOT NULL PRIMARY KEY,
    name VARCHAR (50)    NULL,
    price DECIMAL (10, 2) NULL, 
);
