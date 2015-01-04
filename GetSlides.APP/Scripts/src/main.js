/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />
var GetSlides;
(function (GetSlides) {
    var app = Sammy();

    app.get('#/', function (context) {
        context.partial('/Views/Presentation/Index.html', function (partial) {
        });
    });

    app.get('#/About/', function (context) {
        context.partial('/Views/Presentation/Present/Index.html', function (partial) {
        });
    });

    app.get('#/Watch/', function (context) {
        context.partial('/Views/Presentation/Watch/Index.html', function (partial) {
        });
    });

    app.get('#/Account/', function (context) {
        context.partial('/Views/Settings/Index.html', function (partial) {
        });
    });

    app.run('#/');
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=main.js.map
