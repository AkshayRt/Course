using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course.Controllers
{
    public class HomeController : Controller
    {
        public class JsonValuesData
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
        public ActionResult Index()
        {
            JObject JsonData = GetJsonData();
            List<string> Fee_Dropdown = JsonData.Properties().Select(e => e.Name).ToList();
            ViewBag.FeeDropDown = string.Join(",", Fee_Dropdown.ToArray());
            return View();
        }
        /// <summary>
        /// To read json file
        /// </summary>
        /// <returns></returns>
        private JObject GetJsonData()
        {
            string jsonpath = Server.MapPath("/Content/Program.json");
            string textdata = string.Empty;
            textdata = System.IO.File.ReadAllText(jsonpath);
            JObject j_Object = (JObject)JsonConvert.DeserializeObject(textdata);
            return j_Object;
        }
        /// <summary>
        /// To get Nations
        /// </summary>
        /// <param name="fee"></param>
        /// <returns></returns>
        public ActionResult GetAllNations(string fee)
        {
            List<string> AllNations = new List<string>();
            JObject JsonData = GetJsonData();
            List<string> All_Nations = ((JObject)JsonData[fee]).Properties().Select(e => e.Name).ToList();
            if (All_Nations.Count > 0)
            {
                foreach (var nat in All_Nations)
                {
                    AllNations.Add(nat);
                }
            }
            return Json(AllNations, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// To get complete course
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="nation"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        public ActionResult GetAllCourses(string fee, string nation, string course)
        {
            List<string> AllCourses = new List<string>();
            JObject JsonData = GetJsonData();
            List<string> All_Courses = ((JObject)JsonData[fee][nation][course]).Properties().Select(e => e.Name).ToList();
            if (All_Courses.Count > 0)
            {
                foreach (var nat in All_Courses)
                {
                    AllCourses.Add(nat);
                }
            }
            return Json(AllCourses, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// To get the fee amount
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="nation"></param>
        /// <param name="course"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public ActionResult GetAmount(string fee, string nation, string course, string subject)
        {
            JObject JsonData = GetJsonData();
            string All_Courses = JsonData[fee][nation][course][subject]["amount"].ToString();

            return Json(All_Courses, JsonRequestBehavior.AllowGet);
        }

    }
}