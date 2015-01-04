module GetSlides {

    export class Router {
        
        constructor(){}

        public baseAdress: string = "/api";

        public getJsonAuth(adress: string, token: string, callback: Function) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'GET',
                accepts: "application/json",
                dataType: "application/json",
                beforeSend(xhr) {
                    xhr.setRequestHeader("Authorization", token);
                }
            }).done((data) => {
                callback(null, data);
            }).fail((jqXhr, textStatus, errorThrown) => {
                callback(errorThrown, null);
            });

        }

        public postJsonAuth(adress: string, token: string, callback: Function) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'POST',
                accepts: "application/json",
                dataType: "application/json"
            }).done((data) => {
                    callback(null, data);
                }).fail((jqXhr, textStatus, errorThrown) => {
                    callback(errorThrown, null);
                });

        }

        public getJson(adress: string, callback: Function) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'GET',
                accepts: "application/json",
                dataType: "application/json"
            }).done((data) => {
                callback(null, data);
            }).fail((jqXhr, textStatus, errorThrown) => {
                callback(errorThrown, null);
            });
        }

        public postJson(adress: string, callback: Function) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'GET',
                accepts: "application/json",
                dataType: "application/json"
            }).done((data) => {
                callback(null, data);
            }).fail((jqXhr, textStatus, errorThrown) => {
                callback(errorThrown, null);
            });
        }

        

        public getToken(username: string, password: string, callback: Function) {
            var data = { grant_type: "password", username: username, password: password };
            $.ajax({
                url: "/token",
                type: 'POST',
                accepts: "application/json",
                dataType: "application/json",
                data: data
            }).always((response, error) => {
                callback(error, JSON.parse(response.responseText));
            });
        }

    }

}