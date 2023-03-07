namespace App.Api.Options
{
    /// <summary>
    /// Information of ApiRouter
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public static class ApiRouter
    {
        /// <summary>
        /// ROUTER
        /// </summary>
        public const string ROUTER = "api/[controller]";

        /// <summary>
        /// ID
        /// </summary>
        public const string ID = "{id}";

        /// <summary>
        /// User
        /// </summary>
        public class User
        {
            /// <summary>
            /// GETALLPAGINGUSER
            /// </summary>
            public const string GETALLPAGINGUSER = "GetAllPagingUser";
        }

        /// <summary>
        /// Product
        /// </summary>
        public class Product
        {
            /// <summary>
            /// GETALLPAGINGPRODUCT
            /// </summary>
            public const string GETALLPAGINGPRODUCT = "GetAllPagingProduct";
        }

        /// <summary>
        /// Authentication
        /// </summary>
        public class Authentication
        {
            /// <summary>
            /// LOGINFOREMAIL
            /// </summary>
            public const string LOGINFOREMAIL = "LoginForEmail";
        }
    }
}