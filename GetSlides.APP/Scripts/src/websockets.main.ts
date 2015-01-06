module GetSlides {
    
    export class WebSocketHandler {
        
        public uri: string;
        public id: number;
        websocket: WebSocket;
        public isSubject = false;
        public pin: string;

        constructor(uri: string, isSubject: boolean, onOpen: Function, onClose: Function, onMessage: Function, onError: Function, id?: number) {
            this.uri = uri;
            this.isSubject = isSubject;
            if (isSubject) this.id = id;
            this.websocket = new WebSocket(uri);
            this.websocket.onopen = (event) => {
                this.onOpen(event, onOpen);
            };
            this.websocket.onclose = (event) => {
            };
            this.websocket.onmessage = (event) => {
            };
            this.websocket.onerror = (event) => {
            }
        }

        public onOpen(event: Event, callback: Function) {
            console.log("Open", event);
            if (this.isSubject) {
                router.getJsonAuth("/ws/present/getpin/" + this.id, storage.getItem(storage.keys['auth']), (data) => {
                    this.pin = data;
                    callback(data);
                });
            }
        }

        public onMessage(event: any, callback: Function) {
            console.log("Message", event);
            callback(event.data);
        }

        public onClose(event: Event, callback: Function) {
            console.log("Close", event);
            callback();
        }

        public onError(event: Event, callback: Function) {
            console.log("Error", event);
            callback();
        }

        public send(data: any) {
            if (this.isSubject) this.websocket.send(data);
        }
        

    }

}