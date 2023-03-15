
import axios, { AxiosRequestConfig } from 'axios';

export class SigningService {
  
  public async Sign() : Promise<any> {
    var data = JSON.stringify({
        "title": "As simple as that",
        "description": "This is an important document",
        "externalId": "ae7b9ca7-3839-4e0d-a070-9f14bffbbf55",
        "dataToSign": {
          "base64Content": "VGhpcyB0ZXh0IGNhbiBzYWZlbHkgYmUgc2lnbmVk",
          "fileName": "sample.txt"
        },
        "contactDetails": {
          "email": "test@test.com"
        },
        "signers": [
          {
            "externalSignerId": "uoiahsd321982983jhrmnec2wsadm32",
            "redirectSettings": {
              "redirectMode": "redirect",
              "success": "https://developer.signicat.io/landing-pages/signing-success.html",
              "cancel": "https://developer.signicat.io/landing-pages/something-wrong.html",
              "error": "https://developer.signicat.io/landing-pages/something-wrong.html"
            },
            "signatureType": {
              "mechanism": "pkisignature"
            }
          }
        ]
      }); 
      const config: AxiosRequestConfig = {
        method: 'post',
      maxBodyLength: Infinity,
        url: 'https://api.idfy.io/signature/documents',
        headers: { 
          'Content-Type': 'application/json', 
          'Authorization': 'Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjRiMzBjNDFiOWJjNTRkNmE5MDVjYjQwYmYwOGJhMWRiIiwidHlwIjoiSldUIn0.eyJhdWQiOlsiYWRtaW4iLCJhY2Nlc3MiLCJkZXBvc2l0Iiwic2lnbmF0dXJlIiwiaWRlbnRpZmljYXRpb24iLCJzZXR0aW5ncyIsInNoYXJlIiwidGV4dCIsImh0dHBzOi8vbG9naW4tdGVzdC5pZGZ5Lm5vL3Jlc291cmNlcyJdLCJjbGllbnRfaWQiOiJ0ZmEwZGM3OWI3YThmNDk0OWJhZjdlZjMwMDBiMjMwOTUiLCJjbGllbnRfc2F0IjoiQWNjb3VudCIsImNsaWVudF9zYWkiOiJhMjk0ZGU5Ny1kNjNmLTRlZWEtYjljZC1hMGU2N2FmMTE2NzkiLCJjbGllbnRfb3duZXJ0eXBlIjoiQWNjb3VudCIsImNsaWVudF9vd25lcmlkIjoiYTI5NGRlOTctZDYzZi00ZWVhLWI5Y2QtYTBlNjdhZjExNjc5IiwiY2xpZW50X2VudiI6IlRlc3QiLCJqdGkiOiIyNTYxNjY0NDFCRjU2NkY2NURCNzVEQkIzOUIyRUMzMSIsImlhdCI6MTY3ODg3Mzk3MiwidGVuYW50Ijp7InAiOiJpZGVudGlmeSBkb2N1bWVudF9yZWFkIGRvY3VtZW50X3dyaXRlIGRvY3VtZW50X2ZpbGUgY2xpZW50X3JlYWQgdXNlcl9wcm9maWxlX3JlYWQgc2V0dGluZ3NfcmVhZCB0ZXh0X3JlYWQgdXNlcl9saXN0IGFjY291bnRfcmVhZCBzaGFyZV9yZWFkIGRlcG9zaXRfcmVhZCIsInQiOiJBY2NvdW50IiwiaSI6ImEyOTRkZTk3LWQ2M2YtNGVlYS1iOWNkLWEwZTY3YWYxMTY3OSIsImEiOiJVc2VyIn0sInNjb3BlIjpbImFjY291bnRfcmVhZCIsImFwcCIsImNsaWVudF9yZWFkIiwiZGVwb3NpdF9yZWFkIiwiZG9jdW1lbnRfZmlsZSIsImRvY3VtZW50X3JlYWQiLCJkb2N1bWVudF93cml0ZSIsImlkZW50aWZ5Iiwic2V0dGluZ3NfcmVhZCIsInNoYXJlX3JlYWQiLCJ0ZXh0X3JlYWQiLCJ1c2VyX2xpc3QiLCJ1c2VyX3Byb2ZpbGVfcmVhZCJdLCJuYmYiOjE2Nzg4NzM5NzIsImV4cCI6MTY3ODg3NzU3MiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi10ZXN0LmlkZnkubm8ifQ.ee_jUl27V2JZMr4SRSLFsfJARJ2uhXjRmJf2MScdNePpFI_oq31TzuK5nczyWZM3U9eHJXOitEI5v7jUGf4D35aC-RKNDjZVUXjWQftTEGoHbYNP628MSDZC-lPocwfCp3oKXBW3wZqGcpu8vTbiQMqzh_vaXaR1joNr8EoSMDCDLIHkHuZkzrfYlWziXxWHqMDmcj7Jyhas28XxwGc4e_lEA9TSR1RtGSQfkiZUcqQMcKTZh1PNFRwP7Hc95byTDVjXxrTU7NJY71-wkrBpiqnJ6heJ2WkUCGXXL4QdFF8LmX-XnTyoXwCIsVJuQCxD4GdhRiAJ1m9zXwQT6ABbCQ'
        },
        data : data
      };

    try {
      const data = await axios(config).then((response) => response.data);
      return data;
    } catch (error) {
        console.log(error);
        return Promise.reject();
    }
  }
}

