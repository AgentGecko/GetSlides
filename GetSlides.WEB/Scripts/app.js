/// <reference path="knockout.d.ts" />
var PresentationViewModel;
(function (PresentationViewModel) {
    // Class to represent a presentation
    var Presentation = (function () {
        function Presentation() {
        }
        Presentation.prototype.getID = function () {
            return this.Id;
        };
        Presentation.prototype.setID = function (newID) {
            this.Id = newID;
        };
        Presentation.prototype.getUserID = function () {
            return this.UserId;
        };
        return Presentation;
    })();
    PresentationViewModel.Presentation = Presentation;

    // Overall ViewModel for one user's screen
    var PresetationViewModel = (function () {
        function PresetationViewModel() {
            this.self = this;
        }
        return PresetationViewModel;
    })();
    PresentationViewModel.PresetationViewModel = PresetationViewModel;
})(PresentationViewModel || (PresentationViewModel = {}));
//# sourceMappingURL=app.js.map
