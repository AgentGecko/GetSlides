/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />

class Validate{

    public static RegisterValidation() {

        $('.login-form').validate({
            rules:
            {
                email: {
                    required: true,
                    email: true
                },
                username: {
                    required: true
                },
                password1: {
                    required: true,
                    minlength: 6
                },
                password2: {
                    required: true,
                    equalTo: "input[name='password1']"
                }
            },
            messages: {
                email: {
                    required: "Your email is required for registration.",
                    email: "Your email address must be in the format of name@domain.com"
                },
                password1: {
                    required: "You need to enter a password for Your GetSlides account.",
                    minlength: "Your password must be at least 6 characters long."
                },
                password2: {
                    required: "You need to confirm Your password.",
                    equalTo: "Please enter the same password as above."
                },
                username: {
                    required: "A username is required for registration."
                }
            }
        });
    }

    public static LogInValidation() {

        $(".login-form").validate({
            rules: {
                password: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                }
            },
            messages: {
                password: {
                    required: "Password required for log in."
                },
                email: {
                    required: "Your email is required for logging in.",
                    email: "Your email address must be in the format of name@domain.com"
                }
            }
        });
    }    

    public static UsernameValidation() {
        // Changes in username should first be validated locally then sent to server to check if available
    }
}