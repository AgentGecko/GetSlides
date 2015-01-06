 module GetSlides {
     
     export class Storage {
         
         public keys: {[key: string]: string; } = {
             "pin": "__PIN",
             "grant_type": "__GRANT_TYPE"  
         };

         public isLocal: boolean = false;
         public isSession: boolean = false;
         public isObject: boolean = false;
         private dictionary: { [key: string]: string; } = {};

         constructor() {
             if (typeof (window.localStorage) !== "undefined") {
                 this.isLocal = true;
             } else if (typeof (sessionStorage) !== "undefined") {
                 this.isSession = true;
             } else {
                 this.isObject = true;
             }
         }

         public setItem(key: string, value: string) {
             if (this.isLocal) {
                 window.localStorage.setItem(key, value);
             } else if (this.isSession) {
                 sessionStorage.setItem(key, value);
             } else if (this.isObject) {
                 this.dictionary[key] = value;
             }
         }

         public getItem(key: string) : string {
             if (this.isLocal) {
                 return window.localStorage.getItem(key);
             } else if (this.isSession) {
                 return sessionStorage.getItem(key);
             } else if (this.isObject) {
                 return this.dictionary[key];
             }
             return null;
         }
            
     }

 }