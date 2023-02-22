import { fetchStartMitIDSession } from "../fetch";


export default function MitID(){
    return(
        <section>
        <h1> Welcome to the authentication process </h1>
            <button onClick={() => {
              var fetchData = fetchStartMitIDSession();
              console.log(fetchData);
            }
              }>
              Login with MitId
            </button>
      </section>

    );
}