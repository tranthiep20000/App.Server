namespace App.DAL.Repositories.Commom
{
    /// <summary>
    /// Information of ConfigErrorMessageRepository
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public static class ConfigErrorMessageRepository
    {
        public const string UserByIdNotFound = "Không tìm thấy user có Id là <{0}>";
        public const string UserByNameNotFound = "Không tìm thấy user có tên là <{0}>";
        public const string UserByEmailNotFound = "Không tìm thấy user có email là <{0}>";
        public const string UserByNotLogin = "Tên tài khoản hoặc mật khẩu không chính xác";

        public const string ProductByIdNotFound = "Không tìm thấy product có Id là <{0}>";

        public const string ShopByIdNotFound = "Không tìm thấy shop có Id là <{0}>";

        public const string pageNumber = "Chỉ mục trang phải lớn hơn 0";
        public const string PageSize = "Số bản ghi trên một trang phải lớn hơn 0";
    }
}