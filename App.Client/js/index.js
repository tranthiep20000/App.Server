$(window).on('load', function() {
    $("#loader").fadeOut("slow");
    $("#content").fadeIn("slow");
});
$(document).ready(function() {
    new Index();
})

class Index {
    URL_API = "https://app.somee.com";
    //URL_API = "http://localhost:82";
    UserId = 0;
    ShopId = 0;
    Mode = "";
    PageNumber = 1;
    PageSizeUser = 10;
    PageNumberProduct = 1;
    PageSizeProduct = 15;
    TotalRecordUser = 0;
    TotalPageUser = 0;
    TotalRecordProduct = 0;
    TotalPageProduct = 0;
    UserIdSelected = 0;
    ProductIdSelected = 0;
    constructor() {
        $('#formlogin').hide();
        $('#formhome').show();
        $('#formlistuser').hide();
        $('#toast-message').hide();
        $('#formsaveuser').hide();
        $('#formsaveproduct').hide();
        $('#formdelete').hide();
        $('#formdeleteproduct').hide();
        $('#formlistproduct').hide();
        $('#formmyprofile').hide();
        $('#formwarning').hide();
        $('#myprofile').hide();
        $("#boxNullDataUser").hide();
        $("#formlistshop").hide();
        $("#headerUser").hide();
        $("#headerAdmin").show();
        // event
        this.initEvents();
        this.loadDataUser();
        this.loadDataProduct();
        this.loadDataShop();
    }

    initEvents() {
        this.clickBtnLogin();

        this.clickLinkLogin();

        this.clickLinkMyProflie();

        this.clickLinkHome();

        this.clickLinkListShop();

        this.clickLinkListUser();

        this.clickLinkListProduct();

        this.clickBtnAddUser();

        this.clickBtnBackAddEdit();

        this.clickBtnBackDelete();

        this.clickBtnDelete();

        this.clickBtnEdit();

        this.clickBtnBackDeleteProduct();

        this.clickBtnDeleteProduct();

        this.clickBtnAddUserProduct();

        this.clickBtnBackAddEditProduct();

        this.clickBtnEditProduct();

        this.clickBtnLogout();

        this.clickBtnBackWaring();

        this.clickBtnUpdateUserInfor();

        this.clickBtnSaveUser();

        this.clickBtnConfirmDeleteUser();

        this.clickBtnConfirmDeleteProduct();

        this.clickBtnFilterUser();

        this.clickBtnFilterProduct();

        this.clickBtnNumberPaging();

        this.clickBtnNext();

        this.clickBtnPrevious();

        this.clickBtnNumberPagingProduct();

        this.clickBtnNextProduct();

        this.clickBtnPreviousProduct();

        this.clickBtnNextHome();

        this.clickBtnPreviousHome();

        this.clickBtnItemShop();

        this.clickBtnSaveProduct();

        $("#typeadmin").click(function() {
            $('#dropdowntypeuser').empty();
            $('#dropdowntypeuser').append("Admin");
        })

        $("#typeuser").click(function() {
            $('#dropdowntypeuser').empty();
            $('#dropdowntypeuser').append("User");
        })

        $("#typeAdminSave").click(function() {
            $('#typeSave').empty();
            $('#typeSave').append("Admin");
        })

        $("#typeUserSave").click(function() {
            $('#typeSave').empty();
            $('#typeSave').append("User");
        })

        $("#allUserFilter").click(function() {
            $('#dropdownMenuUser').empty();
            $('#dropdownMenuUser').append("Tất cả");
        })

        $("#adminFilter").click(function() {
            $('#dropdownMenuUser').empty();
            $('#dropdownMenuUser').append("Admin");
        })

        $("#userFilter").click(function() {
            $('#dropdownMenuUser').empty();
            $('#dropdownMenuUser').append("User");
        })

        $("#allStatusFilter").click(function() {
            $('#dropdownMenuStatus').empty();
            $('#dropdownMenuStatus').append("Tất cả");
        })

        $("#notActive").click(function() {
            $('#dropdownMenuStatus').empty();
            $('#dropdownMenuStatus').append("Chưa xóa");
        })

        $("#isActive").click(function() {
            $('#dropdownMenuStatus').empty();
            $('#dropdownMenuStatus').append("Đã xóa");
        })

        $('.paging').click(function() {
            $('.paging').removeClass('active-color');
            $(this).addClass('active-color');
        });

        $("#allTrending").click(function() {
            $('#dropdownTrending').empty();
            $('#dropdownTrending').append("Tất cả");
        })

        $("#hotTrending").click(function() {
            $('#dropdownTrending').empty();
            $('#dropdownTrending').append("Hot");
        })

        $("#normalTrending").click(function() {
            $('#dropdownTrending').empty();
            $('#dropdownTrending').append("Normal");
        })

        $("#lowTrending").click(function() {
            $('#dropdownTrending').empty();
            $('#dropdownTrending').append("Low");
        })

        $("#hotTrendingSave").click(function() {
            $('#dropdownTrendingProduct').empty();
            $('#dropdownTrendingProduct').append("Hot");
        })

        $("#normalTrendingSave").click(function() {
            $('#dropdownTrendingProduct').empty();
            $('#dropdownTrendingProduct').append("Normal");
        })

        $("#lowTrendingSave").click(function() {
            $('#dropdownTrendingProduct').empty();
            $('#dropdownTrendingProduct').append("Low");
        })
    }

