using ED.DbRely;
using ED.DbRely.Model;
using ED.SQLite.Basis;
using ED.SQLite.Model;
using ED.SQLite.Single;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ED.SQLite
{
    public class Export : IDbInstance
    {
        string db = @"Data Source=|DataDirectory|\Data\Data.db;Version=3;Pooling=true;FailIfMissing=false;";
        public Export() 
        {
            this.db = ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
            CreateDb();
        }
        public Export(string db)
        {
            if (!string.IsNullOrEmpty(db) && IsHasDir(db))
                this.db = db;
            else
                this.db = ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
            CreateDb();
        }

        private bool IsHasDir(string db)
        {
            bool has = false;
            string[] sdb = db?.Split(';')[0]?.Split('|');
            
            if(sdb.Length>2)
            {
                string dbpath = sdb[2];
                dbpath = dbpath.Substring(1, dbpath.Length - 1);
                has = File.Exists(dbpath);
            }

            return has;

        }
        private void CreateDb()
        {
            if(!IsHasDir(db))
            {
                AddedArgs param = new AddedArgs(new DbParams[]
                {
                    new DbParams("ID","INTEGER PRIMARY KEY AUTOINCREMENT"),
                    new DbParams("Key","String"),
                    new DbParams("Value","String")
                });
                ExecArgs args = ArgsHelper.Create("Table","Config", param);

                DataBase dbh = new DataBase(db);
                dbh.CreateTable(args);

                param = new AddedArgs(new DbParams[]
                {
                    new DbParams("ID","INTEGER PRIMARY KEY AUTOINCREMENT"),
                    new DbParams("FID","STRING"),
                    new DbParams("File","STRING"),
                    new DbParams("Date","DATETIME"),
                    new DbParams("TargetKey","STRING"),
                    new DbParams("TargetName","STRING"),
                    new DbParams("SampleKey","STRING"),
                    new DbParams("SampleName","STRING"),
                    new DbParams("Value","DOUBLE")
                });
                args = ArgsHelper.Create("Table", "Record", param);
                dbh.CreateTable(args);
                    
            }
        }

        public IDbAlter GetAlter(string name)
        {
            Type type = Type.GetType(string.Format("ED.SQLite.Single.{0}", name));
            IDbAlter alter = null;
            if (type != null)
                alter = Activator.CreateInstance(type,db) as IDbAlter;

            return alter;
        }

        public IDbQuery GetQuery(string name)
        {
            Type type = Type.GetType(string.Format("ED.SQLite.Single.{0}", name));
            IDbQuery query = null;
            if (type != null)
                query = Activator.CreateInstance(type,db) as IDbAlter;

            return query;
        }

        public IDbTrans GetTrans()
        {
            string transName = "DefaltTrans";
            Type type = Type.GetType(string.Format("ED.SQLite.Single.{0}", transName));
            IDbTrans trans = null;
            if (type != null)
                trans = Activator.CreateInstance(type, db) as IDbTrans;

            return trans;
        }
    }
}
