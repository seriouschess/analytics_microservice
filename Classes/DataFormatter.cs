using System.Collections.Generic;
using analytics.Models;
using analytics.Queries;

namespace analytics.Classes
{
    public class DataFormatter
    {
        public DataFormatter(){ }

        public string stripDomain(string url){
            // e.g. https://cnn.com/all -> cnn.com
            string stripped_domain = "";
            bool dot_encountered = false;

            for(var x=0; x < url.Length; x++){
                if(url[x]+"" == "."){  
                    dot_encountered = true;
                }
                if( url[x]+"" == "/" && dot_encountered == true){ //potential end of domain
                    break;
                }else if(url[x]+"" == "/"){
                    stripped_domain = "";
                }else{
                    stripped_domain += url[x];
                }
            }
            return stripped_domain;
        }

        public string stripDate(){
            return "place holder";
        }
    }
}