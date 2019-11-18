using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SentryDataStuff
{
    public class DBStuff : MarshalByRefObject, IDisposable
    {
        private int _CommandTimeOut = 0;
        private int _DefaultCommandTimeOut = 0;
        private int _ConnectionTimeOut = 0;
        private string _ConnectionString = string.Empty;

        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());


        #region Standard SQL
        
        public int CommandTimeOut
        {
            get
            {
                return _CommandTimeOut;
            }
            set
            {
                _CommandTimeOut = value;
            }
        }

        public int ConnectionTimeOut
        {
            get
            {
                return _ConnectionTimeOut;
            }
            set
            {
                _ConnectionTimeOut = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        public Boolean TestConnection(string ConnStr)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {

                try
                {
                    conn.Open();
                }
                catch
                {
                    return false;
                }

                conn.Close();

                return true;
            }
        }

        public SqlConnection ConnectSQL()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(_ConnectionString);

                myConnection.Open();

                return myConnection;
            }

            catch (Exception ex)

            {
                throw ex;
            }
        }

        public DataTable DataTable(String sqlText)
        {
            try
            {
                using (SqlConnection conn = ConnectSQL())
                {
                    SqlDataAdapter myDa = new SqlDataAdapter(sqlText, conn);
                    DataTable myDt = new DataTable();

                    myDa.Fill(myDt);

                    return myDt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DataSet(string sqlText)
        {
            try
            {
                using (SqlConnection conn = ConnectSQL())
                {

                    SqlDataAdapter myDa = new SqlDataAdapter(sqlText, conn);
                    DataSet myDs = new DataSet();
                    myDa.Fill(myDs);

                    return myDs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean SqlCommand(string sqlText)
        {
            using (SqlConnection sqlConn = ConnectSQL())
            {
                SqlCommand myCommand = new SqlCommand();

                try
                {
                    myCommand.Connection = sqlConn;
                    myCommand.CommandText = sqlText;
                    myCommand.CommandTimeout = CommandTimeOut;
                    myCommand.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    return false;
                }

                return true;
            }
        }

        public string SqlCommandS(string sqlText)
        {
            using (SqlConnection sqlConn = ConnectSQL())
            {
                SqlCommand myCommand = new SqlCommand();

                try
                {
                    myCommand.Connection = sqlConn;
                    myCommand.CommandText = sqlText;
                    myCommand.CommandTimeout = CommandTimeOut;
                    myCommand.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    return ex.Message;
                }

                return "";
            }
        }

        public SqlDataReader SqlDataReader(string sqlText)
        {
            return SQLExecute(sqlText);
        }

        public SqlDataReader SQLExecute(string sqlText)
        {
            SqlConnection sqlConn = ConnectSQL();

            SqlCommand myCommand = new SqlCommand();

            try
            {
                myCommand.Connection = sqlConn;
                myCommand.CommandText = sqlText;
                myCommand.CommandTimeout = CommandTimeOut;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return myCommand.ExecuteReader();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion Standard SQL

        #region SQLHelper

        public DataSet ExecuteDataset(string spName, params object[] parameterValues)
        {
            using (SqlConnection sqlConn = ConnectSQL())
            {
                // If we receive parameter values, we need to figure out where they go
                if ((parameterValues != null) && (parameterValues.Length > 0))
                {
                    // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                    SqlParameter[] commandParameters = GetSpParameterSet(sqlConn, spName);

                    // Assign the provided values to these parameters based on parameter order
                    AssignParameterValues(commandParameters, parameterValues);

                    // Call the overload that takes an array of SqlParameters
                    return ExecuteDataset(sqlConn, CommandType.StoredProcedure, spName, commandParameters);
                }
                else
                {
                    // Otherwise we can just call the SP without params
                    return ExecuteDataset(sqlConn, CommandType.StoredProcedure, spName);
                }
            }
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // We must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // Iterate through the SqlParameters, assigning the values from the corresponding position in the 
            // value array
            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                if (parameterValues[i] is IDbDataParameter)
                {
                    IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                    if (paramInstance.Value == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = paramInstance.Value;
                    }
                }
                else if (parameterValues[i] == null)
                {
                    commandParameters[i].Value = DBNull.Value;
                }
                else
                {
                    commandParameters[i].Value = parameterValues[i];
                }
            }
        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            cmd.CommandTimeout = 0;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

                // Return the dataset
                return ds;
            }
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;
            command.CommandTimeout = 0;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (SqlParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }


        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            using (SqlConnection clonedConnection = (SqlConnection)((ICloneable)connection).Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            SqlParameter[] cachedParameters;

            cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                SqlParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
                paramCache[hashKey] = spParameters;
                cachedParameters = spParameters;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion SQLHelper
    }
}
