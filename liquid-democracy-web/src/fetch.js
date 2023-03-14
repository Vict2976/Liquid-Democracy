
 export async function FinalizeVoteForCandidate(userId, electionId, candidateId){
    var myHeaders = new Headers();
    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        //body: raw,
        //redirect: 'follow'
        };
    var response = await fetch(`https://localhost:7236/VoteForCandidate/${userId}/${electionId}/${candidateId}`, requestOptions)

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

export async function FinalizeVoteForDelegate(userId, electionId, delegateId){
  var myHeaders = new Headers();
  var requestOptions = {
      method: 'GET',
      headers: myHeaders,
      //body: raw,
      //redirect: 'follow'
      };
  var response = await fetch(`https://localhost:7236/VoteForDelegate/${userId}/${electionId}/${delegateId}`, requestOptions)

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

