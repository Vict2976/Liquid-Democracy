

function doMore(){
  window.location.replace("http://localhost:3000/"); console.log("asdf");
}

function VoteAfterMitId() {  
  localStorage.clear()

  return (
      <div>
       <button onClick={()=> doMore()
        }> CONFIRM YOUR VOTE</button>
      </div>
  );
  }
  export default VoteAfterMitId;
