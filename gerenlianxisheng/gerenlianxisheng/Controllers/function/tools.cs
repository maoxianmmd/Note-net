
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace gerenlianxisheng.Controllers.tool
{
    /// 非控制器需进行Session配置
    public class ComHttpContext
    {
        private static IHttpContextAccessor _contextAccessor;
        private ISession _session => _contextAccessor.HttpContext.Session;
        /// <summary>
        /// 当前上下文
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                if (_contextAccessor != null)
                {
                    return _contextAccessor.HttpContext;
                }
                else
                {
                    return null;
                }
            }
        }

        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void Set(string title, string neirong)
        {
            _session.SetString(title, neirong);
        }

        public string Get(string title)
        {
            string code = _session.GetString(title);
            return code;
        }
    }
    ///连接数据库
    public class SqlconnectionSql
    {
        //连接字符串
        public static class AppSettingsJson
        {
            public static string ApplicationExeDirectory()
            {
                var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var appRoot = Path.GetDirectoryName(location);
                return appRoot;
            }
            public static IConfigurationRoot GetAppSettings()
            {
                string applicationExeDirectory = ApplicationExeDirectory();
                var builder = new ConfigurationBuilder()
                .SetBasePath(applicationExeDirectory)
                .AddJsonFile("appsettings.json");
                return builder.Build();
            }

        }

        //连接数据库
        public SqlConnection Sqlconnect()
        {
            var asp = AppSettingsJson.GetAppSettings()["ConnectionStrings"];
            SqlConnection conn = new SqlConnection(asp);
            conn.Open();
            return conn;
        }

        ///数据库操作

        //查询母页asid
        public DataTableCollection SelectTypebig()
        {
            SqlConnection conn = Sqlconnect();
            DataSet ds = new DataSet();
            //查询语句
            string asidselect = "select distinct typebig,bigtitle from adminjilu;";
            string otherselect = "select distinct smalltitle ,bigtitle,typesmall,typebig from adminjilu;";
            //查询操作
            SqlDataAdapter asidlist = new SqlDataAdapter(asidselect, conn);
            SqlDataAdapter other = new SqlDataAdapter(otherselect, conn);
            //赋值操作
            asidlist.Fill(ds, "asidlist");
            other.Fill(ds, "other");
            conn.Close();
            return ds.Tables;
        }

        //查询子页asid
        public DataTableCollection SelectJilu(string data)
        {
            SqlConnection conn = Sqlconnect();
            DataSet ds = new DataSet();
            //查询语句
            string TandNselect = $"select * from adminjilu where typebig={data};";
            //查询操作
            SqlDataAdapter TandN = new SqlDataAdapter(TandNselect, conn);
            //赋值操作
            TandN.Fill(ds, "TandN");
            conn.Close();
            return ds.Tables;
        }

        //子页修改
        public string UpdateJilu(string bigtitle, string smalltitle, string title, string neirong, int id)
        {
            SqlConnection conn = Sqlconnect();
            //修改语句
            string update = $"update adminjilu set bigtitle='{bigtitle}',smalltitle='{smalltitle}',title='{title}',contents='{neirong}' where id={id};";
            SqlCommand ds = new SqlCommand(update, conn);
            int intres = ds.ExecuteNonQuery();
            conn.Close();
            if (intres > 0)
            {
                return "修改成功";
            }
            else
            {
                return "修改失败";
            }
        }

        //子页删除
        public string DeleteJilu(int id)
        {
            try
            {
                SqlConnection conn = Sqlconnect();
                //删除语句
                string delete = $"delete from adminjilu where id={id};";
                SqlCommand ds = new SqlCommand(delete, conn);
                ds.ExecuteNonQuery();
                conn.Close();
                return "删除成功";
            }
            catch (InvalidCastException)
            {
                return "删除失败";
            }
        }

        //母页添加
        public string InsertIntoJilu(string bigtitle, string smalltitle, string title, string neirong, int typebig, int typesmall)
        {
            try
            {
                SqlConnection conn = Sqlconnect();
                string insertinto = $"insert into adminjilu(typebig,typesmall,title,contents,bigtitle,smalltitle,show) values('{typebig}','{typesmall}','{title}','{neirong}','{bigtitle}','{smalltitle}','1')";
                SqlCommand ds = new SqlCommand(insertinto, conn);
                ds.ExecuteNonQuery();
                conn.Close();
                return "添加成功";
            }
            catch (InvalidCastException)
            {
                return "添加失败";
            }
        }

        //账号查询
        public string AccountSelect(string zhanghao, string mima)
        {
            //Session部分
            ComHttpContext session = new ComHttpContext();
            //数据库部分
            SqlConnection conn = Sqlconnect();
            DataSet ds = new DataSet();
            string select = $"select tables from users where name='{zhanghao}' and mima='{mima}';";
            SqlDataAdapter users = new SqlDataAdapter(select, conn);
            users.Fill(ds, "users");
            conn.Close();
            if (ds.Tables["users"].Rows.Count == 0)
            {
                session.Set("logtype", "0");
                return "账号或密码错误";
            }
            else
            {
                session.Set("logtype", "1");
                return "登录成功";
            }
        }
    }
}
