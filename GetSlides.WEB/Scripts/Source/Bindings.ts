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

        UserName: string;
        Email: string;

        constructor(username: string) {
            this.UserName = username;
        }
    }

    
    export function BindPrezVM() {
        var prezentacija = new Presentation("Prezentacija", "pic1", "info1", "date1");
        var prezentacija1 = new Presentation("Prezentacija2", "pic2", "info2", "date2");

        var prezentacije = new Array<Presentation>();
        prezentacije.push(prezentacija);
        prezentacije.push(prezentacija1);
        var prezVM = new PresetationViewModel(prezentacije);
        ko.applyBindings(prezVM, document.getElementById('UserPresentationContainer'));
    }
}