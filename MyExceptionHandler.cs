public class MyExceptionHandler{
        public MyExceptionHandler(Exception e){
            if(e is HttpRequestException) {
                Console.WriteLine("Http error");
            }
            else if( e is NullReferenceException){
                Console.WriteLine("Null json value");
            }
        }
    
}