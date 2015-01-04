 module GetSlides {
     
     export function register(username: string, password: string) {
         var data = { "username": username, "password": password }
         $.ajax({
             url: "/api/account/register",
             type: 'POST',
             accepts: "application/json",
             dataType: "application/json",
             data: data
         }).always((data, error) => {
             console.log("REGISTERED", data.responseText, error);
         });
         
     }

 }