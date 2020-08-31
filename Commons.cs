namespace BookRental
{
    public enum BaseMode
    {
        INSERT,
        UPDATE,
        DELETE,
        SELECT,
        NONE
    }

    public class Commons
    {
        // common database connection info
        public static readonly string CONNSTR = @"Data Source=localhost,Port=3306;Database=bookrentalshop;Uid=root;Password=mysql_p@ssw0rd";
    }
}
