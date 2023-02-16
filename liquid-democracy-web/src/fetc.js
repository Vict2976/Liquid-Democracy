
export function fetchingMaterial(){
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjRiMzBjNDFiOWJjNTRkNmE5MDVjYjQwYmYwOGJhMWRiIiwidHlwIjoiSldUIn0.eyJhdWQiOlsiaWRlbnRpZmljYXRpb24iLCJodHRwczovL2xvZ2luLXRlc3QuaWRmeS5uby9yZXNvdXJjZXMiXSwiY2xpZW50X2lkIjoidGZhMGRjNzliN2E4ZjQ5NDliYWY3ZWYzMDAwYjIzMDk1IiwiY2xpZW50X3NhdCI6IkFjY291bnQiLCJjbGllbnRfc2FpIjoiYTI5NGRlOTctZDYzZi00ZWVhLWI5Y2QtYTBlNjdhZjExNjc5IiwiY2xpZW50X293bmVydHlwZSI6IkFjY291bnQiLCJjbGllbnRfb3duZXJpZCI6ImEyOTRkZTk3LWQ2M2YtNGVlYS1iOWNkLWEwZTY3YWYxMTY3OSIsImNsaWVudF9lbnYiOiJUZXN0IiwianRpIjoiNEU3OUY3Q0M5MzQyNjU2NUY5OURGMzA1MUREMjJEQzAiLCJpYXQiOjE2NzY1NTg2OTcsInRlbmFudCI6eyJwIjoiaWRlbnRpZnkiLCJ0IjoiQWNjb3VudCIsImkiOiJhMjk0ZGU5Ny1kNjNmLTRlZWEtYjljZC1hMGU2N2FmMTE2NzkiLCJhIjoiVXNlciJ9LCJzY29wZSI6WyJpZGVudGlmeSJdLCJuYmYiOjE2NzY1NTg2OTcsImV4cCI6MTY3NjU2MjI5NywiaXNzIjoiaHR0cHM6Ly9sb2dpbi10ZXN0LmlkZnkubm8ifQ.hXv3LOSPZyuc7d3oYux_DKX5UL-HsOwXJme5nQqXNgffE_s3UIcv26WDnqxGjX98HsvVBHtZptbLNKmfJ9S4ObrG_k0OaDBrBJ4GKZpcZLlUqBjuykd4sux9ZMiiXxi51wNsf4eKKme7yVHEnlXqJrngLnbx7-RZat-bufQg_vXJHt-HhKFVGpuMXHJCDK54SKMcVu2u5GRsSiPvQHg1ruKzLejJBP5FnL1NiC9KqLCQVtgJcvAeXJKJG4wJybikYeMutb5AuuHm8fOBRBA10LVJ9unsv2cwijc9ZukphseeqGqPi4V2janar_VjaSrYlxZTXxSrLST-FOxLdMHfJw");
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