    convertDate(value) {
        let dateOfBirth = new Date(value);

        let date = dateOfBirth.getDate();
        let month = dateOfBirth.getMonth() + 1;
        let year = dateOfBirth.getFullYear();

        date = (date < 10 ? `0${date}` : date);
        month = (month < 10 ? `0${month}` : month);
        dateOfBirth = `${date}/${month}/${year}`;

        return dateOfBirth;
    }

    hidePassword(password) {
        var hiddenPassword = "";

        for (var i = 0; i < password.length; i++) {
            hiddenPassword += "*";
        }

        return hiddenPassword;
    }

    convertStatus(value) {
        if (value == 1) {
            return "Chưa xóa";
        } else if (value == 2) {
            return "Đã xóa";
        } else {
            return "";
        }
    }

    convertTypeUser(value) {
        if (value == 1) {
            return "Admin";
        } else if (value == 2) {
            return "User";
        } else {
            return "";
        }
    }

    convertTypeUserToEnum(value) {
        if (value.trim() == "Admin") {
            return 1;
        } else if (value.trim() == "User") {
            return 2;
        } else {
            return "";
        }
    }

    convertTypeStatusToEnum(value) {
        if (value.trim() == "Chưa xóa") {
            return 1;
        } else if (value.trim() == "Đã xóa") {
            return 2;
        } else {
            return "";
        }
    }

    convertTypeTrending(value) {
        if (value.trim() == "Hot") {
            return 1;
        } else if (value.trim() == "Normal") {
            return 2;
        } else if (value.trim() == "Low") {
            return 3;
        } else {
            return "";
        }
    }

    convertTypeTrendingToEnum(value) {
        if (value == 1) {
            return "Hot";
        } else if (value == 2) {
            return "Normal";
        } else if (value == 3) {
            return "Low";
        } else {
            return "";
        }
    }

    convertJsonToFormData(jsonObject) {
        let formData = new FormData();
        for (let key in jsonObject) {
            if (jsonObject.hasOwnProperty(key)) {
                formData.append(key, jsonObject[key]);
            }
        }
        return formData;
    }

