namespace App.Application.Services.Commom
{
    /// <summary>
    /// Information of ConfigErrorMessageService
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class ConfigErrorMessageService
    {
        public const string USERBYNAMENOTEMPTY = "UserName không được để trống";
        public const string USERBYNAMEALREADYEXIST = "UserName đã tồn tại";
        public const string USERBYCHARACTER = "UserName phải từ 2-10 kí tự";
        public const int LENGTHMINCHARACTEROFUSERNAME = 2;
        public const int LENGTHMAXCHARACTEROFUSERNAME = 10;
        public const string USERBYPASSWORDNOTEMPTY = "Password không được để trống";
        public const string USERBYPASSWORD = "Password phải từ 8-20 kí tự bao gồm ít nhất 1 kí tự số, " +
            "1 kí tự viết hoa và một kí tự đặc biệt";
        public const string USERBYEMAILNOTEMPTY = "Email không được để trống";
        public const string USERBYEMAILALREADYEXIST = "Email đã tồn tại";
        public const string USERBYEMAILFORMAT = "Email không đúng định dạng";
        public const string USERBYEMAILCHARACTER = "Email phải từ 10-30 kí tự";
        public const int LENGTHMINCHARACTEROFEMAIL = 10;
        public const int LENGTHMAXCHARACTEROFEMAIL = 30;
        public const int LENGTHMINCHARACTEROFPASSWORD = 8;
        public const int LENGTHMAXCHARACTEROFPASSWORD = 20;
        public const string USERBYPASSWORDCHARACTER = "Password phải từ 8-20 kí tự";

        public const string SHOPBYNAMENOTEMPTY = "Shop không được để trống";

        public const string PRODUCTBYNAMENOTEMPTY = "ProductName không được để trống";
        public const int LENGTHMINCHARACTEROFPRODUCTNAME = 2;
        public const int LENGTHMAXCHARACTEROFPRODUCTNAME = 50;
        public const string PRODUCTBYSLUGNOTEMPTY = "Slug không được để trống";
        public const int LENGTHMINCHARACTEROFSLUG = 2;
        public const int LENGTHMAXCHARACTEROFSLUG = 150;
        public const string PRODUCTBYSHOPNOTEMPTY = "Shop không được để trống";
        public const string PRODUCTBYPRICENOTEMPTY = "Price không được để trống";
        public const string PRODUCTBYUPLOADNOTEMPTY = "Image không được để trống";
        public const string PRODUCTBYCHARACTER = "ProductName phải từ 2-50 kí tự";
        public const string PRODUCTBYSLUGCHARACTER = "Slug phải từ 2-150 kí tự";
        public const string PRODUCTBYSHOPNOTFOUND = "Không tìm thấy shop có Id là <{0}>";
    }
}