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
        DateUploaded: number;

        constructor(name: string, pic: string, info: string, dateUploaded: number){
            this.Name = name;
            this.Picture = pic;
            this.Info = info;
            this.DateUploaded = dateUploaded;
        }
    }

    // Overall ViewModel for one user's presentations
    export class PresetationViewModel { 
        UserPresentations: KnockoutObservableArray<Presentation>;
        

        addPresentation(presentation: Presentation) {
            this.UserPresentations.push(presentation);
        }
        deletePresentation() {
             // Somehow need to reference which object.
        }
        renamePresentation() { }
        
    }

}
/*
$(() => {
    ko.applyBindings(new PresentationVM.PresetationViewModel());
});
*/

module UserVM {
    export class User {
        UserName: string;
    }

    export class UserViewModel {
        MyUser: User;


    }
}