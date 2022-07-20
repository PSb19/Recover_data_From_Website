
namespace DataRecovery.DTO{

    class CompanyInfo
    {
        public string Name {get; set;}
        public string PhoneNumber {get; set;}
        public string Email {get; set;}
        public string Website {get; set;}
        public string City {get; set;}

        public override string ToString()
        {
            return "Name: "+Name+"\nPhone: "+PhoneNumber+"\nEmail: "+Email+"\nWebsite: "+Website+"\nCity: "+City+"\n";
        } 
        public CompanyInfo(){}
        public CompanyInfo(string str){
            string searcher;
            searcher = "Name: ";   //get name
            int searcherIndex = 0;
            searcherIndex = str.IndexOf(searcher);
            //cut data out:
            str = str.Remove(0,searcherIndex+searcher.Length);
            searcher = "\n";
            searcherIndex = str.IndexOf(searcher);
            this.Name = str.Remove(searcherIndex);
            str = str.Remove(0,searcherIndex);
                //Get certain data:
            searcher = "Phone: "; //get phone number
            searcherIndex = 0;
            searcherIndex = str.IndexOf(searcher);
            //cut data out:
            str = str.Remove(0,searcherIndex+searcher.Length);
            searcher = "\n";
            searcherIndex = str.IndexOf(searcher);
            this.PhoneNumber = str.Remove(searcherIndex);
            str = str.Remove(0,searcherIndex);
            searcher = "Email: "; //get email
            searcherIndex = 0;
            searcherIndex = str.IndexOf(searcher);
            //cut data out:
            str = str.Remove(0,searcherIndex+searcher.Length);
            searcher = "\n";
            searcherIndex = str.IndexOf(searcher);
            this.Email = str.Remove(searcherIndex);
            str = str.Remove(0,searcherIndex);
            searcher = "Website: "; //get website
            searcherIndex = 0;
            searcherIndex = str.IndexOf(searcher);
            //cut data out:
            str = str.Remove(0,searcherIndex+searcher.Length);
            searcher = "\n";
            searcherIndex = str.IndexOf(searcher);
            this.Website = str.Remove(searcherIndex);
            str = str.Remove(0,searcherIndex);
            searcher = "City: "; //get city
            searcherIndex = 0;
            searcherIndex = str.IndexOf(searcher);
            //cut data out:
            str = str.Remove(0,searcherIndex+searcher.Length);
            searcher = "\n";
            searcherIndex = str.IndexOf(searcher);
            this.City = str.Remove(searcherIndex);
            str = str.Remove(0,searcherIndex);
        }
    }


}