/// <reference path="../typings/jquery/jquery.d.ts" />
var UserAuthentication = (function () {
    function UserAuthentication() {
    }
    UserAuthentication.register = function (username, email, password, confirmPassword) {
        $.ajax({
            url: "//getslidesapi.azurewebsites.net//api/Register/?username=" + username + "&email=" + email + "&password=" + password + "&confirmPassword=" + confirmPassword,
            type: "POST"
        }).done(function () {
            window.location.assign('../Index.html');
        }).fail(function () {
            alert('Failed to register. Try again');
        });
    };
    return UserAuthentication;
})();
//# sourceMappingURL=UserAuthentication.js.map
