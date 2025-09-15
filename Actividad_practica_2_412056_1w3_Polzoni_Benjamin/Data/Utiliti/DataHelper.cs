﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Domain;
using Microsoft.Data.SqlClient;

namespace Actividad_practica_1_412056_1w3_Polzoni_Benjamin.Data.Utiliti
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.SqlConnexion);
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSPQuery(string sp)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException e)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        public DataTable ExecuteSPQuery(string sp, List<Parameter>? param = null)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;

                if (param != null)
                {
                    foreach (Parameter p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }

                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException e)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        public bool ExecuteSpDml(string sp, List<Parameter>? param = null)
        {
            bool result;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (param != null)
                {
                    foreach (Parameter p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }
                int affectedRows = cmd.ExecuteNonQuery();

                result = affectedRows > 0;
            }
            catch (SqlException ex)
            {

                result = false;
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        //public int ExecuteSpDMLTransact(string SP, List<Parameter>? listParamInvo, List<Parameter> details, string SPDetail)
        //{
        //    int result = 1;
        //    SqlConnection cnn = null;
        //    SqlTransaction t = null;
        //    try
        //    {
        //        _connection.Open();
        //        t = _connection.BeginTransaction();

        //        var cmd = new SqlCommand(SP, _connection, t);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        if (listParamInvo != null)
        //        {
        //            foreach (Parameter p in listParamInvo)
        //            {
        //                cmd.Parameters.AddWithValue(p.Name, p.Value);
        //            }
        //        }

        //        SqlParameter param = new SqlParameter("@id", SqlDbType.Int);

        //        param.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(param);

        //        cmd.ExecuteNonQuery();

        //        if (param.Value != null)
        //        {
        //            int idGenrate = Convert.ToInt32(param.Value);

        //            foreach (var detail in details)
        //            {
        //                if (detail.Value == null)
        //                {
        //                    detail.Value = idGenrate;
        //                }
        //                bool bien = ExecuteSpDml(SPDetail, details);
        //                if (!bien)
        //                {
        //                    result = 0;
        //                }
        //            }
        //        }
        //        t.Commit();

        //        return result;
        //    }
        //    catch (SqlException e)
        //    {
        //        if (t != null)
        //        {
        //            t.Rollback();
        //        }
        //        return result = 0;
        //    }
        //    finally
        //    {
        //        // Si no es nula y esta abierta, la cerramos
        //        if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
        //        {
        //            cnn.Close();
        //        }
        //    }
        //    return result;
        //}
    }
}
