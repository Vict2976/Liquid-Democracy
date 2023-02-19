
export function fetchingMaterial(){
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjRiMzBjNDFiOWJjNTRkNmE5MDVjYjQwYmYwOGJhMWRiIiwidHlwIjoiSldUIn0.eyJhdWQiOlsiaWRlbnRpZmljYXRpb24iLCJodHRwczovL2xvZ2luLXRlc3QuaWRmeS5uby9yZXNvdXJjZXMiXSwiY2xpZW50X2lkIjoidGZhMGRjNzliN2E4ZjQ5NDliYWY3ZWYzMDAwYjIzMDk1IiwiY2xpZW50X3NhdCI6IkFjY291bnQiLCJjbGllbnRfc2FpIjoiYTI5NGRlOTctZDYzZi00ZWVhLWI5Y2QtYTBlNjdhZjExNjc5IiwiY2xpZW50X293bmVydHlwZSI6IkFjY291bnQiLCJjbGllbnRfb3duZXJpZCI6ImEyOTRkZTk3LWQ2M2YtNGVlYS1iOWNkLWEwZTY3YWYxMTY3OSIsImNsaWVudF9lbnYiOiJUZXN0IiwianRpIjoiNzU4MDBDN0QzQzI5QjQ3MEI0ODA2NDJCMkZGOTU0QUIiLCJpYXQiOjE2NzY4MTc2MDcsInRlbmFudCI6eyJwIjoiaWRlbnRpZnkiLCJ0IjoiQWNjb3VudCIsImkiOiJhMjk0ZGU5Ny1kNjNmLTRlZWEtYjljZC1hMGU2N2FmMTE2NzkiLCJhIjoiVXNlciJ9LCJzY29wZSI6WyJpZGVudGlmeSJdLCJuYmYiOjE2NzY4MTc2MDcsImV4cCI6MTY3NjgyMTIwNywiaXNzIjoiaHR0cHM6Ly9sb2dpbi10ZXN0LmlkZnkubm8ifQ.L6Djx_t5iDYwlMrSLGx-0Y4YvqXhINLZV9d5ufWSkdPIoWyVR-RqDEwx6QJzO9zXNMfL8g-zpnxFxUHmEyZXl25RLPB8ULXPODpmQcavI7GuRYdirssNEE-cm-F1mj5_NDjzws97nyBNUCh5zYyjCPjqVmknvQbqSblSYN16FvvhIruEfZb72rkJyIBxOqvMbcns5HV1DNBHrbMIySZY4p7m7KibOByWINCqxKO7EMifCxBPPhw9DU3hxaTmafDVixt5YOShqOoQhHhicapyN-0Lc02ScIlxW3sa-wMDryoFSz9HcLloGaU5oRk5uwWvfpxkUBn3twmOdxHPubGHGw");
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
    "flow": "redirect",
    "allowedProviders": [
        "no_bankid_netcentric",
        "no_bankid_mobile"
    ],
    "include": [
        "name",
        "date_of_birth"
    ],
    "redirectSettings": {
        "successUrl": "https://developer.signicat.io/landing-pages/identification-success.html",
        "abortUrl": "https://developer.signicat.io/landing-pages/something-wrong.html",
        "errorUrl": "https://developer.signicat.io/landing-pages/something-wrong.html"
    }
    });

    var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: raw,
    redirect: 'follow'
    };

    fetch("https://api.idfy.io/identification/v2/sessions", requestOptions)
    .then(response => response.text())
    .then(result => console.log(result))
    .catch(error => console.log('error', error));

}



 export function fetchAuthenticationController(){
    var myHeaders = new Headers();
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        //body: raw,
        //redirect: 'follow'
        };

    fetch("http://localhost:5093/authentication-session/getIDToken", requestOptions)
    .then(response => response.text())
    .then(result => console.log(result))
    .catch(error => console.log('error', error));

} 