
 export async function fetchStartMitIDSession(url){
    var myHeaders = new Headers();
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        //body: raw,
        //redirect: 'follow'
        };

    var response = await fetch("https://localhost:7236/authentication-session/getIDToken/", requestOptions)

    if (response.ok) { // if HTTP-status is 200-299
        // get the response body (the method explained below)
        let json = await response.json();
        let url = json.url;
        console.log(json.id)
        
        localStorage.setItem("sessionId", json.id);

        window.open(url);
      } else {
        alert("HTTP-Error: " + response.status);
      }
} 