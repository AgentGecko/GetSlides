/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />
var GetSlides;
(function (GetSlides) {
    GetSlides.app = Sammy();
    GetSlides.router = new GetSlides.Router();
    GetSlides.storage = new GetSlides.Storage();
    GetSlides.pdfViewer;

    GetSlides.app.element_selector = '#pageContainer';

    GetSlides.app.get('#/login', function (context) {
        context.partial('/Views/Login/Index.html', function (partial) {
            console.log("login");
            loginPing();
        });
    });

    GetSlides.app.get('#/', function (context) {
        context.partial('/Views/Presentation/Index.html', function (partial) {
            ping();
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

    GetSlides.app.run('#/login');

    function login(username, password) {
        GetSlides.router.getToken(username, password, function (error, data) {
            console.log(error, data);
            GetSlides.storage.setItem(GetSlides.storage.keys['auth'], data.token_type + " " + data.access_token);
            console.log(data.token_type + " " + data.access_token);
            location.href = '#/';
        });
    }
    GetSlides.login = login;

    function loginPing() {
        console.log("loginPing");
        if (GetSlides.storage[GetSlides.storage.keys['auth']] !== undefined) {
            console.log(GetSlides.storage.getItem(GetSlides.storage.keys['auth']));
            GetSlides.router.getJsonAuth("/account/ping", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (error, data) {
                if (error === null) {
                    location.href = '#/';
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
            GetSlides.router.getJsonAuth("/account/ping", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (error, data) {
                if (error !== null) {
                    console.log(error);
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