    clickBtnSaveUser() {
        var m = this;

        $("#saveUser").click(function() {
            const userName = $('#username').val();
            const password = $('#password').val();
            const email = $('#email').val();
            const typeUser = $('#typeSave').text();
            let type = m.convertTypeUserToEnum(typeUser);

            let user = {
                "UserName": userName,
                "Password": password,
                "Email": email,
                "Type": type
            }

            if (m.Mode == 'add') {
                $.ajax({
                    type: "POST",
                    url: `${m.URL_API}/api/Users`,
                    data: JSON.stringify(user),
                    dataType: "json",
                    async: false,
                    contentType: "application/json",
                    success: function(response) {
                        m.loadDataUser();
                        $('#toast-message').empty();
                        $('#toast-message').append("Thêm mới thông tin user thành công");
                        $('#toast-message').show();
                        setTimeout(function() {
                            $('#toast-message').hide();
                        }, 3000);

                        $("#formsaveuser").hide();
                        console.log(response);
                    },
                    error: function(res) {
                        if (res.responseJSON.StatusCode == 404) {
                            $('#box-message-warning').empty();
                            $('#box-message-warning').append(res.responseJSON.Errors[0]);
                            $('#formwarning').show();
                        }
                        if (res.responseJSON.StatusCode == 500) {
                            $('#box-message-warning').empty();
                            $('#box-message-warning').append(res.responseJSON.Errors[0]);
                            $('#formwarning').show();
                        }
                    }
                });
            } else {
                $.ajax({
                    type: "PUT",
                    url: `${m.URL_API}/api/Users/${m.UserIdSelected}`,
                    data: JSON.stringify(user),
                    dataType: "json",
                    async: false,
                    contentType: "application/json",
                    success: function(response) {
                        m.loadDataUser();
                        $('#toast-message').empty();
                        $('#toast-message').append("Sửa thông tin user thành công");
                        $('#toast-message').show();
                        setTimeout(function() {
                            $('#toast-message').hide();
                        }, 3000);

                        $("#formsaveuser").hide();
                        console.log(response);
                    },
                    error: function(res) {
                        if (res.responseJSON.StatusCode == 404) {
                            $('#box-message-warning').empty();
                            $('#box-message-warning').append(res.responseJSON.Errors[0]);
                            $('#formwarning').show();
                        }
                        if (res.responseJSON.StatusCode == 500) {
                            $('#box-message-warning').empty();
                            $('#box-message-warning').append(res.responseJSON.Errors[0]);
                            $('#formwarning').show();
                        }
                    }
                });
            }
        })
    }

    clickBtnSaveProduct() {
        var m = this;

        $("#saveProduct").click(function() {
            const productName = $('#productname').val();
            const slug = $('#slug').val();
            const shopId = m.ShopId;
            const productDetail = $('#productdetail').val();
            const price = $('#price').val();
            const trending = m.convertTypeTrending($('#dropdownTrendingProduct').text());
            const file = $('#formFile')[0].files[0];

            let product = {
                "ProductName": productName,
                "Slug": slug,
                "ShopId": shopId,
                "ProductDetail": productDetail,
                "Price": price,
                "Trending": trending,
                "File": file
            }

            let formData = m.convertJsonToFormData(product);

            if (m.Mode == 'add') {
                $.ajax({
                    type: "POST",
                    url: `${m.URL_API}/api/Products`,
                    data: formData,
                    processData: false,
                    contentType: false,
                    async: false,
                    success: function(response) {
                        m.loadDataProduct();
                        $('#toast-message').empty();
                        $('#toast-message').append("Thêm mới thông tin product thành công");
                        $('#toast-message').show();
                        setTimeout(function() {
                            $('#toast-message').hide();
                        }, 3000);

                        $("#formsaveproduct").hide();
                        console.log(response);
                    },
                    error: function(res) {
                        if (res.responseJSON.StatusCode == 404) {
                            $('#box-message-warning').empty();
                            $('#box-message-warning').append(res.responseJSON.Errors[0]);
                            $('#formwarning').show();
                        }
                        if (res.responseJSON.StatusCode == 500) {
                            $('#box-message-warning').empty();
                            $('#box-message-warning').append(res.responseJSON.Errors[0]);
                            $('#formwarning').show();
                        }
                    }
                });
            } else {
                $.ajax({
                    type: "PUT",
                    url: `${m.URL_API}/api/Products/${m.ProductIdSelected}`,
                    data: formData,
                    processData: false,
                    contentType: false,
                    async: false,
                    success: function(response) {
                        m.loadDataProduct();
                        $('#toast-message').empty();
                        $('#toast-message').append("Sửa thông tin product thành công");
                        $('#toast-message').show();
                        setTimeout(function() {
                            $('#toast-message').hide();
                        }, 3000);

                        $("#formsaveproduct").hide();
                        console.log(response);
                    },
                    error: function(res) {
                        if (res.responseJSON.StatusCode == 404) {
                            $('#box-message-warning').empty();
                            $('#box-message-warning').append(res.responseJSON.Errors[0]);
                            $('#formwarning').show();
                        }
                        if (res.responseJSON.StatusCode == 500) {
                            $('#box-message-warning').empty();
                            $('#box-message-warning').append(res.responseJSON.Errors[0]);
                            $('#formwarning').show();
                        }
                    }
                });
            }
        })
    }

