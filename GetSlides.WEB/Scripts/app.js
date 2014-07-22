/// <reference path="../../../packages/knockout.TypeScript.DefinitelyTyped.0.5.4/Content/Scripts/typings/knockout/knockout.d.ts" />

var PresentationVM;
(function (PresentationVM) {
    // Class to represent a presentation
    var Presentation = (function () {
        function Presentation(name, pic, info, dateUploaded) {
            this.Name = name;
            this.Picture = pic;
            this.Info = info;
            this.DateUploaded = dateUploaded;
        }
        return Presentation;
    })();
    PresentationVM.Presentation = Presentation;

    // Overall ViewModel for one user's presentations
    var PresetationViewModel = (function () {
        function PresetationViewModel() {
        }
        PresetationViewModel.prototype.addPresentation = function (presentation) {
            this.UserPresentations.push(presentation);
        };
        PresetationViewModel.prototype.deletePresentation = function () {
            // Somehow need to reference which object.
        };
        PresetationViewModel.prototype.renamePresentation = function () {
        };
        return PresetationViewModel;
    })();
    PresentationVM.PresetationViewModel = PresetationViewModel;
})(PresentationVM || (PresentationVM = {}));

/*
$(() => {
ko.applyBindings(new PresentationVM.PresetationViewModel());
});
*/
var UserVM;
(function (UserVM) {
    var User = (function () {
        function User() {
        }
        return User;
    })();
    UserVM.User = User;

    var UserViewModel = (function () {
        function UserViewModel() {
        }
        return UserViewModel;
    })();
    UserVM.UserViewModel = UserViewModel;
})(UserVM || (UserVM = {}));
//# sourceMappingURL=app.js.map
