using ED.DbRely.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.SQLite.Basis
{
    abstract class DbHelper : IDisposable
    {
        public static SQLiteConnection conn;
        public static SQLiteTransaction trans;
        static bool isStartTrans;

        public DbHelper()
        {
            string connStr = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            conn = new SQLiteConnection(connStr);
        }
        public DbHelper(string strConn)
        {
            string str = ConfigurationManager.ConnectionStrings[strConn]?.ConnectionString;
            if (string.IsNullOrEmpty(str))
                conn = new SQLiteConnection(strConn);
            else
                conn = new SQLiteConnection(str);
        }
        public void Dispose()
        {
            conn.Close();
            isStartTrans = false;

        }

        public void BeginTrans()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            trans = conn.BeginTransaction();
            isStartTrans = true;
        }

        public void CommitTrans()
        {
            if (isStartTrans)
                trans.Commit();
            isStartTrans = false;
        }
        public void RollbackTrans()
        {
            if (isStartTrans)
                trans.Rollback();
            isStartTrans = false;
        }

        public void OpenConn()
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }
        protected int ExecEditor(string cmdText, CommandType type, params SQLiteParameter[] param)
        {
            int count = 0;
            SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
            cmd.CommandType = type;
            if(param!=null)
            cmd.Parameters.AddRange(param);

            if (conn.State != ConnectionState.Open)
                conn.Open();

            count = cmd.ExecuteNonQuery();

            return count;
        }
        protected List<T> ExecQuery<T>(string cmdText, CommandType type, params SQLiteParameter[] param)
        {
            List<T> list = new List<T>();

            SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
            cmd.CommandType = type;
            if (param != null)
                cmd.Parameters.AddRange(param);

            SQLiteDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Type target = typeof(T);
                T t = (T)Activator.CreateInstance(target);

                PropertyInfo[] props = target.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in props)
                {
                    prop.SetValue(t, Convert.ChangeType(sdr[prop.Name], prop.PropertyType), null);
                }
                list.Add(t);
            }

            return list;
        }
        protected DataTable ExecQuery(string cmdText,CommandType type,params SQLiteParameter[] param)
        {
            DataTable dt = new DataTable();

            SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
            cmd.CommandType = type;
            if (param != null)
                cmd.Parameters.AddRange(param);

            SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);

            sda.Fill(dt);

            return dt;
        }

        protected object ExecScalar(string cmdText, CommandType type, params SQLiteParameter[] param)
        {
            SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
            cmd.CommandType = type;
            if (param != null)
                cmd.Parameters.AddRange(param);

            return cmd.ExecuteScalar();
        }
    }
}
