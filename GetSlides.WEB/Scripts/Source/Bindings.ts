/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />

module GetSlidesBinding
{
    // Class to represent a presentation 
    class Presentation {

        // Presentation properties
        Name: string;
        Picture: string;
        Info: string;
        DateUploaded: string;

        constructor(name: string, pic: string, info: string, dateUploaded: string) {
            this.Name = name;
            this.Picture = pic;
            this.Info = info;
            this.DateUploaded = dateUploaded;
        }
    }

    // Overall ViewModel for one user's presentations
    class PresetationViewModel {
        UserPresentations: KnockoutObservableArray<Presentation>;

        constructor(userPresentation: Array<Presentation>) {
            this.UserPresentations = ko.observableArray([]);
            userPresentation.forEach(p => this.UserPresentations.push(p));
        }

        addPresentation(presentation: Presentation) {
            this.UserPresentations.push(presentation);
        }
        removePresentation() {
            // Somehow need to reference which object.
        }
        loadPresentations() {
            $.ajax({
                url: 'https://getslidesapi.azurewebsites.net/api/presentation',
                type: 'GET'
            }).done((result: Array<Presentation>) => {
                    result.forEach(p => this.UserPresentations.push(p));
                });
        }

    }

    // Class to represent user
    class User{

        UserName: KnockoutObservable<string>;
        Email: string;

        constructor(username: string, email: string) {
            this.UserName = ko.observable(username);
            this.Email = email;
        }
    }

    // Overall ViewModel for one user's data
    class UserViewModel {
        ThisUser: User;
        
        constructor(userName: string, email: string) {
            this.ThisUser = new User(userName, email);
         }

        changeUsername() {
            // check the format and if available
        }
    }

    export function BindPrezVM() {
        var prezentacija = new Presentation("Prezentacija", "pic1", "info1", "date1");
        var prezentacija1 = new Presentation("Prezentacija2", "pic2", "info2", "date2");

        var prezentacije = new Array<Presentation>();
        prezentacije.push(prezentacija);
        prezentacije.push(prezentacija1);
        var prezVM = new PresetationViewModel(prezentacije);
        ko.cleanNode($('#UserPresentationContainer')[0]);
        ko.applyBindings(prezVM, document.getElementById('UserPresentationContainer'));
    }

    export function BindUserVM() {
        var userVM = new UserViewModel('Probni username', 'Probni mail');

        ko.cleanNode($('#usernamePlaceholder')[0]);
        ko.applyBindings(userVM, document.getElementById('usernamePlaceholder'));

        ko.cleanNode($('#settingsHolder')[0]);
        ko.applyBindings(userVM, document.getElementById('settingsHolder'));
    }
}