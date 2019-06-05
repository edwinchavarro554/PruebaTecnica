using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDetail(string nombre)
        {


            string json = HttpContext.Cache["Json"].ToString();

            List<RootObject> result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RootObject>>(json);


            RootObject root = result.Where(e => e.alpha2Code == nombre).SingleOrDefault();

            string  values = string.Empty;


            values = values + "Code2 : " + root.alpha2Code;

            values = values + " Code3 : " + root.alpha3Code;



            values = values + " Lenguajes : " ;
            foreach (Language obj in root.languages)
            {

                values = values + " : " + obj.name;
            }

            values = values + " flag : " + root.flag;


            values = values + " Traduccion : ";

            values = values + " Es : " + root.translations.es;


            values = values + " Pt : " + root.translations.pt;

            values = values + " fr : " + root.translations.fr;

            values = values + " br : " + root.translations.br;

            values = values + " ja : " + root.translations.ja;

            values = values + " de : " + root.translations.de;

            values = values + " latitud : " + root.latlng[0].ToString();

            values = values + " Long : " + root.latlng[1].ToString();


            values = values + " TimeZone : " + root.timezones[0].ToString();

            values = values + " Codigo Llamada : " + root.callingCodes[0].ToString();

            values = values + " Long : " + root.latlng[1].ToString();


            values = values + " Currency : ";
            foreach (Currency obj in root.currencies)
            {

                values = values + " : " + obj.name;
            }


            return Json(new { json = values }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CountryDetails()
        {

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://restcountries.eu/rest/v2/all");
            myReq.ContentType = "application/json";
            var response = (HttpWebResponse)myReq.GetResponse();
            string text;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            HttpContext.Cache["Json"] = text;


            List<RootObject> x = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RootObject>>(text);
            List<Result> results = new List<Result>();

            foreach (RootObject obj in x)
            {
                results.Add(new Result() { Nombre = obj.name, Capital = obj.capital, Region = obj.region , alpha2Code= obj.alpha2Code });
            }


            string jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(results);
            return Json(new { json = jsonResult }, JsonRequestBehavior.AllowGet);
        }
        public class Result
        {
            public string Nombre { get; set; }

            public string Capital { get; set; }

            public string Region { get; set; }


            public string alpha2Code { get; set; }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class Currency
    {
        public string code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
    }

    public class Language
    {
        public string iso639_1 { get; set; }
        public string iso639_2 { get; set; }
        public string name { get; set; }
        public string nativeName { get; set; }
    }

    public class Translations
    {
        public string de { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string ja { get; set; }
        public string it { get; set; }
        public string br { get; set; }
        public string pt { get; set; }
        public string nl { get; set; }
        public string hr { get; set; }
        public string fa { get; set; }
    }

    public class RootObject
    {
        public string name { get; set; }
        public List<string> topLevelDomain { get; set; }
        public string alpha2Code { get; set; }
        public string alpha3Code { get; set; }
        public List<string> callingCodes { get; set; }
        public string capital { get; set; }
        public List<object> altSpellings { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public int population { get; set; }
        public List<object> latlng { get; set; }
        public string demonym { get; set; }
        public double? area { get; set; }
        public double? gini { get; set; }
        public List<string> timezones { get; set; }
        public List<object> borders { get; set; }
        public string nativeName { get; set; }
        public string numericCode { get; set; }
        public List<Currency> currencies { get; set; }
        public List<Language> languages { get; set; }
        public Translations translations { get; set; }
        public string flag { get; set; }
        public List<object> regionalBlocs { get; set; }
        public string cioc { get; set; }
    }
}