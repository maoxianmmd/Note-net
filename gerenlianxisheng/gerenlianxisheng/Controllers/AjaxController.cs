using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using gerenlianxisheng.Controllers.tool;
using gerenlianxisheng.Models;
using Microsoft.AspNetCore.Mvc;

namespace gerenlianxisheng.Controllers
{
    public class AjaxController : Controller
    {
        ///数据库操作

        //母页目录查询，新增下拉框查询
        public ActionResult Indexasid()
        {
            //获取多个表格
            SqlconnectionSql tables = new SqlconnectionSql();
            DataTable tableasid = tables.SelectTypebig()["asidlist"];
            DataTable tableother = tables.SelectTypebig()["other"];
            //定义输出结果
            IndexAsid result = new IndexAsid();
            //定义输出结果的多个属性
            List<AsidList> asidlist = new List<AsidList>();
            List<TypeBigLists> resultbig = new List<TypeBigLists>();
            //定义模型
            AsidList asidlistrow = new AsidList();
            TypeBigLists resultbigrow = new TypeBigLists();
            //asid_tables的内容
            //此处传值是按地址传值，自己编写的Clone方法新建一个地址用于存放每次的结果
            for (int i = 0; i < tableasid.Rows.Count; i++)
            {
                asidlistrow.type = tableasid.Rows[i]["typebig"].ToString();
                asidlistrow.name = tableasid.Rows[i]["bigtitle"].ToString();
                asidlist.Add(asidlistrow.Clone());
            }
            //释放内存
            GC.SuppressFinalize(tableasid);
            //type_big_lists内容
            //去掉重复的bigtitle
            DataTable typebigs = tableother.DefaultView.ToTable(true, "typebig", "bigtitle");
            for (int i = 0; i < typebigs.Rows.Count; i++)
            {
                List<Small> ss = new List<Small>();
                resultbigrow.big = typebigs.Rows[i]["bigtitle"].ToString();
                //判断smalltitle是否属于bigtitle并添加
                for (int j = 0; j < tableother.Rows.Count; j++)
                {
                    if (typebigs.Rows[i]["typebig"].ToString() == tableother.Rows[j]["typebig"].ToString())
                    {
                        ss.Add(new Small { value = tableother.Rows[j]["smalltitle"].ToString(), key = Convert.ToInt32(tableother.Rows[j]["typesmall"]) }.Clone());
                    }
                }
                resultbigrow.small = ss;
                resultbig.Add(resultbigrow.Clone());
            }
            GC.SuppressFinalize(tableother);
            GC.SuppressFinalize(typebigs);
            //给result属性赋值
            result.type_big_lists = resultbig;
            result.asid_lists = asidlist;
            return Json(new
            {
                result
            });
        }

        //子页目录，内容查询
        public ActionResult TitleAndNerong()
        {
            string data = HttpContext.Request.Form["type"].ToString();
            //获取多个表格
            SqlconnectionSql tables = new SqlconnectionSql();
            DataTable table = tables.SelectJilu(data)["TandN"];
            //定义输出结果
            JiluModel result = new JiluModel();
            //定义模型的多个属性
            List<JiluAsid> jiluasid = new List<JiluAsid>();
            List<JiluNerong> jiluneirong = new List<JiluNerong>();
            //定义模型
            JiluAsid jiluasidrow = new JiluAsid();
            JiluNerong jiluneirongrow = new JiluNerong();
            //tables内容
            for (int i = 0; i < table.Rows.Count; i++)
            {
                jiluneirongrow.id = Convert.ToInt32(table.Rows[i]["id"]);
                jiluneirongrow.bigtitle = table.Rows[i]["bigtitle"].ToString();
                jiluneirongrow.smalltitle = table.Rows[i]["smalltitle"].ToString();
                jiluneirongrow.title = table.Rows[i]["title"].ToString();
                jiluneirongrow.neirong = table.Rows[i]["contents"].ToString();
                jiluneirong.Add(jiluneirongrow.Clone());
            }
            //types内容
            //去掉重复的smalltitle
            DataTable typesmalls = table.DefaultView.ToTable(true, "typesmall", "smalltitle");
            for (int i = 0; i < typesmalls.Rows.Count; i++)
            {
                ArrayList ss = new ArrayList();
                jiluasidrow.type_small = typesmalls.Rows[i]["smalltitle"].ToString();
                //判断mulu_small是否属于type_small并添加
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    if (typesmalls.Rows[i]["typesmall"].ToString() == table.Rows[j]["typesmall"].ToString())
                    {
                        ss.Add(table.Rows[j]["title"]);
                    }
                }
                jiluasidrow.mulu_small = ss;
                jiluasidrow.show = 1;
                jiluasid.Add(jiluasidrow.Clone());
            }
            //给result赋值
            result.tables = jiluneirong;
            result.types = jiluasid;
            return Json(new
            {
                result
            });
        }

        //子页修改功能
        public ActionResult Change()
        {
            int id = Convert.ToInt32(HttpContext.Request.Form["id"]);
            string bigtitle = HttpContext.Request.Form["bigtitle"].ToString();
            string smalltitle = HttpContext.Request.Form["smalltitle"].ToString();
            string title = HttpContext.Request.Form["title"].ToString();
            string neirong = HttpContext.Request.Form["neirong"].ToString();
            SqlconnectionSql tables = new SqlconnectionSql();
            string result = tables.UpdateJilu(bigtitle, smalltitle, title, neirong, id);
            return Json(new
            {
                result
            });
        }

        //子页删除功能
        public ActionResult Delete()
        {
            int id = Convert.ToInt32(HttpContext.Request.Form["id"]);
            SqlconnectionSql tables = new SqlconnectionSql();
            string result = tables.DeleteJilu(id);
            return Json(new
            {
                result
            });
        }

        //母页新增功能
        public ActionResult AddNew()
        {
            int typebig = Convert.ToInt32(HttpContext.Request.Form["typebig"]);
            int typesmall = Convert.ToInt32(HttpContext.Request.Form["typesmall"]);
            string bigtitle = HttpContext.Request.Form["bigtitle"].ToString();
            string smalltitle = HttpContext.Request.Form["smalltitle"].ToString();
            string title = HttpContext.Request.Form["title"].ToString();
            string neirong = HttpContext.Request.Form["neirong"].ToString();
            SqlconnectionSql tables = new SqlconnectionSql();
            string result = tables.InsertIntoJilu(bigtitle, smalltitle, title, neirong, typebig, typesmall);
            return Json(new
            {
                result
            });
        }

        ///登录相关操作

        //账号登录
        public ActionResult Login()
        {
            string zhanghao = HttpContext.Request.Form["zhanghao"].ToString();
            string mima = HttpContext.Request.Form["mima"].ToString();
            SqlconnectionSql tables = new SqlconnectionSql();
            string result = tables.AccountSelect(zhanghao, mima);
            return Json(new
            {
                result
            });
        }

        //验证账号状态
        public ActionResult VerifyLogin()
        {
            ComHttpContext session = new ComHttpContext();
            string result = session.Get("logtype");
            return Json(new
            {
                result
            });
        }
    }
}