using CIISA.RetailOnLine.Droid.Framework.Handheld.DataBase;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Connection
{
    public static class MultiGeneric
    {
        private static SqliteConnection _connection = null;

        private static SqliteTransaction _sqlCeTransaction = null;

        private const string StringConnection = null;

        private static void GetConnection()
        {
            try
            {
                if (_connection == null)
                {
                    //string _directoryPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    string _directoryPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString();
                    string _pahtDB = System.IO.Path.Combine(_directoryPath, DBCF._dataBaseName);
                    string _stringConnection = "Data Source=" + _pahtDB + ";";

                    _connection = new SqliteConnection(_stringConnection);

                    _connection.Open();
                }
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public static string readStringText(StringBuilder psentence)
        {
            return executeScalarAction(psentence);
        }

        private static string executeScalarAction(StringBuilder psentence)
        {
            GetConnection();
            string _consulta = psentence.ToString();
            string _stringText = string.Empty;

            using (var cmd = new SqliteCommand(psentence.ToString(), _connection,_sqlCeTransaction))
            {
                try
                {
                    _stringText = Convert.ToString(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Exception _exception = new Exception("Consulta = " + psentence.ToString() + System.Environment.NewLine, ex);
                }
            }

            return _stringText;
        }

        public static DataTable uploadDataTable(StringBuilder psentence)
        {
            GetConnection();
            string consulta = psentence.ToString();
            DataTable _dt = new DataTable();

            using (var cmd = new SqliteCommand(psentence.ToString(), _connection,_sqlCeTransaction))
            {
                IDataReader reader = null;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow _dataRow = _dt.NewRow();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object data = reader[i];

                        DataColumn _dataColumn = new DataColumn();
                        _dataColumn.ColumnName = reader.GetName(i);
                        _dataColumn.DataType = typeof(string);
                        _dataColumn.DefaultValue = data;

                        if (!_dt.Columns.Contains(reader.GetName(i)))
                        {
                            _dt.Columns.Add(_dataColumn);
                        }
                        _dt.Columns[i].ColumnName = reader.GetName(i);
                        _dataRow[reader.GetName(i)] = data;
                    }
                    _dt.Rows.Add(_dataRow);
                }
                reader.Close();
                _dt.TableName = "Table";
            }
            return _dt;
        }

        private static int executeNonQueryAction(StringBuilder psentence)
        {
            GetConnection();
            string Consulta = psentence.ToString();
            int _insert = Numeric._zeroInteger;

            using (var cmd = new SqliteCommand(psentence.ToString(), _connection,_sqlCeTransaction))
            {
                try
                {
                    _insert = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Exception _exception = new Exception("Consulta = " + psentence.ToString() + Environment.NewLine, ex);
                }
            }

            return _insert;
        }

        public static int updateRecord(StringBuilder psentence)
        {
            return executeNonQueryAction(psentence);
        }

        public static void CloseSession()
        {
            if (_connection != null)
            {
                _connection.Close();

                _connection.Dispose();

                _connection = null;
            }
        }

        public static void BeginTransaction()
        {
            GetConnection();

            if (_sqlCeTransaction == null)
            {
                _sqlCeTransaction = _connection.BeginTransaction();
            }
        }

        public static void Commit()
        {
            if (_sqlCeTransaction != null)
            {
                _sqlCeTransaction.Commit();

                _sqlCeTransaction.Dispose();

                _sqlCeTransaction = null;
            }
        }

        public static void Rollback()
        {
            if (_sqlCeTransaction != null)
            {
                _sqlCeTransaction.Rollback();

                _sqlCeTransaction.Dispose();

                _sqlCeTransaction = null;
            }
        }

        public static int deleteTable(StringBuilder psentence)
        {
            return executeNonQueryAction(psentence);
        }

        public static void insertRecord(StringBuilder psentence)
        {
            executeNonQueryAction(psentence);
        }

        public static decimal readDecimal(StringBuilder psentence)
        {
            string _stringText = executeScalarAction(psentence);

            return FormatUtil.convertStringToDecimal(_stringText);
        }

        public static int uploadGenericTable(StringBuilder psentence)
        {
            return executeNonQueryAction(psentence);
        }

        public static void InsertRecordBackUp(StringBuilder psentence)
        {
            executeNonQueryActionBackUp(psentence);
        }

        private static int executeNonQueryActionBackUp(StringBuilder psentence)
        {
            GetConnection();

            int _insert = Numeric._zeroInteger;

            using (var cmd = new SqliteCommand(psentence.ToString(), _connection))
            {
                try
                {
                    _insert = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    _insert = 1;
                }
            }

            return _insert;
        }

        public static int establishTable(StringBuilder psentence)
        {
            return executeNonQueryAction(psentence);
        }
    }
}