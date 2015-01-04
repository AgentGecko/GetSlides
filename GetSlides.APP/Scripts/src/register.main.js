var GetSlides;
(function (GetSlides) {
    function register(username, password) {
        var data = { "username": username, "password": password };
        $.ajax({
            url: "/api/account/register",
            type: 'POST',
            accepts: "application/json",
            dataType: "application/json",
            data: data
        }).always(function (data, error) {
            console.log("REGISTERED", data.responseText, error);
        });
    }
    GetSlides.register = register;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=register.main.js.map
