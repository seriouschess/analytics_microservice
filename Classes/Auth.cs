using System;
using analytics.Models;
using analytics.Queries;

namespace analytics.Classes
{
    public class Authenticator
    {
        private AnalyticsQueries dbQuery;
        private string charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public Authenticator( AnalyticsQueries _dbQuery){
            dbQuery = _dbQuery;
        }

        public bool authenticateGeneralUse( string token_string ){
            System.Console.WriteLine($"Token String: {token_string}");
            string comparison_string = "";
            if(token_string.Length >= 10 && token_string.Length < 100){
                for( var x = 0; x < 10; x++ ){
                    comparison_string += token_string[x];
                }
            }
            System.Console.WriteLine($"Comparison String: {comparison_string}");
            if(comparison_string == "duaiosfbol"){
                return true;
            }else{
                return false;
            }
        }

        public bool validateToken(int session_id, string token){ //from database
            GenericSession FoundSession = dbQuery.getSessionById(session_id);
            if(FoundSession.token == token){
                return true;
            }else{
                return false;
            }
        }

        public string GenerateToken(){ //produces a random string of length 15 using charset
            Random rand = new Random();
            string api_default_auth = "duaiosfbol";
            string auth_token = api_default_auth;
            for(int x=0; x<15 ;x++){
                auth_token += charset[rand.Next(0, charset.Length)]; //append to standard token
            }
            return auth_token;
        }
    }
}