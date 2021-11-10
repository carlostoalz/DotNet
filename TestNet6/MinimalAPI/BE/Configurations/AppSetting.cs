namespace BE
{
    public class AppSetting
    {
        public string DbConnection { get; set; }
        public Procedures Procedures { get; set; }
    }

    public class Procedures
    {
        public string GetTimezones { get; set; }
    }
}
