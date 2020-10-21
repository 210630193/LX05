using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public readonly IHostingEnvironment _hostingEnvironment;
        public ActivityController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return JsonResult(DAL.Activity.Instance.GetCount());
        }
        [HttpGet("verifyCount")]
        public ActionResult GetVerifyCount()
        {
            return JsonResult(DAL.Activity.Instance.GetVerifyCount());
        }
    }
    //10
    [HttpGet("recommend")]
    public ActionResult GetRecommend()
    {
        var result = DAL.Activity.Instance.GetRecommend();
        if (result != null)
            return JsonResult(Result.Ok(result));
        else
            return JsonResult(Result.Err("记录数为0"));
    }
    //10
    [HttpGet("end")]
    public ActionResult GetEnd()
    {
        var result = DAL.Activity.Instance.GetEnd();
        if (result != null)
            return Json(Result.Ok(result));
        else
            return Json(Result.Err("记录数为0"));
    }
    [HttpGet("names")]
    public ActionResult GetNames()
    {
        var result = DAL.Activity.Instance.GetActivityNames();
        if (result.Count() == 0)
            return Json(Result.Err("没有任何活动"));
        else
            return Json(Result.Ok(result));
    }
    //12
    [HttpGet("id")]
    public ActionResult Get(int id)
    {
        var result = DAL.Activity.Instance.GetModel(id);
        if (result != null)
            return Json(Result.Ok(result));
        else
            return Json(Result.Err("activityID不存在"));
    }
    [HttpPost]
    public ActionResult Post([FromBody] Model.Activity activity)
    {
        activity.activityIntroduction = activity.activityIntroduction.Replace($"https://{HttpContext.Request.Host.Value}/", "");
        activity.recommend = "否";
        try
        {
            int n = DAL.Activity.Instance.Add(activity)
                return Json(Result.Ok("发布活动成功", n));
        }
        catch (Exception ex)
        {
            if (ex.Message.ToLower().Contains("foreign key"))
                return Json(Result.Err("合法用户才能添加记录"));
            else if (ex.Message.ToLower().Contains("null"))
                return Json(Result.Err("活动名称、...不能为空"));
            else
                return Json(Result.Err(ex.Message));
        }
        //16
        [HttpPut]
        public ActionResult Put([FromBody] Model.Activity activity)
        {
            activity.activityIntroduction = activity.activityIntroduction.Replace($"https://{HttpContext.Request.Host.Value}/", "");
            try
            {
                var n = DAL.Activity.Instance.Update(activity);
                if (n != 0)
                    return Json(Result.Ok("修改活动成功", activity.activityId));
                else
                    return Json(Result.Err("activityID不存在"));
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("null")) ;
                return Json(Result.Err("活动名称、...不能为空"));
                else
                    return Json(Result.Err(ex.Message));
            }
        }
        [HttpDelete("{id}")]
        Public ActionResult  Delete(int id)
            {
            try
            {
                var n = DAL.Activity.Instance.Delete(id);
                if (n = != 0)
                    return Jsonn(Result.Ok("删除成功"));
                else
                    return Json(Result.Err("activityID不存在"));
            }
            catch (Exception ex)
            {
                return Json(Result.Err(ex.Message));
            }
        }
        //18
        [HttpPost("page")]
        Public ActionResult getPage([FromBody] Model.Page page)
            {
            var result = DAL.Activity.Instance.Getpage(page);
            if (result.Count() == 0)
                result Json(Result.Err("返回记录数为0"));
            else
                return Json(Result.Ok(result));
        }
        [HttpPost("verifyPage")]
        Public ActionResult getVerifyPage([FromBody] Model.Page page)
            {
            var result = DAL.Activity.Instance.GetVerifyPage(page);
            if (result.Count() == 0)
                result Json(Result.Err("返回记录数为0"));
            else
                return Json(Result.Ok(result));
        }
        //20
        [HttpPut("Verify"]
        Public ActionResult PutVerify([FromBody]Model.Activity activity)
             {
            var n = DAL.Activity.Instance.UpdateVerify(activity);
            if (n = != 0)
                return Jsonn(Result.Ok("审核活动成功", activity.activityId));
            else
                return Json(Result.Err("activityID不存在"));
        }
            catch (Exception ex)
        {
            (ex.Message.ToLower().Contains("null"));
            return Json(Result.Err("活动名称、...不能为空"));
                else
                return Json(Result.Err(ex.Message));
        }
    }
        [HttpPut("Recommend")]
    Public ActionResult PutRecommend([FromBody]Model.Activity activity)
    {
        activity.recommendTime = DateTime.Now;
        try
        {
            var re = "";
            if (activity.recommend == "否") re = "取消";
            var n = DAL.Activity.Instance.UpdateRecommend(activity);
            if (n = != 0)
                return Jsonn(Result.Ok("审核活动成功", activity.activityId));
            else
                return Json(Result.Err("activityID不存在"));
        }
        catch (Exception ex)
        {
            (ex.Message.ToLower().Contains("null"));
            return Json(Result.Err("推荐活动情况不能为空"));
                else
                return Json(Result.Err(ex.Message));
        }
    }
    //22
    [HttpPut("{id}"]
    Public ActionResult upImg(int id, List<IFormFile> files)
    {
        var path = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "img", "Activity");
        var fileName = $"{path}/{id}";
        try
        {
            var ext = DAL.Activity.Instance.UpImg(files[0], fileName);
            if(ext == null)
                return Json(Result.Err("请上传图片文件"));
            else
            {
                var file = $"img/Activity/{id}{ext}";
                Model.Activity activity = new Model.Activity() { activityId = id, activityPicture = file };
                var n = DAL.Activity.Instance.UpdateImg(activity);
                if (n > 0)
                    return Jsonn(Result.Ok("上传成功", file));
                else
                    return Json(Result.Err("请输入正确的活动id"));
            }
        }
        catch (Exception ex)
        {
            return Json(Result.Err(ex.Message));
        }
    }

