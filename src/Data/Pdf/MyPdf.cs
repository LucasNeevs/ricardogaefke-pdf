using System;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using RicardoGaefke.Domain;

namespace RicardoGaefke.Data
{
  public class MyPdf : IMyPdf
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connStr;

    public MyPdf(IOptions<Secrets.ConnectionStrings> ConnectionStrings)
    {
      _connStr = ConnectionStrings;
    }

    public Form Insert(Form data)
    {
      int id = 0;
      string guid = string.Empty;

      using (SqlConnection Con = new SqlConnection(_connStr.Value.SqlServer))
      {
        using (SqlCommand Cmd = new SqlCommand())
        {
          Cmd.CommandType = CommandType.StoredProcedure;
          Cmd.Connection = Con;
          Cmd.CommandText = "[sp_PDF_ADD]";

          Cmd.Parameters.AddWithValue("@QUESTION1", data.Question1);
          Cmd.Parameters.AddWithValue("@QUESTION2", data.Question2);
          Cmd.Parameters.AddWithValue("@QUESTION3", data.Question3);
          Cmd.Parameters.AddWithValue("@QUESTION4", data.Question4);
          Cmd.Parameters.AddWithValue("@QUESTION5", data.Question5);
          Cmd.Parameters.AddWithValue("@APPROVED", data.Approved);
          Cmd.Parameters.AddWithValue("@INFO", data.Info);
          Cmd.Parameters.AddWithValue("@NAME", data.Name);
          Cmd.Parameters.AddWithValue("@EMAIL", data.Email);

          Con.Open();

          using (SqlDataReader MyDR = Cmd.ExecuteReader())
          {
            if (MyDR.HasRows)
            {
              MyDR.Read();

              id = MyDR.GetInt32(0);
              guid = MyDR.GetGuid(1).ToString();
            }
          }
        }
      }

      return new Form(id, guid);
    }
  }
}
