interface IValidation {
    isValid(): boolean;
}

class Validation implements IValidation{
    firstInput: HTMLInputElement;
    secondInput: HTMLInputElement;

    constructor(inpu: HTMLInputElement, inp: HTMLInputElement) {
        this.firstInput = inpu;
        this.secondInput= inp;
    }

    isValid() {
        if (this.isEmail(this.firstInput.value.toString()) && this.isValidPass(this.secondInput.value.toString()))
            return true;
        else if (this.isEmail(this.firstInput.value.toString())) {
            alert("Invalid password.Try again.");
            return false;
        }
        else if (this.isValidPass(this.secondInput.value.toString())) {
            alert("Invalid e-mail.Try again.");
            return false;
        }
        else
        {
            alert("Invalid e-mail and password.");
            return false;
        }
    }

    isEmail(t: string){
        if (t.indexOf('@')!=-1)
            return true;
        else
            return false;
    }
    isValidPass(t: string) {
        
        if (t.length < 6)
            return false;
        else
            return true;
    }
}

function startValidation (){
    var el1 = <HTMLInputElement> document.getElementById('eMail');
    var el2 = <HTMLInputElement> document.getElementById('password');
    var val = new Validation(el1, el2);
    val.isValid();
}
