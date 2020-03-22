namespace analytics.JsonMessage
{
    public class JsonResponse
    {
        public string response {get;set;}
        public JsonResponse(string input_string){
            response = input_string;
        }
    }
}