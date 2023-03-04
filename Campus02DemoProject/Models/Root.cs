namespace Campus02DemoProject.Models
{
    public class Data
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public int employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
    }
}
