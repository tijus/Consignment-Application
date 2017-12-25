using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RLogicServiceMvc.Service.Internal;
using System.Data.SqlClient;
using System.Data;

namespace RLogicServiceMvc.Service.DataAccess
{
    public class SqlDataAceessLayer
    {
        private string _connectionString;

        public SqlDataAceessLayer(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public SqlDataAceessLayer()
        {
            _connectionString = CompanyConfigService.GetParameter("DBCredential");
        }
        public SqlParameter MakeInParams(string paramName, SqlDbType paramType, int size, object obj)
        {
            return MakeParameter(paramName, paramType, size, ParameterDirection.Input, obj);
        }

        public SqlParameter MakeOutParams(string paramName, SqlDbType paramType, int size)
        {
            return MakeParameter(paramName, paramType, size, ParameterDirection.Output, null);
        }

        private SqlParameter MakeParameter(string paramName, SqlDbType paramType, int size, ParameterDirection paramDirection, object obj)
        {
            SqlParameter sqlParam = null;
            if (size > 0)
            {
                sqlParam = new SqlParameter(paramName, paramType, size);
            }
            else
            {
                sqlParam = new SqlParameter(paramName, paramType);
            }
            sqlParam.Direction = paramDirection;
            if (!(sqlParam.Direction == paramDirection & obj == null))
            {
                sqlParam.Value = obj;
            }
            return sqlParam;
        }
        private SqlCommand GetCommand()
        {
            SqlConnection sqlCon = new SqlConnection(_connectionString);
            sqlCon.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlCon;
            cmd.CommandTimeout = 500;
            return cmd;
        }

        public DataSet ExecuteSql(string SqlStatement)
        {
            SqlCommand sqlCommand = GetCommand();
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.CommandText = SqlStatement;

            SqlDataAdapter dtAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            dtAdapter.Fill(dataSet);

            sqlCommand.Dispose();
            sqlCommand.Connection.Close();
            return dataSet;
        }

        public void ExecuteStoredProcedure(string ProcedureName, SqlParameter[] @params, ref DataSet DataSet)
        {
            SqlCommand sqlCommand = GetCommand();
            sqlCommand.CommandText = ProcedureName;
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (SqlParameter param in @params)
            {
                sqlCommand.Parameters.Add(param);
            }

            SqlDataAdapter dtAdapter = new SqlDataAdapter(sqlCommand);
            DataSet dtSetObject = new DataSet();
            dtAdapter.Fill(dtSetObject);
            DataSet = dtSetObject;

            sqlCommand.Dispose();
            sqlCommand.Connection.Close();
        }
    }
}