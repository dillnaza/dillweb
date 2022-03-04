using System.Data.SqlClient;

namespace web.Models
{
    public class CountBook
    {
        public int Category { get; set; } =1;
        public string CatCount { get; set; }

        public int GetCategory()
        {
            Category = 1;
            return Category;
        }

        public CountBook GetCount()
        {
            string connectionString = "Data Source = DESKTOP-HQF96MO\\SQLEXPRESS; Initial Catalog = master; Integrated Security=True; TrustServerCertificate=True";
            SqlConnection sqlConnection = new(connectionString);
            sqlConnection.Open();

            string CategoryCount = "SELECT Count(BOOK.COUNT) AS [BOOKCOUNT] " +
                "FROM BOOK GROUP BY BOOK.CATEGORY " +
                "HAVING(BOOK.CATEGORY = " + Category + ")";

            SqlCommand cmd = new(CategoryCount, sqlConnection);

            SqlDataReader reader = cmd.ExecuteReader();

            CountBook count = new();

            if (reader.Read())
            {
                count.CatCount = reader["BOOKCOUNT"].ToString();
            }

            sqlConnection.Close();

            return count;
        }
    }
}
