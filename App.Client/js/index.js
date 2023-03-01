$(document).ready(function() {
    new Index();
})

class Index {
    URL_API = "";
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

        // event
        this.initEvents();
    }

    initEvents() {
        // click btnlogin
        this.clickBtnLogin();

        // click linklogin
        this.clickLinkLogin();

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
            debugger
            $.ajax({
                type: "POST",
                url: "https://localhost:9999/api/v1/Employees",
                data: JSON.stringify(user),
                dataType: "json",
                async: false,
                contentType: "application/json",
                success: function(response) {

                },
                error: function(res) {

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
        })
    }

    clickLinkHome() {
        $("#home").click(function() {
            $("#formhome").show();
            $("#formlogin").hide();
            $("#formlistuser").hide();
            $("#formlistproduct").hide();
        })
    }

    clickLinkListUser() {
        $("#listuser").click(function() {
            $("#formlistuser").show();
            $("#formlogin").hide();
            $("#formhome").hide();
            $("#formlistproduct").hide();
        })
    }

    clickLinkListProduct() {
        $("#listproduct").click(function() {
            $("#formlistproduct").show();
            $("#formlistuser").hide();
            $("#formlogin").hide();
            $("#formhome").hide();
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
}