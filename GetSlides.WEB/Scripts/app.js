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
var Validation = (function () {
    function Validation(inpu, inp) {
        this.firstInput = inpu;
        this.secondInput = inp;
    }
    Validation.prototype.isValid = function () {
        if (this.isEmail(this.firstInput.value.toString()) && this.isValidPass(this.secondInput.value.toString()))
            return true;
        else if (this.isEmail(this.firstInput.value.toString())) {
            alert("Invalid password.Try again.");
            return false;
        } else if (this.isValidPass(this.secondInput.value.toString())) {
            alert("Invalid e-mail.Try again.");
            return false;
        } else {
            alert("Invalid e-mail and password.");
            return false;
        }
    };

    Validation.prototype.isEmail = function (t) {
        if (t.indexOf('@') != -1)
            return true;
        else
            return false;
    };
    Validation.prototype.isValidPass = function (t) {
        if (t.length < 6)
            return false;
        else
            return true;
    };
    return Validation;
})();

function startValidation() {
    var el1 = document.getElementById('eMail');
    var el2 = document.getElementById('password');
    var val = new Validation(el1, el2);
    val.isValid();
}
//# sourceMappingURL=app.js.map
