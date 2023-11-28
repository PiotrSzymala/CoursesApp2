namespace CoursesApp.Repositories
{
    public class MsSqlAppDbContextConnectionString
    {
        public string Value { get; set; }

        public MsSqlAppDbContextConnectionString(IConfiguration config)
        {
            Value = config.GetConnectionString("MsSqlConnectionString");
        }
    }
}
