$(document).ready(function() {
    new Index();
})

class Index {
    URL_API = "https://app.somee.com";
    UserId = 0;
    Mode = "";
    PageNumber = 1;
    PageSizeUser = 10;
    PageSizeProduct = 15;
    TotalRecordUser = 0;
    TotalPageUser = 0;
    TotalRecordProduct = 0;
    TotalPageProduct = 0;
    UserIdSelected = 0;
    constructor() {
        $('#formlogin').hide();
        $('#formhome').hide();
        $('#formlistuser').hide();
        $('#toast-message').hide();
        $('#formsaveuser').hide();
        $('#formsaveproduct').hide();
        $('#formdelete').hide();
        $('#formdeleteproduct').hide();
        $('#formlistproduct').show();
        $('#formmyprofile').hide();
        $('#formwarning').hide();
        $('#myprofile').hide();
        $("#boxNullDataUser").hide();
        // event
        this.initEvents();
        this.loadDataUser();
        this.loadDataProduct();
    }

    initEvents() {
        // click btnlogin
        this.clickBtnLogin();

        // click linklogin
        this.clickLinkLogin();

        // click linkmyprofile
        this.clickLinkMyProflie();

        // click linkhome
        this.clickLinkHome();

        // click linklistuser
        this.clickLinkListUser();

        // click linklistproduct
        this.clickLinkListProduct();

        // click btnadduser
        this.clickBtnAddUser();

        // click btnbackaddedit
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

        this.clickBtnFilterUser();

        this.clickBtnFilterProduct();

        this.clickBtnNumberPaging();

        this.clickBtnNext();

        this.clickBtnPrevious();

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
    }

    pagings(pagenumber, totalPage) {
        let numShown = 3;
        numShown = Math.min(numShown, totalPage);
        let first = pagenumber - Math.floor(numShown / 2);
        first = Math.max(first, 1);
        first = Math.min(first, totalPage - numShown + 1);
        return [...Array(numShown)].map((k, i) => i + first);
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
        })
    }

    clickLinkMyProflie() {
        $("#myprofile").click(function() {
            $("#formmyprofile").show();
            $("#formlogin").hide();
            $("#formhome").hide();
            $("#formlistuser").hide();
            $("#formlistproduct").hide();
        })
    }

    clickLinkHome() {
        $("#home").click(function() {
            $("#formhome").show();
            $("#formlogin").hide();
            $("#formlistuser").hide();
            $("#formlistproduct").hide();
            $("#formmyprofile").hide();
        })
    }

    clickLinkListUser() {
        $("#listuser").click(function() {
            $("#formlistuser").show();
            $("#formlogin").hide();
            $("#formhome").hide();
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
        })
    }

    clickBtnAddUser() {
        var m = this;
        $("#adduser").click(function() {
            $("#formsaveuser").show();
            m.Mode = "add";
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
                    console.log("Lỗi Api");
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
        $("#deleteproduct").click(function() {
            $("#formdeleteproduct").show();
        })
    }

    clickBtnBackDeleteProduct() {
        $("#backdeleteproduct").click(function() {
            $("#formdeleteproduct").hide();
        })
    }

    clickBtnAddUserProduct() {
        $("#addproduct").click(function() {
            $("#formsaveproduct").show();
        })
    }

    clickBtnEditProduct() {
        $("#editproduct").click(function() {
            $("#formsaveproduct").show();
        })
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

    clickBtnFilterUser() {
        var m = this;
        $("#filterUser").click(function() {
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
                            <th scope="row">${user.UserId}</th>
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

                    var pages = m.pagings(m.PageNumber, m.TotalPageUser);

                    $('#paging').empty();
                    for (const page of pages) {
                        let trHTML = $(`<li class="page-item">
                            <div class="page-link pageActive" data-id="${page}">${page}</div>
                        </li>`);
                        $('#paging').append(trHTML);
                    }

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

        let products = [];

        let valueFilter = $("#valueFilterProductName").val();
        let status = m.convertTypeTrending($('#dropdownTrending').text());

        $.ajax({
            type: "GET",
            url: `${m.URL_API}/api/Products/GetAllPagingProduct?valueFilter=${valueFilter}&trendingEnum=${status}&pageNumber=${m.PageNumber}&pageSize=${m.PageSizeProduct}`,
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
                            <th scope="row">${product.ProductId}</th>
                            <td>${product.ProductName}</td>
                            <td>${product.Slug}</td>
                            <td>${m.getShopNameById(product.ShopId)}</td>
                            <td>${product.ProductDetail}</td>
                            <td>${product.Price}</td>
                            <td>${m.convertTypeTrendingToEnum(product.Trending)}</td>
                            <td>
                                <div class="text-center">
                                    <img src="data:image/jpeg;base64,${btoa(String.fromCharCode(...new Uint8Array(product.Upload)))}">
                                </div>
                            </td>
                            <td>
                                <div class="btn btn-secondary btnEditProduct" data-id1="${product.ProductId}">Edit</div>
                                <div class="btn btn-danger btnDeleteProduct" data-id="${product.ProductId}">Delete</div>
                            </td>
                        </tr>`);
                        $('#tbodyProduct').append(trHTML);
                    }

                    var pages = m.pagings(m.PageNumber, m.TotalPageProduct);

                    $('#pagingProduct').empty();
                    for (const page of pages) {
                        let trHTML = $(`<li class="page-item">
                            <div class="page-link pageActive" data-id="${page}">${page}</div>
                        </li>`);
                        $('#pagingProduct').append(trHTML);
                    }

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