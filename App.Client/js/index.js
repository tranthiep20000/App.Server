$(document).ready(function() {
    new Index();
})

class Index {
    URL_API = "https://app.somee.com";
    UserId = 0;
    constructor() {
        $('#formlogin').show();
        $('#formhome').hide();
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
        // event
        this.initEvents();
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
                    $('#myprofile').show();
                    $('#formmyprofile').show();
                    $('#login').hide();
                    $('#formlogin').hide();

                    m.UserId = jwt_decode(response.Data).UserId
                    debugger
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
        $("#adduser").click(function() {
            $("#formsaveuser").show();
        })
    }

    clickBtnEdit() {
        $("#edit").click(function() {
            $("#formsaveuser").show();
        })
    }

    clickBtnBackAddEdit() {
        $("#backaddedit").click(function() {
            $("#formsaveuser").hide();
        })
    }

    clickBtnDelete() {
        $("#delete").click(function() {
            $("#formdelete").show();
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
}