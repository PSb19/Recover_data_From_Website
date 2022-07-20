using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Web;
// using OfficeOpenXml.Core.ExcelPackage;
using System.Data;
using ClosedXML.Excel;

using System.Text.RegularExpressions;

using DataRecovery.DTO;

namespace DataRecovery.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/
        public List<string> companyInfosstr;
        public HelloWorldController(){
            companyInfosstr = new List<string>();
        }
        public IActionResult Index()
        {
            List<CompanyInfo> companyInfos = new List<CompanyInfo>();
            //List<string> companyInfosstr = new List<string>();
            string url;
            string ending = "firmy,1.html";
            // Szkoly jezykowe
            //url = "https://panoramafirm.pl/szko%C5%82a_j%C4%99zykowa/%C5%9Bl%C4%85skie,,rybnik/";
            url = "https://panoramafirm.pl/szko%C5%82a_j%C4%99zykowa/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 30; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending, ref companyInfos);
            }
            ending = "firmy,1.html";
            // Silownie/ fitness cluby -174 na panoramie
            url = "https://panoramafirm.pl/si%C5%82ownie_i_fitness/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 7; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending, ref companyInfos);
            }
            ending = "firmy,1.html";
            // artykupy sportowe - 500 na panoramie
            url = "https://panoramafirm.pl/artyku%C5%82y_sportowe/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 20; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending, ref companyInfos);
            }
            ending = "firmy,1.html";
            // prowadzenie szkolen
                //Szkolenia biznesowe - 76 na panoramie
            // url = "https://panoramafirm.pl/szkolenia_biznesowe/%C5%9Bl%C4%85skie/";
            // for (int i = 1; i <= 3; i++){
            //     ending = "firmy,"+i.ToString()+".html";
            //     fillCompanyInfoListFromSite(url+ending, ref companyInfos);
            // }
            // ending = "firmy,1.html";
            
            // firmy transportowe - transport osob - 526 na panoramie
            url = "https://panoramafirm.pl/transport_os%C3%B3b/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 15; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending, ref companyInfos);
            }
            ending = "firmy,1.html";
            url = "https://panoramafirm.pl/przewozy_autokarowe/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 15; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending, ref companyInfos);
            }
            ending = "firmy,1.html";
            // firmy transportowe - tiry - 9099 firm na panoramie
            string sort1 = "?sort1=1";
            url = "https://panoramafirm.pl/transport_tirem/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 50; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending+sort1, ref companyInfos);
            }
            ending = "firmy,1.html";

            // materialy edukacyjne - X

            // produkty zywieniowe dla osob z ograniczeniami zywieniowymi - X
            // szkolki muzyczne - X

            // placowki sportowe

            // biura posrednictwa pracy

            // obozy mlodziezowe

            // biura turystyczne
            url = "https://panoramafirm.pl/informacja_turystyczna/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 5; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending+sort1, ref companyInfos);
            }
            ending = "firmy,1.html";
            // studia fotograiczne / fotograf
            url = "https://panoramafirm.pl/fotograf/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 35; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending+sort1, ref companyInfos);
            }
            ending = "firmy,1.html";
            // oprawa eventow / imprez - 450
            url = "https://panoramafirm.pl/imprezy_organizacja/%C5%9Bl%C4%85skie/";
            for (int i = 1; i <= 10; i++){
                ending = "firmy,"+i.ToString()+".html";
                fillCompanyInfoListFromSite(url+ending+sort1, ref companyInfos);
            }
            ending = "firmy,1.html";
            string siteUrl = url+ending;

            //Template:
                // url = "https://panoramafirm.pl/fryzjerzy_i_salony_fryzjerskie/";
                // for (int i = 1; i <= 5; i++)
                // {
                //     ending = "firmy,"+i.ToString()+".html";
                //     fillCompanyInfoListFromSite(url+ending, ref companyInfos);
                // }
                // ending = "firmy,1.html";

            //Get strings containing company info into list
            //List<string> cutListOfCompanies = new List<string>();
            foreach (var item in companyInfos){
                companyInfosstr.Add(item.ToString());
                //cutListOfCompanies.Add(new CompanyInfo(item.ToString()).ToString());
            }
            return Export();  //- final return to get everything to excel
            //ViewData["Cut"] = cutIndex;
            ViewData["Count"] = companyInfosstr.Count(); //Number of found companies
            //ViewData["Count"] = numberOfCompanies; //Number of found companies
            //ViewData["Contents"] = companyInfosstr;
            ViewData["CompanyInfos"] = companyInfosstr;
            //ViewData["SiteContent"] = siteContentstr; //Content of site
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;
        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }


        [HttpPost]
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { 
                                            new DataColumn("Nazwa firmy"),
                                            new DataColumn("Adres E-mail"),
                                            new DataColumn("Numer telefonu"),
                                            new DataColumn("Strona internetowa"),
                                            new DataColumn("Adres firmy") });
            List<CompanyInfo> companyInfos = new List<CompanyInfo>();
            foreach (var company in companyInfosstr){
                companyInfos.Add(new CompanyInfo(company));
            }
            foreach (var company in companyInfos)
            {
                dt.Rows.Add(company.Name, company.Email, company.PhoneNumber, company.Website, company.City);
            }
    
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }


        private void fillCompanyInfoListFromSite(string siteUrl, ref List<CompanyInfo> companyInfos){
            string siteContent = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(siteUrl);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())  // Go query google
            using(Stream responseStream = response.GetResponseStream())               // Load the response stream
            using(StreamReader streamReader = new StreamReader(responseStream))       // Load the stream reader to read the response
            {
                siteContent = streamReader.ReadToEnd(); // Read the entire response and store it in the siteContent variable
            }
            //var markers = [{"coordinates"
            string searchString = "markers = [{\"coordinates\"";
            if(!siteContent.Contains(searchString))
                return;
            int cutIndex = siteContent.IndexOf(searchString);
            siteContent = siteContent.Remove(0,cutIndex);
            searchString = "\"companies\"";
            cutIndex = siteContent.IndexOf(searchString);
            siteContent = siteContent.Remove(0,cutIndex);
            searchString = "var locationResult = {";
            cutIndex = siteContent.IndexOf(searchString);
            siteContent = siteContent.Remove(cutIndex);
            string siteContentstr = siteContent;
            int numberOfCompanies;
            searchString = "\"companies\"";
            numberOfCompanies = Regex.Matches(siteContent, searchString).Count();
            int frontCutIndex;
            for (int i = 0; i < numberOfCompanies; i++){
                CompanyInfo company = new CompanyInfo();
                //Primary cut data:
                frontCutIndex = siteContent.IndexOf("\"name");
                siteContent = siteContent.Remove(0,frontCutIndex);
                searchString = "}]}";
                cutIndex = siteContent.IndexOf(searchString);
                string companyInfo = siteContent.Remove(cutIndex);
                siteContent = siteContent.Remove(0,cutIndex+searchString.Length);
                
                string searcher;
                //Get certain data:
                searcher = "\"name\":\"";
                company.Name = getCertainInfoFromDataSet(searcher, ref companyInfo,"\",");
                searcher = "\"address\":\"";
                company.City = getCertainInfoFromDataSet(searcher, ref companyInfo,"\",");
                searcher = "\"www\":\"";
                company.Website = getCertainInfoFromDataSet(searcher, ref companyInfo,"\",");
                searcher = "\"email\":\"";
                company.Email = getCertainInfoFromDataSet(searcher, ref companyInfo,"\",");
                searcher = "\"formatted\":\"";
                company.PhoneNumber = getCertainInfoFromDataSet(searcher, ref companyInfo,"\"}");
                if(company.Website!=null && company.Email!=null && company.PhoneNumber!=null)
                    if(!company.City.Contains("Katowice") && !company.City.Contains("Gliwice")
                    && !company.City.Contains("Częstochowa") && !company.City.Contains("Bielsko-Biała"))
                        companyInfos.Add(company);
            }
        }
        private string getCertainInfoFromDataSet(string searched, ref string data, string ending){
            string result;
            string searcher = searched;
            int searcherIndex = 0;
            searcherIndex = data.IndexOf(searcher);
            if(searcherIndex==-1){
                return null;
            }
            //cut data out:
            data = data.Remove(0,searcherIndex+searcher.Length);
            searcher = ending;
            searcherIndex = data.IndexOf(searcher);
            result = data.Remove(searcherIndex);
            data = data.Remove(0,searcherIndex+1);
            result = result.Replace("\\u0022","\"").Replace("\\/","/").Replace("\\u0026","&")
                .Replace("\\u0105","ą").Replace("\\u0104","Ą")
                .Replace("\\u0107","ć").Replace("\\u0106","Ć")
                .Replace("\\u0119","ę").Replace("\\u0118","Ę")
                .Replace("\\u0142","ł").Replace("\\u0141","Ł")
                .Replace("\\u0144","ń").Replace("\\u0143","Ń")
                .Replace("\\u00f3","ó").Replace("\\u00d3","Ó")
                .Replace("\\u015b","ś").Replace("\\u015a","Ś")
                .Replace("\\u017a","ź").Replace("\\u0179","Ź")
                .Replace("\\u017c","ż").Replace("\\u017b","Ż");
            return result;
        }
    }
}