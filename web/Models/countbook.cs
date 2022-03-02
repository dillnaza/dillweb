using Microsoft.Data.SqlClient;

namespace web.Models
{
    public class Countbook
    {
        public string CatCount { get; set; }
        public Countbook GetCountName()
        {
            string connectionString = "Data Source = DESKTOP-HQF96MO\\SQLEXPRESS; Initial Catalog = master; Integrated Security=True; TrustServerCertificate=True";
            SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();

            string CategoryCount = "SELECT Count(BOOK.COUNT) AS [BOOKCOUNT] " +
                "FROM BOOK GROUP BY BOOK.CATEGORY " +
                "HAVING(BOOK.CATEGORY = 1)";

            SqlCommand cmd = new(CategoryCount, sqlConnection);

            SqlDataReader reader = cmd.ExecuteReader();

            Countbook count = new();

            if (reader.Read())
            {
                count.CatCount = reader["BOOKCOUNT"].ToString();
            }

            sqlConnection.Close();

            return count;
        }
    }
}
