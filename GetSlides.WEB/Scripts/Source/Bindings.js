/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />
var GetSlidesBinding;
(function (GetSlidesBinding) {
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

    // Class to represent user
    var User = (function () {
        function User(username, email) {
            this.UserName = ko.observable(username);
            this.Email = email;
        }
        return User;
    })();

    // Overall ViewModel for one user's data
    var UserViewModel = (function () {
        function UserViewModel(userName, email) {
            this.ThisUser = new User(userName, email);
        }
        UserViewModel.prototype.changeUsername = function () {
            // check the format and if available
        };
        return UserViewModel;
    })();

    function BindPrezVM() {
        var prezentacija = new Presentation("Prezentacija", "pic1", "info1", "date1");
        var prezentacija1 = new Presentation("Prezentacija2", "pic2", "info2", "date2");

        var prezentacije = new Array();
        prezentacije.push(prezentacija);
        prezentacije.push(prezentacija1);
        var prezVM = new PresetationViewModel(prezentacije);
        ko.cleanNode($('#UserPresentationContainer')[0]);
        ko.applyBindings(prezVM, document.getElementById('UserPresentationContainer'));
    }
    GetSlidesBinding.BindPrezVM = BindPrezVM;

    function BindUserVM() {
        var userVM = new UserViewModel('Probni username', 'Probni mail');

        ko.cleanNode($('#usernamePlaceholder')[0]);
        ko.applyBindings(userVM, document.getElementById('usernamePlaceholder'));

        ko.cleanNode($('#settingsHolder')[0]);
        ko.applyBindings(userVM, document.getElementById('settingsHolder'));
    }
    GetSlidesBinding.BindUserVM = BindUserVM;
})(GetSlidesBinding || (GetSlidesBinding = {}));
//# sourceMappingURL=Bindings.js.map
