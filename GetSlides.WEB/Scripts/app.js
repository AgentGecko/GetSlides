var Greeter = (function () {
    function Greeter(element) {
        this.element = element;
        this.element.innerHTML += "The time is: ";
        this.span = document.createElement('span');
        this.element.appendChild(this.span);
        this.span.innerText = new Date().toUTCString();
    }
    Greeter.prototype.start = function () {
        var _this = this;
        this.timerToken = setInterval(function () {
            return _this.span.innerHTML = new Date().toUTCString();
        }, 500);
    };

    Greeter.prototype.stop = function () {
        clearTimeout(this.timerToken);
    };
    return Greeter;
})();

window.onload = function () {
    var el = document.getElementById('content');
    var greeter = new Greeter(el);
    greeter.start();
};
// Regular Expressions for pattern testing and validation.
var mailRegEx = /^(\w+|(\w+\.\w+)+)@\w+\.\w+$/;

var onlyNumberRegEx = /^\d+$/;
var onlyLetterRegEx = /^[a-zA-Z]+$/;

var azLetterRegEx = /[a-z]+/;
var AZLetterRegEx = /[A-Z]+/;
var numberRegEx = /\d+/;

var LoginValidation = (function () {
    function LoginValidation(inpu, inp) {
        this.firstInput = inpu;
        this.secondInput = inp;
    }
    LoginValidation.prototype.isValid = function () {
        if (this.informationEntered()) {
            if (this.isEmail(this.firstInput.value.toString()) && this.isValidPassword(this.secondInput.value.toString()))
                return true;
            else if (this.isEmail(this.firstInput.value.toString())) {
                alert("Invalid password.Try again.");
                return false;
            } else if (this.isValidPassword(this.secondInput.value.toString())) {
                alert("Invalid e-mail.Try again.");
                return false;
            } else {
                alert("Invalid e-mail and password.");
                return false;
            }
        } else
            return false;
    };

    LoginValidation.prototype.isEmail = function (t) {
        if (mailRegEx.test(t.trim()))
            return true;
        else
            return false;
    };
    LoginValidation.prototype.isValidPassword = function (t) {
        if (t.length < 6)
            return false;
        else
            return true;
    };
    LoginValidation.prototype.informationEntered = function () {
        if (this.firstInput.value == null && this.secondInput.value == null) {
            alert("You need to enter Your e-mail and password.");
            return false;
        } else if (this.firstInput.value == null) {
            alert("You need to enter Your e-mail.");
            return false;
        } else if (this.secondInput.value == null) {
            alert("You need to enter Your password.");
            return false;
        } else
            return true;
    };
    return LoginValidation;
})();
var RegistrationValidation = (function () {
    function RegistrationValidation() {
    }
    return RegistrationValidation;
})();

function startLoginValidation() {
    var el1 = document.getElementById('eMail');
    var el2 = document.getElementById('password');
    var val = new LoginValidation(el1, el2);
    val.isValid();
}
//# sourceMappingURL=app.js.map