    clickBtnUpdateUserInfor() {
        var m = this;
        $("#btnUpdateInfor").click(function() {
            const userName = $('#usernameinfor').val();
            const password = $('#passwordinfor').val();
            const passwordConfirm = $('#passwordconfirminfor').val();
            const email = $('#emailinfor').val();
            const typeUser = $('#dropdowntypeuser').text();
            let type = m.convertTypeUserToEnum(typeUser);

            if (password != passwordConfirm) {
                $('#box-message-warning').empty();
                $('#box-message-warning').append("password và passwordconfirm không khớp");
                $('#formwarning').show();

                return;
            }

            let user = {
                "UserName": userName,
                "Password": password,
                "Email": email,
                "Type": type
            }

            $.ajax({
                type: "PUT",
                url: `${m.URL_API}/api/Users/${m.UserId}`,
                data: JSON.stringify(user),
                dataType: "json",
                async: false,
                contentType: "application/json",
                success: function(response) {
                    m.getUserById(m.UserId, m);
                    $('#toast-message').empty();
                    $('#toast-message').append("Sửa thông tin thành công");
                    $('#toast-message').show();
                    setTimeout(function() {
                        $('#toast-message').hide();
                    }, 3000);

                    console.log(response);
                },
                error: function(res) {
                    if (res.responseJSON.StatusCode == 404) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                    if (res.responseJSON.StatusCode == 500) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                }
            });
        })
    }

    getUserById(id, m) {
        $.ajax({
            type: "GET",
            url: `${m.URL_API}/api/Users/${id}`,
            async: false,
            dataType: "json",
            success: function(response) {
                let user = response.Data

                $("#usernameinfor").val(user.UserName);
                $("#passwordinfor").val(atob(user.Password));
                $("#passwordconfirminfor").val(atob(user.Password));
                $("#emailinfor").val(user.Email);
                $('#dropdowntypeuser').empty();
                $('#dropdowntypeuser').append(m.convertTypeUser(user.Type));
            },
            error: function(res) {
                if (res.responseJSON.StatusCode == 404) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
                if (res.responseJSON.StatusCode == 500) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
            }
        });
    }

    clickBtnLogin() {
        let m = this;

        $("#btnLogin").click(function() {
            const email = $('#exampleInputEmail1').val();
            const password = $('#exampleInputPassword1').val();

            let user = {
                "Email": email,
                "Password": password
            }

            $.ajax({
                type: "POST",
                url: `${m.URL_API}/api/Authentications/LoginForEmail`,
                data: JSON.stringify(user),
                dataType: "json",
                async: false,
                contentType: "application/json",
                success: function(response) {
                    m.UserId = jwt_decode(response.Data).UserId

                    m.getUserById(m.UserId, m);

                    if (m.UserId == 2) {
                        $('#headerUser').show();
                        $('#headerAdmin').hide();
                    } else {
                        $('#headerUser').hide();
                        $('#headerAdmin').show();
                    }

                    $('#myprofile').show();
                    $('#formmyprofile').show();
                    $('#login').hide();
                    $('#formlogin').hide();
                },
                error: function(res) {
                    if (res.responseJSON.StatusCode == 404) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                    if (res.responseJSON.StatusCode == 500) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                }
            });
        })
    }

    clickLinkLogin() {
        $("#login").click(function() {
            $("#formlogin").show();
            $("#formhome").hide();
            $("#formlistuser").hide();
            $("#formlistproduct").hide();
            $("#formmyprofile").hide();
            $("#formlistshop").hide();
        })
    }

    clickLinkMyProflie() {
        $("#myprofile").click(function() {
            $("#formmyprofile").show();
            $("#formlogin").hide();
            $("#formhome").hide();
            $("#formlistuser").hide();
            $("#formlistproduct").hide();
            $("#formlistshop").hide();
        })
    }

    clickLinkHome() {
        $("#home").click(function() {
            $("#formhome").show();
            $("#formlogin").hide();
            $("#formlistuser").hide();
            $("#formlistproduct").hide();
            $("#formmyprofile").hide();
            $("#formlistshop").hide();
        })
    }

    clickLinkListUser() {
        $("#listuser").click(function() {
            $("#formlistuser").show();
            $("#formlogin").hide();
            $("#formhome").hide();
            $("#formlistproduct").hide();
            $("#formmyprofile").hide();
            $("#formlistshop").hide();
        })
    }

