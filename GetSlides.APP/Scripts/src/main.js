/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />
var GetSlides;
(function (GetSlides) {
    var $pageContainer = $("#pageContainer")[0];
    GetSlides.app = Sammy();
    GetSlides.router = new GetSlides.Router();
    GetSlides.storage = new GetSlides.Storage();
    GetSlides.pdfViewer;
    GetSlides.viewmodel;

    GetSlides.app.element_selector = '#pageContainer';

    GetSlides.app.get('#/login/', function (context) {
        context.partial('/Views/Login/Index.html', function (partial) {
            loginPing();
        });
    });

    GetSlides.app.get('#/', function (context) {
        context.partial('/Views/Presentation/Index.html', function (partial) {
            ping();
            GetSlides.router.getJsonAuth("/presentation/get", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                ko.cleanNode($pageContainer);
                GetSlides.viewmodel = ko.mapping.fromJS(data);
                ko.applyBindings(GetSlides.viewmodel, $pageContainer);
                GetSlides.enableFileUploader();
            });
        });
    });

    GetSlides.app.get('#/account/', function (context) {
        context.partial('/Views/Settings/Index.html', function (partial) {
            ping();
        });
    });

    GetSlides.app.get('#/watch/:id/', function (context) {
        context.partial('/Views/Presentation/Watch/Index.html', function (partial) {
            ping();
        });
    });

    GetSlides.app.get('#/present/:id/', function (context) {
        context.partial('/Views/Presentation/Present/Index.html', function (partial) {
            ping();
        });
    });

    GetSlides.app.run('#/login/');

    function login(username, password) {
        GetSlides.router.getToken(username, password, function (error, data) {
            console.log(error, data);
            if (data.access_token !== undefined) {
                GetSlides.storage.setItem(GetSlides.storage.keys['auth'], data.token_type + " " + data.access_token);
                console.log(data.token_type + " " + data.access_token);
                location.href = '#/';
            } else {
                console.log(data.error_description);
            }
        });
    }
    GetSlides.login = login;

    function loginPing() {
        console.log("loginPing");
        if (GetSlides.storage.getItem(GetSlides.storage.keys['auth']) !== undefined) {
            console.log(GetSlides.storage.getItem(GetSlides.storage.keys['auth']));
            GetSlides.router.getJsonAuth("/account/ping", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                if (data === "Ok") {
                    console.log("loginPingOk");
                    location.href = '#/';
                } else if (data !== "Ok") {
                    console.log("loginPingNotOk");
                }
            });
        } else {
            console.log("auth empty");
        }
    }
    GetSlides.loginPing = loginPing;

    function ping() {
        console.log("ping");
        if (GetSlides.storage.getItem(GetSlides.storage.keys['auth']) !== undefined) {
            console.log(GetSlides.storage.getItem(GetSlides.storage.keys['auth']));
            GetSlides.router.getJsonAuth("/account/ping", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                if (data === "Ok") {
                    console.log("pingOk");
                } else if (data !== "Ok") {
                    console.log("pingNotOk");
                    location.href = '#/login/';
                }
            });
        } else {
            console.log("auth empty");
        }
    }
    GetSlides.ping = ping;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=main.js.map
