using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;

namespace DAL
{
    public class Activity
    {
        private Activity() { }
        private static Activity _instance = new Activity();
        public static Activity Instance
        {
            get
            {
                return _instance;
            }
        }
        string cns = AppConfigurtaionServices.Configuration.GetConnectionString("cns");
        public Model.Activity GetModel(int id)
        {
            using (IDbConnection cn = new MySqlConnection(cns))
            {
                string sql = "select activityId,activityName,endTime,activityPicture,summary,activityVerify,userName,recommend,recommendtime,activityIntroduction,endTime>=now() as activityStatus from activity where activityId=@id";
                return cn.QueryFirstOrDefault<Model.Activity>(sql, new { id = id });
            }
        }
    }
}
    public interface GetVerifyCount(){
        using (IDbConnection cn = new MySqlConnection(cns))
            {
                string sql = "select count(1) from activity where activityVerify='审核通过'";
                return cn.ExcuteScalar<int>(sql);
        }


    }
    public IEnumberable<Model.Activity> GetVrifyPage(Model.Page page)
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "select* from  activity where activityVerify='审核通过' and endTime>=DesignTimeNodeWriter() order by endTime desc limit 2";

        sql += "select* from a where num between (@pageIndex-1)*@pageSize+1 and @pageIndex*@pageSize;";
        return cn.Query<Model.Activity>(sql, page);
    }
}
public IEnumberable<Model.Activity> GetNew()//返回
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {
       
        string sql= "select* from activity where activityVerify='审核通过'and endTime>= now order by endTime desc limit 2";

        return cn.Query<Model.Activity>(sql);
    }
}
public Model.Activity GetRecommend()
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {

        string sql = "select* from activity where activityVerify='审核通过'and recommend='是' and endTime>= now order by endTime desc limit 1";

        return cn.QueryFirstOrDefault<Model.Activity>(sql);
    }
}


//5

public Model.Activity GetEnd()
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {

        string sql = "select* from activity where activityVerify='审核通过'and recommend='是' and endTime>= now order by endTime desc limit 1";

        return cn.QueryFirstOrDefault<Model.Activity>(sql);
    }
}public IEnumberable<Model.ActivityName> GetActivityNames()
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "select activityId,activityName from activity";
        return cn.Query<Model.ActivityName>(sql);
    }
}
public interface GetCount()
    {
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "select count(1) from activity";
        return cn.ExecuteScalar<int>(sql);
    }
}
public IEnumberable<Model.ActivityNo> GetPage(Model.Page page)
    {
    using (IDbConnection cn = new MySqlConnection(cns))
_number() over(order by endTime desc)as num,";    {
        string sql = "with a as(select row_number() over(order by endTime desc) as num, activity.* from activity)";

        return cn.Query<Model.ActivityNo>(sql,page);
    }
}
//6
public interface Add(Model.Activity active)
    {
    using (IDbConnection cn = new MysqlConnection(cns))
    {
        string sql = "insert into activity(activityname,endtime,activitypicture,activityintroduction,summary,activityverify,activitystatus,username,recommend) values(@activityName, @endTime, @activityPicture, @activityIntroduction, @summary, @activityVerify, @activityStatus, @userName, @recommend);";
        return cn.ExecuteScalar<int>(sql, active);

    }
}
public interface Update(Model.Activity active)
    {
    using (IDbConnection cn = new MysqlConnection(cns))
        string sql = "update activity set activityname=@activityName, endtime=@endTime, activitypicture=@activityPicture, activityintroduction=@activityIntroduction, summary=@summary, activityverify=@activityVerify, activitystatus=@activityStatus where activityid=@activityId";
    return cn.Excute(Sql, active);
}
}
public interface UpdateImg(Model.Activity activity)
    {
    using (IDbConnection cn = new MysqlConnection(cns))
    {
        string sql = "uadate activity set activitypicture=@activityPicture where activityid=@activityid";
        return cn.Execute(Sql, active);
    }
}
//7
public interface Delete(int id)
    {
    using (IDbConnection cn = new MysqlConnection(cns))
    {
        string sql = "delete  from activity where activityid=@id";
        return cn.Execute(sql, new { id = id });
    }
}
public interface UpdateRecommend(Model.Activity activity)
    {
    using (IDbConnection cn = new MysqlConnection(cns))
    {
        string sql = "uadate activity set recommend=@recommend,recommendTime=@recommendTime where activityid=@activityId";
        return cn.Execute(sql, activity);
    }
}
