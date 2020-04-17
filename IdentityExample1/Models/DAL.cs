using Dapper;
using IdentityExample1.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample1.Models
{
    public class DAL
    {
        private SqlConnection conn;

        public DAL(string connectionString)
        {
            conn = new SqlConnection(connectionString);
        }
         
        public IEnumerable<UserTasks> GetAllTasks()
        {
            string queryString = "SELECT * FROM UserTasks";

            return conn.Query<UserTasks>(queryString);

        }

        public int CreateTask(UserTasks t)
        {
            t.Complete = 1; //always create status=1

            string addQuery = "INSERT INTO UserTasks (OwnerId, Description, DueDate, Complete) ";
            addQuery += "VALUES (@OwnerId, @Description, @DueDate, @Complete)";

            return conn.Execute(addQuery, t);
        }

        public LoginViewModel GetUserById(int id)
        {
            string queryString = "SELECT * FROM IdentityUser WHERE Id = @id";

            return conn.QueryFirstOrDefault<LoginViewModel>(queryString, new { id = id });
        }

        public IEnumerable<UserTasks> GetTasksByUserId(int id)
        {
            string queryString = "SELECT * FROM UserTasks WHERE OwnerId = @id";
            return conn.Query<UserTasks>(queryString, new { id = id });
        }

        public UserTasks GetTaskById(int id)
        {
            string queryString = "SELECT * FROM UserTasks WHERE TaskId = @id";
            return conn.QueryFirstOrDefault<UserTasks>(queryString, new { id = id });
        }

        public int DeleteTask(UserTasks t)
        {
            string markcomplete = "DELETE FROM UserTasks WHERE TaskId = @TaskId";

            return conn.Execute(markcomplete, t);
        }
        public IEnumerable<UserTasks> SortByDateAsc(UserTasks t)
        {
            string queryString = "SELECT * FROM UserTasks ORDER BY DueDate";

            return conn.Query<UserTasks>(queryString);
        }
        public IEnumerable<UserTasks> SortByDateDesc(UserTasks t)
        {
            string queryString = "SELECT * FROM UserTasks ORDER BY DueDate DESC";

            return conn.Query<UserTasks>(queryString);
        }
        public IEnumerable<UserTasks> GetTasksBySearch(string search)
        {
            string queryString = "SELECT * FROM UserTasks ";
            queryString += "where Description like @search ";
            return conn.Query<UserTasks>(queryString, new { Search = "%" + search + "%" });
        }

        public int UpdateTaskById(UserTasks t)
        {
            string editString = "UPDATE UserTasks SET Description = @Description, DueDate = @DueDate ";
            editString += "WHERE TaskId = @TaskId";
            return conn.Execute(editString, t);
        }
        public int MarkComplete(UserTasks t)
        {
            string markcomplete = "Update UserTasks SET Complete = '0' WHERE TaskId = @TaskId";

            return conn.Execute(markcomplete, t);
        }


    }
}
