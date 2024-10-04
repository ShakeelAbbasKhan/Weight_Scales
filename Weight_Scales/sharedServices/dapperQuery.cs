using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Weight_Scales.Configuration;

namespace Weight_Scales.Services
{
    public class dapperQuery
    {
        private readonly string _connectionString;

        public dapperQuery(IConnectionService connectionService)
        {
            _connectionString = connectionService.ConnectionString;
        }

        // for identity column to increment one
        public T QrySingle<T>(string sql)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.QueryFirstOrDefault<T>(sql);
            }
        }

        public IEnumerable<T> Qry<T>(string sql)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<T>(sql);
            }
        }

        //async
        public async Task<IEnumerable<T>> QryAsync<T>(string sql)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<T>(sql);
            }
        }

        public IEnumerable<T> QryResult<T>(string sql)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<T>(sql).ToList();
            }
        }

        // async
        public async Task<IEnumerable<T>> QryResultAsync<T>(string sql)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<T>(sql);
            }
        }

        public IEnumerable<int> CRUDQry(string query, DynamicParameters parameters)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var rowAffected = con.Execute(query, parameters, commandType: CommandType.Text);
                yield return rowAffected;
            }
        }

        // async
        public async Task<int> CRUDQryAsync(string query, DynamicParameters parameters)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.ExecuteAsync(query, parameters, commandType: CommandType.Text);
            }
        }

        // return single parameters
        public IEnumerable<string> SPReturn<T>(string procedure, T model)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var row = con.Query<string>(procedure, model, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                    con.Close();

                return row;
            }
        }

        // return multiple parameters
        public IEnumerable<dynamic> SPReturn1<T>(string procedure, T model)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var rows = con.Query<dynamic>(procedure, model, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                    con.Close();

                return rows;
            }
        }

        // async
        public async Task<IEnumerable<string>> SPReturnAsync<T>(string procedure, T model)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var row = await con.QueryAsync<string>(procedure, model, commandType: CommandType.StoredProcedure);

                if (con.State == ConnectionState.Open)
                    con.Close();

                return row;
            }
        }

        public string saveImageFile(string regPath, string name, string binData, string ext)
        {
            string path = regPath; // Path
            // Check if directory exists
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); // Create directory if it doesn't exist
            }

            string imageName = name + "." + ext;

            // Set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(binData);

            System.IO.File.WriteAllBytes(imgPath, imageBytes);

            return "Ok";
        }
    }
}