    clickLinkListShop() {
        $("#shop").click(function() {
            $("#formlistshop").show();
            $("#formlogin").hide();
            $("#formhome").hide();
            $("#formlistuser").hide();
            $("#formlistproduct").hide();
            $("#formmyprofile").hide();
        })
    }

    clickLinkListProduct() {
        $("#listproduct").click(function() {
            $("#formlistproduct").show();
            $("#formlistuser").hide();
            $("#formlogin").hide();
            $("#formhome").hide();
            $("#formmyprofile").hide();
            $("#formlistshop").hide();
        })
    }

    clickBtnAddUser() {
        var m = this;
        $("#adduser").click(function() {
            $("#formsaveuser").show();
            m.Mode = "add";

            $('#username').val("");
            $('#password').val("");
            $('#email').val("");
            $('#typeSave').text("User");
        })
    }

    clickBtnEdit() {
        var m = this;
        $('#tbodyUser').on('click', '.btnEditUser', function() {
            m.UserIdSelected = $(this).data('id1');
            $("#formsaveuser").show();
            m.Mode = "edit";

            $.ajax({
                type: "GET",
                url: `${m.URL_API}/api/Users/${m.UserIdSelected}`,
                success: function(response) {
                    $("#username").val(response.Data.UserName);
                    $("#password").val(atob(response.Data.Password));
                    $("#email").val(response.Data.Email);
                    $('#typeSave').empty();
                    $('#typeSave').append(m.convertTypeUser(response.Data.Type));
                },
                error: function(res) {
                    if (res.responseJSON.StatusCode == 404) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                    if (res.responseJSON.StatusCode == 500) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                }
            })
        });
    }

    clickBtnBackAddEdit() {
        $("#backaddedit").click(function() {
            $("#formsaveuser").hide();
        })
    }

    clickBtnDelete() {
        var m = this;
        $('#tbodyUser').on('click', '.btnDeleteUser', function() {
            m.UserIdSelected = $(this).data('id');
            $("#formdelete").show();
        });
    }

    clickBtnNumberPaging() {
        var m = this;
        $('#paging').on('click', '.pageActive', function() {
            m.PageNumber = $(this).data('id');
            m.loadDataUser();
        });
    }

    clickBtnNumberPagingProduct() {
        var m = this;
        $('#pagingProduct').on('click', '.pageActiveProduct', function() {
            m.PageNumberProduct = $(this).data('id');
            m.loadDataProduct();
        });
    }

    clickBtnItemShop() {
        var m = this;
        $('#shopProduct').on('click', '.itemShop', function() {
            $("#dropdownShop").text($(this).text());

            m.ShopId = $(this).closest('[data-id]').data('id');
        });
    }

    clickBtnNext() {
        var m = this;
        $("#nextUser").click(function() {
            if (m.PageNumber < m.TotalPageUser) {
                m.PageNumber++;
                m.loadDataUser();
            } else {
                $('#box-message-warning').empty();
                $('#box-message-warning').append("Đã đến trang cuối cùng");
                $('#formwarning').show();
            }
        })
    }

    clickBtnNextProduct() {
        var m = this;
        $("#nextProduct").click(function() {
            if (m.PageNumberProduct < m.TotalPageProduct) {
                m.PageNumberProduct++;
                m.loadDataProduct();
            } else {
                $('#box-message-warning').empty();
                $('#box-message-warning').append("Đã đến trang cuối cùng");
                $('#formwarning').show();
            }
        })
    }

    clickBtnNextHome() {
        var m = this;
        $("#nextHome").click(function() {
            if (m.PageNumberProduct < m.TotalPageProduct) {
                m.PageNumberProduct++;
                m.loadDataProduct();
            } else {
                $('#box-message-warning').empty();
                $('#box-message-warning').append("Đã đến trang cuối cùng");
                $('#formwarning').show();
            }
        })
    }

    clickBtnPrevious() {
        var m = this;
        $("#previousUser").click(function() {
            if (m.PageNumber > 1) {
                m.PageNumber--;
                m.loadDataUser();
            } else {
                $('#box-message-warning').empty();
                $('#box-message-warning').append("Đã đến trang đầu tiên");
                $('#formwarning').show();
            }
        })
    }

