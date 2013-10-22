using AspLab.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspLab_AppCode
{
	public class DataBaseProvider
	{
		#region Props

		private readonly SqlConnection sqlConnection;

		private List<string> categoriesList;
		public List<string> CategoriesList
		{
			get { return categoriesList; }
		}

		private List<Product> productsList;
		public List<Product> ProductsList
		{
			get { return productsList; }
		}

		private List<Product> ordersList;
		public List<Product> OrdersList
		{
			get { return ordersList; }
			set { ordersList = value; }
		}

		private Dictionary<string, int?> categoriesDictionary;

		#endregion

		#region Ctor

		public DataBaseProvider()
		{
			this.sqlConnection = new SqlConnection("Data Source=aspLabs.mssql.somee.com;Initial Catalog=aspLabs;Persist Security Info=True;User ID=ratatui_SQLLogin_1;Password=pxyg3qg19i");
			this.categoriesList = new List<string>();
			this.productsList = new List<Product>();
			this.categoriesDictionary = new Dictionary<string, int?>();
			this.ordersList = new List<Product>();
		}

		#endregion

		#region Methods

		public List<string> LoadCategoriesList()
		{
			this.categoriesList.Clear();
			this.productsList.Clear();
			this.categoriesDictionary.Clear();

			var query = @"SELECT * FROM Categories ORDER BY Name";
			SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
			sqlCommand.Connection.Open();

			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				var category = sqlDataReader["Name"].ToString();
				this.categoriesList.Add(category);
				this.categoriesDictionary.Add(category, sqlDataReader["CategoryId"] as int?);
			}
			sqlDataReader.Close();
			sqlCommand.Connection.Close();

			return this.categoriesList;
		}

		public List<Product> LoadProductsList(string categoryName)
		{
			this.productsList.Clear();

			int? categoryId = null;
			if (this.categoriesDictionary.Keys.Contains(categoryName))
				categoryId = this.categoriesDictionary[categoryName];
			else
				return null;

			var query = @"SELECT * FROM Products WHERE CategoryId = " + categoryId.ToString() + @" ORDER BY Name";
			SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
			sqlCommand.Connection.Open();

			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				this.productsList.Add(new Product() { ProductName = sqlDataReader["Name"].ToString(), CategoryId = sqlDataReader["CategoryId"] as int?, ProductId = sqlDataReader["ProductId"] as int? });
			}
			sqlDataReader.Close();
			sqlCommand.Connection.Close();

			return this.productsList;
		}

		public List<Product> LoadOrdersList(string userName)
		{
			var query = @"SELECT * FROM Products INNER JOIN Orders ON Products.ProductId = Orders.ProductId WHERE Orders.Username = '" + userName + "'";
			SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
			sqlCommand.Connection.Open();

			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				this.ordersList.Add(new Product() { ProductName = sqlDataReader["Name"].ToString() });
			}
			sqlDataReader.Close();
			sqlCommand.Connection.Close();

			return this.ordersList;
		}

		public void CreateOrderLines(List<Product> ordersList, string userName)
		{
			this.sqlConnection.Open();
			foreach (var item in ordersList)
			{
				var query = @"INSERT INTO Orders(ProductId, Username) VALUES(@param1, @param2)";
				SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
				sqlCommand.Parameters.Add("param1", System.Data.SqlDbType.Int).Value = item.ProductId;
				sqlCommand.Parameters.Add("param2", System.Data.SqlDbType.NVarChar).Value = userName;
				sqlCommand.ExecuteNonQuery();
			}
			this.sqlConnection.Close();
		}

		#endregion
	}
}