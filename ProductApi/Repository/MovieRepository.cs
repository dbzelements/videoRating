using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ProductApi.Repository.Interfaces;
using ProductApi.Models;

namespace ProductApi.Repository
{

    public class MovieRepository : IMovieRepository, IDisposable
    {
        private readonly IConfiguration _config;

         public MovieRepository()
        {
        
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection("Data Source=.;Initial Catalog=Videoden;Trusted_Connection=True");
            }
        }

        public async Task<Movie> GetByID(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery =  "SELECT Id,Title,Category,Rating,IsDeleted,DeletedDate FROM MovieList WHERE Id = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Movie>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<MovieDisplay>> GetActiveAllMovies()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT M.Id, M.Title, C.CategoryName, M.Rating FROM MovieList M INNER JOIN Category C ON C.id = M.Category WHERE IsDeleted = 0";
                conn.Open();
                var result = await conn.QueryAsync<MovieDisplay>(sQuery);
                return result.ToList();
            }
        }

        public async Task<List<MovieDisplay>> GetActiveAllMoviesFiltered(string filter)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT M.Id, M.Title, C.CategoryName, M.Rating FROM MovieList M INNER JOIN Category C ON C.id = M.Category WHERE IsDeleted = 0 AND M.Title LIKE '%" + filter + "%'";
                conn.Open();
                var result = await conn.QueryAsync<MovieDisplay>(sQuery);
                return result.ToList();
            }
        }

        public async Task<List<MovieDisplay>> GetActiveAllMoviesRating(int rating)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT M.Id, M.Title, C.CategoryName, M.Rating FROM MovieList M INNER JOIN Category C ON C.id = M.Category WHERE IsDeleted = 0 AND M.Rating = '"+ rating + "'";
                conn.Open();
                var result = await conn.QueryAsync<MovieDisplay>(sQuery);
                return result.ToList();
            }
        }


        public async Task<Category> GetCategory(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery =  "SELECT Id, CategoryName FROM Category WHERE Id = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Category>(sQuery, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<List<Category>> GetAllCategory()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT Id, CategoryName FROM Category";
                conn.Open();
                var result = await conn.QueryAsync<Category>(sQuery);
                return result.ToList();
            }
        }

        
        public bool UpdateMovie(long Id, Movie updateMovie)
        {
            var updateStatement = "UPDATE MovieList \n";
            updateStatement += "  SET Title = @Title \n";
            updateStatement += " , Category = @Category \n";
            updateStatement += " , Rating = @Rating \n";
            updateStatement += " , IsDeleted = @IsDeleted \n";
            updateStatement += " , DeletedDate = @DeletedDate \n";
            updateStatement += " WHERE Id = @Id";

            return Connection.Execute(updateStatement, updateMovie) > 0;
        }

        public bool create(Movie newMovie)
        {
            var insertStatement = "INSERT INTO [MovieList]  \n";

            insertStatement += "([Title] \n";
            insertStatement += ",[Category] \n";
            insertStatement += ",[Rating] \n";
            insertStatement += ",[IsDeleted] \n";
            insertStatement += ") VALUES ( \n";
            insertStatement += "  @Title \n";
            insertStatement += " ,@Category \n";
            insertStatement += " ,@Rating \n";
            insertStatement += " ,@IsDeleted \n";
            insertStatement += ")";

            return Connection.Execute(insertStatement, newMovie) > 0;
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MovieRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }


}