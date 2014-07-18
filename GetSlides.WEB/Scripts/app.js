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
// We will be using jquery.validation library
/*
// Regular Expressions for pattern testing and validation.
var mailRegEx = /^(\w+|(\w+\.\w+)+)@\w+\.\w+$/
var onlyNumberRegEx = /^\d+$/;
var onlyLetterRegEx = /^[a-zA-Z]+$/;
var azLetterRegEx = /[a-z]+/;
var AZLetterRegEx = /[A-Z]+/;
var numberRegEx = /\d+/;
interface IValidation {
isValid(): boolean;
informationEntered(): boolean;
}
class LoginValidation implements IValidation{
firstInput: HTMLInputElement;
secondInput: HTMLInputElement;
constructor(inpu: HTMLInputElement, inp: HTMLInputElement) {
this.firstInput = inpu;
this.secondInput= inp;
}
isValid() {
if (this.informationEntered()) {
if (this.isEmail(this.firstInput.value.toString()) && this.isValidPassword(this.secondInput.value.toString()))
return true;
else if (this.isEmail(this.firstInput.value.toString()))
{
alert("Invalid password.Try again.");
return false;
}
else if (this.isValidPassword(this.secondInput.value.toString()))
{
alert("Invalid e-mail.Try again.");
return false;
}
else
{
alert("Invalid e-mail and password.");
return false;
}
}
else return false;
}
isEmail(t: string){
if (mailRegEx.test(t.trim()))
return  true;
else
return false;
}
isValidPassword(t: string) {
if (t.length < 6)
return false;
else
return true;
}
informationEntered() {
if (this.firstInput.value == null && this.secondInput.value == null) {
alert("You need to enter Your e-mail and password.");
return false;
}
else if (this.firstInput.value == null) {
alert("You need to enter Your e-mail.");
return false;
}
else if (this.secondInput.value == null) {
alert("You need to enter Your password.");
return false;
}
else
return true;
}
}
class RegistrationValidation { }
function startLoginValidation() {
var el1 = <HTMLInputElement> document.getElementById('eMail');
var el2 = <HTMLInputElement> document.getElementById('password');
var val = new LoginValidation(el1, el2);
if (val.isValid())
window.location.href = "Page.html";
}
*/
//# sourceMappingURL=app.js.map
