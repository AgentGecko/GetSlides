﻿/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="pdf.main.ts" />
/// <reference path="storage.ts" />
/// <reference path="presentations.main.ts" />
/// <reference path="websockets.main.ts" />
/// <reference path="router.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />
var GetSlides;
(function (GetSlides) {
    var $pageContainer = $("#pageContainer")[0];
    GetSlides.app = Sammy();
    GetSlides.router = new GetSlides.Router();
    GetSlides.storage = new GetSlides.Storage();
    GetSlides.pdfViewer;
    GetSlides.viewmodel = {};

    $(window).resize(function () {
        resizeCanvas();
    });

    GetSlides.app.element_selector = '#pageContainer';

    GetSlides.app.get('#/logout/', function (context) {
        console.log("LOGOUT");
        $("#navbar-username").text("Signed in as Anon");
        GetSlides.storage.setItem(GetSlides.storage.keys['auth'], null);
        location.href = '#/login/';
    });

    GetSlides.app.get('#/login/', function (context) {
        context.partial('/Views/Login/Index.html', function (partial) {
            loginPing();
            navbarToggle(false);
        });
    });

    GetSlides.app.get('#/', function (context) {
        context.partial('/Views/Presentation/Index.html', function (partial) {
            ping();
            navbarToggle(true);
            GetSlides.router.getJsonAuth("/presentation/get", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                var vdata = { "presentations": data };
                updateViewModel(vdata);
                GetSlides.enableFileUploader();
            });
        });
    });

    GetSlides.app.get('#/account/', function (context) {
        context.partial('/Views/Settings/Index.html', function (partial) {
            ping();
            navbarToggle(true);
        });
    });

    GetSlides.app.get('#/watch/connect/', function (context) {
        context.partial('/Views/Presentation/Watch/Index.html', function (partial) {
            navbarToggle(false);
            resizeCanvas();
            GetSlides.pdfViewer = new GetSlides.PdfViewer(GetSlides.selectedPresentationUri, "canvas", false, "ws://localhost:6316/api/ws/watch/" + GetSlides.spin);
        });
    });

    GetSlides.app.get('#/watch/', function (context) {
        context.partial('/Views/Presentation/Watch/InsertPin.html', function (partial) {
            ping();
            navbarToggle(true);
        });
    });

    GetSlides.app.get('#/present/:id/', function (context) {
        context.partial('/Views/Presentation/Present/Index.html', function (partial) {
            ping();
            navbarToggle(true);
            resizeCanvas();
            GetSlides.router.getJsonAuth("/presentation/get/" + context.params["id"], GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (presentation) {
                GetSlides.router.getJsonAuth("/account/username", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (username) {
                    console.log(presentation, username);
                    GetSlides.pdfViewer = new GetSlides.PdfViewer(presentation.presentationUri, "canvas", true, "ws://localhost:6316/api/ws/present/" + presentation.id + "/username/" + username, presentation.id);
                });
            });
        });
    });

    GetSlides.app.run('#/login/');

    function navbarToggle(login) {
        if (login) {
            $("#myPresentations").css("display", "block");
            $("#watchanon").css("display", "none");
            $("#watchlogin").css("display", "block");
            $("#settings").css("display", "block");
        } else {
            $("#myPresentations").css("display", "none");
            $("#watchanon").css("display", "block");
            $("#watchlogin").css("display", "none");
            $("#settings").css("display", "none");
        }
    }
    GetSlides.navbarToggle = navbarToggle;

    function resizeCanvas() {
        var canvas = document.getElementById("canvas");
        canvas.height = window.innerHeight - 120;
        canvas.width = this.canvas.height * 27 / 19;
        $("#canvas").css("margin-left", ((window.innerWidth - this.canvas.width) / 2));
    }
    GetSlides.resizeCanvas = resizeCanvas;

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

    function loginPing(callback) {
        console.log("loginPing");
        if (GetSlides.storage.getItem(GetSlides.storage.keys['auth']) !== undefined) {
            console.log(GetSlides.storage.getItem(GetSlides.storage.keys['auth']));
            GetSlides.router.getJsonAuth("/account/ping", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                if (data === "Ok") {
                    GetSlides.router.getJsonAuth("/account/username", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                        $("#navbar-username").text("Signed in as " + data);
                        location.href = '#/';
                    });
                    console.log("loginPingOk");
                } else if (data !== "Ok") {
                    console.log("loginPingNotOk");
                }
            });
        } else {
            console.log("auth empty");
        }
    }
    GetSlides.loginPing = loginPing;

    function ping(callback) {
        console.log("ping");
        if (GetSlides.storage.getItem(GetSlides.storage.keys['auth']) !== undefined) {
            console.log(GetSlides.storage.getItem(GetSlides.storage.keys['auth']));
            GetSlides.router.getJsonAuth("/account/ping", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                if (data === "Ok") {
                    console.log("pingOk");

                    GetSlides.router.getJsonAuth("/account/username", GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                        $("#navbar-username").text("Signed in as " + data);
                    });
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

    function updateViewModel(data) {
        if (GetSlides.viewmodel.presentations != undefined)
            ko.cleanNode($pageContainer);
        GetSlides.viewmodel = ko.mapping.fromJS(data);
        ko.applyBindings(GetSlides.viewmodel, $pageContainer);
    }
    GetSlides.updateViewModel = updateViewModel;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=main.js.map
