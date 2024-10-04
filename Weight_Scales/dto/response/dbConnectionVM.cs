namespace Weight_Scales.dto.response
{


    public class DBConnectionVM
    {
        public string InstanceName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsConnected { get; set; }
        public string ErrorMessage { get; set; }

        //public void Disconnect()
        //{
        //    InstanceName = null;
        //    Username = null;
        //    Password = null;
        //    IsConnected = false;
        //    ErrorMessage = null;
        //    QueryResult = null;
        //}
    }



    public class DateRangeViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string QueryResult { get; set; }  // Store query result
        public string ErrorMessage { get; set; }  // To display error messages
    }
}