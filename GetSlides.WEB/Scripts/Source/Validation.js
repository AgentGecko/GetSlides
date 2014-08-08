﻿/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />
var Validate = (function () {
    function Validate() {
    }
    Validate.RegisterValidation = function () {
        $('.login-form').validate({
            rules: {
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
    };

    Validate.LogInValidation = function () {
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
    };
    return Validate;
})();
//# sourceMappingURL=Validation.js.map
