namespace App.DAL.Repositories.Commom
{
    /// <summary>
    /// Information of ConfigErrorMessageRepository
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public static class ConfigErrorMessageRepository
    {
        public const string USERBYIDNOTFOUND = "Không tìm thấy user có Id là <{0}>";
        public const string USERBYNAMENOTFOUND = "Không tìm thấy user có tên là <{0}>";
        public const string USERBYEMAILNOTFOUND = "Không tìm thấy user có email là <{0}>";
        public const string USERBYNOTLOGIN = "Tên tài khoản hoặc mật khẩu không chính xác";

        public const string PRODUCTBYIDNOTFOUND = "Không tìm thấy product có Id là <{0}>";

        public const string SHOPBYIDNOTFOUND = "Không tìm thấy shop có Id là <{0}>";

        public const string PAGENUMBER = "Chỉ mục trang phải lớn hơn 0";
        public const string PAGESIZE = "Số bản ghi trên một trang phải lớn hơn 0";

        public const string ENTITYBYIDNOTFOUND = "Không tìm thấy Id là <{0}>";
    }
}