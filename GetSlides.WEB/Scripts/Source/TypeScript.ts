/// <reference path="knockout.d.ts" />
module PresentationViewModel {

    export interface IPresentation {
        Id: number;
    }

    // Class to represent a presentation 
    export class Presentation implements IPresentation {
        Id: number;
        UserId: number;
        ImgUri: any;
        Info: string;
        DateUploaded: number;

        getID() {
            return this.Id;
        }
        setID(newID: number) {
            this.Id = newID;
        }
        getUserID() {
            return this.UserId;
        }

    }

    // Overall ViewModel for one user's screen
    export class PresetationViewModel {
        self: any = this;
        UserId: number;

        UserPresentations: KnockoutObservableArray<Presentation>;

        
        

    }
}