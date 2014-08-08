﻿/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout.viewmodel/knockout.viewmodel.d.ts" />
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
        function PresetationViewModel(userPresentation) {
            var _this = this;
            this.UserPresentations = ko.observableArray([]);
            userPresentation.forEach(function (p) {
                return _this.UserPresentations.push(p);
            });
        }
        PresetationViewModel.prototype.addPresentation = function (presentation) {
            this.UserPresentations.push(presentation);
        };
        PresetationViewModel.prototype.removePresentation = function () {
            // Somehow need to reference which object.
        };
        PresetationViewModel.prototype.loadPresentations = function () {
            var _this = this;
            $.ajax({
                url: 'https://getslidesapi.azurewebsites.net/api/presentation',
                type: 'GET'
            }).done(function (result) {
                result.forEach(function (p) {
                    return _this.UserPresentations.push(p);
                });
            });
        };
        return PresetationViewModel;
    })();
    PresentationVM.PresetationViewModel = PresetationViewModel;
})(PresentationVM || (PresentationVM = {}));

var moja = (function () {
    function moja() {
    }
    moja.presentations = function () {
        var prezentacija = new PresentationVM.Presentation("Prezentacija", "pic1", "info1", "date1");
        var prezentacija1 = new PresentationVM.Presentation("Prezentacija2", "pic2", "info2", "date2");

        var prezentacije = new Array();
        prezentacije.push(prezentacija);
        prezentacije.push(prezentacija1);
        var prezVM = new PresentationVM.PresetationViewModel(prezentacije);

        // prezVM.loadPresentations();
        ko.applyBindings(prezVM);
    };
    return moja;
})();

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
//# sourceMappingURL=TypeScript.js.map
