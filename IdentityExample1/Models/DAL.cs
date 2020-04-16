﻿using Dapper;
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


    }
}