    clickBtnPreviousProduct() {
        var m = this;
        $("#previousProduct").click(function() {
            if (m.PageNumberProduct > 1) {
                m.PageNumberProduct--;
                m.loadDataProduct();
            } else {
                $('#box-message-warning').empty();
                $('#box-message-warning').append("Đã đến trang đầu tiên");
                $('#formwarning').show();
            }
        })
    }

    clickBtnPreviousHome() {
        var m = this;
        $("#previousHome").click(function() {
            if (m.PageNumberProduct > 1) {
                m.PageNumberProduct--;
                m.loadDataProduct();
            } else {
                $('#box-message-warning').empty();
                $('#box-message-warning').append("Đã đến trang đầu tiên");
                $('#formwarning').show();
            }
        })
    }

    clickBtnBackDelete() {
        $("#backdelete").click(function() {
            $("#formdelete").hide();
        })
    }

    clickBtnBackWaring() {
        $("#backwaring").click(function() {
            $("#formwarning").hide();
        })
    }

    clickBtnDeleteProduct() {
        var m = this;
        $('#tbodyProduct').on('click', '.btnDeleteProduct', function() {
            m.ProductIdSelected = $(this).data('id');
            $("#formdeleteproduct").show();
        });
    }

    clickBtnBackDeleteProduct() {
        $("#backdeleteproduct").click(function() {
            $("#formdeleteproduct").hide();
        })
    }

    clickBtnAddUserProduct() {
        var m = this;
        $("#addproduct").click(function() {
            $("#formsaveproduct").show();
            $("#shopCreate").show();
            $("#shopEdit").hide();
            m.Mode = "add";

            $('#productname').val("");
            $('#slug').val("");
            $('#productdetail').val("");
            $('#price').val("");
            $('#dropdownTrendingProduct').text("Low");
        })
    }

    clickBtnEditProduct() {
        var m = this;
        $('#tbodyProduct').on('click', '.btnEditProduct', function() {
            m.ProductIdSelected = $(this).data('id1');
            $("#formsaveproduct").show();
            $("#shopCreate").hide();
            $("#shopEdit").show();
            m.Mode = "edit";

            $.ajax({
                type: "GET",
                url: `${m.URL_API}/api/Products/${m.ProductIdSelected}`,
                success: function(response) {
                    $("#productname").val(response.Data.ProductName);
                    $("#slug").val(response.Data.Slug);
                    m.ShopId = response.Data.ShopId;
                    $("#labelShopName").val(m.getShopNameById(response.Data.ShopId));
                    $("#productdetail").val(response.Data.ProductDetail);
                    $("#price").val(response.Data.Price);
                    $("#dropdownTrendingProduct").empty();
                    $('#dropdownTrendingProduct').append(m.convertTypeTrendingToEnum(response.Data.Trending));

                    const formData = new FormData();
                    formData.append('Upload', response.Data.Upload);
                    $("#fromFile").val(formData);
                },
                error: function(res) {
                    if (res.responseJSON.StatusCode == 404) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                    if (res.responseJSON.StatusCode == 500) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                }
            })
        });
    }

    clickBtnBackAddEditProduct() {
        $("#backaddeditproduct").click(function() {
            $("#formsaveproduct").hide();
        })
    }

    clickBtnLogout() {
        $("#btnlogout").click(function() {
            $("#login").show();
            $("#formlogin").show();
            $("#formmyprofile").hide();
            $("#myprofile").hide();
        })
    }

    clickBtnConfirmDeleteUser() {
        var m = this;

        $("#btnDeleteUser").click(function() {
            $.ajax({
                type: "DELETE",
                url: `${m.URL_API}/api/Users/${m.UserIdSelected}`,
                success: function(response) {
                    m.loadDataUser();

                    $('#toast-message').empty();
                    $('#toast-message').append("Xóa thành công");
                    $('#toast-message').show();
                    $("#formdelete").hide();
                    setTimeout(function() {
                        $('#toast-message').hide();
                    }, 3000);

                    console.log(response);
                },
                error: function(res) {
                    if (res.responseJSON.StatusCode == 404) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                    if (res.responseJSON.StatusCode == 500) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                }
            });
        })
    }

