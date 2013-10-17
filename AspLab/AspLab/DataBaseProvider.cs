using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspLab
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

		private List<string> productsList;
		public List<string> ProductsList
		{
			get { return productsList; }
		}

		private Dictionary<string, int?> categoriesDictionary;

		#endregion

		#region Ctor

		public DataBaseProvider()
		{
			this.sqlConnection = new SqlConnection("Data Source=aspLabs.mssql.somee.com;Initial Catalog=aspLabs;Persist Security Info=True;User ID=ratatui_SQLLogin_1;Password=pxyg3qg19i");
			this.categoriesList = new List<string>();
			this.productsList = new List<string>();
			this.categoriesDictionary = new Dictionary<string, int?>();
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

		public List<string> LoadProductsList(string categoryName)
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
				this.productsList.Add(sqlDataReader["Name"].ToString());
			}
			sqlDataReader.Close();
			sqlCommand.Connection.Close();

			return this.productsList;
		}

		#endregion
	}
}