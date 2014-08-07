/// <reference path="../typings/jquery/jquery.d.ts" />

class UserAuthentication
{
    public static register(username: string, email: string, password: string, confirmPassword: string)
    {
        $.ajax({
            url: "api/Register/?username=" + username + "&email=" + email + "&password=" + password + "&confirmPassword=" + confirmPassword,
            type: "POST"
        }).done(function () {
                alert('Successful registration. To continue confirm email.')

            }).fail(function () {
                alert('Failed to register. Try again')

            });
    }

}