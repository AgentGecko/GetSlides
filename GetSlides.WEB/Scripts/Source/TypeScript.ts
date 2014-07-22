/// <reference path="../../../packages/knockout.TypeScript.DefinitelyTyped.0.5.4/Content/Scripts/typings/knockout/knockout.d.ts" />

declare var $: any;

module PresentationVM {

    export interface IPresentation {
    }

    // Class to represent a presentation 
    export class Presentation implements IPresentation {
        
        // Presentation properties - only those which are to be shown in the View
        // Those properties which the user will be allowed to change are knockout observeables.
        Name: string;
        Picture: string; 
        Info:string; 
        DateUploaded: string;

        constructor(name: string, pic: string, info: string, dateUploaded: string){
            this.Name = name;
            this.Picture = pic;
            this.Info = info;
            this.DateUploaded = dateUploaded;
        }
    }

    // Overall ViewModel for one user's presentations
    export class PresetationViewModel { 
        UserPresentations: KnockoutObservableArray<Presentation>;

        constructor(userPresentation: Array<Presentation>){
            this.UserPresentations = ko.observableArray([]);
            userPresentation.forEach(p => this.UserPresentations.push(p));
        }

        addPresentation(presentation : Presentation) {
            this.UserPresentations.push(presentation);
        }
        removePresentation(){
             // Somehow need to reference which object.
        }
       }

}

$(() => {
    var prezentacija = new PresentationVM.Presentation("Prezentacija", "pic1", "info1", "date1");
    var prezentacija1 = new PresentationVM.Presentation("Prezentacija2", "pic2","info2","date2" );

    var prezentacije = new Array<PresentationVM.Presentation>();
    prezentacije.push(prezentacija);
    prezentacije.push(prezentacija1);

    var prezVM = new PresentationVM.PresetationViewModel(prezentacije);
    ko.applyBindings(prezVM);
});


module UserVM {
    export class User {
        UserName: string;
    }

    export class UserViewModel {
        MyUser: User;
    }
}