namespace App.Application.Services.Commom
{
    /// <summary>
    /// Information of ConfigErrorMessageService
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ConfigErrorMessageService
    {
        public const string UserByNameNotEmpty = "UserName không được để trống";
        public const string UserByNameAlreadyExist = "UserName đã tồn tại";
        public const string UserByCharacter = "UserName phải từ 2-10 kí tự";
        public const int LengthMinCharacterOfUserName = 2;
        public const int LengthMaxCharacterOfUserName = 10;
        public const string UserByPasswordNotEmpty = "Password không được để trống";
        public const string UserByPassword = "Password phải từ 8-20 kí tự bao gồm ít nhất 1 kí tự số, " +
            "1 kí tự viết hoa và một kí tự đặc biệt";
        public const string UserByEmailNotEmpty = "Email không được để trống";
        public const string UserByEmailAlreadyExist = "Email đã tồn tại";
        public const string UserByEmailFormat = "Email không đúng định dạng";
        public const string UserByEmailCharacter = "Email phải từ 10-30 kí tự";
        public const int LengthMinCharacterOfEmail = 10;
        public const int LengthMaxCharacterOfEmail = 30;
        public const int LengthMinCharacterOfPassword = 8;
        public const int LengthMaxCharacterOfPassword = 20;
        public const string UserByPasswordCharacter = "Password phải từ 8-20 kí tự";

        public const string ShopByNameNotEmpty = "Shop không được để trống";

        public const string ProductByNameNotEmpty = "Product không được để trống";
        public const int LengthMinCharacterOfProductName = 2;
        public const int LengthMaxCharacterOfProductName = 50;
        public const string ProductBySlugNotEmpty = "Slug không được để trống";
        public const int LengthMinCharacterOfSlug = 2;
        public const int LengthMaxCharacterOfSlug = 150;
        public const string ProductByShopNotEmpty = "Shop không được để trống";
        public const string ProductByPriceNotEmpty = "Price không được để trống";
        public const string ProductByUploadNotEmpty = "Image không được để trống";
        public const string ProductByCharacter = "Product phải từ 2-50 kí tự";
        public const string ProductBySlugCharacter = "Slug phải từ 2-150 kí tự";
        public const string ProductByShopNotFound = "Không tìm thấy shop có Id là <{0}>";
    }
}