    clickBtnConfirmDeleteProduct() {
        var m = this;

        $("#btnConfirmDelete").click(function() {
            $.ajax({
                type: "DELETE",
                url: `${m.URL_API}/api/Products/${m.ProductIdSelected}`,
                success: function(response) {
                    m.loadDataProduct();

                    $('#toast-message').empty();
                    $('#toast-message').append("Xóa thành công");
                    $('#toast-message').show();
                    $("#formdeleteproduct").hide();
                    setTimeout(function() {
                        $('#toast-message').hide();
                    }, 3000);

                    console.log(response);
                },
                error: function(res) {
                    if (res.responseJSON.StatusCode == 404) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                    if (res.responseJSON.StatusCode == 500) {
                        $('#box-message-warning').empty();
                        $('#box-message-warning').append(res.responseJSON.Errors[0]);
                        $('#formwarning').show();
                    }
                }
            });
        })
    }

    clickBtnFilterUser() {
        var m = this;

        $("#filterUser").click(function() {
            m.PageNumber = 1;
            m.loadDataUser();

            $('#toast-message').empty();
            $('#toast-message').append("Lọc thành công");
            $('#toast-message').show();
            $("#formdelete").hide();
            setTimeout(function() {
                $('#toast-message').hide();
            }, 3000);
        })
    }

    clickBtnFilterProduct() {
        var m = this;
        $("#btnFilterProduct").click(function() {
            m.PageNumberProduct = 1;
            m.loadDataProduct();

            $('#toast-message').empty();
            $('#toast-message').append("Lọc thành công");
            $('#toast-message').show();
            $("#formdelete").hide();
            setTimeout(function() {
                $('#toast-message').hide();
            }, 3000);
        })
    }

    getShopNameById(idShop) {
        var m = this;
        let shopName = "";
        $.ajax({
            type: "GET",
            url: `${m.URL_API}/api/Shops/${idShop}`,
            async: false,
            dataType: "json",
            success: function(response) {
                shopName = response.Data.ShopName;
            },
            error: function(res) {
                if (res.responseJSON.StatusCode == 404) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
                if (res.responseJSON.StatusCode == 500) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
            }
        });

        return shopName;
    }

    loadDataUser() {
        var m = this;
        $('#tbodyUser').empty();

        let users = [];

        let valueFilter = $("#valueFilterUserName").val();
        let createAt = $("#createAtUser").val();
        let typeUser = m.convertTypeUserToEnum($('#dropdownMenuUser').text());
        let status = m.convertTypeStatusToEnum($('#dropdownMenuStatus').text());

        $.ajax({
            type: "GET",
            url: `${m.URL_API}/api/Users/GetAllPagingUser?valueFilter=${valueFilter}&createAt=${createAt}&typeEnum=${typeUser}&deleteEnum=${status}&pageNumber=${m.PageNumber}&pageSize=${m.PageSizeUser}`,
            async: false,
            dataType: "json",
            success: function(response) {
                users = response.Data.Data;
                m.TotalRecordUser = response.Data.ToTalRecord;
                m.TotalPageUser = response.Data.ToTalPage;

                if (users.length <= 0) {
                    $("#boxNullDataUser").show();
                    $("#boxDataUser").hide();
                } else {
                    $("#boxDataUser").show();
                    $("#boxNullDataUser").hide();

                    for (const user of users) {
                        let trHTML = $(`<tr>
                            <td scope="row">${user.UserId}</td>
                            <td>${user.UserName}</td>
                            <td>${user.Email}</td>
                            <td>${m.hidePassword(atob(user.Password))}</td>
                            <td>${m.convertTypeUser(user.Type)}</td>
                            <td>${m.convertDate(user.CreateAt)}</td>
                            <td>${m.convertStatus(user.DeleteAt)}</td>
                            <td>
                                <div class="btn btn-secondary btnEditUser" data-id1="${user.UserId}">Edit</div>
                                <div class="btn btn-danger btnDeleteUser" data-id="${user.UserId}">Delete</div>
                            </td>
                        </tr>`);
                        $('#tbodyUser').append(trHTML);
                    }

                    $('#paging').empty();
                    let trHTML = $(`<li class="page-item">
                            <div class="page-link">${m.PageNumber}/${m.TotalPageUser}</div>
                        </li>`);
                    $('#paging').append(trHTML);
                }
            },
            error: function(res) {
                if (res.responseJSON.StatusCode == 404) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
                if (res.responseJSON.StatusCode == 500) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
            }
        });
    }

    loadDataProduct() {
        var m = this;
        $('#tbodyProduct').empty();
        $('#boxCardsProduct').empty();

        let products = [];

        let valueFilter = $("#valueFilterProductName").val();
        let status = m.convertTypeTrending($('#dropdownTrending').text());

        $.ajax({
            type: "GET",
            url: `${m.URL_API}/api/Products/GetAllPagingProduct?valueFilter=${valueFilter}&trendingEnum=${status}&pageNumber=${m.PageNumberProduct}&pageSize=${m.PageSizeProduct}`,
            async: false,
            dataType: "json",
            success: function(response) {
                products = response.Data.Data;
                m.TotalRecordProduct = response.Data.ToTalRecord;
                m.TotalPageProduct = response.Data.ToTalPage;

                if (products.length <= 0) {
                    $("#boxNullDataProduct").show();
                    $("#boxDataProduct").hide();
                } else {
                    $("#boxDataProduct").show();
                    $("#boxNullDataProduct").hide();

                    for (const product of products) {
                        let trHTML = $(`<tr>
                            <td scope="row">${product.ProductId}</td>
                            <td>${product.ProductName}</td>
                            <td>${product.Slug}</td>
                            <td>${m.getShopNameById(product.ShopId)}</td>
                            <td>${product.ProductDetail}</td>
                            <td>${product.Price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</td>
                            <td>${m.convertTypeTrendingToEnum(product.Trending)}</td>
                            <td>
                                <div class="text-center">
                                    <img class="img-product" src="${product.Upload}">
                                </div>
                            </td>
                            <td>
                                <div class="btn btn-secondary btnEditProduct" data-id1="${product.ProductId}">Edit</div>
                                <div class="btn btn-danger btnDeleteProduct" data-id="${product.ProductId}">Delete</div>
                            </td>
                        </tr>`);
                        $('#tbodyProduct').append(trHTML);

                        let divHTML = $(`
                            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 card mb-3">
                                <img src="${product.Upload}" class="card-img-top img-product" alt="...">
                                <div class="card-body">
                                    <h5 class="card-title">Tên: ${product.ProductName}</h5>
                                    <p class="card-text">Giá: ${product.Price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</p>
                                    <p class="card-text">Mô tả: ${product.ProductDetail}</p>
                                </div>
                            </div>
                        `);
                        $('#boxCardsProduct').append(divHTML);
                    }

                    $('#pagingProduct').empty();
                    let trHTML = $(`<li class="page-item">
                            <div class="page-link">${m.PageNumberProduct}/${m.TotalPageProduct}</div>
                        </li>`);
                    $('#pagingProduct').append(trHTML);

                    $('#pagingHome').empty();
                    let trhome = $(`<li class="page-item">
                            <div class="page-link">${m.PageNumberProduct}/${m.TotalPageProduct}</div>
                        </li>`);
                    $('#pagingHome').append(trhome);
                }
            },
            error: function(res) {
                if (res.responseJSON.StatusCode == 404) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
                if (res.responseJSON.StatusCode == 500) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
            }
        });
    }

    loadDataShop() {
        var m = this;

        let shops = [];

        $.ajax({
            type: "GET",
            url: `${m.URL_API}/api/Shops`,
            async: false,
            dataType: "json",
            success: function(response) {
                shops = response.Data;
                $('#shopProduct').empty();
                $('#boxCardsShop').empty();
                for (const shop of shops) {
                    let trHTML = $(`<div class="dropdown-item itemShop" data-id="${shop.ShopId}">${shop.ShopName}</div>`);
                    $('#shopProduct').append(trHTML);

                    let divHTML = $(`
                        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 card mb-3">
                            <div class="card-body">
                                <h5 class="card-title">Id shop: ${shop.ShopId}</h5>
                                <h5 class="card-title">Tên shop: ${shop.ShopName}</h5>
                            </div>
                        </div>
                    `);
                    $('#boxCardsShop').append(divHTML);

                }
            },
            error: function(res) {
                if (res.responseJSON.StatusCode == 404) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
                if (res.responseJSON.StatusCode == 500) {
                    $('#box-message-warning').empty();
                    $('#box-message-warning').append(res.responseJSON.Errors[0]);
                    $('#formwarning').show();
                }
            }
        });
    }
}